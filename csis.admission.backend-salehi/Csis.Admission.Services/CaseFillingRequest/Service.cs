using MediatR;
using AutoMapper;
using Csis.Notification;
using Csis.FileManagement;
using Csis.Authorization.Services;
using Microsoft.Extensions.Logging;
using Csis.Admission.Domain.Entities;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Interfaces.Repositories;

namespace Csis.Admission.Services;

/// <inheritdoc/>
internal sealed partial class CaseFillingRequestService(
    IMapper mapper,
    IMediator mediator,
    ILogger<CaseFillingRequestService> logger,
    IRepository<Domain.Entities.CaseFillingRequest, long> repo,
    IRepository<AdmissionCaseUser,Guid> caseUserRepository,
    IEmployeeDataService employeeService,
    IRepository<StudentSummary> studentRepo,
    ICsisNotificationService notificationService,
    ICsisAuthenticatedUserService authenticatedUser,
    IStudentMobileRepository studentMobileRepository,
    ICsisFileManagementService fileManagementService) : ICaseFillingRequestService
{ }
