using Csis.Admission.Application.Features.Famous.Commands;
using Csis.Admission.Application.Features.Famouses.Commands;
using Csis.Admission.Application.Features.Researches.Commands;
using Csis.Admission.Domain.Entities;
using Csis.Admission.Domain.Enums;

//using Csis.Admission.Application.Features.Documents.Commands;
//using Csis.Admission.Application.Features.Marriages.Commands;
//using Csis.Admission.Application.Features.NonStudentDependants.Commands;
//using Csis.Admission.Application.Features.NonStudents.Commands;
//using Csis.Admission.Application.Features.People.Commands;
//using Csis.Admission.Domain.Entities;
//using Csis.Admission.Domain.Enums;
//namespace Csis.Admission.IntegrationTests;
//internal partial class Testing
//{

//    internal static async Task<Person> CreatePersonAsync(
//        string fidaCode,
//        string nationalCode,
//        string yektaCode,
//        string bankAccountNumber = "frnyaget",
//        string birthCertDescription = "tvdspsft",
//        string birthCertIssuePlace = "cxcofxmm",
//        string birthCertIssueProvince = "vprmxjmy",
//        string birthCertNumber = "avzfvxdt",
//        string birthCertSeri = "dzhwbs",
//        string birthCertSerial = "pggyof",
//        string email = "Nat.Parisian20@gmail.com",
//        string fatherName = "womhtfmu",
//        string firstName = "Helmer",
//        string lastName = "McLaughlin",
//        string latinLastName = null,
//        string mobile = "428.615.230",
//        string nickName = "pswjhqor",
//        string passportNumber = "mvcezzqf",
//        short nationality = 4263,
//        bool isDead = false,
//        bool isSadat = false,
//        DateOnly? birthDate = null,
//        DateOnly? deathDate = null,
//        DateOnly? residenceExpireDate = null,
//        DeathCause? deathCause = null,
//        Gender gender = 0,
//        Religion religion = 0,
//        SingleStatus? singleStatus = null,
//        int? fatherPersonId = null,
//        int? motherPersonId = null) {
//        var command = new CreatePersonCommand {
//            BankAccountNumber = bankAccountNumber,
//            BirthCertDescription = birthCertDescription,
//            BirthCertIssuePlace = birthCertIssuePlace,
//            BirthCertIssueProvince = birthCertIssueProvince,
//            BirthCertNumber = birthCertNumber,
//            BirthCertSeri = birthCertSeri,
//            BirthCertSerial = birthCertSerial,
//            Email = email,
//            FatherName = fatherName,
//            FidaCode = fidaCode,
//            FirstName = firstName,
//            LastName = lastName,
//            LatinLastName = latinLastName,
//            Mobile = mobile,
//            NationalCode = nationalCode,
//            NickName = nickName,
//            PassportNumber = passportNumber,
//            YektaCode = yektaCode,
//            Nationality = nationality,
//            //BirthDate = birthDate,
//            FatherPersonId = fatherPersonId,
//            MotherPersonId = motherPersonId,
//            IsDead = isDead,
//            IsSadat = isSadat,
//            //DeathDate = deathDate,
//            //ResidenceExpireDate = residenceExpireDate,
//            DeathCause = deathCause,
//            Gender = gender,
//            Religion = religion,
//            SingleStatus = singleStatus,
//        };

//        var id = await SendAsync(command);

//        return await FindAsync<Person>(id);
//    }

//    internal static async Task<NonStudent> CreateNonStudentAsync(int personId) {
//        var command = new CreateNonStudentCommand {
//            PersonId = personId,
//        };

//        var id = await SendAsync(command);

//        return await FindAsync<NonStudent>(id);
//    }

//    internal static async Task<NonStudentDependant> CreateNonStudentDependantAsync(
//        int personId,
//        long nonStudentCodm,
//        bool isActive = false,
//        Relationship relationship = 0) {
//        var command = new CreateNonStudentDependantCommand {
//            PersonId = personId,
//            NonStudentCodm = nonStudentCodm,
//            IsActive = isActive,
//            Relationship = relationship,
//        };

//        var id = await SendAsync(command);

//        return await FindAsync<NonStudentDependant>(id);
//    }

//    internal static async Task<Marriage> CreateMarriageAsync(int? husbandPersonId = null, int? wifePersonId = null) {
//        var command = new CreateMarriageCommand {
//            HusbandPersonId = husbandPersonId,
//            WifePersonId = wifePersonId,
//        };

//        var id = await SendAsync(command);

//        return await FindAsync<Marriage>(id);
//    }

//    internal static async Task<Document> CreateDocumentAsync(
//        string fileIdentifier,
//        int personId,
//        int? tableId = null,
//        int? tableRecordId = null,
//        FileType type = 0,
//        int? nonStudentCodm = null,
//        int? nonStudentDependantId = null,
//        int? studentCodm = null,
//        int? studentDependantId = null) {
//        var command = new CreateDocumentCommand {
//            FileIdentifier = fileIdentifier,
//            NonStudentCodm = nonStudentCodm,
//            NonStudentDependantId = nonStudentDependantId,
//            PersonId = personId,
//            StudentCodm = studentCodm,
//            StudentDependantId = studentDependantId,
//            TableId = tableId,
//            TableRecordId = tableRecordId,
//            Type = type,
//        };

//        var id = await SendAsync(command);

//        return await FindAsync<Document>(id);
//    }
//
async Task<Research> CreateResearchAsync(
        string articlePublication = "nphwimsd",
        string bookPublisher = "fqjnyzsu",
        string bookShabak = "rqhxypwi",
        string projectEmployer = "cdxqewas",
        string title = "porro",
        short? subjectId = null,
        short? year = null,
        int codm = 4513) {
        var command = new CreateResearchCommand {
            ArticlePublication = articlePublication,
            BookPublisher = bookPublisher,
            BookShabak = bookShabak,
            ProjectEmployer = projectEmployer,
            Title = title,
            SubjectId = subjectId,
            Year = year,
            Codm = codm,
        };

        var id = await SendAsync(command);

        return await FindAsync<Research>(id);
    }

async Task<Famous> CreateFamousAsync(
        string actionPlace = "kxzffaeh",
        string position = "msvqingt",
        int codm = 77230) {
        var command = new CreateFamousCommand {
            Codm = codm,
            Type = TypeEnum.ReligiousAuthorities,
            Role = RoleEnum.BoardOfTrusteesAndManagersOfReligiousCenters,
            Area = AreaEnum.International
        };

        var id = await SendAsync(command);

        return await FindAsync<Famous>(id);
    }

    internal static async Task<CaseValidityReason> CreateCaseValidityReasonsAsync() {
        var caseValidityReasons = new CaseValidityReason {
        };

        await AddAsync(caseValidityReasons);

        return caseValidityReasons;
    }

    internal static async Task<CaseBlockReason> CreateCaseBlockReasonAsync() {
        var caseBlockReason = new CaseBlockReason {
        };

        await AddAsync(caseBlockReason);

        return caseBlockReason;
    }

    internal static async Task<DependentActiveReason> CreateDependentActiveReasonAsync() {
        var dependentActiveReason = new DependentActiveReason {
        };

        await AddAsync(dependentActiveReason);

        return dependentActiveReason;
    }

    internal static async Task<DependentDeActiveReason> CreateDependentDeActiveReasonAsync() {
        var dependentDeActiveReason = new DependentDeActiveReason {
        };

        await AddAsync(dependentDeActiveReason);

        return dependentDeActiveReason;
    }

    internal static async Task<ShiaMinitory> CreateShiaMinitoryAsync() {
        var shiaMinitory = new ShiaMinitory {
        };

        await AddAsync(shiaMinitory);

        return shiaMinitory;
    }
}
