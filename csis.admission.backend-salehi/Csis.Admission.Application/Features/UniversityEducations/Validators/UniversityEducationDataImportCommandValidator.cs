using Csis.Admission.Application.Features.UniversityEducations.Commands;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Csis.Admission.Application.Features.UniversityEducations.Validators;

public sealed class UniversityEducationDataImportCommandValidator : BaseValidator<UniversityEducationDataImportCommand>
{
    public UniversityEducationDataImportCommandValidator() {
        RuleFor(x => x.Codm)
            .GreaterThan(0)
            .WithName("کد مرکز خدمات");

        RuleFor(x => x.DependentId)
            .NotNull()
            .GreaterThan(0)
            .WithName("آی دی تکفل خدمات");


        When(x => x.StudyLevel is not null, () => {
            RuleFor(x => x.StudyLevel)
            .IsInEnum()
            .WithName("مدرک تحصیل");
        });

        RuleFor(x => x.CourseStudy)
            .MaximumLength(200)
            .WithName("رشته");

        When(x => x.UniversityType is not null, () => {
            RuleFor(x => x.UniversityType)
            .IsInEnum()
            .WithName("نوع دانشگاه");
        });

        RuleFor(x => x.UniversityName)
            .MaximumLength(200)
            .WithName("نام دانشگاه");

        RuleFor(x => x.ProvinceTitle)
             .MaximumLength(200)
             .WithName("نام استان");

        When(x => x.StartDate is not null, () => {
            RuleFor(x => x.StartDate)
            .Must(date => {
                var dateStr = date.ToString();
                return Regex.IsMatch(dateStr, @"^(13|14)\d{2}(0[1-9]|1[0-2])(0[1-9]|[12][0-9]|3[01])$");
            })
            .WithMessage("تاریخ شروع وارد شده معتبر نیست.");
        });

        When(x => x.EndDate is not null, () => {
            RuleFor(x => x.EndDate)
            .Must(date => {
                var dateStr = date.ToString();
                return Regex.IsMatch(dateStr, @"^(13|14)\d{2}(0[1-9]|1[0-2])(0[1-9]|[12][0-9]|3[01])$");
            })
            .WithMessage("تاریخ پایان وارد شده معتبر نیست.");
        });

        When(x => x.ValidityDate is not null, () => {
            RuleFor(x => x.ValidityDate)
            .Must(date => {
                var dateStr = date.ToString();
                return Regex.IsMatch(dateStr, @"^(13|14)\d{2}(0[1-9]|1[0-2])(0[1-9]|[12][0-9]|3[01])$");
            })
            .WithMessage("تاریخ اعتبار وارد شده معتبر نیست.");
        });
    }
}
