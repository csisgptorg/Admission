using AutoMapper;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Domain.Entities;
using Csis.Authorization.Services;
using Csis.FileManagement;
using Csis.Notification;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Csis.Admission.Services;

/// <inheritdoc/>
internal sealed partial class RequestService(
    IMapper mapper,
    IMediator mediator,
    ILogger<RequestService> logger,
    IRepository<Request, long> repo,
    IRepository<TraceLog, long> logRepo,
    IHttpContextAccessor contextAccessor,
    IEmployeeDataService employeeService,
    IRepository<StudentSummary> studentRepo,
    IRepository<DependentSummary, long> dependentRepo,
    ICsisNotificationService notificationService,
    ICsisAuthenticatedUserService authenticatedUser,
    IStudentMobileRepository studentMobileRepository,
    ICsisFileManagementService fileManagementService,
     IServiceProvider serviceProvider) : IRequestService
{ }
