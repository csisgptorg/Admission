using FluentValidation;
using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Common.Models;

/// <summary>ثبت</summary>
public sealed record CreateRequestCommand : BaseCommandDto<CreateRequestCommand, Request, long>
{
    /// <summary></summary>
    public CreateRequestCommand(object payload, RequestFlow flow, RequestType? type = null) {

        var dependentProp = payload.GetType().GetProperty("DependentId");
        _ = long.TryParse(dependentProp?.GetValue(payload)?.ToString(), out var dependentId);
        DependentId = dependentId;

        // در این صورت کد ام از اطلاعات تکفل استخراج میشود
        if ( DependentId.GetValueOrDefault() == 0 ) {
            var codmProp = payload.GetType().GetProperty("Codm");
            Codm = int.Parse(codmProp.GetValue(payload).ToString());
        }
        
        Flow = flow;
        Payload = payload.ToJson();
        PayloadModel = payload.GetType().Name.Replace("RequestCommand", "Command");
        Type = type ?? GetRequestType(payload.GetType().Name);
    }

    /// <summary>اضافه کردن مستندات</summary>
    public void AddDocument(Guid fileId) {
        Documents = [new RequestDocumentDto(fileId, DocumentType.Unknown)];
    }

    /// <summary>اضافه کردن تایید کنندگان طلبه</summary>
    public void AddDualStudentsCodm(int[] dualStudentsCodm) {
        DualStudentsCodm = dualStudentsCodm;
    }

    /// <summary></summary>
    public CreateRequestCommand() { }

    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }

    /// <summary>شناسه تکفل</summary>
    public long? DependentId { get; set; }

    /// <summary>فلو</summary>
    public RequestFlow Flow { get; set; }

    /// <summary>پی لود</summary>
    public string Payload { get; set; }

    /// <summary>مدل پی لود</summary>
    public string PayloadModel { get; set; }

    /// <summary>نوع درخواست</summary>
    public RequestType Type { get; set; }

    /// <summary>مستندات</summary>
    public RequestDocumentDto[] Documents { get; set; }

    /// <summary>کد های مرکز خدمات دو طلبه</summary>
    public int[] DualStudentsCodm { get; set; }

    /// <summary>دریافت نوع درخواست</summary>
    public static RequestType GetRequestType(string commandName) {
        var type = commandName.Replace("Command", "").Replace("Request", "");
        return Enum.Parse<RequestType>(type);
    }
}

/// <summary>اعتبار سنجی</summary>
public sealed class CreateRequestCommandValidator : BaseValidator<CreateRequestCommand>
{
    /// <inheritdoc/>
    public CreateRequestCommandValidator() {
        RuleFor(x => x.Codm).NotEmpty().WithName("کد مرکز خدمات");
        RuleFor(x => x.Flow).IsInEnum().WithName("فرایند");
        RuleFor(x => x.Payload).NotEmpty().WithName("کد مرکز خدمات");
    }
}
