#region Usings
using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Common.Dtos.RequestService;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Mappings.RequestComparision;
using Csis.Admission.Application.Extensions;
using Csis.Admission.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using System.Collections.Generic;

#endregion
namespace Csis.Admission.Services;

/// <summary>
/// دریافت مقایسه کامل درخواست با داده‌های فعلی
/// </summary>
internal sealed partial class RequestService : IRequestService
{
    /// <summary>
    /// دریافت مقایسه کامل درخواست با داده‌های فعلی
    /// شامل: داده فعلی دیتابیس + تغییرات درخواستی
    /// </summary>
    public async Task<RequestComparisonDetailResult> GetRequestComparisonDetailAsync(
        long requestId,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("شروع دریافت مقایسه کامل Request با شناسه {RequestId}", requestId);

        // دریافت درخواست از دیتابیس
        var request = await repo.GetByIdAsync(requestId, false, cancellationToken: cancellationToken);

        if (request == null)
        {
            logger.LogWarning("Request با شناسه {RequestId} یافت نشد", requestId);
            throw new CommandValidationException($"درخواست با شناسه {requestId} یافت نشد");
        }

        logger.LogInformation(
            "Request یافت شد - Type: {RequestType}, Codm: {Codm}, DependentId: {DependentId}, PayloadModel: {PayloadModel}",
            request.Type, request.Codm, request.DependentId, request.PayloadModel);

        var result = new RequestComparisonDetailResult
        {
            RequestId = requestId,
            RequestType = request.Type,
            TimeCreated = request.TimeCreated,
            DateCreated = request.DateCreated.IntDateToString(),
            Status = request.ApprovalStatus,
        };

        // Deserialize کردن Payload درخواست
        JsonElement payloadElement;
        try
        {
            payloadElement = JsonSerializer.Deserialize<JsonElement>(request.Payload);
            result.RequestedChanges = payloadElement;

            if (payloadElement.ValueKind == JsonValueKind.Undefined || 
                payloadElement.ValueKind == JsonValueKind.Null)
            {
                logger.LogWarning("Payload خالی است");
                throw new CommandValidationException("داده‌های درخواست خالی است");
            }
        }
        catch (JsonException ex)
        {
            logger.LogError(ex, "خطا در Parse کردن Payload");
            throw new CommandValidationException($"خطا در Parse کردن داده‌های درخواست: {ex.Message}");
        }

        // دریافت داده فعلی از Repository
        try
        {
            var currentData = await GetCurrentDataAsync(request, cancellationToken);
            
            // فیلتر کردن property‌های currentData بر اساس payload
            if (currentData != null)
            {
                var filteredData = FilterPropertiesByPayload(currentData, request.Payload);
                result.CurrentData = filteredData ?? currentData;

                logger.LogInformation(
                    "داده فعلی با موفقیت دریافت و فیلتر شد برای RequestId: {RequestId}",
                    requestId);
            }
            else
            {
                result.CurrentData = null;
                logger.LogWarning(
                    "داده فعلی یافت نشد برای RequestId: {RequestId}",
                    requestId);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "خطا در دریافت CurrentData برای Request: {RequestId}", requestId);
            // در صورت خطا، نتیجه را بدون CurrentData برمی‌گردانیم
        }

        return result;
    }

    /// <summary>
    /// دریافت داده فعلی از Repository به صورت Dynamic بر اساس PayloadModel
    /// </summary>
    /// <param name="request">درخواست</param>
    /// <param name="cancellationToken">توکن لغو</param>
    /// <returns>داده فعلی Entity یا null</returns>
    private async Task<object> GetCurrentDataAsync(Request request, CancellationToken cancellationToken)
    {
        try
        {
            // دریافت نوع Entity از PayloadModel
            var entityType = PayloadModelToEntityMapping.GetEntityType(request.PayloadModel);

            if (entityType == null)
            {
                logger.LogWarning(
                    "نوع Entity برای PayloadModel: {PayloadModel} یافت نشد",
                    request.PayloadModel);
                return null;
            }

            logger.LogDebug(
                "نوع Entity یافت شد: {EntityType} برای PayloadModel: {PayloadModel}",
                entityType.Name, request.PayloadModel);

            // دریافت IRepository<TEntity, int> از ServiceProvider
            var repositoryInterfaceType = typeof(IRepository<,>).MakeGenericType(entityType, typeof(int));
            var repository = serviceProvider.GetService(repositoryInterfaceType);

            if (repository == null)
            {
                logger.LogWarning(
                    "Repository برای Entity: {EntityType} یافت نشد",
                    entityType.Name);
                return null;
            }

            logger.LogDebug("Repository Interface Type: {RepoType}", repositoryInterfaceType.FullName);

            // ساخت Expression برای Query
            var parameter = Expression.Parameter(entityType, "x");
            Expression predicate = null;

            // بررسی وجود پراپرتی Codm
            var codmProperty = entityType.GetProperty("Codm", BindingFlags.Public | BindingFlags.Instance);
            if (codmProperty != null)
            {
                var codmExpression = Expression.Equal(
                    Expression.Property(parameter, codmProperty),
                    Expression.Constant(request.Codm)
                );
                predicate = codmExpression;
            }

            // بررسی وجود پراپرتی DependentId
            var dependentIdProperty = entityType.GetProperty("DependentId", BindingFlags.Public | BindingFlags.Instance);
            if (dependentIdProperty != null && request.DependentId.HasValue)
            {
                var dependentIdExpression = Expression.Equal(
                    Expression.Property(parameter, dependentIdProperty),
                    Expression.Constant(request.DependentId, dependentIdProperty.PropertyType)
                );

                predicate = predicate == null
                    ? dependentIdExpression
                    : Expression.AndAlso(predicate, dependentIdExpression);
            }

            // اگر هیچ predicate ساخته نشد
            if (predicate == null)
            {
                logger.LogWarning(
                    "نتوانستیم predicate بسازیم برای Entity: {EntityType}",
                    entityType.Name);
                return null;
            }

            // ساخت Lambda Expression
            var lambdaType = typeof(Func<,>).MakeGenericType(entityType, typeof(bool));
            var lambda = Expression.Lambda(lambdaType, predicate, parameter);

            logger.LogDebug("Lambda expression ساخته شد");

            // جستجوی متد GetOneAsync
            var allMethods = repositoryInterfaceType.GetMethods(
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy
            );
            
            logger.LogDebug("تعداد کل متدهای repository: {Count}", allMethods.Length);
            
            var getOneAsyncMethods = allMethods
                .Where(m => m.Name == "GetOneAsync")
                .ToList();
            
            logger.LogDebug("تعداد متدهای GetOneAsync: {Count}", getOneAsyncMethods.Count);

            // پیدا کردن متد مناسب
            MethodInfo getOneAsyncMethod = null;
            foreach (var method in getOneAsyncMethods)
            {
                var parameters = method.GetParameters();
                logger.LogDebug(
                    "بررسی متد GetOneAsync با {ParamCount} پارامتر: {Params}",
                    parameters.Length,
                    string.Join(", ", parameters.Select(p => 
                        $"{p.ParameterType.Name} {p.Name}"
                    ))
                );

                if (parameters.Length == 3)
                {
                    var param0 = parameters[0].ParameterType;
                    var param1 = parameters[1].ParameterType;
                    var param2 = parameters[2].ParameterType;

                    // بررسی دقیق‌تر
                    bool isExpressionParam = param0.IsGenericType && 
                        param0.GetGenericTypeDefinition() == typeof(Expression<>);
                    bool isBoolParam = param1 == typeof(bool);
                    bool isCancellationTokenParam = param2 == typeof(CancellationToken);

                    logger.LogDebug(
                        "Match result - Expression: {Expr}, Bool: {Bool}, CToken: {CT}",
                        isExpressionParam, isBoolParam, isCancellationTokenParam
                    );

                    if (isExpressionParam && isBoolParam && isCancellationTokenParam)
                    {
                        getOneAsyncMethod = method;
                        logger.LogInformation("متد GetOneAsync مناسب پیدا شد!");
                        break;
                    }
                }
            }

            if (getOneAsyncMethod == null)
            {
                logger.LogWarning("متد GetOneAsync با signature مناسب یافت نشد");
                logger.LogWarning("Repository Type: {RepType}", repositoryInterfaceType.FullName);
                logger.LogWarning("Entity Type: {EntityType}", entityType.FullName);
                return null;
            }

            // فراخوانی متد
            logger.LogDebug("در حال فراخوانی GetOneAsync...");
            var task = (Task)getOneAsyncMethod.Invoke(repository, new object[] {
                lambda, // Expression را مستقیماً پاس می‌کنیم
                false,  // includeDeleted
                cancellationToken
            });

            await task.ConfigureAwait(false);
            logger.LogDebug("GetOneAsync با موفقیت اجرا شد");

            // دریافت نتیجه
            var resultProperty = task.GetType().GetProperty("Result");
            var entity = resultProperty?.GetValue(task);

            if (entity == null)
            {
                logger.LogWarning(
                    "Entity با Codm={Codm}, DependentId={DependentId} یافت نشد",
                    request.Codm, request.DependentId);
                return null;
            }

            logger.LogDebug("Entity پیدا شد: {EntityType}", entity.GetType().Name);

            // تبدیل Entity به Command Type با استفاده از Reflection
            var commandType = FindCommandType(request.PayloadModel);
            if (commandType != null)
            {
                var mappedCommand = MapEntityToCommand(entity, commandType);
                logger.LogDebug(
                    "Entity به Command تبدیل شد: {EntityType} -> {CommandType}",
                    entityType.Name, commandType.Name);
                return mappedCommand;
            }

            // اگر Command Type یافت نشد، خود Entity را برمی‌گردانیم
            logger.LogDebug(
                "Command Type برای: {PayloadModel} یافت نشد، خود Entity برگردانده می‌شود",
                request.PayloadModel);
            return entity;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "خطا در GetCurrentDataAsync برای Request: {RequestId}", request.Id);
            return null;
        }
    }

    /// <summary>
    /// پیدا کردن نوع Command مربوط به PayloadModel
    /// </summary>
    private Type FindCommandType(string commandName)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(commandName))
                return null;

            // جستجو در Assembly های Application
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName != null && a.FullName.Contains("Csis.Admission.Application"));

            foreach (var assembly in assemblies)
            {
                var commandType = assembly.GetTypes()
                    .FirstOrDefault(t => t.Name == commandName);

                if (commandType != null)
                {
                    return commandType;
                }
            }

            return null;
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "خطا در پیدا کردن Command Type برای: {CommandName}", commandName);
            return null;
        }
    }

    /// <summary>
    /// Map کردن Entity به Command با استفاده از Reflection
    /// </summary>
    private object MapEntityToCommand(object entity, Type commandType)
    {
        try
        {
            // ایجاد instance از Command
            // از FormatterServices استفاده می‌کنیم تا بتوانیم بدون constructor instance بسازیم
            object commandInstance;
            
            try
            {
                // سعی می‌کنیم با constructor بدون پارامتر
                commandInstance = Activator.CreateInstance(commandType);
            }
            catch (MissingMethodException)
            {
                // اگر constructor بدون پارامتر نداشت، از FormatterServices استفاده می‌کنیم
                logger.LogDebug(
                    "Command {CommandType} constructor بدون پارامتر ندارد، از FormatterServices استفاده می‌شود",
                    commandType.Name);
                
                commandInstance = System.Runtime.Serialization.FormatterServices
                    .GetUninitializedObject(commandType);
            }

            if (commandInstance == null)
            {
                logger.LogWarning("نتوانستیم instance از {CommandType} بسازیم", commandType.Name);
                return null;
            }

            var entityType = entity.GetType();
            var entityProperties = entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var commandProperties = commandType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanWrite)
                .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

            int mappedCount = 0;
            
            // کپی کردن property ها
            foreach (var entityProp in entityProperties)
            {
                if (commandProperties.TryGetValue(entityProp.Name, out var commandProp))
                {
                    try
                    {
                        var value = entityProp.GetValue(entity);
                        
                        // بررسی سازگاری نوع
                        if (commandProp.PropertyType == entityProp.PropertyType)
                        {
                            // نوع دقیقاً یکسان است
                            commandProp.SetValue(commandInstance, value);
                            mappedCount++;
                        }
                        else if (commandProp.PropertyType.IsAssignableFrom(entityProp.PropertyType))
                        {
                            // نوع قابل assign است (مثلاً از child به parent)
                            commandProp.SetValue(commandInstance, value);
                            mappedCount++;
                        }
                        else if (value != null && IsNullableType(commandProp.PropertyType))
                        {
                            // اگر مقصد Nullable است و مقدار null نیست
                            var underlyingType = Nullable.GetUnderlyingType(commandProp.PropertyType);
                            if (underlyingType == entityProp.PropertyType)
                            {
                                commandProp.SetValue(commandInstance, value);
                                mappedCount++;
                            }
                        }
                        else if (value == null && IsNullableType(commandProp.PropertyType))
                        {
                            // اگر مقصد Nullable است و مقدار null است
                            commandProp.SetValue(commandInstance, null);
                            mappedCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.LogWarning(ex, 
                            "خطا در کپی property {PropertyName} از {EntityType} به {CommandType}",
                            entityProp.Name, entityType.Name, commandType.Name);
                    }
                }
            }

            logger.LogDebug(
                "تعداد {Count} property از {EntityType} به {CommandType} map شد",
                mappedCount, entityType.Name, commandType.Name);

            return commandInstance;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "خطا در MapEntityToCommand");
            return null;
        }
    }

    /// <summary>
    /// بررسی اینکه آیا یک Type از نوع Nullable است یا خیر
    /// </summary>
    private static bool IsNullableType(Type type)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
    }

    /// <summary>
    /// پیدا کردن نوع DTO مربوط به Entity
    /// </summary>
    private Type FindDtoTypeForEntity(string commandName)
    {
        try
        {
            // نام احتمالی DTO: EntityNameDto
            var dtoTypeName = $"{commandName}";

            // جستجو در Assembly های Application
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName != null && a.FullName.Contains("Csis.Admission.Application"));

            foreach (var assembly in assemblies)
            {
                var dtoType = assembly.GetTypes()
                    .FirstOrDefault(t => t.Name == dtoTypeName);

                if (dtoType != null)
                {
                    return dtoType;
                }
            }

            return null;
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "خطا در پیدا کردن DTO برای Entity: {EntityType}", commandName);
            return null;
        }
    }

    /// <summary>
    /// فیلتر کردن property‌های آبجکت بر اساس property‌های موجود در payload
    /// </summary>
    /// <param name="data">داده اصلی (Entity یا DTO)</param>
    /// <param name="payloadJson">JSON payload درخواست</param>
    /// <returns>Dictionary شامل فقط property‌های موجود در payload</returns>
    private Dictionary<string, object> FilterPropertiesByPayload(object data, string payloadJson)
    {
        try
        {
            if (data == null || string.IsNullOrWhiteSpace(payloadJson))
            {
                return null;
            }

            // Parse کردن payload برای دریافت نام property‌ها
            var payloadElement = JsonSerializer.Deserialize<JsonElement>(payloadJson);
            if (payloadElement.ValueKind != JsonValueKind.Object)
            {
                logger.LogWarning("Payload یک JSON Object معتبر نیست");
                return null;
            }

            // دریافت نام property‌های موجود در payload (case-insensitive)
            var payloadPropertyNames = new HashSet<string>(
                payloadElement.EnumerateObject().Select(p => p.Name),
                StringComparer.OrdinalIgnoreCase
            );

            // دریافت نوع داده
            var dataType = data.GetType();
            var filteredData = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            // فیلتر کردن property‌ها
            foreach (var property in dataType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                // اگر property در payload موجود باشد
                if (payloadPropertyNames.Contains(property.Name))
                {
                    try
                    {
                        var value = property.GetValue(data);
                        filteredData[property.Name] = value;
                    }
                    catch (Exception ex)
                    {
                        logger.LogWarning(ex, "خطا در خواندن property: {PropertyName}", property.Name);
                    }
                }
            }

            logger.LogDebug(
                "فیلتر property‌ها: از {TotalProperties} property، {FilteredCount} property برگردانده شد",
                dataType.GetProperties().Length,
                filteredData.Count
            );

            return filteredData.Count > 0 ? filteredData : null;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "خطا در FilterPropertiesByPayload");
            return null;
        }
    }
}
