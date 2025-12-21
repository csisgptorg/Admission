using SpouseRelationalType = Csis.Admission.Application.Common.Models.ValidateSpousalRelationship.ValidateSpousalRelationshipRequest;
using ParentRelationalType = Csis.Admission.Application.Common.Models.ValidateParentChildRelationshipRequest;

namespace Csis.Admission.Application.Features.People.Dtos;

/// <summary>
/// انتساب نسبت خانوادگی به شخص
/// </summary>
/// <param name="ParentNationalCode"></param>
/// <param name="ParentBirthDate"></param>
/// <param name="ChildNationalCode"></param>
/// <param name="ChildBirthDate"></param>
/// <param name="RelationType"></param>
public sealed record AssignFamilyDto(string ParentNationalCode, int ParentBirthDate, string ChildNationalCode,
        string ChildBirthDate, ParentRelationalType.RelationTypeEnum RelationType);

/// <summary>
/// انتساب نسبت همسری به شخص
/// </summary>
/// <param name="NationalCode"></param>
/// <param name="BirthDate"></param>
/// <param name="NationalCodeSpouse"></param>
/// <param name="BirthDateSpouse"></param>
/// <param name="EventDate"></param>
public sealed record AssignSpouseDto(string NationalCode, int? BirthDate, string NationalCodeSpouse, string BirthDateSpouse,
        string EventDate);
