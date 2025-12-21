using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Common.Interfaces.Repositories.BasicData;
using Csis.Admission.Application.Common.Interfaces.Repositories.ImamJamaat;
using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Persistence.Interceptors;
using Csis.Admission.Persistence.Repositories;
using Csis.Admission.Persistence.Repositories.BasicData;
using Csis.Admission.Persistence.Repositories.ImamJamaat;
using Csis.Admission.Persistence.Repositories.QueryBuilders;
using Csis.Admission.Persistence.Repositories.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Csis.Admission.Persistence;

/// <summary>
/// Dependency registrar for persistence layer
/// </summary>
public static partial class DependencyInjection
{
    /// <summary>
    /// Register custom repositories here
    /// </summary>
    /// <param name="services"></param>
    private static void AddRepositories(this IServiceCollection services, IConfiguration configuration) {
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<INonStudentRepository, NonStudentRepository>();
        services.AddScoped<INonStudentDependantRepository, NonStudentDependantRepository>();
        services.AddScoped<IPersonMarriageRepository, MarriageRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IStudentMobileRepository, StudentMobileRepository>();
        services.AddScoped<IStudentDependentRepository, StudentDependentRepository>();
        services.AddScoped<IStudentBankAccountRepository, StudentBankAccountRepository>();
        services.AddScoped<IAdmissionAuditLogRepository, AdmissionAuditLogRepository>();
        services.AddScoped<IEmployeeViewStudentLogRepository, EmployeeViewStudentLogRepository>();
        services.AddScoped<AppDapperContext>();
        services.AddScoped<IBasicDataRepository, BasicDataRepository>();
        services.AddScoped<IQueryBuilderRepository, QueryBuilderRepository>();
        services.AddScoped<IMosqueRepository, MosqueRepository>();
        services.AddScoped<IStudentSummaryRepository,StudentSummaryRepository>();
        services.AddScoped<ICaseFillingRequestRepository, DeleteAllRequestTestRepository>();

    }

    /// <summary>
    /// Register other persistence related services here
    /// </summary>
    /// <param name="services"></param>
    private static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration) {
        //var dbOptions = configuration.GetSection(nameof(DatabaseOptions)).Get<DatabaseOptions>();

        //if ( dbOptions.EnablePooling ) {
        //    services.AddTransient<AuditLogSaveChangesInterceptor>();
        //} else {
        //    services.AddScoped<AuditLogSaveChangesInterceptor>();
        //}

        services.AddTransient<AuditLogSaveChangesInterceptor>();
    }

    /// <summary>
    /// Register custom interceptors
    /// If the interceptor needs to be resolved from DI container it must be registered first then added to DbContext
    /// If pooling is enabled interceptor must be registered as transient otherwise as scoped
    /// Registration of interceptors should be done in <see cref="AddPersistenceServices"/> method
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="optionsBuilder"></param>
    /// <param name="configuration"></param>
    private static void AddInterceptors(IServiceProvider serviceProvider, DbContextOptionsBuilder optionsBuilder, IConfiguration configuration) {
        optionsBuilder.AddInterceptors(serviceProvider.GetRequiredService<AuditLogSaveChangesInterceptor>());
    }
}
