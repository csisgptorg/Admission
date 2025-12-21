using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Csis.Admission.Application.Common.Behaviors;

/// <summary>
/// بررسی خودکار وضعیت فوت برای جلوگیری از انجام عملیات روی افراد فوت شده
/// </summary>
public class DeceasedValidationBehavior<TRequest, TResponse>(IHttpContextAccessor httpContext, IRepository<StudentSummary> studentRepository, IRepository<DependentSummary, long> dependentSummaryRepository, ILogger<DeceasedValidationBehavior<TRequest, TResponse>> logger, ICurrentUserService currentUserService) : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) {

        if ( (await currentUserService.PersonnelId()).HasValue ) {
            return await next(cancellationToken);
        }

        // اگر در نامش Query وجود داشت، از بررسی وضعیت فوت صرفنظر کن
        if ( request.GetType().Name.Contains("Query") ) {
            return await next(cancellationToken);
        }
        var controllerName = httpContext.HttpContext?.GetRouteData()?.Values["controller"]?.ToString();

        //TODO : باید کلا از پذیرش خارج شود
        if ( controllerName == "People" ) {
            return await next(cancellationToken);
        }

        // استخراج Codm و DependentId از درخواست
        var codm = GetPropertyValue<int?>(request, "Codm");
        var dependentId = GetPropertyValue<long?>(request, "DependentId");

        // اگر DependentId وجود داشت، بررسی وضعیت فوت
        if ( codm.HasValue || dependentId.HasValue ) {
            var student = await studentRepository.GetOneAsync(x => x.Codm == codm.Value, cancellationToken: cancellationToken);
            var dependent = (await dependentSummaryRepository.GetOneAsync(x => x.Codm == codm.Value && x.Id == dependentId, cancellationToken: cancellationToken));

            if ( student is null && dependent is null ) {
                throw new CommandValidationException("دانشجو یا تکفل مورد نظر یافت نشد.");
            }

            if ( student?.IsDead == true ) {
                throw new CommandValidationException("امکان انجام عملیات برای طلبه فوت شده وجود ندارد");
            }

            if ( dependent?.IsDead == true ) {
                throw new CommandValidationException("امکان انجام عملیات برای تکفل فوت شده وجود ندارد");
            }
        }


        return await next(cancellationToken);
    }

    private static T? GetPropertyValue<T>(object obj, string propertyName) {
        var property = obj.GetType().GetProperty(propertyName);
        if ( property == null )
            return default;

        var value = property.GetValue(obj);
        return value != null ? (T) value : default;
    }
}
