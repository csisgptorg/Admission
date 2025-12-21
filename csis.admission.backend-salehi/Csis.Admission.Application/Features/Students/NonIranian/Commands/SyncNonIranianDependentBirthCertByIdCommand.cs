using Csis.Authorization.Services;
using System.Text.Json.Serialization;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.Students.Commands;

/// <summary>»—Ê“ —”«‰? «ÿ·«⁄«  ‘‰«”‰«„Â «? »—«”«” «·„’ÿ›? - »œÊ‰ ‰?«“ »Â Ê—Êœ? òœ ?ò «</summary>
public sealed record SyncNonIranianDependentBirthCertByIdCommand : IRequest
{
    /// <summary>‘‰«”Â ⁄÷Ê Œ«‰Ê«œÂ</summary>
    public long Id { get; init; }
    
    /// <summary> «??œ</summary>
    [JsonIgnore]
    public bool? Confirmed { get; set; }
}

internal sealed class SyncNonIranianDependentBirthCertByIdCommandHandler(
    IStudentRepository studentRepo, 
    IRepository<DependentSummary, long> dependentSummaryRpo,
    ICsisWsmService wsmService, 
    ICsisAuthenticatedUserService authenticatedUser)
    : IRequestHandler<SyncNonIranianDependentBirthCertByIdCommand>
{
    public async Task Handle(SyncNonIranianDependentBirthCertByIdCommand command, CancellationToken cancellation)
    {
        // œ—?«›  «ÿ·«⁄«  ⁄÷Ê Œ«‰Ê«œÂ «“ ”?” „
        var dependent = await dependentSummaryRpo.GetOneAsync(x => x.Id == command.Id, false, cancellation);

        if (string.IsNullOrEmpty(dependent.YektaCode))
        {
            throw new CommandValidationException("òœ ?ò « ⁄÷Ê Œ«‰Ê«œÂ œ— ”?” „ À»  ‰‘œÂ «” .");
        }

        // œ—?«›  «ÿ·«⁄«  «“ «·„’ÿ›? »« «” ›«œÂ «“ òœ ?ò « À»  ‘œÂ
        var identityInfo = await wsmService.GetIdentityInfoByYektaCode(dependent.YektaCode, cancellation);
        
        if (string.IsNullOrEmpty(identityInfo.YektaCode))
        {
            throw new CommandValidationException(nameof(identityInfo), 
                "«ÿ·«⁄«  œ— «·„’ÿ›? ?«›  ‰‘œ/ òœ ?ò « „⁄ »— ‰„? »«‘œ.");
        }

        var certInfo = identityInfo.BirthCertInfo();

        // get identity
        if (command.Confirmed != true)
        {
            throw new ConfirmedValidationException(new
            {
                certInfo.YektaCode,
                certInfo.FirstName,
                certInfo.LastName,
                certInfo.FatherName,
                certInfo.BirthDate,
                certInfo.Gender,
                certInfo.IsDead,
                certInfo.Nationality
            });
        }

        // »Âù—Ê“—”«‰? «ÿ·«⁄«  ⁄÷Ê Œ«‰Ê«œÂ
        var dependentIdentity = new UpdateDependentBirthCertInfoRepoCommand
        {
            Id = command.Id,
            YektaCode = certInfo.YektaCode,
            BirthDate = certInfo.BirthDate.StringDateToInt().Value,
            BirthCertDescription = null,
            Codm = dependent.Codm,
            IsSadat = dependent.IsSadat,
            NationalCode = null,
            Religion = dependent.Religion,
            DataSource = DataSource.WebService,
            PersonnelId = await authenticatedUser.GetPersonnelIdAsync() ?? 0,
            ApplicationId = 66,
            UserId = await authenticatedUser.GetUserIdAsync() ?? 0
        };

        await studentRepo.UpdateDependentBirthCertInfo(dependentIdentity);
    }
}
