//using Csis.Abstractions.Exceptions;
//using Csis.Admission.Application.Features.People.Commands;
//using Csis.Admission.Application.Features.People.Validators;
//using Csis.Admission.Domain.Entities;
//using Csis.Admission.Domain.Enums;
//using Csis.Utilities;
//using Csis.Utilities.Extensions;
//using FluentValidation.TestHelper;

//namespace Csis.Admission.IntegrationTests.Application.People;

//internal sealed class UpdatePersonCommandTests : BaseTestFixture
//{
//    [Test]
//    public async Task Handle_WhenCalled_ShouldUpdatePerson() {
//        var personId = (await CreatePersonAsync(fidaCode: StringHelper.Random(6), nationalCode: StringHelper.Random(6), yektaCode: StringHelper.Random(6))).Id;

//        personId.Should().BePositive();

//        var command = new UpdatePersonCommand {
//            Id = personId,
//            BankAccountNumber = "osnrvxmk",
//            BirthCertDescription = "wqfopkxx",
//            BirthCertIssuePlace = "ycjsrsfp",
//            BirthCertIssueProvince = "fcbmzulo",
//            BirthCertNumber = "vtkgjsjl",
//            BirthCertSeri = "drxhae",
//            BirthCertSerial = "123",
//            Email = "Lia_Hilpert@yahoo.com",
//            FatherName = "kvquyueb",
//            FidaCode = "ctlireoc",
//            FirstName = "Demond",
//            LastName = "Lemke",
//            LatinLastName = "ebvnpvok",
//            Mobile = "748.894.257",
//            NationalCode = "jyimhdsp",
//            NickName = "iozsbmrj",
//            PassportNumber = "pypzucar",
//            YektaCode = "rradnizv",
//            Nationality = 2101,
//            BirthDate = new DateOnly(2015, 8, 1),
//            FatherPersonId = 28633, //TODO: Change this to use correct foreign key
//            MotherPersonId = 76495, //TODO: Change this to use correct foreign key
//            IsDead = false,
//            IsSadat = false,
//            DeathDate = new DateOnly(2015, 8, 1),
//            ResidenceExpireDate = new DateOnly(2024, 11, 1),
//            DeathCause = 0,
//            Gender = 0,
//            Religion = 0,
//            SingleStatus = 0,
//            Citizenship = new Citizenship(),
//        };

//        await SendAsync(command);

//        var person = await FindAsync<Person>(personId);

//        person.Id.Should().Be(personId);
//        person.BankAccountNumber.Should().Be("osnrvxmk");
//        person.BirthCertDescription.Should().Be("wqfopkxx");
//        person.BirthCertIssuePlace.Should().Be("ycjsrsfp");
//        person.BirthCertIssueProvince.Should().Be("fcbmzulo");
//        person.BirthCertNumber.Should().Be("vtkgjsjl");
//        person.BirthCertSeri.Should().Be("drxhae");
//        person.BirthCertSerial.Should().Be(123);
//        person.Email.Should().Be("Lia_Hilpert@yahoo.com");
//        person.FatherName.Should().Be("kvquyueb");
//        person.FidaCode.Should().Be("ctlireoc");
//        person.FirstName.Should().Be("Demond");
//        person.LastName.Should().Be("Lemke");
//        person.LatinLastName.Should().Be("ebvnpvok");
//        person.Mobile.Should().Be("748.894.257");
//        person.NationalCode.Should().Be("jyimhdsp");
//        person.NickName.Should().Be("iozsbmrj");
//        person.PassportNumber.Should().Be("pypzucar");
//        person.YektaCode.Should().Be("rradnizv");
//        person.Nationality.Should().Be(2101);
//        person.BirthDate.Should().Be((14030506).ToDateOnly());
//        person.FatherPersonId.Should().Be(28633);
//        person.MotherPersonId.Should().Be(76495);
//        person.IsDead.Should().Be(false);
//        person.IsSadat.Should().Be(false);
//        //person.DeathDate.Should().Be(new DateOnly(2015, 8, 1));
//        //person.ResidenceExpireDate.Should().Be(new DateOnly(2024, 11, 1));
//        person.DeathCause.Should().Be(0);
//        person.Gender.Should().Be(0);
//        person.Religion.Should().Be(0);
//        person.SingleStatus.Should().Be(0);
//        person.Citizenship.Should().Be(new Citizenship());
//        person.UpdatedOn.Should().NotBeNull();
//        person.LastUpdatedById.Should().NotBeNull();
//        person.DeletedOn.Should().Be(null);
//        person.DeletedById.Should().Be(null);
//        person.Deleted.Should().BeFalse();
//    }

//    [Test]
//    public async Task HandleUpdate_WhenPersonFidaCodeIsDuplicate_ShouldThrowCommandValidationException() {
//        var command = new CreatePersonCommand {
//            BankAccountNumber = "brptftkt",
//            BirthCertDescription = "wiqdggks",
//            BirthCertIssuePlace = "dymktsxz",
//            BirthCertIssueProvince = "wnyhxgtk",
//            BirthCertNumber = "usvldvkw",
//            BirthCertSeri = "biiyci",
//            BirthCertSerial = "jurymp",
//            Email = "Breanna89@gmail.com",
//            FatherName = "mtfbqhya",
//            FidaCode = "ebwminzk",
//            FirstName = "Eleonore",
//            LastName = "Block",
//            LatinLastName = "yemqarcp",
//            Mobile = "990.824.801",
//            NationalCode = "qsasszsu",
//            NickName = "xnlughdt",
//            PassportNumber = "cqsbuvpi",
//            YektaCode = "mmulyviw",
//            Nationality = 3577,
//            BirthDate = new DateOnly(2015, 8, 1),
//            FatherPersonId = 7758, //TODO: Change this to use correct foreign key
//            MotherPersonId = 25156, //TODO: Change this to use correct foreign key
//            IsDead = false,
//            IsSadat = false,
//            DeathDate = new DateOnly(2018, 8, 1),
//            ResidenceExpireDate = new DateOnly(2011, 5, 1),
//            DeathCause = 0,
//            Gender = 0,
//            Religion = 0,
//            SingleStatus = 0,
//            Citizenship = new Citizenship(),
//        };

//        var id = await SendAsync(command);

//        var command2 = new CreatePersonCommand {
//            BankAccountNumber = "gqylzcdn",
//            BirthCertDescription = "kfrvsdbm",
//            BirthCertIssuePlace = "clmfgqqe",
//            BirthCertIssueProvince = "pqzvsvzu",
//            BirthCertNumber = "srruiwdn",
//            BirthCertSeri = "qzzuiq",
//            BirthCertSerial = "iepelg",
//            Email = "Obie59@hotmail.com",
//            FatherName = "xgmahzfw",
//            FidaCode = "fnmjcklx",
//            FirstName = "Ed",
//            LastName = "Gibson",
//            LatinLastName = "qtsyfhdm",
//            Mobile = "351.260.176",
//            NationalCode = "taroydcc",
//            NickName = "fsiuankw",
//            PassportNumber = "dcgtrivq",
//            YektaCode = "wnewioyf",
//            Nationality = 5230,
//            BirthDate = new DateOnly(2015, 8, 1),
//            FatherPersonId = 67058, //TODO: Change this to use correct foreign key
//            MotherPersonId = 41987, //TODO: Change this to use correct foreign key
//            IsDead = false,
//            IsSadat = false,
//            DeathDate = new DateOnly(2019, 4, 1),
//            ResidenceExpireDate = new DateOnly(2022, 9, 1),
//            DeathCause = 0,
//            Gender = 0,
//            Religion = 0,
//            SingleStatus = 0,
//            Citizenship = new Citizenship(),
//        };

//        var id2 = await SendAsync(command2);

//        var command3 = new UpdatePersonCommand {
//            Id = id2,
//            BankAccountNumber = "vixqsuih",
//            BirthCertDescription = "gdjmguzz",
//            BirthCertIssuePlace = "rzxvclrf",
//            BirthCertIssueProvince = "gjapyaui",
//            BirthCertNumber = "rtgnqnls",
//            BirthCertSeri = "xhpuqt",
//            BirthCertSerial = "umtlxy",
//            Email = "Kamron.Lueilwitz86@yahoo.com",
//            FatherName = "ikttdmau",
//            FidaCode = "ebwminzk",
//            FirstName = "Declan",
//            LastName = "Stoltenberg",
//            LatinLastName = "zpclviix",
//            Mobile = "998.997.632",
//            NationalCode = "huktwkkz",
//            NickName = "zbsdmshk",
//            PassportNumber = "dpqbjprx",
//            YektaCode = "nwmdtpru",
//            Nationality = 305,
//            BirthDate = new DateOnly(2015, 8, 1),
//            FatherPersonId = 43246, //TODO: Change this to use correct foreign key
//            MotherPersonId = 49403, //TODO: Change this to use correct foreign key
//            IsDead = false,
//            IsSadat = false,
//            DeathDate = new DateOnly(2023, 5, 1),
//            ResidenceExpireDate = new DateOnly(2022, 9, 1),
//            DeathCause = 0,
//            Gender = 0,
//            Religion = 0,
//            SingleStatus = 0,
//            Citizenship = new Citizenship(),
//        };

//        await FluentActions
//            .Invoking(() => SendAsync(command3))
//            .Should()
//            .ThrowAsync<CommandValidationException>()
//            .WithMessage("شناسه فیدا وارد شده تکراری است");
//    }

//    [Test]
//    public async Task HandleUpdate_WhenPersonNationalCodeIsDuplicate_ShouldThrowCommandValidationException() {
//        var command = new CreatePersonCommand {
//            BankAccountNumber = "npyoyubx",
//            BirthCertDescription = "dgtxkydp",
//            BirthCertIssuePlace = "aqzpnpgp",
//            BirthCertIssueProvince = "gogqnpqn",
//            BirthCertNumber = "lpibmpqt",
//            BirthCertSeri = "qldvim",
//            BirthCertSerial = "jgjlwm",
//            Email = "Jarvis_Kemmer90@yahoo.com",
//            FatherName = "yejukjog",
//            FidaCode = "ohwmzhns",
//            FirstName = "Aniya",
//            LastName = "Feil",
//            LatinLastName = "ygbregcm",
//            Mobile = "1-706-424-5",
//            NationalCode = "brlylsdn",
//            NickName = "plqtwoxo",
//            PassportNumber = "laxinrrn",
//            YektaCode = "isylpkyc",
//            Nationality = 9184,
//            BirthDate = new DateOnly(2015, 8, 1),
//            FatherPersonId = 37090, //TODO: Change this to use correct foreign key
//            MotherPersonId = 16190, //TODO: Change this to use correct foreign key
//            IsDead = false,
//            IsSadat = false,
//            DeathDate = new DateOnly(2010, 6, 1),
//            ResidenceExpireDate = new DateOnly(2010, 3, 1),
//            DeathCause = 0,
//            Gender = 0,
//            Religion = 0,
//            SingleStatus = 0,
//            Citizenship = new Citizenship(),
//        };

//        var id = await SendAsync(command);

//        var command2 = new CreatePersonCommand {
//            BankAccountNumber = "vlhvzdge",
//            BirthCertDescription = "asgibssz",
//            BirthCertIssuePlace = "cpfdppfn",
//            BirthCertIssueProvince = "djdersyq",
//            BirthCertNumber = "tiailpxe",
//            BirthCertSeri = "cboyfo",
//            BirthCertSerial = "bixdox",
//            Email = "Emmet_Bayer34@hotmail.com",
//            FatherName = "jgtwhefh",
//            FidaCode = "gzfhcqak",
//            FirstName = "Wyatt",
//            LastName = "Rippin",
//            LatinLastName = "tzclfunq",
//            Mobile = "1-595-749-9",
//            NationalCode = "ktoouney",
//            NickName = "cjmcnimd",
//            PassportNumber = "qdyryukt",
//            YektaCode = "zkdbdokx",
//            Nationality = 7339,
//            BirthDate = new DateOnly(2015, 8, 1),
//            FatherPersonId = 64010, //TODO: Change this to use correct foreign key
//            MotherPersonId = 26880, //TODO: Change this to use correct foreign key
//            IsDead = false,
//            IsSadat = false,
//            DeathDate = new DateOnly(2016, 4, 1),
//            ResidenceExpireDate = new DateOnly(2017, 10, 1),
//            DeathCause = 0,
//            Gender = 0,
//            Religion = 0,
//            SingleStatus = 0,
//            Citizenship = new Citizenship(),
//        };

//        var id2 = await SendAsync(command2);

//        var command3 = new UpdatePersonCommand {
//            Id = id2,
//            BankAccountNumber = "lljvphoc",
//            BirthCertDescription = "ocheogxn",
//            BirthCertIssuePlace = "jugexmab",
//            BirthCertIssueProvince = "cyoicxjb",
//            BirthCertNumber = "eszgshmb",
//            BirthCertSeri = "puxkmi",
//            BirthCertSerial = "zkjkba",
//            Email = "Weston97@yahoo.com",
//            FatherName = "zmcsjvni",
//            FidaCode = "bcfpazkt",
//            FirstName = "Lorine",
//            LastName = "Fadel",
//            LatinLastName = "exfeuxli",
//            Mobile = "1-565-349-8",
//            NationalCode = "brlylsdn",
//            NickName = "ogfksqng",
//            PassportNumber = "ygdbaxlm",
//            YektaCode = "hzegryuj",
//            Nationality = 2421,
//            BirthDate = new DateOnly(2015, 8, 1),
//            FatherPersonId = 40129, //TODO: Change this to use correct foreign key
//            MotherPersonId = 87963, //TODO: Change this to use correct foreign key
//            IsDead = false,
//            IsSadat = false,
//            DeathDate = new DateOnly(2014, 10, 1),
//            ResidenceExpireDate = new DateOnly(2014, 4, 1),
//            DeathCause = 0,
//            Gender = 0,
//            Religion = 0,
//            SingleStatus = 0,
//            Citizenship = new Citizenship(),
//        };

//        await FluentActions
//            .Invoking(() => SendAsync(command3))
//            .Should()
//            .ThrowAsync<CommandValidationException>()
//            .WithMessage("کد ملی وارد شده تکراری است");
//    }

//    [Test]
//    public async Task HandleUpdate_WhenPersonYektaCodeIsDuplicate_ShouldThrowCommandValidationException() {
//        var command = new CreatePersonCommand {
//            BankAccountNumber = "ctazaaaz",
//            BirthCertDescription = "vdkmqjxd",
//            BirthCertIssuePlace = "hcinkqzh",
//            BirthCertIssueProvince = "fvzndohd",
//            BirthCertNumber = "yzihdxuy",
//            BirthCertSeri = "nwadqy",
//            BirthCertSerial = "pbqpox",
//            Email = "Alize15@hotmail.com",
//            FatherName = "jasydqcl",
//            FidaCode = "siogzbuj",
//            FirstName = "Hollie",
//            LastName = "Jacobi",
//            LatinLastName = "quldbjui",
//            Mobile = "703-730-254",
//            NationalCode = "awmucius",
//            NickName = "oaizslpf",
//            PassportNumber = "nftehupa",
//            YektaCode = "mqfmxiip",
//            Nationality = 8600,
//            BirthDate = new DateOnly(2015, 8, 1),
//            FatherPersonId = 26250, //TODO: Change this to use correct foreign key
//            MotherPersonId = 92505, //TODO: Change this to use correct foreign key
//            IsDead = false,
//            IsSadat = false,
//            DeathDate = new DateOnly(2024, 11, 1),
//            ResidenceExpireDate = new DateOnly(2022, 3, 1),
//            DeathCause = 0,
//            Gender = 0,
//            Religion = 0,
//            SingleStatus = 0,
//            Citizenship = new Citizenship(),
//        };

//        var id = await SendAsync(command);

//        var command2 = new CreatePersonCommand {
//            BankAccountNumber = "adzryzgd",
//            BirthCertDescription = "yqvaphhs",
//            BirthCertIssuePlace = "mxbwmkba",
//            BirthCertIssueProvince = "zvxszdwz",
//            BirthCertNumber = "togdipdy",
//            BirthCertSeri = "vbahpt",
//            BirthCertSerial = "oflepy",
//            Email = "Antonia71@gmail.com",
//            FatherName = "enqbsdfx",
//            FidaCode = "etghyrjm",
//            FirstName = "Jody",
//            LastName = "Johns",
//            LatinLastName = "lhheqdhn",
//            Mobile = "547.613.138",
//            NationalCode = "bfuznhlm",
//            NickName = "rtkzuthv",
//            PassportNumber = "ziwvidez",
//            YektaCode = "iacxtjxe",
//            Nationality = 9323,
//            BirthDate = new DateOnly(2015, 8, 1),
//            FatherPersonId = 74472, //TODO: Change this to use correct foreign key
//            MotherPersonId = 36837, //TODO: Change this to use correct foreign key
//            IsDead = false,
//            IsSadat = false,
//            DeathDate = new DateOnly(2016, 3, 1),
//            ResidenceExpireDate = new DateOnly(2018, 2, 1),
//            DeathCause = 0,
//            Gender = 0,
//            Religion = 0,
//            SingleStatus = 0,
//            Citizenship = new Citizenship(),
//        };

//        var id2 = await SendAsync(command2);

//        var command3 = new UpdatePersonCommand {
//            Id = id2,
//            BankAccountNumber = "bstydyas",
//            BirthCertDescription = "ngivvghs",
//            BirthCertIssuePlace = "noidadje",
//            BirthCertIssueProvince = "ganvfsjq",
//            BirthCertNumber = "xcubnmkm",
//            BirthCertSeri = "pajhkm",
//            BirthCertSerial = "lccuei",
//            Email = "Moises.Moore9@hotmail.com",
//            FatherName = "tpqwobii",
//            FidaCode = "fndblmqx",
//            FirstName = "Marc",
//            LastName = "Schaden",
//            LatinLastName = "pzesgoys",
//            Mobile = "901-666-781",
//            NationalCode = "urrveimp",
//            NickName = "werwexss",
//            PassportNumber = "sebbvkza",
//            YektaCode = "mqfmxiip",
//            Nationality = 7638,
//            BirthDate = new DateOnly(2015, 8, 1),
//            FatherPersonId = 23727, //TODO: Change this to use correct foreign key
//            MotherPersonId = 60078, //TODO: Change this to use correct foreign key
//            IsDead = false,
//            IsSadat = false,
//            DeathDate = new DateOnly(2018, 7, 1),
//            ResidenceExpireDate = new DateOnly(2010, 10, 1),
//            DeathCause = 0,
//            Gender = 0,
//            Religion = 0,
//            SingleStatus = 0,
//            Citizenship = new Citizenship(),
//        };

//        await FluentActions
//            .Invoking(() => SendAsync(command3))
//            .Should()
//            .ThrowAsync<CommandValidationException>()
//            .WithMessage("شناسه یکتا وارد شده تکراری است");
//    }

//    [TestCase(-1)]
//    [TestCase(0)]
//    [TestCase(int.MaxValue)]
//    public async Task HandleUpdate_WhenFatherPersonIdIsInvalid_ShouldThrowCommandValidationException(int fatherPersonId) {
//        var command = new CreatePersonCommand {
//            BankAccountNumber = "wgxkrwdl",
//            BirthCertDescription = "mvswqnqp",
//            BirthCertIssuePlace = "ceyfyija",
//            BirthCertIssueProvince = "rpzlfynl",
//            BirthCertNumber = "smvsbgro",
//            BirthCertSeri = "shjyoe",
//            BirthCertSerial = "ylqfcq",
//            Email = "Holly_Parisian@yahoo.com",
//            FatherName = "bdwtqukj",
//            FidaCode = "xzqlzwyd",
//            FirstName = "Korbin",
//            LastName = "Marvin",
//            LatinLastName = "xbmriyfb",
//            Mobile = "735.613.016",
//            NationalCode = "xymqjexk",
//            NickName = "aaefugnt",
//            PassportNumber = "vbemalmv",
//            YektaCode = "hnypvlpn",
//            Nationality = 5913,
//            BirthDate = new DateOnly(2015, 8, 1),
//            FatherPersonId = 23718, //TODO: Change this to use correct foreign key
//            MotherPersonId = 84843, //TODO: Change this to use correct foreign key
//            IsDead = false,
//            IsSadat = false,
//            DeathDate = new DateOnly(2020, 9, 1),
//            ResidenceExpireDate = new DateOnly(2010, 4, 1),
//            DeathCause = 0,
//            Gender = 0,
//            Religion = 0,
//            SingleStatus = 0,
//            Citizenship = new Citizenship(),
//        };

//        var id = await SendAsync(command);

//        var command2 = new UpdatePersonCommand {
//            Id = id,
//            BankAccountNumber = "abyupjgt",
//            BirthCertDescription = "ntbiceqg",
//            BirthCertIssuePlace = "ltrcczev",
//            BirthCertIssueProvince = "hjfulzhx",
//            BirthCertNumber = "mixuyjyr",
//            BirthCertSeri = "lcxasn",
//            BirthCertSerial = "wvfxgm",
//            Email = "Kasey_Thompson@hotmail.com",
//            FatherName = "fnxlqhuj",
//            FidaCode = "bpmjolex",
//            FirstName = "Marques",
//            LastName = "Gaylord",
//            LatinLastName = "opnhqoyw",
//            Mobile = "(903) 657-2",
//            NationalCode = "pqhoaowx",
//            NickName = "yjyhnsnj",
//            PassportNumber = "chsqzkoj",
//            YektaCode = "ncgvezer",
//            Nationality = 8453,
//            BirthDate = new DateOnly(2015, 8, 1),
//            FatherPersonId = fatherPersonId,
//            MotherPersonId = 81112, //TODO: Change this to use correct foreign key
//            IsDead = false,
//            IsSadat = false,
//            DeathDate = new DateOnly(2018, 8, 1),
//            ResidenceExpireDate = new DateOnly(2016, 1, 1),
//            DeathCause = 0,
//            Gender = 0,
//            Religion = 0,
//            SingleStatus = 0,
//            Citizenship = new Citizenship(),
//        };

//        await FluentActions
//            .Invoking(() => SendAsync(command2))
//            .Should()
//            .ThrowAsync<CommandValidationException>()
//            .WithMessage("پدر انتخاب شده نامعتبر است");
//    }

//    [TestCase(-1)]
//    [TestCase(0)]
//    [TestCase(int.MaxValue)]
//    public async Task HandleUpdate_WhenMotherPersonIdIsInvalid_ShouldThrowCommandValidationException(int motherPersonId) {
//        var command = new CreatePersonCommand {
//            BankAccountNumber = "nucdckcj",
//            BirthCertDescription = "oporkazr",
//            BirthCertIssuePlace = "syieojtz",
//            BirthCertIssueProvince = "vqeghhwp",
//            BirthCertNumber = "nfjagcig",
//            BirthCertSeri = "fvfebz",
//            BirthCertSerial = "loqaxh",
//            Email = "Robert.Welch@hotmail.com",
//            FatherName = "pxxznqaj",
//            FidaCode = "rzzqlqrw",
//            FirstName = "Evans",
//            LastName = "Carter",
//            LatinLastName = "mlealxtm",
//            Mobile = "796.619.183",
//            NationalCode = "gxqsrhfv",
//            NickName = "itjldtif",
//            PassportNumber = "ljksbcsw",
//            YektaCode = "klhgedtt",
//            Nationality = 8460,
//            BirthDate = new DateOnly(2015, 8, 1),
//            FatherPersonId = 77082, //TODO: Change this to use correct foreign key
//            MotherPersonId = 47077, //TODO: Change this to use correct foreign key
//            IsDead = false,
//            IsSadat = false,
//            DeathDate = new DateOnly(2013, 2, 1),
//            ResidenceExpireDate = new DateOnly(2012, 8, 1),
//            DeathCause = 0,
//            Gender = 0,
//            Religion = 0,
//            SingleStatus = 0,
//            Citizenship = new Citizenship(),
//        };

//        var id = await SendAsync(command);

//        var command2 = new UpdatePersonCommand {
//            Id = id,
//            BankAccountNumber = "lestalgn",
//            BirthCertDescription = "cjufzdcx",
//            BirthCertIssuePlace = "vhhfoeyc",
//            BirthCertIssueProvince = "hlmoguzg",
//            BirthCertNumber = "tmxarymd",
//            BirthCertSeri = "jshoex",
//            BirthCertSerial = "kazbpz",
//            Email = "Kristoffer.Abernathy99@hotmail.com",
//            FatherName = "gmfahbux",
//            FidaCode = "zecazlvb",
//            FirstName = "Micah",
//            LastName = "Medhurst",
//            LatinLastName = "titnebxg",
//            Mobile = "395.395.013",
//            NationalCode = "oefnrlzb",
//            NickName = "fyoijbyz",
//            PassportNumber = "nflohvkz",
//            YektaCode = "fsonislu",
//            Nationality = 9406,
//            BirthDate = new DateOnly(2015, 8, 1),
//            FatherPersonId = 39737, //TODO: Change this to use correct foreign key
//            MotherPersonId = motherPersonId,
//            IsDead = false,
//            IsSadat = false,
//            DeathDate = new DateOnly(2013, 2, 1),
//            ResidenceExpireDate = new DateOnly(2018, 10, 1),
//            DeathCause = 0,
//            Gender = 0,
//            Religion = 0,
//            SingleStatus = 0,
//            Citizenship = new Citizenship(),
//        };

//        await FluentActions
//            .Invoking(() => SendAsync(command2))
//            .Should()
//            .ThrowAsync<CommandValidationException>()
//            .WithMessage("مادر انتخاب شده نامعتبر است");
//    }

//    [TestCase(-1)]
//    [TestCase(0)]
//    [TestCase(int.MaxValue)]
//    public async Task HandleUpdate_WhenPersonIdIsInvalid_ShouldThrowException(int id) {
//        await FluentActions
//            .Invoking(() => SendAsync(new UpdatePersonCommand { Id = id }))
//            .Should()
//            .ThrowAsync<RecordNotFoundException<Person>>();
//    }

//    [Test]
//    public async Task Handle_WhenUpdatePersonCommandInputIsInvalid_ShouldHaveValidationError() {
//        var command = new UpdatePersonCommand();
//        var validator = new UpdatePersonCommandValidator();

//        var result = await validator.TestValidateAsync(command);

//        result.ShouldHaveValidationErrorFor(x => x.BirthCertIssuePlace);
//        result.ShouldHaveValidationErrorFor(x => x.BirthCertIssueProvince);
//        result.ShouldHaveValidationErrorFor(x => x.BirthCertNumber);
//        result.ShouldHaveValidationErrorFor(x => x.BirthCertSeri);
//        result.ShouldHaveValidationErrorFor(x => x.BirthCertSerial);
//        result.ShouldHaveValidationErrorFor(x => x.FatherName);
//        result.ShouldHaveValidationErrorFor(x => x.FidaCode);
//        result.ShouldHaveValidationErrorFor(x => x.FirstName);
//        result.ShouldHaveValidationErrorFor(x => x.LastName);
//        result.ShouldHaveValidationErrorFor(x => x.Mobile);
//        result.ShouldHaveValidationErrorFor(x => x.NationalCode);
//        result.ShouldHaveValidationErrorFor(x => x.PassportNumber);
//    }
//}
