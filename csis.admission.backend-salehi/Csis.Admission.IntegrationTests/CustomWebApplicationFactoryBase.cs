/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Configuration;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Persistence;
using Csis.Admission.Persistence.Interceptors;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Csis.Admission.IntegrationTests;

/// <summary>
/// Custom web application factory for running integration tests
/// </summary>
internal sealed partial class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    internal readonly MemoryCache _memoryCache;
    internal static readonly Mock<Authorization.Services.ICsisAuthenticatedUserService> _csisAuthenticatedUserServiceMock = new(MockBehavior.Strict);
    internal static readonly Mock<Authorization.Services.ICsisAuthorizationService> _csisAuthorizationServiceMock = new(MockBehavior.Strict);
    internal static readonly Mock<IStudentDataService> _studentDataServiceMock = new(MockBehavior.Strict);
    internal static readonly Mock<IEmployeeDataService> _employeeDataServiceMock = new(MockBehavior.Strict);
    internal static readonly Mock<IPersonInfoService> _personInfoServiceMock = new(MockBehavior.Strict);
    internal static readonly Mock<ICurrentUserService> _currentUserServiceMock = new(MockBehavior.Strict);
    internal static readonly Mock<IDateTimeService> _dateTimeServiceMock = new(MockBehavior.Strict);
    internal static readonly Mock<IIpAddressService> _ipAddressServiceMock = new(MockBehavior.Strict);
    internal static readonly Mock<IDistributedRequestQueue> _distributedRequestQueueMock = new(MockBehavior.Strict);
    internal static readonly Mock<IDistributedPubSubService> _distributedPubSubServiceMock = new(MockBehavior.Strict);
    internal const int DefaultCurrentUserId = 123;
    internal const string DefaultUsername = "admin";
    internal const string DefaultCurrentStudentCodm = "82000";
    internal const string DefaultIpAddress = "127.0.0.1";

    public CustomWebApplicationFactory() {
        _memoryCache = new MemoryCache(new MemoryCacheOptions());
    }

    public void ClearCache() {
        _memoryCache.Clear();
        _memoryCache.Compact(1.0);
    }

    public static void ResetMocks() {
        SetupICsisAuthenticatedUserServiceMocks();
        SetupICsisAuthorizationServiceMocks();
        SetupDistributedRequestQueueMocks();
        SetupDistributedPubSubServiceMocks();
        SetupCustomMocks();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder) {
        builder.ConfigureAppConfiguration(configurationBuilder => {
            var integrationConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.Developer.json", optional: true)
                .Build();

            configurationBuilder.AddConfiguration(integrationConfig);
        });

        builder.UseEnvironment("Testing");

        builder.ConfigureLogging(logging => {
            logging.ClearProviders();
        });

        builder.ConfigureServices((builder, services) => {
            RegisterMockServices(services);

            var ConnectionString = builder.Configuration.GetSection(nameof(DatabaseOptions)).Get<DatabaseOptions>().ConnectionStrings.SqlServerTest;

            services
                .Remove<DbContextOptions<AppDbContext>>()
                .Remove<AppDbContext>()
                .AddDbContextPool<AppDbContext>((sp, options) => {
                    options.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
                    options.UseSqlServer(ConnectionString, sql =>
                        sql.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
                        .MigrationsHistoryTable(Template.Application.Common.Constants.Db.MigrationsTableName, Template.Application.Common.Constants.Db.DefaultSchema));

                    options.AddInterceptors(sp.GetRequiredService<BaseEntitySaveChangesInterceptor>());

                    AddInterceptors(services, sp, builder.Configuration);
                });

            services
               .Remove<IMemoryCache>()
               .AddSingleton<IMemoryCache>(_memoryCache);

            // Remove all hosted services
            var hostedServices = services.Where(service => service.ServiceType == typeof(IHostedService)).ToList();
            foreach ( var hostedService in hostedServices ) {
                services.Remove(hostedService);
            }

            // Remove all MediatR behaviors
            var behaviors = services.Where(service => service.ServiceType == typeof(IPipelineBehavior<,>)).ToList();
            foreach ( var behavior in behaviors ) {
                services.Remove(behavior);
            }
        });

        builder.ConfigureTestServices(services => {
            // Disable logging
            services.AddSingleton<ILoggerFactory, NullLoggerFactory>();
        });
    }

    private static void RegisterMockServices(IServiceCollection services) {
        services.Replace(ServiceDescriptor.Transient(_ => {
            return _csisAuthenticatedUserServiceMock.Object;
        }));

        services.Replace(ServiceDescriptor.Transient(_ => {
            return _csisAuthorizationServiceMock.Object;
        }));

        services.Replace(ServiceDescriptor.Scoped(_ => {
            return _distributedRequestQueueMock.Object;
        }));

        services.Replace(ServiceDescriptor.Scoped(_ => {
            return _distributedPubSubServiceMock.Object;
        }));

        services.Replace(ServiceDescriptor.Scoped(_ => {
            return _currentUserServiceMock.Object;
        }));

        services.Replace(ServiceDescriptor.Scoped(_ => {
            return _dateTimeServiceMock.Object;
        }));

        services.Replace(ServiceDescriptor.Scoped(_ => {
            return _ipAddressServiceMock.Object;
        }));

        RegisterCustomMockServices(services);

        _currentUserServiceMock.Setup(x => x.GetUserIdAsync())
            .ReturnsAsync(DefaultCurrentUserId);

        _dateTimeServiceMock.Setup(x => x.Now)
            .Returns(new DateTime(2024, 1, 1, 12, 30, 0));

        _ipAddressServiceMock.Setup(x => x.GetIpAddress())
            .Returns(DefaultIpAddress);
    }

    private static void SetupICsisAuthenticatedUserServiceMocks() {
        _csisAuthenticatedUserServiceMock.Reset();
        _csisAuthenticatedUserServiceMock
            .Setup(x => x.GetUserIdAsync(It.IsAny<bool>()))
            .ReturnsAsync(DefaultCurrentUserId);

        _csisAuthenticatedUserServiceMock
            .Setup(x => x.GetDelegatedUserIdAsync(It.IsAny<bool>()))
            .ReturnsAsync((int?) null);

        _csisAuthenticatedUserServiceMock
            .Setup(x => x.GetStudentCodmAsync())
            .ReturnsAsync(DefaultCurrentStudentCodm)
            .Verifiable();
    }

    private static void SetupICsisAuthorizationServiceMocks() {
        _csisAuthorizationServiceMock.Reset();
        _csisAuthorizationServiceMock
            .Setup(x => x.LoginAsync(It.Is<string>(x => x == DefaultUsername), It.IsAny<string>()))
            .ReturnsAsync(new Abstractions.Results.Result<Authorization.Models.TokenResponse> {
                Code = 200,
                Succeeded = true,
                Data = new Authorization.Models.TokenResponse {
                    ExpiresOn = DateTime.UtcNow.AddDays(1),
                    IssuedOn = DateTime.UtcNow,
                    JwToken = "ey12345647898",
                    RefreshToken = "123456"
                }
            });

        _csisAuthorizationServiceMock
            .Setup(x => x.LoginAsync(It.Is<string>(x => x != DefaultUsername), It.IsAny<string>()))
            .ReturnsAsync(new Abstractions.Results.Result<Authorization.Models.TokenResponse> {
                Code = 401,
                Succeeded = false,
                Data = null,
                Message = "Incorrect credentials"
            });

        _csisAuthorizationServiceMock
            .Setup(x => x.LoginStudentAsync(It.Is<string>(x => x == DefaultCurrentStudentCodm)))
            .ReturnsAsync(new Abstractions.Results.Result<Authorization.Models.TokenResponse> {
                Code = 200,
                Succeeded = true,
                Data = new Authorization.Models.TokenResponse {
                    ExpiresOn = DateTime.UtcNow.AddDays(1),
                    IssuedOn = DateTime.UtcNow,
                    JwToken = "ey12345647898",
                    RefreshToken = "123456"
                }
            });

        _csisAuthorizationServiceMock
            .Setup(x => x.LoginStudentAsync(It.Is<string>(x => x != DefaultCurrentStudentCodm)))
            .ReturnsAsync(new Abstractions.Results.Result<Authorization.Models.TokenResponse> {
                Code = 401,
                Succeeded = false,
                Data = null,
                Message = "Invalid token"
            });
    }

    private static void SetupDistributedRequestQueueMocks() {
        _distributedRequestQueueMock.Reset();
        _distributedRequestQueueMock.Setup(x => x.WaitInQueueAsync(It.IsAny<string>(), It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _distributedRequestQueueMock.Setup(x => x.WaitInQueueAsync(It.IsAny<string>(), It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _distributedRequestQueueMock.Setup(x => x.WaitInQueueAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _distributedRequestQueueMock.Setup(x => x.WaitInQueueAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _distributedRequestQueueMock.Setup(x => x.GenerateRequestId())
            .Returns("mocked-request");
        _distributedRequestQueueMock.Setup(x => x.DequeueAsync(It.IsAny<string>()))
            .Returns(Task.CompletedTask);
        _distributedRequestQueueMock.Setup(x => x.DequeueAsync(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Task.CompletedTask);
    }

    private static void SetupDistributedPubSubServiceMocks() {
        _distributedPubSubServiceMock.Reset();
        _distributedPubSubServiceMock
            .Setup(x => x.Publish(It.IsAny<string>(), It.IsAny<string>()));
        _distributedPubSubServiceMock
            .Setup(x => x.Subscribe(It.IsAny<string>(), It.IsAny<Action<string>>()));
        _distributedPubSubServiceMock
            .Setup(x => x.PublishAsync(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Task.CompletedTask);
        _distributedPubSubServiceMock
            .Setup(x => x.SubscribeAsync(It.IsAny<string>(), It.IsAny<Func<string, Task>>()))
            .Returns(Task.CompletedTask);
    }
}
