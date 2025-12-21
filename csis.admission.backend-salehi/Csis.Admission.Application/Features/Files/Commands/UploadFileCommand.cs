/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.FileManagement;
using Microsoft.AspNetCore.Http;

namespace Csis.Admission.Application.Features.Files.Commands;

/// <summary>
/// آپلود فایل
/// </summary>
/// <param name="File">فایل</param>
/// <param name="FileType">نوع فایل</param>
public sealed record UploadFileCommand(IFormFile File, FileTypes FileType) : IRequest<Guid>;

internal sealed class UploadFileCommandHandler(
    ICsisFileManagementService fileManagementFileService,
    IFileRepository fileRepo,
    ILogger<UploadFileCommandHandler> logger) : IRequestHandler<UploadFileCommand, Guid>
{
    public async Task<Guid> Handle(UploadFileCommand request, CancellationToken cancellationToken) {
        logger.LogDebug("Uploading file with type: {FileType}", request.FileType);

        using var ms = new MemoryStream();
        await request.File.CopyToAsync(ms, cancellationToken);
        var identifier = await fileManagementFileService.Upload(request.File.FileName, ms.ToArray(), cancellationToken);
        if ( identifier.Equals(Guid.Empty) ) {
            throw new CommandValidationException("خطا در بارگذاری فایل");
        }

        logger.LogDebug("File uploaded with identifier {identifier}", identifier);

        await fileRepo.InsertAsync(new UploadedFile {
            FileIdentifier = identifier,
            Type = request.FileType
        }, cancellationToken: cancellationToken);

        return identifier;
    }
}

