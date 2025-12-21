//using Csis.Abstractions.Exceptions;
//using Csis.Admission.Application.Features.NonStudentDependants.Dtos;
//using Csis.Admission.Application.Features.NonStudentDependants.Queries;
//using Csis.Admission.Domain.Entities;
//using Csis.Admission.Domain.Enums;
//using Csis.Utilities;

//namespace Csis.Admission.IntegrationTests.Application.NonStudentDependants;

//internal sealed class GetNonStudentDependantByIdQueryTests : BaseTestFixture
//{
//    private NonStudentDependant _nonStudentDependant;

//    [SetUp]
//    public async Task SetUp() {
//        var person = await CreatePersonAsync(fidaCode: "cddaxibh", nationalCode: "fjkykgdp", yektaCode: "tjjdxlff");
//        _nonStudentDependant = await CreateNonStudentDependantAsync(personId: person.Id, nonStudentCodm: 302750, isActive: true);
//    }

//    [Test]
//    public async Task Handle_WhenCalled_ShouldReturnNonStudentDependant() {
//        var nonStudentDependant = await SendAsync(new GetNonStudentDependantByIdQuery(_nonStudentDependant.Id));

//        nonStudentDependant.Should().NotBeNull();
//        nonStudentDependant.Should().BeOfType<NonStudentDependantDto>();
//        nonStudentDependant.Id.Should().Be(_nonStudentDependant.Id);
//        nonStudentDependant.PersonId.Should().Be(_nonStudentDependant.PersonId);
//        nonStudentDependant.NonStudentCodm.Should().Be(_nonStudentDependant.NonStudentCodm);
//        nonStudentDependant.IsActive.Should().Be(_nonStudentDependant.IsActive);
//        nonStudentDependant.Relationship.Should().Be(_nonStudentDependant.Relationship);
//        nonStudentDependant.Branch.Should().Be(_nonStudentDependant.Branch);
//        nonStudentDependant.CaseCreateDate.Should().Be(_nonStudentDependant.CaseCreateDate);
//        nonStudentDependant.CaseDeactiveDate.Should().Be(_nonStudentDependant.CaseDeactiveDate);
//        nonStudentDependant.RelationshipOrder.Should().Be(_nonStudentDependant.RelationshipOrder);
//        nonStudentDependant.StatusReason.Should().Be(_nonStudentDependant.StatusReason);
//    }

//    [TestCase(-1)]
//    [TestCase(0)]
//    [TestCase(int.MaxValue)]
//    public async Task HandleGetById_WhenNonStudentDependantIdIsInvalid_ShouldThrowException(int id) {
//        await FluentActions
//            .Invoking(() => SendAsync(new GetNonStudentDependantByIdQuery(id)))
//            .Should()
//            .ThrowAsync<RecordNotFoundException<NonStudentDependant>>();
//    }
//}
