using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.BlockServices.Commands;

/// <summary>ثبت</summary>
public sealed record CreateDependentBlockServiceCommand : BaseCommandDto<CreateDependentBlockServiceCommand, DependentBlockService>, IRequest<int>
{
    /// <summary>شناسه تکفل</summary>
    public long DependentId { get; init; }

    /// <summary>شناسه سرویس</summary>
    public int ServiceId { get; init; }

    /// <summary>علت</summary>
    public string Reason { get; init; }

    /// <summary>تاریخ انسداد</summary>
    public string BlockDate { get; init; }

    /// <inheritdoc/>
    public override void ReverseCustomMappings(IMappingExpression<CreateDependentBlockServiceCommand, DependentBlockService> mapping) {
        mapping.ForMember(model => model.BlockDate, config => config.MapFrom(dto => dto.BlockDate.StringDateToInt()));
    }
}

internal sealed class CreateDependentBlockServiceCommandHandler(IRepository<DependentBlockService> repo, IRepository<DependentSummary, long> dependentRepo)
    : IRequestHandler<CreateDependentBlockServiceCommand, int>
{
    public async Task<int> Handle(CreateDependentBlockServiceCommand command, CancellationToken cancellation) {

        //TODO ثبت درخواست
        if ( await repo.ExistsAsync(x => x.DependentId == command.DependentId && x.ServiceId == command.ServiceId) ) {
            throw new CommandValidationException("این خدمت برای تکفل مسدود شده است.");
        }

        var dependentBlockService = command.ToEntity();
        dependentBlockService.Codm = (await dependentRepo.GetByIdAsync(command.DependentId, false, cancellationToken:cancellation)).Codm;
        await repo.InsertAsync(dependentBlockService);
        return dependentBlockService.Id;
    }
}
