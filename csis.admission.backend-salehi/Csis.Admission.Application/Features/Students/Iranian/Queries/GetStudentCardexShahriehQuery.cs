//using Csis.Admission.Application.Features.Students.Dtos;
//using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

//namespace Csis.Admission.Application.Features.Students.Queries;

///// <summary>کاردکس اطلاعات شهریه طلبه</summary>
///// <param name="Codm">کد مرکز</param>
///// <param name="Tail">چند رکورد آخر برگردانده شود</param>
///// <param name="PayDateFrom">پرداخت از تاریخ</param>
///// <param name="PayDateTo">پرداخت تا تاریخ</param>
//public sealed record GetStudentCardexShahriehQuery(int Codm, int? Tail, DateTime? PayDateFrom, DateTime? PayDateTo) 
//    : IRequest<List<StudentCardexShahriehDto>>;

///// <summary>کاردکس اطلاعات شهریه طلبه</summary>
//internal sealed class GetStudentCardexShahriehQueryHandler : IRequestHandler<GetStudentCardexShahriehQuery, List<StudentCardexShahriehDto>>
//{
//    private readonly IStudentRepository _studentRepo;
//    public GetStudentCardexShahriehQueryHandler(IStudentRepository studentRepo) {
//        _studentRepo = studentRepo;
//    }

//    public async Task<List<StudentCardexShahriehDto>> Handle(GetStudentCardexShahriehQuery query, CancellationToken cancellationToken) {

//        var cardexShahrieh = await _studentRepo.GetStudentCardexShahrieh(query);
//        return cardexShahrieh;
//    }
//}
