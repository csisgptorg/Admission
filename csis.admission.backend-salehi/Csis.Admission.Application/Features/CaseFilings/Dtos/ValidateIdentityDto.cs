namespace Csis.Admission.Application.Features.CaseFilings.Dtos;

internal record ValidateIdentityDto(string FirstName, string LastName, string FatherName, Citizenship Citizenship, string BirthDate,
    string NationalCode = null, string YektaCode = null, Gender? Gender = null, string CommissionStatus = null, int? CommissionNumber = null);
