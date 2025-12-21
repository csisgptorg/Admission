using Csis.FileManagement;
using Microsoft.AspNetCore.Http;
using Csis.Admission.Application.Features.CaseFilings.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories;

namespace Csis.Admission.Application.Features.Files.Commands;

/// <summary>
/// بارگذاری فایل ثبت نام طلبه
/// </summary>
/// <param name="Token"></param>
/// <param name="File"></param>
/// <param name="FileType"></param>
public sealed record CreateCaseFillingFileUploadCommand(Guid Token, IFormFile File, FileTypes FileType) : IRequest<Guid>;

internal sealed class StudentRegistrationFileUploadCommandHandler(
    IFileRepository fileRepo,
    IRepository<AdmissionCaseUser, Guid> caseUserRepo,
    ICsisFileManagementService fileManagementFileService)
    : IRequestHandler<CreateCaseFillingFileUploadCommand, Guid>
{
    public async Task<Guid> Handle(CreateCaseFillingFileUploadCommand request, CancellationToken cancellationToken) {

        if ( await caseUserRepo.GetByIdAsync<AdmissionCaseUserDto>(request.Token, cancellationToken: cancellationToken) == null ) {
            throw new CommandValidationException("شما دسترسی لازم برای بارگذاری فایل را ندارید.");
        }

        using var ms = new MemoryStream();
        await request.File.CopyToAsync(ms, cancellationToken);
        var identifier = await fileManagementFileService.Upload(request.File.FileName, ms.ToArray(), cancellationToken);

        await fileRepo.InsertAsync(new UploadedFile {
            FileIdentifier = identifier,
            Type = request.FileType
        }, cancellationToken: cancellationToken);

        return identifier;
    }
}

