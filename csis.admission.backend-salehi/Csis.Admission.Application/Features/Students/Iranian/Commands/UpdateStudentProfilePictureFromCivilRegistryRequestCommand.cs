using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Extensions;
using Csis.FileManagement;

namespace Csis.Admission.Application.Features.Students.Commands;

/// <summary>
/// درخواست بروزرسانی عکس پروفایل از ثبت احوال
/// </summary>
public sealed record UpdateStudentProfilePictureFromCivilRegistryRequestCommand(int Codm, bool Confirmed) : IRequest<long>;

internal sealed class UpdateStudentProfilePictureFromCivilRegistryRequestCommandHandler(
    IStudentRepository repo,
    ICurrentUserService currentUser,
    IRequestService requestService,
    ICsisFileManagementService fileManagementService,
    ICsisWsmService csisWsmService,
    IRepository<StudentSummary> studentRepo)
    : IRequestHandler<UpdateStudentProfilePictureFromCivilRegistryRequestCommand, long>
{
    public async Task<long> Handle(UpdateStudentProfilePictureFromCivilRegistryRequestCommand request, CancellationToken cancellationToken) {
        // بررسی دسترسی کاربر
        var isEmployee = await currentUser.IsEmployee();

        if ( !isEmployee ) {
            throw new CommandValidationException("فقط کارمندان و مدیران مجاز به انجام این عملیات هستند.");
        }

        // دریافت اطلاعات طلبه
        var student = await studentRepo.GetOneAsync(x => x.Codm == request.Codm, cancellationToken: cancellationToken);
        if ( student == null ) {
            throw new CommandValidationException("طلبه با این کد مرکز خدمات یافت نشد.");
        }

        if ( !student.BirthDate.HasValue ) {
            throw new CommandValidationException("تاریخ تولد طلبه در سیستم ثبت نشده است.");
        }

        // دریافت عکس فعلی طلبه
        var currentProfileImage = await repo.GetProfileImageByCodm(request.Codm);

        // دریافت اطلاعات از سرویس full-mixed
        var identityInfo = await csisWsmService.GetIranianImageFromSabteAhval(
            student.NationalCode,
            student.BirthDate.Value.IntDateToString(),
            cancellationToken);

        // دریافت آخرین عکس از Images
        var civilRegistryImage = identityInfo?.Images?.LastOrDefault(x => !string.IsNullOrEmpty(x.Image))?.Image;

        if ( string.IsNullOrEmpty(civilRegistryImage) ) {
            throw new CommandValidationException("تصویری از ثبت احوال برای این طلبه یافت نشد.");
        }

        // تبدیل عکس جدید به بایت و آپلود
        var newImageBytes = Convert.FromBase64String(civilRegistryImage);
        var newImageFileId = await fileManagementService.Upload(
            $"civil_registry_{student.NationalCode}_{DateTime.Now:yyyyMMddHHmmss}.jpg",
            newImageBytes,
            cancellationToken);

        // آپلود عکس قدیمی برای مقایسه
        Guid? oldImageFileId = null;
        if ( currentProfileImage?.Image != null && currentProfileImage.Image.Length > 0 ) {
            oldImageFileId = await fileManagementService.Upload(
                $"old_profile_{student.NationalCode}_{DateTime.Now:yyyyMMddHHmmss}.jpg",
                currentProfileImage.Image,
                cancellationToken);
        }

        if ( !request.Confirmed ) {
            // دریافت لینک‌های عکس‌ها
            var newImageDownloadInfo = await fileManagementService.DownloadLink(newImageFileId, cancellationToken: cancellationToken);
            var newImageLink = newImageDownloadInfo?.Link;

            string oldImageLink = null;
            if ( oldImageFileId.HasValue ) {
                var oldImageDownloadInfo = await fileManagementService.DownloadLink(oldImageFileId.Value, cancellationToken: cancellationToken);
                oldImageLink = oldImageDownloadInfo?.Link;
            }

            var imageData = new {
                NewImage = new { FileId = newImageFileId, Link = newImageLink, Title = "عکس جدید از ثبت احوال" },
                OldImage = oldImageFileId.HasValue
                    ? new { FileId = oldImageFileId.Value, Link = oldImageLink, Title = "عکس فعلی" }
                    : null
            };

            throw new ConfirmedValidationException(imageData);
        }

        // ذخیره موقت عکس جدید
        await repo.SaveTemporaryProfilePicture(newImageFileId, newImageBytes);

        // ایجاد کامند برای اعمال تغییرات
        var updatePictureCommand = new UpdateStudentProfilePictureFromCivilRegistryCommand(
            request.Codm,
            newImageFileId,
            oldImageFileId,
            -1);

        // تعیین فلو درخواست
        var requestFlow = RequestFlow.DirectRegistration;

        // ایجاد درخواست
        var requestCommand = new CreateRequestCommand(
            updatePictureCommand,
            requestFlow,
            RequestType.UpdateStudentProfilePictureFromCivilRegistry);

        requestCommand.AddDocument(newImageFileId);

        var requestId = await requestService.Create(requestCommand, cancellationToken);

        return requestId;
    }
}
