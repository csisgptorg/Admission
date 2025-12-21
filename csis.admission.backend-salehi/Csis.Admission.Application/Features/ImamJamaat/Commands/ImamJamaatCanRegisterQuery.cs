using Csis.Admission.Application.Common.Helpers;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Utilities;

namespace Csis.Admission.Application.Features.ImamJamaat.Commands;

/// <summary>
/// (استعلام امکان ثبت درخواست امام جماعت(اعتبارسنجی
/// </summary>
/// <param name="Codm"></param>
public sealed record ImamJamaatCanRegisterQuery(int Codm) : IRequest<bool>;
internal sealed class ImamJamaatCanRegisterQueryHandler(IStudentRepository studentRepository, IRepository<Preach> preachRepository) : IRequestHandler<ImamJamaatCanRegisterQuery, bool>
{
    public async Task<bool> Handle(ImamJamaatCanRegisterQuery request, CancellationToken cancellationToken) {
        var imamJamaatInfo = await studentRepository.GetStudentInfoByCodm(request.Codm);

        switch ( imamJamaatInfo ) {
            case null or { Religion: Religion.Sunni }:
                throw new CommandValidationException("امکان ثبت درخواست برای امام جماعت اهل سنت وجود ندارد.");
            case { IsDead: true } or { Gender: Gender.Female }:
                throw new CommandValidationException("امکان ثبت درخواست برای امام جماعت زن یا متوفی وجود ندارد.");
            case { BirthDate: var birthDate }:
                if ( !ValidateAge(birthDate) ) {
                    throw new CommandValidationException("امکان ثبت درخواست برای امام جماعت کمتر از 18 سال وجود ندارد.");
                }
                break;
        }
        return true;
    }
    //TODO: انسداد اضافه شود


    private static bool ValidateAge(string birthdate) { //13810214
        var normalizedBirth= PersianDateTime.Parse(birthdate);
        var threshold = PersianDateTime.Now.AddYears(-18);
        var age = normalizedBirth < threshold;

        if ( !age ) {
            throw new CommandValidationException("سن متقاضی کمتر از 18 سال می باشد");
        }
        return true;
    }
}
