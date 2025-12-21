using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;
using Csis.Authorization.Services;
using Csis.CompareImageAi.Dtos;
using Csis.CompareImageAi.Models;
using Csis.CompareImageAi.Services;
using Csis.FileManagement;
using Microsoft.AspNetCore.Http;
using System.Drawing;

namespace Csis.Admission.Application.Features.Students.Commands;

/// <inheritdoc/>
public sealed record UpdateStudentProfilePictureRequestCommand(int Codm, IFormFile File, bool? UserConfirmed) : IRequest<long>;

internal sealed class UpdateStudentProfilePictureRequestCommandHandler(
    IStudentRepository repo,
    ICurrentUserService currentUser,
    IRequestService requestService,
    ICsisFileManagementService fileManagementService,
    IFaceCompareImageClient compareImageClient)
    : IRequestHandler<UpdateStudentProfilePictureRequestCommand, long>
{
    public async Task<long> Handle(UpdateStudentProfilePictureRequestCommand request, CancellationToken cancellationToken) {
        Validation(request);

        var isSenior = await currentUser.IsSenior();
        var isEmployee = await currentUser.IsEmployee();
        var isStudent = await currentUser.IsStudent();

        if (isSenior || isEmployee ) {
            return await HandleEmployeeOrSeniorRequest(request, cancellationToken);
        }

        if ( isStudent ) {
            return await HandleStudentRequest(request, cancellationToken);
        }

        throw new CommandValidationException("کاربر جاری مجوز انجام این عملیات را ندارد.");
    }

    private async Task<long> HandleEmployeeOrSeniorRequest(UpdateStudentProfilePictureRequestCommand request, CancellationToken cancellationToken) {
        var (fileId, imageBytes) = await UploadAndSaveImageAsync(request.File, cancellationToken);

        var similarity = new ImageAnalysisResultDto {
            Description = "تأیید شده توسط کارمند یا مدیر ارشد",
            AiPercent = 100,
            AiResult = 100,
            Fail = false,
            NewPicQuality = 100,
            OldPicQuality = 100,
            Similarity = "تأیید شده توسط کارمند یا مدیر ارشد"
        };

        return await CreateRequestAsync(request.Codm, similarity, fileId, RequestFlow.DirectRegistration, cancellationToken);
    }

    private async Task<long> HandleStudentRequest(UpdateStudentProfilePictureRequestCommand request, CancellationToken cancellationToken) {
        var studentProfileImage = await repo.GetProfileImageByCodm(request.Codm);
        var (fileId, imageBytes) = await UploadAndSaveImageAsync(request.File, cancellationToken);

        var similarity = await SimilarityImageValidation(request.Codm, imageBytes, studentProfileImage.Image, cancellationToken);

        return await CreateRequestAsync(request.Codm, similarity, fileId, RequestFlow.StudentToEmployee, cancellationToken);
    }

    private async Task<(Guid fileId, byte[] imageBytes)> UploadAndSaveImageAsync(IFormFile file, CancellationToken cancellationToken) {
        await using var stream = new MemoryStream();
        await file.CopyToAsync(stream, cancellationToken);
        var imageBytes = stream.ToArray();

        var fileId = await fileManagementService.Upload(file.FileName, imageBytes, cancellationToken);
        await repo.SaveTemporaryProfilePicture(fileId, imageBytes);

        return (fileId, imageBytes);
    }

    private async Task<long> CreateRequestAsync(int codm, ImageAnalysisResultDto similarity, Guid fileId, RequestFlow flow, CancellationToken cancellationToken) {
        var updatePictureCommand = new UpdateStudentProfilePictureCommand(codm, similarity, fileId, -1);
        var requestCommand = new CreateRequestCommand(updatePictureCommand, flow);
        requestCommand.AddDocument(fileId);

        return await requestService.Create(requestCommand, cancellationToken);
    }

    private static void Validation(UpdateStudentProfilePictureRequestCommand request) {
        var isFileTooLarge = request.File.Length / 1024.0 > 20;
        if ( isFileTooLarge ) {
            throw new CommandValidationException("حجم فایل بیش از ۲۰ کیلوبایت است.");
        }

#pragma warning disable CA1416
        using var stream = request.File.OpenReadStream();
        using var img = Image.FromStream(stream);
        var aspectRatio = (double) img.Width / img.Height;
        var expectedRatio = 3.0 / 4.0;
        var tolerance = 0.3;
        var isValidRatio = Math.Abs(aspectRatio - expectedRatio) < tolerance;
        if ( !isValidRatio ) {
            throw new CommandValidationException("نسبت ابعاد تصویر باید ۳×۴ باشد.");
        }
#pragma warning restore CA1416
    }

    private async Task<ImageAnalysisResultDto> SimilarityImageValidation(int codm, byte[] newProfile, byte[] oldProfile, CancellationToken cancellationToken) {
        var oldBase64Image = Convert.ToBase64String(oldProfile);
        var newBase64Image = Convert.ToBase64String(newProfile);

        var analyzeRequest = new Base64UploadRequest { Codm = codm, OldImageBase64 = oldBase64Image, NewImageBase64 = newBase64Image };
        Console.WriteLine($"AnalyzeRequest:********************************************** {analyzeRequest.ToJson()} *********************************");
        var analysisResult = await compareImageClient.AnalyzeWithBase64Async(analyzeRequest);
        Console.WriteLine($"SimilarityJson: {analysisResult.ToJson()}");
        return analysisResult;
    }
}
