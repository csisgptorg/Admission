using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Extensions;
using Csis.Authorization.Services;
using System.Text.Json.Serialization;

namespace Csis.Admission.Application.Features.Students.Commands;

/// <summary>»—Ê“ —”«‰? «ÿ·«⁄«  ‘‰«”‰«„Â «? »—«”«” À»  «ÕÊ«· - »œÊ‰ ‰?«“ »Â Ê—Êœ? òœ„·? Ê  «—?Œ  Ê·œ</summary>
public sealed record SyncStudentBirthCertByCodmCommand : IRequest
{
    /// <summary>òœ „—ò“ Œœ„« </summary>
    public int Codm { get; init; }
    
    /// <summary> «??œ</summary>
    [JsonIgnore]
    public bool? Confirmed { get; set; }
}

internal sealed class SyncStudentBirthCertByCodmCommandHandler(
    IStudentRepository studentRepo, 
    IRepository<StudentSummary> studentSummaryRpo,
    ICsisWsmService wsmService, 
    ICsisAuthenticatedUserService authenticatedUser)
    : IRequestHandler<SyncStudentBirthCertByCodmCommand>
{
    public async Task Handle(SyncStudentBirthCertByCodmCommand command, CancellationToken cancellation)
    {
        // œ—?«›  «ÿ·«⁄«  œ«‰‘ÃÊ «“ ”?” „
        var student = await studentSummaryRpo.GetOneAsync(x => x.Codm == command.Codm, false, cancellation);

        if (string.IsNullOrEmpty(student.NationalCode))
        {
            throw new CommandValidationException("òœ „·? œ«‰‘ÃÊ œ— ”?” „ À»  ‰‘œÂ «” .");
        }

        if (student.BirthDate == null || student.BirthDate == 0)
        {
            throw new CommandValidationException(" «—?Œ  Ê·œ œ«‰‘ÃÊ œ— ”?” „ À»  ‰‘œÂ «” .");
        }

        // œ—?«›  «ÿ·«⁄«  «“ À»  «ÕÊ«· »« «” ›«œÂ «“ «ÿ·«⁄«  À»  ‘œÂ
        var birthDateString = student.BirthDate.ToString();
        var identityRequest = new GetIdentityInfoByNationalCodeRequestApiM(
            student.NationalCode, 
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

        // »Âù—Ê“—”«‰? «ÿ·«⁄«  œ«‰‘ÃÊ
        var studentIdentity = new UpdateStudentBirthCertInfoRepoCommand
        {
            Codm = command.Codm,
            NationalCode = student.NationalCode,
            YektaCode = null,
            BirthDate = certInfo.BirthDate.StringDateToInt().Value,
            IsSadat = certInfo.IsSadat,
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
