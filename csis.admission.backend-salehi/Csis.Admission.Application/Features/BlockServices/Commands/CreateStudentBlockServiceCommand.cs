using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.BlockServices.Commands;

/// <summary>ثبت</summary>
public sealed record CreateStudentBlockServiceCommand : BaseCommandDto<CreateStudentBlockServiceCommand, StudentBlockService>, IRequest<int>
{
    /// <summary>کد مرکز</summary>
    public int Codm { get; init; }

    /// <summary>شناسه سرویس</summary>
    public int ServiceId { get; init; }

    /// <summary>علت</summary>
    public string Reason { get; init; }

    /// <summary>تاریخ انسداد</summary>
    public string BlockDate { get; init; }

    /// <inheritdoc/>
    public override void ReverseCustomMappings(IMappingExpression<CreateStudentBlockServiceCommand, StudentBlockService> mapping) {
        mapping.ForMember(model => model.BlockDate, config => config.MapFrom(dto => dto.BlockDate.StringDateToInt()));
    }
}

internal sealed class CreateStudentBlockServiceCommandHandler(IRepository<StudentBlockService> repo) 
    : IRequestHandler<CreateStudentBlockServiceCommand, int>
{
    public async Task<int> Handle(CreateStudentBlockServiceCommand command, CancellationToken cancellation) {

        //TODO ثبت درخواست

        if(await repo.ExistsAsync(x => x.Codm == command.Codm && x.ServiceId == command.ServiceId) ) {
            throw new CommandValidationException("این خدمت برای طلبه مسدود شده است.");
        }

        var studentBlockService = command.ToEntity();
        await repo.InsertAsync(studentBlockService);
        return studentBlockService.Id;
    }
}
