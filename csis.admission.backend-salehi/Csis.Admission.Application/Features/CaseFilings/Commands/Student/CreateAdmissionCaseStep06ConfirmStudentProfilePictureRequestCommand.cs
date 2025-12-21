using Csis.Admission.Application.Common.Helpers;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Features.CaseFilings.Dtos;
using Csis.CompareImageAi.Dtos;
using Csis.CompareImageAi.Models;
using Csis.CompareImageAi.Services;
using Csis.FileManagement;
using System.Drawing;

namespace Csis.Admission.Application.Features.CaseFilings.Commands;

/// <inheritdoc/>
public sealed record ConfirmStudentProfilePictureRequestCommand(Guid Token, Guid FileId) : IRequest;

internal sealed class ConfirmStudentProfilePictureRequestCommandHandler(
    ICsisWsmService csisWsmService,
    IStudentRepository repo,
    IRepository<AdmissionCaseUser, Guid> repository,
    ICsisFileManagementService fileManagementService,
    IFaceCompareImageClient compareImageClient)
    : IRequestHandler<ConfirmStudentProfilePictureRequestCommand>
{

    public async Task Handle(ConfirmStudentProfilePictureRequestCommand request, CancellationToken cancellationToken) {
        var caseUser = await repository.GetByIdAsTrackingAsync(request.Token, cancellationToken: cancellationToken)
                       ?? throw new CommandValidationException("شناسه نامعتبر است.");

        var requestFileToBase64 = await fileManagementService.GetPrivateFileById(request.FileId, cancellationToken);
        var requestFileInfo = await fileManagementService.GetFileInfoById(request.FileId, cancellationToken);
        Validation(requestFileToBase64);
        await repo.SaveTemporaryProfilePicture(request.FileId, requestFileToBase64);

        var picturePayload = new CreateAdmissionCasePictureDto();
        switch ( caseUser.Citizenship ) {
            case Citizenship.Iranian:
                Guid? oldImageFileId = null;
             
                var studentShenasnameImage = (await csisWsmService
                    .GetIdentityInfoByNationalCode(
                        new GetIdentityInfoByNationalCodeRequest(-1, caseUser.NationalCode, caseUser.BirthDate),
                        cancellationToken))?.Images?.LastOrDefault(x=>!string.IsNullOrEmpty(x.Image))?.Image;

                ImageAnalysisResultDto similarity = null;

                if ( !string.IsNullOrEmpty(studentShenasnameImage) ) {
                    oldImageFileId = await fileManagementService.Upload("old_" + requestFileInfo.FullName,
                        Convert.FromBase64String(studentShenasnameImage), cancellationToken);

                    // مقایسه تصویر جدید با تصویر شناسنامه با هوش مصنوعی
                    similarity = await SimilarityImageValidation(-1, requestFileToBase64, studentShenasnameImage, cancellationToken);
                }

                picturePayload = new CreateAdmissionCasePictureDto {
                    FileId = request.FileId,
                    OldImageFileId = oldImageFileId,
                    ImageAnalysisResultDto = similarity
                };
                break;
            case Citizenship.NonIranian:
                picturePayload = new CreateAdmissionCasePictureDto { FileId = request.FileId };
                break;
        }


        caseUser.CaseStep = AdmissionCaseStep.PictureUploaded;
        caseUser.Payloads = PayloadHelper.AddPayloadsToString(picturePayload, caseUser.Payloads, nameof(AdmissionCasePayloadName.Picture));
        await repository.UpdateAsync(caseUser, cancellationToken: cancellationToken);

    }


    private static void Validation(byte[] requestFileToBase64) {
        var isFileTooLarge = requestFileToBase64.Length / 1024.0 > 20;
        if ( isFileTooLarge ) {
            throw new CommandValidationException("حجم فایل بیش از ۲۰ کیلوبایت است.");
        }

#pragma warning disable CA1416 // Validate platform compatibility
        using var stream = new MemoryStream(requestFileToBase64);
        using var img = Image.FromStream(stream);
        var aspectRatio = (double) img.Width / img.Height;
        var expectedRatio = 3.0 / 4.0;
        var tolerance = 0.3;
        var isValidRatio = Math.Abs(aspectRatio - expectedRatio) < tolerance;
        if ( !isValidRatio ) {
            throw new CommandValidationException("نسبت ابعاد تصویر باید ۳×۴ باشد.");
        }
    }

    private async Task<ImageAnalysisResultDto> SimilarityImageValidation(int codm, byte[] newProfile, string oldProfile, CancellationToken cancellationToken) {

        var newBase64Image = Convert.ToBase64String(newProfile);

        var analyzeRequest = new Base64UploadRequest { Codm = codm, OldImageBase64 = oldProfile, NewImageBase64 = newBase64Image };
        var analysisResult = await compareImageClient.AnalyzeWithBase64Async(analyzeRequest);
        return analysisResult;
    }
}

