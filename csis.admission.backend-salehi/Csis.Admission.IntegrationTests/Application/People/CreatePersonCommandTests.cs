using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Features.People.Commands;
using Csis.Admission.Application.Features.People.Validators;
using Csis.Admission.Domain.Entities;
using Csis.Admission.Domain.Enums;
using Csis.Utilities;
using Csis.Utilities.Extensions;
using FluentValidation.TestHelper;

namespace Csis.Admission.IntegrationTests.Application.People;

internal sealed class CreatePersonCommandTests : BaseTestFixture
{
    [Test]
    public async Task Handle_WhenCalled_ShouldCreatePerson() {
        var command = new CreatePersonManualCommand {
            BankAccountNumber = "zczbzcmf",
            BirthCertDescription = "lmfluvhh",
            BirthCertIssuePlace = "ibhmfsol",
            BirthCertIssueProvince = "cmletnof",
            BirthCertNumber = "ieyjliaz",
            BirthCertSeri = "gufgra",
            BirthCertSerial = "123",
            FatherName = "mivcqeby",
            FidaCode = "ygsagidb",
            FirstName = "Mike",
            LastName = "Green",
            Mobile = "214.367.102",
            NationalCode = "tvfemacz",
            PassportNumber = "ynhkngea",
            Nationality = 5611,
            //BirthDate = "2013, 9, 1",
            //FatherPersonId = 79189, //TODO: Change this to use correct foreign key
            //MotherPersonId = 32731, //TODO: Change this to use correct foreign key
            IsDead = false,
            IsSadat = false,
            //DeathDate = "2013, 9, 1",
            //ResidenceExpireDate = "2010, 2, 1",
            DeathCause = 0,
            Gender = 0,
            Religion = 0,
            SingleStatus = 0,
            Citizenship = new Citizenship(),
        };

        var id = await SendAsync(command);

        var person = await FindAsync<Person>(id);

        person.BankAccountNumber.Should().Be("zczbzcmf");
        person.BirthCertDescription.Should().Be("lmfluvhh");
        person.BirthCertIssuePlace.Should().Be("ibhmfsol");
        person.BirthCertIssueProvince.Should().Be("cmletnof");
        person.BirthCertNumber.Should().Be("ieyjliaz");
        person.BirthCertSeri.Should().Be("gufgra");
        person.BirthCertSerial.Should().Be(123);
        person.FatherName.Should().Be("mivcqeby");
        person.FidaCode.Should().Be("ygsagidb");
        person.FirstName.Should().Be("Mike");
        person.LastName.Should().Be("Green");
        person.Mobile.Should().Be("214.367.102");
        person.NationalCode.Should().Be("tvfemacz");
        person.PassportNumber.Should().Be("ynhkngea");
        person.YektaCode.Should().Be("ypipsmlj");
        person.Nationality.Should().Be(5611);
        person.BirthDate.Should().Be( new DateOnly(2013, 9, 1));
        person.FatherPersonId.Should().Be(79189);
        person.MotherPersonId.Should().Be(32731);
        person.IsDead.Should().Be(false);
        person.IsSadat.Should().Be(false);
        //person.DeathDate.Should().Be("2013, 9, 1");
        //person.ResidenceExpireDate.Should().Be("2010, 2, 1");
        person.DeathCause.Should().Be(0);
        person.Gender.Should().Be(0);
        person.Religion.Should().Be(0);
        person.Citizenship.Should().Be(new Citizenship());
        person.UpdatedOn.Should().Be(null);
        person.LastUpdatedById.Should().Be(null);
        person.DeletedOn.Should().Be(null);
        person.DeletedById.Should().Be(null);
        person.Deleted.Should().BeFalse();
    }

    [Test]
    public async Task HandleCreate_WhenPersonFidaCodeIsDuplicate_ShouldThrowCommandValidationException() {
        var command = new CreatePersonManualCommand {
            BankAccountNumber = "zczbzcmf",
            BirthCertDescription = "lmfluvhh",
            BirthCertIssuePlace = "ibhmfsol",
            BirthCertIssueProvince = "cmletnof",
            BirthCertNumber = "ieyjliaz",
            BirthCertSeri = "gufgra",
            BirthCertSerial = "zzgsio",
            FatherName = "mivcqeby",
            FidaCode = "ygsagidb",
            FirstName = "Mike",
            LastName = "Green",
            Mobile = "214.367.102",
            NationalCode = "tvfemacz",
            PassportNumber = "ynhkngea",
            Nationality = 5611,
            //BirthDate = "2013, 9, 1",
            //FatherPersonId = 79189, //TODO: Change this to use correct foreign key
            //MotherPersonId = 32731, //TODO: Change this to use correct foreign key
            IsDead = false,
            IsSadat = false,
            //DeathDate = "2013, 9, 1",
            //ResidenceExpireDate = "2010, 2, 1",
            DeathCause = 0,
            Gender = 0,
            Religion = 0,
            SingleStatus = 0,
            Citizenship = new Citizenship(),
        };

        var id = await SendAsync(command);

        var command2 = new CreatePersonManualCommand {
            BankAccountNumber = "zudtyfop",
            BirthCertDescription = "zkvggfpa",
            BirthCertIssuePlace = "fbobilxn",
            BirthCertIssueProvince = "frtprgrj",
            BirthCertNumber = "urvpvceq",
            BirthCertSeri = "wamdpx",
            BirthCertSerial = "fwxiue",
            FatherName = "tkgkxiwv",
            FidaCode = "ygsagidb",
            FirstName = "Rita",
            LastName = "Schumm",
            Mobile = "367.472.128",
            NationalCode = "ulbvxrfd",
            PassportNumber = "kzhqwooq",
            Nationality = 7417,
            //BirthDate = "2013, 9, 1",
            //FatherPersonId = 72734, //TODO: Change this to use correct foreign key
            //MotherPersonId = 78193, //TODO: Change this to use correct foreign key
            IsDead = false,
            IsSadat = false,
            //DeathDate = "2023, 9, 1",
            //ResidenceExpireDate = "2018, 3, 1",
            DeathCause = 0,
            Gender = 0,
            Religion = 0,
            SingleStatus = 0,
            Citizenship = new Citizenship(),
        };

        await FluentActions
            .Invoking(() => SendAsync(command2))
            .Should()
            .ThrowAsync<CommandValidationException>()
            .WithMessage("شناسه فیدا وارد شده تکراری است");
    }

    [Test]
    public async Task HandleCreate_WhenPersonNationalCodeIsDuplicate_ShouldThrowCommandValidationException() {
        var command = new CreatePersonManualCommand {
            BankAccountNumber = "zczbzcmf",
            BirthCertDescription = "lmfluvhh",
            BirthCertIssuePlace = "ibhmfsol",
            BirthCertIssueProvince = "cmletnof",
            BirthCertNumber = "ieyjliaz",
            BirthCertSeri = "gufgra",
            BirthCertSerial = "zzgsio",
            FatherName = "mivcqeby",
            FidaCode = "ygsagidb",
            FirstName = "Mike",
            LastName = "Green",
            Mobile = "214.367.102",
            NationalCode = "tvfemacz",
            PassportNumber = "ynhkngea",
            Nationality = 5611,
            //BirthDate = "2013, 9, 1",
            //FatherPersonId = 79189, //TODO: Change this to use correct foreign key
            //MotherPersonId = 32731, //TODO: Change this to use correct foreign key
            IsDead = false,
            IsSadat = false,
            //DeathDate = "2013, 9, 1",
            //ResidenceExpireDate = "2010, 2, 1",
            DeathCause = 0,
            Gender = 0,
            Religion = 0,
            SingleStatus = 0,
            Citizenship = new Citizenship(),
        };

        var id = await SendAsync(command);

        var command2 = new CreatePersonManualCommand {
            BankAccountNumber = "vsaygdpw",
            BirthCertDescription = "clxcvasr",
            BirthCertIssuePlace = "rpcizggl",
            BirthCertIssueProvince = "oeiofptx",
            BirthCertNumber = "hginmqeo",
            BirthCertSeri = "xavwgf",
            BirthCertSerial = "plsdcx",
            FatherName = "ygdeotga",
            FidaCode = "kjlzihpi",
            FirstName = "Danny",
            LastName = "Simonis",
            Mobile = "954.813.425",
            NationalCode = "tvfemacz",
            PassportNumber = "cmsmxgga",
            Nationality = 8899,
            //BirthDate = "2013, 9, 1",
            //FatherPersonId = 80635, //TODO: Change this to use correct foreign key
            //MotherPersonId = 47505, //TODO: Change this to use correct foreign key
            IsDead = false,
            IsSadat = false,
            //DeathDate = "2023, 6, 1",
            //ResidenceExpireDate = "2020, 7, 1",
            DeathCause = 0,
            Gender = 0,
            Religion = 0,
            SingleStatus = 0,
            Citizenship = new Citizenship(),
        };

        await FluentActions
            .Invoking(() => SendAsync(command2))
            .Should()
            .ThrowAsync<CommandValidationException>()
            .WithMessage("کد ملی وارد شده تکراری است");
    }

    [Test]
    public async Task HandleCreate_WhenPersonYektaCodeIsDuplicate_ShouldThrowCommandValidationException() {
        var command = new CreatePersonManualCommand {
            BankAccountNumber = "zczbzcmf",
            BirthCertDescription = "lmfluvhh",
            BirthCertIssuePlace = "ibhmfsol",
            BirthCertIssueProvince = "cmletnof",
            BirthCertNumber = "ieyjliaz",
            BirthCertSeri = "gufgra",
            BirthCertSerial = "zzgsio",
            FatherName = "mivcqeby",
            FidaCode = "ygsagidb",
            FirstName = "Mike",
            LastName = "Green",
            Mobile = "214.367.102",
            NationalCode = "tvfemacz",
            PassportNumber = "ynhkngea",
            Nationality = 5611,
            //BirthDate = "2013, 9, 1",
            //FatherPersonId = 79189, //TODO: Change this to use correct foreign key
            //MotherPersonId = 32731, //TODO: Change this to use correct foreign key
            IsDead = false,
            IsSadat = false,
            //DeathDate = "2013, 9, 1",
            //ResidenceExpireDate = "2010, 2, 1",
            DeathCause = 0,
            Gender = 0,
            Religion = 0,
            SingleStatus = 0,
            Citizenship = new Citizenship(),
        };

        var id = await SendAsync(command);

        var command2 = new CreatePersonManualCommand {
            BankAccountNumber = "phuvpvcj",
            BirthCertDescription = "kesjpyjg",
            BirthCertIssuePlace = "zpbdufae",
            BirthCertIssueProvince = "godrigor",
            BirthCertNumber = "qhgumifm",
            BirthCertSeri = "qtlebu",
            BirthCertSerial = "wnkgdc",
            FatherName = "fqunhlxe",
            FidaCode = "vauyfcia",
            FirstName = "Monique",
            LastName = "Kertzmann",
            Mobile = "766.798.647",
            NationalCode = "exnosmpz",
            PassportNumber = "nykflsxw",
            Nationality = 8798,
            //BirthDate = "2013, 9, 1",
            //FatherPersonId = 10751, //TODO: Change this to use correct foreign key
            //MotherPersonId = 5723, //TODO: Change this to use correct foreign key
            IsDead = false,
            IsSadat = false,
            //DeathDate = "2019, 6, 1",
            //ResidenceExpireDate = "2016, 5, 1",
            DeathCause = 0,
            Gender = 0,
            Religion = 0,
            SingleStatus = 0,
            Citizenship = new Citizenship(),
        };

        await FluentActions
            .Invoking(() => SendAsync(command2))
            .Should()
            .ThrowAsync<CommandValidationException>()
            .WithMessage("شناسه یکتا وارد شده تکراری است");
    }

    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(int.MaxValue)]
    public async Task HandleCreate_WhenFatherPersonIdIsInvalid_ShouldThrowCommandValidationException(int fatherPersonId) {
        var command = new CreatePersonManualCommand {
            BankAccountNumber = "zczbzcmf",
            BirthCertDescription = "lmfluvhh",
            BirthCertIssuePlace = "ibhmfsol",
            BirthCertIssueProvince = "cmletnof",
            BirthCertNumber = "ieyjliaz",
            BirthCertSeri = "gufgra",
            BirthCertSerial = "zzgsio",
            FatherName = "mivcqeby",
            FidaCode = "ygsagidb",
            FirstName = "Mike",
            LastName = "Green",
            Mobile = "214.367.102",
            NationalCode = "tvfemacz",
            PassportNumber = "ynhkngea",
            Nationality = 5611,
            //BirthDate = "2013, 9, 1",
            //FatherPersonId = fatherPersonId, //TODO: Change this to use correct foreign key
            //MotherPersonId = 32731, //TODO: Change this to use correct foreign key
            IsDead = false,
            IsSadat = false,
            //DeathDate = "2013, 9, 1",
            //ResidenceExpireDate = "2010, 2, 1",
            DeathCause = 0,
            Gender = 0,
            Religion = 0,
            SingleStatus = 0,
            Citizenship = new Citizenship(),
        };

        await FluentActions
            .Invoking(() => SendAsync(command))
            .Should()
            .ThrowAsync<CommandValidationException>()
            .WithMessage("پدر انتخاب شده نامعتبر است");
    }

    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(int.MaxValue)]
    public async Task HandleCreate_WhenMotherPersonIdIsInvalid_ShouldThrowCommandValidationException(int motherPersonId) {
        var command = new CreatePersonManualCommand {
            BankAccountNumber = "zczbzcmf",
            BirthCertDescription = "lmfluvhh",
            BirthCertIssuePlace = "ibhmfsol",
            BirthCertIssueProvince = "cmletnof",
            BirthCertNumber = "ieyjliaz",
            BirthCertSeri = "gufgra",
            BirthCertSerial = "zzgsio",
            FatherName = "mivcqeby",
            FidaCode = "ygsagidb",
            FirstName = "Mike",
            LastName = "Green",
            Mobile = "214.367.102",
            NationalCode = "tvfemacz",
            PassportNumber = "ynhkngea",
            Nationality = 5611,
            //BirthDate = "2013, 9, 1",
            //FatherPersonId = 79189, //TODO: Change this to use correct foreign key
            //MotherPersonId = motherPersonId, //TODO: Change this to use correct foreign key
            IsDead = false,
            IsSadat = false,
            //DeathDate = "2013, 9, 1",
            //ResidenceExpireDate = "2010, 2, 1",
            DeathCause = 0,
            Gender = 0,
            Religion = 0,
            SingleStatus = 0,
            Citizenship = new Citizenship(),
        };

        await FluentActions
            .Invoking(() => SendAsync(command))
            .Should()
            .ThrowAsync<CommandValidationException>()
            .WithMessage("مادر انتخاب شده نامعتبر است");
    }

    [Test]
    public async Task Handle_WhenCreatePersonCommandInputIsInvalid_ShouldHaveValidationError() {
        var command = new CreatePersonManualCommand();
        var validator = new CreatePersonCommandValidator();

        var result = await validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.BirthCertIssuePlace);
        result.ShouldHaveValidationErrorFor(x => x.BirthCertIssueProvince);
        result.ShouldHaveValidationErrorFor(x => x.BirthCertNumber);
        result.ShouldHaveValidationErrorFor(x => x.BirthCertSeri);
        result.ShouldHaveValidationErrorFor(x => x.BirthCertSerial);
        result.ShouldHaveValidationErrorFor(x => x.FatherName);
        result.ShouldHaveValidationErrorFor(x => x.FidaCode);
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
        result.ShouldHaveValidationErrorFor(x => x.LastName);
        result.ShouldHaveValidationErrorFor(x => x.Mobile);
        result.ShouldHaveValidationErrorFor(x => x.NationalCode);
        result.ShouldHaveValidationErrorFor(x => x.PassportNumber);
    }
}
