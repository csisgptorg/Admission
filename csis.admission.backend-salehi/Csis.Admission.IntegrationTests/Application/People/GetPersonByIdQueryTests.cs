//using Csis.Abstractions.Exceptions;
//using Csis.Admission.Application.Features.People.Dtos;
//using Csis.Admission.Application.Features.People.Queries;
//using Csis.Admission.Domain.Entities;
//using Csis.Admission.Domain.Enums;
//using Csis.Utilities;

//namespace Csis.Admission.IntegrationTests.Application.People;

//internal sealed class GetPersonByIdQueryTests : BaseTestFixture
//{
//    private Person _person;

//    [SetUp]
//    public async Task SetUp() {
//        _person = await CreatePersonAsync(fidaCode: StringHelper.Random(6), nationalCode: StringHelper.Random(6), yektaCode: StringHelper.Random(6));
//    }

//    [Test]
//    public async Task Handle_WhenCalled_ShouldReturnPerson() {
//        var person = await SendAsync(new GetPersonByIdQuery(_person.Id));

//        person.Should().NotBeNull();
//        person.Should().BeOfType<PersonDto>();
//        person.Id.Should().Be(_person.Id);
//        person.BankAccountNumber.Should().Be(_person.BankAccountNumber);
//        person.BirthCertDescription.Should().Be(_person.BirthCertDescription);
//        person.BirthCertIssuePlace.Should().Be(_person.BirthCertIssuePlace);
//        person.BirthCertIssueProvince.Should().Be(_person.BirthCertIssueProvince);
//        person.BirthCertNumber.Should().Be(_person.BirthCertNumber);
//        person.BirthCertSeri.Should().Be(_person.BirthCertSeri);
//        person.BirthCertSerial.Should().Be(_person.BirthCertSerial.ToString());
//        person.Email.Should().Be(_person.Email);
//        person.FatherName.Should().Be(_person.FatherName);
//        person.FidaCode.Should().Be(_person.FidaCode);
//        person.FirstName.Should().Be(_person.FirstName);
//        person.LastName.Should().Be(_person.LastName);
//        person.LatinLastName.Should().Be(_person.LatinLastName);
//        person.Mobile.Should().Be(_person.Mobile);
//        person.NationalCode.Should().Be(_person.NationalCode);
//        person.NickName.Should().Be(_person.NickName);
//        person.PassportNumber.Should().Be(_person.PassportNumber);
//        person.YektaCode.Should().Be(_person.YektaCode);
//        person.Nationality.Should().Be(_person.Nationality);
//        person.BirthDate.Should().Be(_person.BirthDate);
//        person.FatherPersonId.Should().Be(_person.FatherPersonId);
//        person.MotherPersonId.Should().Be(_person.MotherPersonId);
//        person.IsDead.Should().Be(_person.IsDead);
//        person.IsHouseholder.Should().Be(_person.IsHouseholder);
//        person.IsMarried.Should().Be(_person.IsMarried);
//        person.IsSadat.Should().Be(_person.IsSadat);
//        person.SabteAhvalConfirm.Should().Be(_person.SabteAhvalConfirm);
//        person.DeathDate.Should().Be(_person.DeathDate);
//        person.ResidenceExpireDate.Should().Be(_person.ResidenceExpireDate);
//        person.DeathCause.Should().Be(_person.DeathCause);
//        person.Gender.Should().Be(_person.Gender);
//        person.Religion.Should().Be(_person.Religion);
//        person.SingleStatus.Should().Be(_person.SingleStatus);
//        person.Citizenship.Should().Be(_person.Citizenship);
//    }

//    [TestCase(-1)]
//    [TestCase(0)]
//    [TestCase(int.MaxValue)]
//    public async Task HandleGetById_WhenPersonIdIsInvalid_ShouldThrowException(int id) {
//        await FluentActions
//            .Invoking(() => SendAsync(new GetPersonByIdQuery(id)))
//            .Should()
//            .ThrowAsync<RecordNotFoundException<Person>>();
//    }
//}
