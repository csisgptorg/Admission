using Csis.Admission.Application.Common.Dtos;
using FluentValidation;

namespace Csis.Admission.Application.Common.Models;

/// <summary>مستندات درخواست</summary>
public sealed record CaseFillingRequestDocumentDto : BaseCommandDto<CaseFillingRequestDocumentDto, CaseFillingRequestDocument, long>
{
    /// <inheritdoc/>
    public CaseFillingRequestDocumentDto(Guid fileId, DocumentType type) {
        FileId = fileId;
        Type = type;
    }

    /// <inheritdoc/>
    public CaseFillingRequestDocumentDto() {
    }

    /// <summary>شناسه فایل</summary>
    public Guid FileId { get; set; }

    /// <summary>نوع</summary>
    public DocumentType Type { get; set; }
}

/// <summary>اعتبار سنجی</summary>
public sealed class CaseFillingRequestDocumentDtoValidator : BaseValidator<CaseFillingRequestDocumentDto>
{
    /// <inheritdoc/>
    public CaseFillingRequestDocumentDtoValidator() {
        RuleFor(x => x.FileId).NotEmpty().WithName("شناسه فایل");
    }
}
