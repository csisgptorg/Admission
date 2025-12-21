using FluentValidation;
using System.Text.RegularExpressions;
using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.Memorizers.Commands;

namespace Csis.Admission.Application.Features.Memorizers.Validators;

public sealed class MemorizerDataImportCommandValidator : BaseValidator<MemorizerDataImportCommand>
{
    public MemorizerDataImportCommandValidator() {
        RuleFor(x => x.Codm)
            .NotEmpty()
            .WithName("کد مرکز");

        RuleFor(x => x.JozCount)
            .NotEmpty()
            .Must(x => x <= 30)
            .WithName("تعداد جزء حفظ شده");

        RuleFor(x => x.ApprovalCenter)
            .IsInEnum()
            .WithName("مرجع تایید کنننده حوزوی");

        RuleFor(x => x.ExpireDate)
            .NotEmpty()
            .Must(date => {
                var dateStr = date.ToString();
                return Regex.IsMatch(dateStr, @"^(14)\d{2}(0[1-9]|1[0-2])(0[1-9]|[12][0-9]|3[01])$");
            })
            .WithMessage("تاریخ وارد شده معتبر نیست.")
            .Must(x => Convert.ToDateTime(
                  x.IntDateToString()).Date.ToPersianDateTime() <= 
                  DateTime.Now.AddYears(1).Date.ToPersianDateTime())
            .WithMessage("تاریخ انقضا نباید بیشتر از یکساال از تاریخ جاری باشد.");
    }
}
