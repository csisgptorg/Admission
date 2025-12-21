using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Protests.Commands;

/// <summary>
/// ثبت اعتراض بر اساس کد مرکز
/// </summary>
public sealed record CreateProtestByCodmCommand : BaseCommandDto<CreateProtestByCodmCommand, Protest, long>, IRequest<long>
{
    /// <summary>
    /// ثبت اعتراض بر اساس کد مرکز
    /// </summary>
    public int Codm { get; init; }

    /// <summary>شناسه فیلد مورد اعتراض</summary>
    public ProtestFormTitle FieldId { get; init; }

    /// <summary>
    ///  این فیلد برای اعتراضات مربوط به سوابق مسکن می‌باشد (BeingLandlord, HousingBuySellHistory, PersonalHousingHistory)
    /// </summary>
    public bool? HasHousingHistory { get; init; }

    /// <summary>توضیحات</summary>
    public string FieldDescription { get; init; }
}

internal sealed class CreateProtestByCodmCommandHandler(IRepository<Protest, long> protestRepository)
    : IRequestHandler<CreateProtestByCodmCommand, long>
{
    public async Task<long> Handle(CreateProtestByCodmCommand request, CancellationToken cancellationToken) {

        var entity = request.ToEntity();
        await protestRepository.InsertAsync(entity, cancellationToken: cancellationToken);
        return entity.Id;
    }
}
