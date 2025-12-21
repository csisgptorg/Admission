using Csis.Admission.Domain.Enums;
using Csis.Shared.Kernel.Public.Enums;
using Csis.Shared.Kernel.Public.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Csis.Admission.IntegrationTests;

/// <summary>
/// Custom web application factory for running integration tests
/// </summary>
internal sealed partial class CustomWebApplicationFactory
{
    /// <summary>
    /// Register custom interceptors for test DbContext
    /// </summary>
    /// <param name="services"></param>
    /// <param name="serviceProvider"></param>
    /// <param name="configuration"></param>
    private static void AddInterceptors(IServiceCollection services, IServiceProvider serviceProvider, IConfiguration configuration) {

    }

    /// <summary>
    /// Register custom global mocked services here
    /// </summary>
    /// <param name="services"></param>
    private static void RegisterCustomMockServices(IServiceCollection services) {
        services.Replace(ServiceDescriptor.Scoped(_ => {
            return _currentUserServiceMock.Object;
        }));

        services.Replace(ServiceDescriptor.Scoped(_ => {
            return _dateTimeServiceMock.Object;
        }));

        services.Replace(ServiceDescriptor.Scoped(_ => {
            return _studentDataServiceMock.Object;
        }));
    }

    private static void SetupCustomMocks() {
        _currentUserServiceMock.Reset();
        _currentUserServiceMock.Setup(x => x.GetUserIdAsync())
           .ReturnsAsync(DefaultCurrentUserId);

        _dateTimeServiceMock.Reset();
        _dateTimeServiceMock.Setup(x => x.Now)
            .Returns(new DateTime(2024, 1, 1, 12, 30, 0));

        #region Student Data Service
        _studentDataServiceMock.Reset();
        #region Students List
        var codm1 = new PersonInfoExtended {
            Codm = "1",
            FirstName = "علی",
            LastName = "علیپور",
            NationalId = "1111111111",
            Gender = (byte) Gender.Male,
            Nationality = (byte) Nationality.Iranian,
            BirthDate = "1340/01/01",
            BranchId = 0,
            IsBlocked = false,
            CaseStatus = 1,
            DeathDate = null,
            IsDead = false,
            EmploymentStatus = EmploymentStatus.NoEmployed,
            MarriatalStatus = 1,
            Religion = 1,
            RelationId = null,
            Dependants = [
                new() {
                    Codm = "1",
                    TakafolId = 10,
                    FirstName = "حامد",
                    LastName = "علیپور",
                    NationalId = "1111111110",
                    Gender = (byte) Gender.Male,
                    Nationality = (byte) Nationality.Iranian,
                    BirthDate = "1370/01/01",
                    BranchId = 0,
                    IsBlocked = false,
                    CaseStatus = 1,
                    DeathDate = null,
                    IsDead = false,
                    EmploymentStatus = EmploymentStatus.NoEmployed,
                    MarriatalStatus = 1,
                    Religion = 1,
                    RelationId = 2,
                },
                new() {
                    Codm = "1",
                    TakafolId = 11,
                    FirstName = "زهرا",
                    LastName = "علیپور",
                    NationalId = "1111111110",
                    Gender = (byte) Gender.Female,
                    Nationality = (byte) Nationality.Iranian,
                    BirthDate = "1375/01/01",
                    BranchId = 0,
                    IsBlocked = false,
                    CaseStatus = 1,
                    DeathDate = null,
                    IsDead = false,
                    EmploymentStatus = EmploymentStatus.NoEmployed,
                    MarriatalStatus = 1,
                    Religion = 1,
                    RelationId = 2,
                }
            ]
        };

        var codm2 = new PersonInfoExtended {
            Codm = "2",
            FirstName = "حسین",
            LastName = "حسینی",
            NationalId = "2222222222",
            Gender = (byte) Gender.Male,
            Nationality = (byte) Nationality.Iranian,
            BirthDate = "1320/01/01",
            BranchId = 0,
            IsBlocked = true,
            CaseStatus = 1,
            DeathDate = "1400/01/01",
            IsDead = true,
            EmploymentStatus = EmploymentStatus.NoEmployed,
            MarriatalStatus = 1,
            Religion = 1,
            RelationId = null
        };

        var codm3 = new PersonInfoExtended {
            Codm = "3",
            FirstName = "محمد",
            LastName = "محمدی",
            NationalId = "3333333333",
            Gender = (byte) Gender.Male,
            Nationality = (byte) Nationality.NonIranian,
            BirthDate = "1380/01/01",
            BranchId = 1,
            IsBlocked = false,
            CaseStatus = 1,
            IsDead = false,
            EmploymentStatus = EmploymentStatus.NoEmployed,
            MarriatalStatus = 1,
            Religion = 2,
            RelationId = null
        };

        var codm4 = new PersonInfoExtended {
            Codm = "4",
            FirstName = "جواد",
            LastName = "جوادی",
            NationalId = "4444444444",
            Gender = (byte) Gender.Male,
            Nationality = (byte) Nationality.Iranian,
            BirthDate = "1360/01/01",
            BranchId = 1,
            IsBlocked = false,
            CaseStatus = 1,
            IsDead = false,
            EmploymentStatus = EmploymentStatus.NoEmployed,
            MarriatalStatus = 1,
            Religion = 2,
            RelationId = null
        };

        var allStudents = new List<PersonInfoExtended> {
            codm1, codm2, codm3, codm4
        };

        var allDependants = allStudents.SelectMany(s => s.Dependants).ToList();
        #endregion

        #region GetCsisBranchesAsync
        _studentDataServiceMock
            .Setup(x => x.GetCsisBranchesAsync())
            .ReturnsAsync([
                new() {
                    Title = "قم",
                    Code = 0,
                    ParentCode = -1,
                    Province = "قم"
                },
                new() {
                    Title = "اروميه",
                    Code = 1,
                    ParentCode = -1,
                    Province = "آذربايجان غربي"
                },
                new() {
                    Title = "تبريز",
                    Code = 2,
                    ParentCode = -1,
                    Province = "آذربايجان شرقي"
                },
                new() {
                    Title = "اصفهان",
                    Code = 3,
                    ParentCode = -1,
                    Province = "اصفهان"
                },
                new() {
                    Title = "ايلام",
                    Code = 4,
                    ParentCode = -1,
                    Province = "ايلام"
                },
                new() {
                    Title = "بوشهر",
                    Code = 5,
                    ParentCode = -1,
                    Province = "بوشهر"
                },
                new() {
                    Title = "شهرکرد",
                    Code = 7,
                    ParentCode = -1,
                    Province = "چهارمحال و بختياري"
                },
                new() {
                    Title = "اهواز",
                    Code = 8,
                    ParentCode = -1,
                    Province = "خوزستان"
                },
                new() {
                    Title = "زنجان",
                    Code = 9,
                    ParentCode = -1,
                    Province = "زنجان"
                },
                new() {
                    Title = "سمنان",
                    Code = 10,
                    ParentCode = -1,
                    Province = "سمنان"
                },
                new() {
                    Title = "زاهدان",
                    Code = 11,
                    ParentCode = -1,
                    Province = "سيستان و بلوچستان"
                },
                new() {
                    Title = "شيراز",
                    Code = 12,
                    ParentCode = -1,
                    Province = "فارس"
                },
                new() {
                    Title = "کرمان",
                    Code = 14,
                    ParentCode = -1,
                    Province = "کرمان"
                },
                new() {
                    Title = "کرمانشاه",
                    Code = 15,
                    ParentCode = -1,
                    Province = "کرمانشاه"
                },
                new() {
                    Title = "ياسوج",
                    Code = 16,
                    ParentCode = -1,
                    Province = "کهگيلويه و بوير احمد"
                },
                new() {
                    Title = "رشت",
                    Code = 17,
                    ParentCode = -1,
                    Province = "گيلان"
                },
                new() {
                    Title = "خرم آباد",
                    Code = 18,
                    ParentCode = -1,
                    Province = "لرستان"
                },
                new() {
                    Title = "ساري",
                    Code = 19,
                    ParentCode = -1,
                    Province = "مازندران"
                },
                new() {
                    Title = "اراک",
                    Code = 20,
                    ParentCode = -1,
                    Province = "مرکزي"
                },
                new() {
                    Title = "بندرعباس",
                    Code = 21,
                    ParentCode = -1,
                    Province = "هرمزگان"
                },
                new() {
                    Title = "همدان",
                    Code = 22,
                    ParentCode = -1,
                    Province = "همدان"
                },
                new() {
                    Title = "يزد",
                    Code = 23,
                    ParentCode = -1,
                    Province = "يزد"
                },
                new() {
                    Title = "کرج",
                    Code = 24,
                    ParentCode = -1,
                    Province = "البرز"
                },
                new() {
                    Title = "کاشان",
                    Code = 26,
                    ParentCode = -1,
                    Province = "اصفهان"
                },
                new() {
                    Title = "گرگان",
                    Code = 28,
                    ParentCode = -1,
                    Province = "گلستان"
                },
                new() {
                    Title = "قزوين",
                    Code = 29,
                    ParentCode = -1,
                    Province = "قزوين"
                },
                new() {
                    Title = "اردبيل",
                    Code = 31,
                    ParentCode = -1,
                    Province = "اردبيل"
                },
                new() {
                    Title = "مشهد",
                    Code = 42,
                    ParentCode = -1,
                    Province = "خراسان رضوي"
                },
                new() {
                    Title = "تهران",
                    Code = 43,
                    ParentCode = -1,
                    Province = "تهران"
                },
                new() {
                    Title = "سنندج",
                    Code = 44,
                    ParentCode = -1,
                    Province = "کردستان"
                },
                new() {
                    Title = "ستاد",
                    Code = 99,
                    ParentCode = -1,
                    Province = "قم"
                },
                new() {
                    Title = "بيرجند",
                    Code = 41,
                    ParentCode = -1,
                    Province = "خراسان جنوبي"
                },
                new() {
                    Title = "بجنورد",
                    Code = 40,
                    ParentCode = -1,
                    Province = "خراسان شمالي"
                }
            ]);
        #endregion

        #region GetPersonInfoAsync
        foreach ( var student in allStudents ) {
            _studentDataServiceMock
                .Setup(x => x.GetStudentInfoAsync(It.Is<string>(x => x == student.Codm)))
                .ReturnsAsync(student);
        }
        #endregion

        #region GetStudentWithDependantsAsync
        foreach ( var student in allStudents ) {
            _studentDataServiceMock
                .Setup(x => x.GetStudentWithDependantsAsync(It.Is<string>(x => x == student.Codm), It.IsAny<bool>()))
                .ReturnsAsync(student);
        }
        #endregion

        #region SearchStudentAsync
        foreach ( var student in allStudents ) {
            _studentDataServiceMock
                .Setup(x => x.SearchStudentAsync(It.Is<string>(x => x == student.Codm)))
                .ReturnsAsync(student);
        }
        #endregion

        #region GetStudentGroupInfoAsync
        _studentDataServiceMock
            .Setup(x => x.GetStudentGroupInfoAsync(It.IsAny<IEnumerable<string>>(), It.IsAny<int>()))
            .ReturnsAsync(allStudents);
        #endregion

        #region GetDependantsGroupInfoAsync
        _studentDataServiceMock
            .Setup(x => x.GetDependantsGroupInfoAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<int>()))
            .ReturnsAsync(allDependants);
        #endregion

        #region GetStudentsAndDependantsGroupInfoAsync
        _studentDataServiceMock
            .Setup(x => x.GetStudentsAndDependantsGroupInfoAsync(It.IsAny<IEnumerable<string>>(), It.IsAny<IEnumerable<int>>(), It.IsAny<int>()))
            .ReturnsAsync([.. allStudents, .. allDependants]);
        #endregion

        #region GetDependantsAsync
        foreach ( var student in allStudents ) {
            _studentDataServiceMock
                .Setup(x => x.GetDependantsAsync(It.Is<string>(x => x == student.Codm), It.IsAny<bool>()))
                .ReturnsAsync(student.Dependants);
        }
        #endregion
        #endregion
    }
}
