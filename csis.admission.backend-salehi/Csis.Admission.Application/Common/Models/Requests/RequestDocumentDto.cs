using Csis.Admission.Application.Common.Dtos;
using FluentValidation;

namespace Csis.Admission.Application.Common.Models;

/// <summary>مستندات درخواست</summary>
public sealed record RequestDocumentDto : BaseCommandDto<RequestDocumentDto, RequestDocument, long>
{
    /// <inheritdoc/>
    public RequestDocumentDto(Guid fileId, DocumentType type) {
        FileId = fileId;
        Type = type;
    }

    /// <inheritdoc/>
    public RequestDocumentDto() {
    }

    /// <summary>شناسه فایل</summary>
    public Guid FileId { get; set; }

    /// <summary>نوع</summary>
    public DocumentType Type { get; set; }
}

/// <summary>اعتبار سنجی</summary>
public sealed class RequestDocumentDtoValidator : BaseValidator<RequestDocumentDto>
{
    /// <inheritdoc/>
    public RequestDocumentDtoValidator() {
        RuleFor(x => x.FileId).NotEmpty().WithName("شناسه فایل");
    }
}
