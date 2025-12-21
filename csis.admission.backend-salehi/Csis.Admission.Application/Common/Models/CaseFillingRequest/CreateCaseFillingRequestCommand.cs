using FluentValidation;
using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Common.Models;

/// <summary>ثبت</summary>
public sealed record CreateCaseFillingRequestCommand : BaseCommandDto<CreateCaseFillingRequestCommand, CaseFillingRequest, long>
{
    /// <summary></summary>
    public CreateCaseFillingRequestCommand(object payload, RequestFlow flow, string payloadModel, RequestType? type = null) {
        var codmProp = payload.GetType().GetProperty("Codm");
        Codm = int.Parse(codmProp.GetValue(payload).ToString());

        Flow = flow;
        Payload = payload.ToJson();
        PayloadModel = payloadModel;
        Type = type ?? GetRequestType(payload.GetType().Name);
    }

    /// <summary>اضافه کردن مستندات</summary>
    public void AddDocument(Guid fileId) {
        Documents = [new CaseFillingRequestDocumentDto(fileId, DocumentType.Unknown)];
    }

    /// <summary></summary>
    public CreateCaseFillingRequestCommand() { }

    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }

    /// <summary>فلو</summary>
    public RequestFlow Flow { get; set; }

    /// <summary>پی لود</summary>
    public string Payload { get; set; }

    /// <summary>مدل پی لود</summary>
    public string PayloadModel { get; set; }

    /// <summary>نوع درخواست</summary>
    public RequestType Type { get; set; }

    /// <summary>مستندات</summary>
    public CaseFillingRequestDocumentDto[] Documents { get; set; }

    /// <summary>دریافت نوع درخواست</summary>
    public static RequestType GetRequestType(string commandName) {
        var type = commandName.Replace("Command", "").Replace("Request", "");
        return Enum.Parse<RequestType>(type);
    }
}

/// <summary>اعتبار سنجی</summary>
public sealed class CreateCaseFillingRequestCommandValidator : BaseValidator<CreateCaseFillingRequestCommand>
{
    /// <inheritdoc/>
    public CreateCaseFillingRequestCommandValidator() {
        RuleFor(x => x.Codm).NotEmpty().WithName("کد مرکز خدمات");
        RuleFor(x => x.Flow).IsInEnum().WithName("فرایند");
        RuleFor(x => x.Payload).NotEmpty().WithName("کد مرکز خدمات");
    }
}
