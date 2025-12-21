using Csis.Notification;

namespace Csis.Admission.Application.Common.Services;
internal interface IOtpSenderService
{
    Task SendOtpAsync(string mobile, string type, CancellationToken cancellationToken);
    Task<bool> VerifyOtpAsync(string otp, string mobile, string handlerName, CancellationToken cancellationToken);
}
//TODO: پیاده سازی باید به لایه سرویس منتقل شود , چون وابستگی به سرویس نوتیفیکیشن دارد
/// <summary>
/// خدمات ارسال کد تایید
/// </summary>
/// <param name="notificationAdvancedService"></param>
public class OtpSenderService(ICsisNotificationAdvancedService notificationAdvancedService)
    : IOtpSenderService
{

    /// <summary>
    /// ارسال کد تایید به موبایل
    /// </summary>
    /// <param name="mobile"></param>
    /// <param name="type"></param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="CommandValidationException"></exception>
    public async Task SendOtpAsync(string mobile, string type, CancellationToken cancellationToken) {
        var sendOtpToMobileRequest = new SendOtpToMobile(mobile, type);
        var result = await notificationAdvancedService.SendOtpToMobile(sendOtpToMobileRequest, cancellationToken);
        if ( !result.Succeeded ) {
            throw new CommandValidationException(result.Message);
        }
    }

    /// <summary>
    /// اعتبارسنجی کد تایید
    /// </summary>
    /// <param name="otp"></param>
    /// <param name="mobile"></param>
    /// <param name="commandName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> VerifyOtpAsync(string otp, string mobile, string commandName, CancellationToken cancellationToken) {
        var verifiedOtp = await notificationAdvancedService.VerifyOtp(
            new VerifyOtp(otp, mobile, commandName), cancellationToken);
        return verifiedOtp.Data;
    }

}
