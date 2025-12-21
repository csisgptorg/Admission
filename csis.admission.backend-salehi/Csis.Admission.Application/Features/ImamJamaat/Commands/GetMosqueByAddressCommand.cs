using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.ImamJamaat.Dtos;
using System.Linq.Expressions;

namespace Csis.Admission.Application.Features.ImamJamaat.Commands;

public sealed record GetMosqueByAddressCommand : IRequest<List<MosqueAddressWithNameDto>>
{
    public short? ProvinceId { get; init; }

    /// <summary>شهرستان </summary>
    public short? CityId { get; init; }

    /// <summary>بخش</summary>
    public short? PortionId { get; init; }

    /// <summary>شهر</summary>
    public short? TownId { get; init; }

    /// <summary>دهستان</summary>
    public short? RuralId { get; init; }

    /// <summary>شهرک</summary>
    public string? Township { get; init; }

    /// <inheritdoc/>
    public string? Village { get; init; }

    /// <summary>محله</summary>
    public string? District { get; init; }

    /// <summary>خیابان اصلی</summary>
    public string? Avenue { get; init; }

    /// <summary>خیابان فرعی</summary>
    public string? Street { get; init; }

    /// <summary>کوچه اصلی</summary>
    public string? Alley { get; init; }

    /// <summary>کوچه فرعی</summary>
    public string? Lane { get; init; }

    /// <summary>پلاک</summary>
    public string? Number { get; init; }

    /// <summary>مجتمع</summary>
    public string? Complex { get; init; }

    /// <summary>بلوک</summary>
    public string? Block { get; init; }

    /// <summary>واحد</summary>
    public string? Unit { get; init; }

    /// <inheritdoc/>
    public short? Floor { get; init; }

    /// <inheritdoc/>
    public long? ZipCode { get; init; }

    /// <summary>نام مسجد (جستجو در نام رسمی و نام محلی)</summary>
    public string? MosqueName { get; init; }
};

internal sealed class GetMosqueByAddressCommandHandler(
    IRepository<Mosque> mosquesRepository,
    IMapper mapper)
    : IRequestHandler<GetMosqueByAddressCommand, List<MosqueAddressWithNameDto>>
{
    public async Task<List<MosqueAddressWithNameDto>> Handle(GetMosqueByAddressCommand request, CancellationToken cancellationToken) {
        // Build a dynamic predicate based on the provided parameters
        Expression<Func<Mosque, bool>> predicate = x => !x.Deleted;

        // First, ensure we have MosqueAddress and ProjectCode = 7
        predicate = CombineExpressions(predicate, x => x.MosqueAddress != null && x.MosqueAddress.ProjectCode == 7);

        // Add condition for mosque name if provided - search in both OfficialName and LocalNames
        if ( !string.IsNullOrWhiteSpace(request.MosqueName) ) {
            predicate = CombineExpressions(
                predicate,
                x => (x.OfficialName != null && x.OfficialName.Contains(request.MosqueName)) ||
                     (x.LocalNames != null && x.LocalNames.Contains(request.MosqueName))
            );
        }

        // Add conditions for each provided parameter using null-safe navigation
        if ( request.ProvinceId.HasValue ) {
            predicate = CombineExpressions(predicate, x => x.MosqueAddress.ProvinceId == request.ProvinceId);
        }

        if ( request.CityId.HasValue ) {
            predicate = CombineExpressions(predicate, x => x.MosqueAddress.CityId == request.CityId);
        }

        if ( request.PortionId.HasValue ) {
            predicate = CombineExpressions(predicate, x => x.MosqueAddress.PortionId == request.PortionId);
        }

        if ( request.TownId.HasValue ) {
            predicate = CombineExpressions(predicate, x => x.MosqueAddress.TownId == request.TownId);
        }

        if ( request.RuralId.HasValue ) {
            predicate = CombineExpressions(predicate, x => x.MosqueAddress.RuralId == request.RuralId);
        }

        // Check for string properties with string comparison methods
        if ( !string.IsNullOrWhiteSpace(request.Township) ) {
            predicate = CombineExpressions(predicate, x => x.MosqueAddress.Township != null && x.MosqueAddress.Township.Contains(request.Township));
        }

        if ( !string.IsNullOrWhiteSpace(request.Village) ) {
            predicate = CombineExpressions(predicate, x => x.MosqueAddress.Village != null && x.MosqueAddress.Village.Contains(request.Village));
        }

        if ( !string.IsNullOrWhiteSpace(request.District) ) {
            predicate = CombineExpressions(predicate, x => x.MosqueAddress.District != null && x.MosqueAddress.District.Contains(request.District));
        }

        if ( !string.IsNullOrWhiteSpace(request.Avenue) ) {
            predicate = CombineExpressions(predicate, x => x.MosqueAddress.Avenue != null && x.MosqueAddress.Avenue.Contains(request.Avenue));
        }

        if ( !string.IsNullOrWhiteSpace(request.Street) ) {
            predicate = CombineExpressions(predicate, x => x.MosqueAddress.Street != null && x.MosqueAddress.Street.Contains(request.Street));
        }

        if ( !string.IsNullOrWhiteSpace(request.Alley) ) {
            predicate = CombineExpressions(predicate, x => x.MosqueAddress.Alley != null && x.MosqueAddress.Alley.Contains(request.Alley));
        }

        if ( !string.IsNullOrWhiteSpace(request.Lane) ) {
            predicate = CombineExpressions(predicate, x => x.MosqueAddress.Lane != null && x.MosqueAddress.Lane.Contains(request.Lane));
        }

        if ( !string.IsNullOrWhiteSpace(request.Number) ) {
            predicate = CombineExpressions(predicate, x => x.MosqueAddress.Number != null && x.MosqueAddress.Number.Contains(request.Number));
        }

        if ( !string.IsNullOrWhiteSpace(request.Complex) ) {
            predicate = CombineExpressions(predicate, x => x.MosqueAddress.Complex != null && x.MosqueAddress.Complex.Contains(request.Complex));
        }

        if ( !string.IsNullOrWhiteSpace(request.Block) ) {
            predicate = CombineExpressions(predicate, x => x.MosqueAddress.Block != null && x.MosqueAddress.Block.Contains(request.Block));
        }

        if ( !string.IsNullOrWhiteSpace(request.Unit) ) {
            predicate = CombineExpressions(predicate, x => x.MosqueAddress.Unit != null && x.MosqueAddress.Unit.Contains(request.Unit));
        }

        if ( request.Floor.HasValue ) {
            predicate = CombineExpressions(predicate, x => x.MosqueAddress.Floor == request.Floor);
        }

        if ( request.ZipCode.HasValue ) {
            predicate = CombineExpressions(predicate, x => x.MosqueAddress.ZipCode == request.ZipCode);
        }

        // Execute the query with the built predicate, include MosqueAddress navigation property
        var mosques = await mosquesRepository.GetAllAsync(
            predicate,
            cancellationToken: cancellationToken,
            x => x.MosqueAddress);
        
        if ( mosques.Count > 0 )
            return mapper.Map<List<MosqueAddressWithNameDto>>(mosques);
        return [];
    }

    /// <summary>
    /// Combines two expressions with AND operator
    /// </summary>
    private static Expression<Func<Mosque, bool>> CombineExpressions(
        Expression<Func<Mosque, bool>> expr1,
        Expression<Func<Mosque, bool>> expr2) {
        // Create a parameter for the combined expression
        var parameter = Expression.Parameter(typeof(Mosque), "x");

        // Replace parameter in the first expression
        var leftVisitor = new ReplaceParameterVisitor(expr1.Parameters[0], parameter);
        var left = leftVisitor.Visit(expr1.Body);

        // Replace parameter in the second expression
        var rightVisitor = new ReplaceParameterVisitor(expr2.Parameters[0], parameter);
        var right = rightVisitor.Visit(expr2.Body);

        // Combine expressions with AND
        var body = Expression.AndAlso(left, right);

        // Create new combined lambda expression
        return Expression.Lambda<Func<Mosque, bool>>(body, parameter);
    }

    /// <summary>
    /// Helper class to replace expression parameters
    /// </summary>
    private class ReplaceParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
        : ExpressionVisitor
    {
        protected override Expression VisitParameter(ParameterExpression node) {
            return node == oldParameter ? newParameter : base.VisitParameter(node);
        }
    }
}
