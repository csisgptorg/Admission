using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Reflection;

namespace Csis.Admission.Application.Common.Behaviors;

/// <summary>
/// Behavior برای تنظیم خودکار Codm در درخواست‌های public
/// اگر مسیر حاوی کلمه "public" باشد، Codm را از کاربر جاری دریافت و در درخواست تنظیم می‌کند
/// </summary>
public class PublicRouteCodmBehavior<TRequest, TResponse>(
    IHttpContextAccessor httpContextAccessor,
    ICurrentUserService currentUserService,
    ILogger<PublicRouteCodmBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        var path = httpContext?.Request.Path.Value;

        // بررسی وجود کلمه "public" در مسیر
        if (string.IsNullOrEmpty(path) || !path.Contains("public", StringComparison.OrdinalIgnoreCase))
        {
            return await next(cancellationToken);
        }

        var requestType = typeof(TRequest);
        var requestTypeName = requestType.Name;

        // دریافت Codm از کاربر جاری
        var codm = (await currentUserService.GetCodmAsync()).ToIntNullable();

        if (!codm.HasValue || codm.Value <= 0)
        {
            return await next(cancellationToken);
        }

        logger.LogDebug(
            "پردازش درخواست {RequestType} در مسیر public با Codm: {Codm}",
            requestTypeName,
            codm.Value);

        // 1. بررسی و set کردن property Codm در body درخواست
        SetCodmInRequestBody(request, requestType, codm.Value, requestTypeName);

        // 2. بررسی و set کردن Codm در route parameters
        if (httpContext != null)
        {
            SetCodmInRouteParameters(httpContext, codm.Value, requestTypeName);
        }

        return await next(cancellationToken);
    }

    /// <summary>
    /// تنظیم Codm در body درخواست
    /// </summary>
    private void SetCodmInRequestBody(TRequest request, Type requestType, int codmValue, string requestTypeName)
    {
        var codmProperty = requestType.GetProperty("Codm", BindingFlags.Public | BindingFlags.Instance);

        if (codmProperty == null)
        {
            return;
        }

        if (!codmProperty.CanWrite)
        {
            logger.LogDebug(
                "Property 'Codm' در درخواست {RequestType} قابل نوشتن نیست",
                requestTypeName);
            return;
        }

        var propertyType = codmProperty.PropertyType;
        var isNullableInt = Nullable.GetUnderlyingType(propertyType) == typeof(int);
        var isInt = propertyType == typeof(int);

        if (!isInt && !isNullableInt)
        {
            return;
        }

        try
        {
            codmProperty.SetValue(request, codmValue);
            
            logger.LogDebug(
                "Codm در body درخواست {RequestType} تنظیم شد: {Codm}",
                requestTypeName,
                codmValue);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "خطا در set کردن Codm در body برای درخواست {RequestType}",
                requestTypeName);
        }
    }

    /// <summary>
    /// تنظیم Codm در route parameters و query string
    /// </summary>
    private void SetCodmInRouteParameters(HttpContext httpContext, int codmValue, string requestTypeName)
    {
        try
        {
            // بررسی route values
            var routeData = httpContext.GetRouteData();
            if (routeData?.Values != null)
            {
                // بررسی وجود کلید codm (case-insensitive)
                var codmKey = routeData.Values.Keys
                    .FirstOrDefault(k => k.Equals("codm", StringComparison.OrdinalIgnoreCase));

                if (codmKey != null)
                {
                    routeData.Values[codmKey] = codmValue;
                    
                    logger.LogDebug(
                        "Codm در route parameters تنظیم شد. Key: {Key}, Value: {Codm}",
                        codmKey,
                        codmValue);
                }
            }

            // بررسی query string
            if (httpContext.Request.Query.ContainsKey("codm") || 
                httpContext.Request.Query.ContainsKey("Codm") ||
                httpContext.Request.Query.ContainsKey("CODM"))
            {
                // برای query string نمی‌توانیم مستقیم تغییر دهیم چون readonly است
                // اما می‌توانیم در route values آن را set کنیم
                if (routeData?.Values != null && !routeData.Values.ContainsKey("codm"))
                {
                    routeData.Values["codm"] = codmValue;
                    
                    logger.LogDebug(
                        "Codm از query string در route values تنظیم شد: {Codm}",
                        codmValue);
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "خطا در set کردن Codm در route/query برای درخواست {RequestType}",
                requestTypeName);
        }
    }
}
