using Csis.Authorization.Services;
using System.Text.Json.Serialization;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.Students.Commands;

/// <summary>»—Ê“ —”«‰? «ÿ·«⁄«  ‘‰«”‰«„Â «? »—«”«” «·„’ÿ›? - »œÊ‰ ‰?«“ »Â Ê—Êœ? òœ ?ò «</summary>
public sealed record SyncNonIranianStudentBirthCertByCodmCommand : IRequest
{
    /// <summary>òœ „—ò“ Œœ„« </summary>
    public int Codm { get; init; }
    
    /// <summary> «??œ</summary>
    [JsonIgnore]
    public bool? Confirmed { get; set; }
}

internal sealed class SyncNonIranianStudentBirthCertByCodmCommandHandler(
    IStudentRepository studentRepo, 
    IRepository<StudentSummary> studentSummaryRpo,
    ICsisWsmService wsmService, 
    ICsisAuthenticatedUserService authenticatedUser)
    : IRequestHandler<SyncNonIranianStudentBirthCertByCodmCommand>
{
    public async Task Handle(SyncNonIranianStudentBirthCertByCodmCommand command, CancellationToken cancellation)
    {
        // œ—?«›  «ÿ·«⁄«  œ«‰‘ÃÊ «“ ”?” „
        var student = await studentSummaryRpo.GetOneAsync(x => x.Codm == command.Codm, false, cancellation);

        if (string.IsNullOrEmpty(student.YektaCode))
        {
            throw new CommandValidationException("òœ ?ò « œ«‰‘ÃÊ œ— ”?” „ À»  ‰‘œÂ «” .");
        }

        // œ—?«›  «ÿ·«⁄«  «“ «·„’ÿ›? »« «” ›«œÂ «“ òœ ?ò « À»  ‘œÂ
        var identityInfo = await wsmService.GetIdentityInfoByYektaCode(student.YektaCode, cancellation);
        
        if (string.IsNullOrWhiteSpace(identityInfo.YektaCode))
        {
            throw new CommandValidationException(nameof(identityInfo), 
                "òœ ?ò « œ— «·„’ÿ›? ?«›  ‰‘œ / òœ ?ò « „⁄ »— ‰„? »«‘œ.");
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

        // »Âù—Ê“—”«‰? «ÿ·«⁄«  œ«‰‘ÃÊ
        var studentIdentity = new UpdateStudentBirthCertInfoRepoCommand
        {
            Codm = command.Codm,
            NationalCode = null,
            YektaCode = certInfo.YektaCode,
            BirthDate = certInfo.BirthDate.StringDateToInt().Value,
            IsSadat = student.IsSadat,
            BirthCertDescription = null,
            Religion = student.Religion,
            UserId = await authenticatedUser.GetUserIdAsync() ?? 0,
            ApplicationId = 66,
            DataSource = DataSource.WebService,
            PersonnelId = await authenticatedUser.GetPersonnelIdAsync() ?? 0,
        };

        await studentRepo.UpdateStudentBirthCertInfo(studentIdentity);
    }
}
