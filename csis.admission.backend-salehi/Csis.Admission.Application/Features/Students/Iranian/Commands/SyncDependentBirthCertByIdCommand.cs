using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Extensions;
using Csis.Authorization.Services;
using System.Text.Json.Serialization;

namespace Csis.Admission.Application.Features.Students.Commands;

/// <summary>»—Ê“ —”«‰? «ÿ·«⁄«  ‘‰«”‰«„Â «? »—«”«” À»  «ÕÊ«· - »œÊ‰ ‰?«“ »Â Ê—Êœ? òœ„·? Ê  «—?Œ  Ê·œ</summary>
public sealed record SyncDependentBirthCertByIdCommand : IRequest
{
    /// <summary>‘‰«”Â ⁄÷Ê Œ«‰Ê«œÂ</summary>
    public long Id { get; init; }
    
    /// <summary> «??œ</summary>
    [JsonIgnore]
    public bool? Confirmed { get; set; }
}

internal sealed class SyncDependentBirthCertByIdCommandHandler(
    IStudentRepository studentRepo, 
    IRepository<DependentSummary, long> dependentSummaryRpo,
    ICsisWsmService wsmService, 
    ICsisAuthenticatedUserService authenticatedUser)
    : IRequestHandler<SyncDependentBirthCertByIdCommand>
{
    public async Task Handle(SyncDependentBirthCertByIdCommand command, CancellationToken cancellation)
    {
        // œ—?«›  «ÿ·«⁄«  ⁄÷Ê Œ«‰Ê«œÂ «“ ”?” „
        var dependent = await dependentSummaryRpo.GetOneAsync(x => x.Id == command.Id, false, cancellation);

        if (string.IsNullOrEmpty(dependent.NationalCode))
        {
            throw new CommandValidationException("òœ „·? ⁄÷Ê Œ«‰Ê«œÂ œ— ”?” „ À»  ‰‘œÂ «” .");
        }

        if (dependent.BirthDate == null || dependent.BirthDate == 0)
        {
            throw new CommandValidationException(" «—?Œ  Ê·œ ⁄÷Ê Œ«‰Ê«œÂ œ— ”?” „ À»  ‰‘œÂ «” .");
        }

        // œ—?«›  «ÿ·«⁄«  «“ À»  «ÕÊ«· »« «” ›«œÂ «“ «ÿ·«⁄«  À»  ‘œÂ
        var birthDateString = dependent.BirthDate.ToString();
        var identityRequest = new GetIdentityInfoByNationalCodeRequestApiM(
            dependent.NationalCode, 
            birthDateString.Replace("/", ""));
        
        var identityInfo = await wsmService.GetIdentityInfoByNationalCode(identityRequest, cancellation);
        
        if (string.IsNullOrEmpty(identityInfo.Nin))
        {
            throw new CommandValidationException(nameof(identityInfo), 
                "«ÿ·«⁄«  œ— À»  «ÕÊ«· ?«›  ‰‘œ/ òœ „·? Ê  «—?Œ  Ê·œ „⁄ »— ‰„? »«‘‰œ.");
        }

        var certInfo = identityInfo.BirthCertInfo();

        // get identity
        if (command.Confirmed != true)
        {
            throw new ConfirmedValidationException(new
            {
                certInfo.NationalCode,
                certInfo.FirstName,
                certInfo.LastName,
                certInfo.FatherName,
                certInfo.IsSadat,
                certInfo.BirthDate,
                certInfo.BirthCertNumber,
                certInfo.BirthCertSeri,
                certInfo.BirthCertSerial,
                certInfo.Gender,
                certInfo.IsDead
            });
        }

        // »Âù—Ê“—”«‰? «ÿ·«⁄«  ⁄÷Ê Œ«‰Ê«œÂ
        var dependentIdentity = new UpdateDependentBirthCertInfoRepoCommand
        {
            Id = command.Id,
            Codm = dependent.Codm,
            BirthDate = certInfo.BirthDate.StringDateToInt().Value,
            IsSadat = certInfo.IsSadat,
            NationalCode = certInfo.NationalCode,
            Religion = dependent.Religion,
            YektaCode = null,
            BirthCertDescription = null,
            ApplicationId = 66,
            DataSource = DataSource.WebService,
            PersonnelId = (await authenticatedUser.GetPersonnelIdAsync()) ?? 0,
            UserId = (await authenticatedUser.GetUserIdAsync()) ?? 0
        };

        await studentRepo.UpdateDependentBirthCertInfo(dependentIdentity);
    }
}
