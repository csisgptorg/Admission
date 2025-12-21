using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models.Repository;

namespace Csis.Admission.Application.Features.Students.Queries;
// به خاطر انسجام و قاطی نشدن هر سه کوئری در یک فایل جدا شدند
/// <summary>
/// دریافت اطلاعات برای اجرای پرداخت بر اساس کد مرکز (PayRun)
/// </summary>
/// <param name="Codm"></param>
public sealed record GetDataForPayRunByCodmQuery(int Codm) : IRequest<StudentDataForPayRunDto>;
internal sealed class GetDataForPayRunByCodmHandler(IStudentRepository studentRepository, IMapper mapper) : IRequestHandler<GetDataForPayRunByCodmQuery, StudentDataForPayRunDto>
{
    public async Task<StudentDataForPayRunDto> Handle(GetDataForPayRunByCodmQuery request, CancellationToken cancellationToken) {
        var result = await studentRepository.GetDataForPayRunByCodm(request.Codm);
        return mapper.Map<StudentDataForPayRunDto>(result);
    }
}




/// <summary>
/// دریافت اطلاعات برای اجرای پرداخت بر اساس لیست کد مرکز (PayRun)
/// </summary>
/// <param name="CodmList"></param>
public sealed record GetDataForPayRunByCodmListQuery(List<int> CodmList) : IRequest<List<StudentDataForPayRunDto>>;
internal sealed class GetDataForPayRunByCodmListHandler(IStudentRepository studentRepository, IMapper mapper) : IRequestHandler<GetDataForPayRunByCodmListQuery, List<StudentDataForPayRunDto>>
{
    public async Task<List<StudentDataForPayRunDto>> Handle(GetDataForPayRunByCodmListQuery request, CancellationToken cancellationToken) {
        var codmList = string.Join(",", request.CodmList);
        var result = await studentRepository.GetDataForPayRunByCodmList(codmList);
        return [.. result.Select(mapper.Map<StudentDataForPayRunDto>)];
    }
}





/// <summary>
/// دریافت اطلاعات برای اجرای پرداخت بر اساس بازه کد مرکز (PayRun)
/// </summary>
public sealed record GetDataForPayRunByStartEndCodmQuery(int StartCodm, int EndCodm) : IRequest<List<StudentDataForPayRunDto>>;
internal sealed class GetDataForPayRunByStartEndCodmHandler(IStudentRepository studentRepository, IMapper mapper) : IRequestHandler<GetDataForPayRunByStartEndCodmQuery, List<StudentDataForPayRunDto>>
{
    public async Task<List<StudentDataForPayRunDto>> Handle(GetDataForPayRunByStartEndCodmQuery request, CancellationToken cancellationToken) {
        var result = await studentRepository.GetDataForPayRunByStartEndCodm(request.StartCodm, request.EndCodm);
        return [.. result.Select(mapper.Map<StudentDataForPayRunDto>)];
    }
}





/// <summary>
/// دریافت بیشترین کد مرکز برای اجرای پرداخت (PayRun)
/// </summary>
public sealed record GetMaxCodmForPayRunQuery() : IRequest<int>;
internal sealed class GetMaxCodmForPayRunHandler(IRepository<StudentSummary> studentRepository) : IRequestHandler<GetMaxCodmForPayRunQuery, int>
{
    public async Task<int> Handle(GetMaxCodmForPayRunQuery request, CancellationToken cancellationToken) {
        var result = (await studentRepository.GetAllAsync(cancellationToken: cancellationToken)).Max(x => x.Codm);
        return result;
    }
}
