using Microsoft.AspNetCore.Http;
using Csis.Authorization.Services;
using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.Addresses.Commands;

/// <summary>ثبت/ویرایش آدرس</summary>
public sealed record CreateOrUpdateStudentAddressCommand : BaseCommandDto<CreateOrUpdateStudentAddressCommand, Address>, IRequest<int>
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }

    /// <summary>استان</summary>
    public short? ProvinceId { get; set; }

    /// <summary>شهرستان </summary>
    public short? CityId { get; set; }

    /// <summary>بخش</summary>
    public short? PortionId { get; set; }

    /// <summary>شهر</summary>
    public short? TownId { get; set; }

    /// <summary>دهستان</summary>
    public short? RuralId { get; set; }

    /// <summary>شهرک</summary>
    public string Township { get; set; }

    /// <inheritdoc/>
    public string Village { get; set; }

    /// <summary>محله</summary>
    public string District { get; set; }

    /// <summary>خیابان اصلی</summary>
    public string Avenue { get; set; }

    /// <summary>خیابان فرعی</summary>
    public string Street { get; set; }

    /// <summary>کوچه اصلی</summary>
    public string Alley { get; set; }

    /// <summary>کوچه فرعی</summary>
    public string Lane { get; set; }

    /// <summary>پلاک</summary>
    public string Number { get; set; }

    /// <summary>مجتمع</summary>
    public string Complex { get; set; }

    /// <summary>بلوک</summary>
    public string Block { get; set; }

    /// <summary>واحد</summary>
    public string Unit { get; set; }

    /// <inheritdoc/>
    public short? Floor { get; set; }

    /// <inheritdoc/>
    public long? ZipCode { get; set; }

    /// <inheritdoc/>
    public string ConfirmDate { get; set; }

    /// <summary>همیشه یک</summary>
    public short ProjectCode { get; set; }

    /// <summary>همیشه یک</summary>
    public bool? Flag { get; set; }

    /// <summary>نیازمند تایید دو طلبه دیگر</summary>
    public bool? RequiresDualStudentApproval { get; set; }

    /// <summary>کد مرکز خدمات طلابی که آدرس را تایید خواهند کرد</summary>
    public int[] ConfirmedStudentCodms { get; set; } = null;

    /// <summary>شناسه درخواست</summary>
    public long RequestId { get; set; }//TODO نباید باشد چون با یک درخواست دو کامند باید اجرا شود اینگونه شده است

    /// <inheritdoc/>
    public override void ReverseCustomMappings(IMappingExpression<CreateOrUpdateStudentAddressCommand, Address> mapping) {
        mapping.ForMember(model => model.ConfirmDate, config => config.MapFrom(dto => dto.ConfirmDate.StringDateToInt()));
    }
}

internal sealed class UpdateStudentAddressCommandHandler(
    IRepository<Address> repo,
    IHttpContextAccessor context,
    IStudentRepository studentRepository,
    ICsisAuthenticatedUserService authenticatedUser)
    : IRequestHandler<CreateOrUpdateStudentAddressCommand, int>
{
    public async Task<int> Handle(CreateOrUpdateStudentAddressCommand command, CancellationToken cancellationToken) {

        var address = await repo.GetOneAsTrackingAsync(x => x.Codm == command.Codm, false, cancellationToken);

        var addressId = 0;
        if ( address == null ) {
            var entity = command.ToEntity();
            await repo.InsertAsync(entity, true, cancellationToken);
            addressId= entity.Id;

        } else {
            var entity = command.ToEntity(address);
            await repo.UpdateAsync(entity, true, cancellationToken);
            addressId=entity.Id;
        }

        // update branch and agency
        var repoCommand = new UpdateBranchAndAgencyRepoCommand { Codm = command.Codm };
        await Common.Utilities.SetLogParam(repoCommand, authenticatedUser, context);
        repoCommand.RequestId=command.RequestId;
        await studentRepository.UpdateBranchAndAgency(repoCommand);

        // return address Id
        return addressId;
    }
}
