namespace Csis.Admission.Application.Common.Behaviors;

/// <summary>
/// بررسی خودکار کد یکتا در درخواست‌ها
/// تمام پراپرتی‌هایی که در نامشان YektaCode وجود دارد را بررسی می‌کند
/// </summary>
public class YektaCodeValidationBehavior<TRequest, TResponse>(ILogger<YektaCodeValidationBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) {
        // یافتن تمام پراپرتی‌هایی که در نامشان YektaCode وجود دارد
        var yektaCodeProperties = GetYektaCodeProperties(request);

        if (yektaCodeProperties.Any()) {
            logger.LogDebug("تعداد {Count} پراپرتی YektaCode در request یافت شد", yektaCodeProperties.Count);

            foreach (var (propertyName, propertyValue) in yektaCodeProperties) {
                if (propertyValue != null) {
                    var yektaCodeString = ConvertToString(propertyValue);

                    if (!string.IsNullOrWhiteSpace(yektaCodeString) && !yektaCodeString.StartsWith("9")) {
                        logger.LogWarning(
                            "کد یکتا نامعتبر در {PropertyName}: {YektaCode}. کد یکتا باید با 9 شروع شود.", 
                            propertyName, 
                            yektaCodeString);
                        throw new CommandValidationException("لطفا کد یکتا معتبر وارد کنید");
                    }

                    logger.LogDebug("کد یکتا معتبر در {PropertyName}: {YektaCode}", propertyName, yektaCodeString);
                }
            }
        } else {
            logger.LogTrace("هیچ پراپرتی YektaCode در request نوع {RequestType} یافت نشد", typeof(TRequest).Name);
        }

        return await next(cancellationToken);
    }

    /// <summary>
    /// استخراج تمام پراپرتی‌هایی که در نامشان YektaCode وجود دارد
    /// </summary>
    private static List<(string PropertyName, object? PropertyValue)> GetYektaCodeProperties(TRequest request) {
        var requestType = request.GetType();
        var properties = requestType.GetProperties(
            System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

        var result = new List<(string, object?)>();

        foreach (var property in properties) {
            // بررسی اینکه آیا نام پراپرتی حاوی YektaCode است
            if (property.Name.Contains("YektaCode", StringComparison.OrdinalIgnoreCase)) {
                var value = property.GetValue(request);
                result.Add((property.Name, value));
            }
        }

        return result;
    }

    /// <summary>
    /// تبدیل مقدار به string
    /// اگر مقدار از نوع عددی باشد، به string تبدیل می‌شود
    /// </summary>
    private static string? ConvertToString(object? value) {
        if (value == null) {
            return null;
        }

        if (value is string strValue) {
            return strValue;
        }

        if (value is int intValue) {
            return intValue.ToString();
        }

        if (value is long longValue) {
            return longValue.ToString();
        }

        // برای سایر انواع عددی
        return value.ToString();
    }
}
