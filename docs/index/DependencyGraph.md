<div dir="rtl">

# نمودار وابستگی‌ها (Dependency Graph) - سیستم پذیرش

## مقدمه

این سند شامل **نمودارهای وابستگی** در سطوح مختلف معماری سیستم است.

---

## 1. نمودار Solution Level

### 1.1. وابستگی‌های پروژه‌ها

```mermaid
graph TD
    subgraph Presentation
        WebApi[Csis.Admission.WebApi]
        Csis[Csis.Admission]
    end
    
    subgraph Core
        Application[Csis.Admission.Application]
        Domain[Csis.Admission.Domain]
    end
    
    subgraph Infrastructure
        Persistence[Csis.Admission.Persistence]
        Services[Csis.Admission.Services]
    end
    
    subgraph Tests
        UnitTests[Csis.Admission.UnitTests]
        IntegrationTests[Csis.Admission.IntegrationTests]
        WebApiTests[Csis.Admission.WebApiTests]
    end
    
    WebApi --> Application
    WebApi --> Services
    WebApi --> Persistence
    
    Csis --> Application
    Csis --> Services
    Csis --> Persistence
    
    Application --> Domain
    
    Services --> Application
    Services --> Domain
    
    Persistence --> Application
    Persistence --> Domain
    
    UnitTests --> Application
    UnitTests --> Domain
    
    IntegrationTests --> WebApi
    IntegrationTests --> Persistence
    
    WebApiTests --> WebApi
    
    style WebApi fill:#4ecdc4
    style Application fill:#45b7d1
    style Domain fill:#f39c12
    style Persistence fill:#e74c3c
    style Services fill:#9b59b6
```

---

### 1.2. جدول وابستگی‌های پروژه

| پروژه | وابسته به | نوع | توضیح |
|------|----------|-----|-------|
| **WebApi** | Application | Project Ref | استفاده از Features |
| **WebApi** | Services | Project Ref | استفاده از External Services |
| **WebApi** | Persistence | Project Ref | DI Registration |
| **Application** | Domain | Project Ref | استفاده از Entities & Enums |
| **Persistence** | Application | Project Ref | پیاده‌سازی Interfaces |
| **Persistence** | Domain | Project Ref | EF Configurations |
| **Services** | Application | Project Ref | پیاده‌سازی Services |

---

## 2. نمودار Layer Dependencies

```mermaid
graph LR
    subgraph External
        IdentityServer[Identity Server API]
        SeminaryCenter[Seminary Center API]
        CivilRegistry[Civil Registry API]
        FileService[File Management Service]
        EmployeeData[Employee Data Service]
        StudentData[Student Data Service]
    end
    
    subgraph WebApi Layer
        Controllers[Controllers]
        Middleware[Middleware]
    end
    
    subgraph Application Layer
        Features[Features - CQRS]
        Handlers[MediatR Handlers]
        Validators[FluentValidation]
        Mappers[AutoMapper Profiles]
    end
    
    subgraph Domain Layer
        Entities[Entities]
        Enums[Enums]
        ValueObjects[Value Objects]
    end
    
    subgraph Services Layer
        ExternalServices[External Service Integrations]
        BackgroundServices[Background Services]
        CacheServices[Cache Services]
    end
    
    subgraph Persistence Layer
        DbContext[EF Core DbContext]
        Dapper[Dapper Context]
        Repositories[Repositories]
        Configurations[Entity Configurations]
    end
    
    subgraph Database
        SqlServer[(SQL Server)]
        Redis[(Redis Cache)]
    end
    
    Controllers --> Handlers
    Handlers --> Features
    Features --> Repositories
    Features --> ExternalServices
    
    Repositories --> DbContext
    Repositories --> Dapper
    
    DbContext --> SqlServer
    Dapper --> SqlServer
    
    CacheServices --> Redis
    
    ExternalServices --> IdentityServer
    ExternalServices --> SeminaryCenter
    ExternalServices --> CivilRegistry
    ExternalServices --> FileService
    
    Handlers --> Entities
    DbContext --> Entities
    Configurations --> Entities
    
    style Controllers fill:#4ecdc4
    style Features fill:#45b7d1
    style Entities fill:#f39c12
    style DbContext fill:#e74c3c
    style ExternalServices fill:#9b59b6
```

---

## 3. نمودار Use Case Level (نمونه: تشکیل پرونده)

### UC-030: تشکیل پرونده دانشجوی جدید

```mermaid
graph TD
    Start[شروع فرآیند ثبت‌نام] --> Controller[CaseFilingsController]
    
    Controller --> Step01[Step01: Identity]
    Controller --> Step02[Step02: Mobile + OTP]
    Controller --> Step03[Step03: Seminary Validation]
    Controller --> Step04[Step04: Civil Registry]
    Controller --> Step05[Step05: Address]
    Controller --> Step06[Step06: Picture]
    Controller --> Step07[Step07: Bank Account]
    Controller --> Step08[Step08: Employment]
    Controller --> Step09[Step09: Complete Info]
    Controller --> Step10[Step10: Create User]
    
    Step01 --> Handler01[CreateAdmissionCaseStep01Handler]
    Step02 --> Handler02[CreateAdmissionCaseStep02Handler]
    Step03 --> Handler03[CreateAdmissionCaseStep03Handler]
    
    Handler01 --> EF1[EF: AdmissionCaseUser]
    Handler02 --> Dapper1[Dapper: GenerateOtpCode]
    Handler03 --> ExtSeminary[External: Seminary Center API]
    
    Step04 --> ExtCivil[External: Civil Registry API]
    Step06 --> ExtFile[External: File Management Service]
    Step10 --> ExtIdentity[External: Identity Server]
    
    Step09 --> DapperSP[Dapper SP: SetNewStudent]
    
    DapperSP --> DB[(SQL Server)]
    EF1 --> DB
    
    ExtSeminary --> ValidateResult{معتبر؟}
    ValidateResult -->|بله| Continue[ادامه]
    ValidateResult -->|خیر| Reject[رد درخواست]
    
    Continue --> Step10
    Step10 --> Success[✅ پرونده ایجاد شد]
    
    style Start fill:#4ecdc4
    style Success fill:#4CAF50
    style Reject fill:#f44336
```

---

## 4. نمودار Feature Dependencies (نمونه: Students Feature)

```mermaid
graph TD
    StudentsFeature[Students Feature] --> Commands[Commands]
    StudentsFeature --> Queries[Queries]
    
    Commands --> UpdateBirthCert[UpdateStudentBirthCertCommand]
    Commands --> UpdatePicture[UpdateStudentProfilePictureCommand]
    Commands --> ExtendCase[StudentExtensionCaseCommand]
    
    Queries --> GetInfo[GetStudentInfoByCodmQuery]
    Queries --> GetCase[GetStudentCaseByCodmQuery]
    Queries --> GetAddress[GetStudentAddressByCodmQuery]
    
    UpdateBirthCert --> Handler1[Handler]
    UpdatePicture --> Handler2[Handler]
    GetInfo --> Handler3[Handler]
    
    Handler1 --> StudentRepo[StudentRepository]
    Handler1 --> ExtWSM[External: WSM Service - Civil Registry]
    
    Handler2 --> ExtFile[External: File Management Service]
    Handler2 --> DapperPic[Dapper: SetStudentPictureV4]
    
    Handler3 --> DapperInfo[Dapper: GetStudentInfoV4]
    
    StudentRepo --> DapperRepo[Dapper: SetStudentBirthCertInfo]
    
    DapperInfo --> DB[(SQL Server)]
    DapperPic --> DB
    DapperRepo --> DB
    
    style StudentsFeature fill:#4ecdc4
    style ExtWSM fill:#9b59b6
    style ExtFile fill:#9b59b6
```

---

## 5. نمودار Service Dependencies

```mermaid
graph TD
    subgraph WebApi
        Controllers[Controllers]
    end
    
    subgraph Application
        Handlers[MediatR Handlers]
    end
    
    subgraph Services
        CaseFillingService[CaseFillingRequestService]
        NotificationService[NotificationService]
        DateTimeService[DateTimeService]
        CacheService[CacheService]
        PersonInfoService[PersonInfoService]
        EmployeeDataService[EmployeeDataService]
    end
    
    subgraph External
        IdentityAPI[Identity Server API]
        NotificationAPI[Notification API]
        FileAPI[File Management API]
        EmployeeAPI[Employee Data API]
    end
    
    Controllers --> Handlers
    Handlers --> CaseFillingService
    Handlers --> NotificationService
    
    CaseFillingService --> EmployeeDataService
    CaseFillingService --> NotificationService
    CaseFillingService --> CacheService
    
    NotificationService --> NotificationAPI
    EmployeeDataService --> EmployeeAPI
    
    style Controllers fill:#4ecdc4
    style Handlers fill:#45b7d1
    style CaseFillingService fill:#9b59b6
```

---

## 6. نمودار Data Access Layer

```mermaid
graph TD
    subgraph Application Layer
        Commands[Commands]
        Queries[Queries]
    end
    
    subgraph Repositories
        GenericRepo[Repository<T>]
        StudentRepo[StudentRepository]
        PersonRepo[PersonRepository]
        AuditLogRepo[AdmissionAuditLogRepository]
    end
    
    subgraph Data Access
        EFContext[AppDbContext - EF Core]
        DapperContext[AppDapperContext - Dapper]
    end
    
    subgraph Database
        Tables[Tables - 108 Entities]
        SPs[Stored Procedures - 90 SPs]
    end
    
    Commands --> StudentRepo
    Queries --> StudentRepo
    
    StudentRepo --> EFContext
    StudentRepo --> DapperContext
    
    PersonRepo --> EFContext
    AuditLogRepo --> DapperContext
    
    EFContext --> Tables
    DapperContext --> SPs
    
    Tables --> DB[(SQL Server Schema: stu)]
    SPs --> DB
    
    style EFContext fill:#e74c3c
    style DapperContext fill:#e67e22
```

---

## 7. نمودار External Service Integrations

```mermaid
graph LR
    subgraph Application
        Features[Features/Commands/Queries]
    end
    
    subgraph Services
        IdentityService[ICsisIdentityServerService]
        WSMService[ICsisWsmService]
        FileService[ICsisFileManagementService]
        NotificationService[ICsisNotificationService]
        EmployeeService[IEmployeeDataService]
        StudentService[IStudentDataService]
    end
    
    subgraph External APIs
        IdentityServer[Identity Server<br/>JWT, Login, Users]
        CivilRegistry[Civil Registry<br/>ثبت احوال]
        SeminaryCenter[Seminary Center<br/>مرکز حوزوی]
        FileManagement[File Management<br/>آپلود/دانلود فایل]
        NotificationHub[Notification Hub<br/>SMS, Email, Push]
        EmployeeData[Employee Data<br/>اطلاعات کارکنان]
    end
    
    Features --> IdentityService
    Features --> WSMService
    Features --> FileService
    Features --> NotificationService
    
    IdentityService --> IdentityServer
    WSMService --> CivilRegistry
    WSMService --> SeminaryCenter
    FileService --> FileManagement
    NotificationService --> NotificationHub
    EmployeeService --> EmployeeData
    
    style IdentityServer fill:#ff6b6b
    style CivilRegistry fill:#ff6b6b
    style SeminaryCenter fill:#ff6b6b
    style FileManagement fill:#4ecdc4
```

---

## 8. نمودار Background Services

```mermaid
graph TD
    Start[Application Startup] --> CheckConfig{GlobalOptions.<br/>RunBackgroundServices?}
    
    CheckConfig -->|true| StartBG[Start Background Services]
    CheckConfig -->|false| Skip[Skip]
    
    StartBG --> SyncPermissions[SyncPermissionsBackgroundService]
    StartBG --> SendNotification[SendNotificationBackgroundService]
    
    SyncPermissions --> Timer1[هر X دقیقه]
    SendNotification --> Timer2[هر Y دقیقه]
    
    Timer1 --> SyncAPI[Sync with Identity Server]
    Timer2 --> QueueCheck[بررسی صف نوتیفیکیشن‌ها]
    
    QueueCheck --> SendSMS[ارسال SMS]
    QueueCheck --> SendEmail[ارسال Email]
    QueueCheck --> SendPush[ارسال Push Notification]
    
    style StartBG fill:#4ecdc4
```

---

## 9. نمودار Cache Strategy

```mermaid
graph TD
    Request[درخواست داده] --> CheckCache{در Cache هست؟}
    
    CheckCache -->|بله| ReturnCached[برگرداندن از Cache]
    CheckCache -->|خیر| FetchDB[دریافت از Database]
    
    FetchDB --> CheckRedis{Redis فعال است؟}
    
    CheckRedis -->|بله| UseRedis[استفاده از Redis<br/>Distributed Cache]
    CheckRedis -->|خیر| UseMemory[استفاده از Memory Cache]
    
    UseRedis --> SetCache1[ذخیره در Redis]
    UseMemory --> SetCache2[ذخیره در Memory]
    
    SetCache1 --> Return1[برگرداندن داده]
    SetCache2 --> Return2[برگرداندن داده]
    
    ReturnCached --> CheckExpiry{منقضی شده؟}
    CheckExpiry -->|بله| FetchDB
    CheckExpiry -->|خیر| ReturnData[برگرداندن داده]
    
    style UseRedis fill:#e74c3c
    style UseMemory fill:#f39c12
```

---

## 10. نمودار Transaction Boundaries

### مشکل: EF Core و Dapper در Transaction های جدا

```mermaid
sequenceDiagram
    participant H as Handler
    participant EF as EF Core
    participant D as Dapper
    participant DB as SQL Server
    
    Note over H: ❌ رویکرد نادرست
    H->>EF: BeginTransaction()
    H->>EF: Add Student
    H->>EF: SaveChanges()
    H->>D: SetStudentPicture (SP)
    Note over D,DB: در Transaction جدا!
    H->>EF: Commit Transaction
    
    Note over H: ⚠️ اگر SP خطا دهد، Student ذخیره شده است!
    
    Note over H: ✅ رویکرد صحیح
    H->>DB: GetDbConnection()
    H->>DB: BeginTransaction()
    H->>EF: UseTransaction(transaction)
    H->>D: UseConnection(connection)
    H->>EF: Add Student
    H->>D: SetStudentPicture (SP)
    H->>DB: Commit Transaction
    
    Note over H: ✅ هر دو در یک Transaction
```

---

## 11. جدول خلاصه وابستگی‌ها

### وابستگی‌های اصلی Features

| Feature | EF Entities | Dapper SPs | External Services | Internal Services |
|---------|------------|-----------|------------------|------------------|
| **Students** | Student, Person | GetStudentInfoV4, SetStudentBirthCertInfo | WSM, FileManagement | - |
| **CaseFilings** | AdmissionCaseUser | SetNewStudent, ValidateStudentStatus | Seminary, Civil, Identity | CaseFillingRequestService |
| **Auth** | - | GenerateOtpCode | IdentityServer | - |
| **BankAccounts** | - | SetStudentBankAccountNumberV4, CheckDuplicate | - | - |
| **Addresses** | Address | CheckAddressApproveV4 | - | - |
| **Marriages** | Marriage | SetStudentDivorceV4, SetDependentChildMarriage | - | - |
| **ImamJamaat** | Mosque, MosqueInfo | ValidateReligiousRoleV4 | - | - |

---

### وابستگی‌های External Services

| Service | Target API | Usage | Authentication |
|---------|-----------|-------|----------------|
| **ICsisIdentityServerService** | Identity Server | Login, JWT, Users | API Key |
| **ICsisWsmService** | Civil Registry + Seminary | اعتبارسنجی هویت، حوزه | API Key |
| **ICsisFileManagementService** | File Management | Upload/Download | API Key |
| **ICsisNotificationService** | Notification Hub | SMS, Email, Push | API Key |
| **IEmployeeDataService** | Employee Data | اطلاعات کارکنان | API Key |
| **IStudentDataService** | Student Data | اطلاعات دانشجویان | API Key |

---

## 12. نمودار Deployment (احتمالی)

```mermaid
graph TD
    subgraph Client
        Browser[مرورگر کاربر]
        MobileApp[اپلیکیشن موبایل]
    end
    
    subgraph API Gateway
        Gateway[API Gateway / Reverse Proxy]
    end
    
    subgraph Application Servers
        API1[WebApi Instance 1]
        API2[WebApi Instance 2]
        API3[WebApi Instance 3]
    end
    
    subgraph Data Layer
        SQLServer[(SQL Server<br/>Primary)]
        SQLReplica[(SQL Server<br/>Read Replica)]
        Redis[(Redis Cluster)]
    end
    
    subgraph External
        IdentityServer[Identity Server]
        FileService[File Service]
        NotificationService[Notification Service]
    end
    
    Browser --> Gateway
    MobileApp --> Gateway
    
    Gateway --> API1
    Gateway --> API2
    Gateway --> API3
    
    API1 --> SQLServer
    API2 --> SQLServer
    API3 --> SQLServer
    
    API1 --> SQLReplica
    API2 --> SQLReplica
    API3 --> SQLReplica
    
    API1 --> Redis
    API2 --> Redis
    API3 --> Redis
    
    API1 --> IdentityServer
    API1 --> FileService
    API1 --> NotificationService
    
    style Gateway fill:#4ecdc4
    style SQLServer fill:#e74c3c
    style Redis fill:#e67e22
```

---

## نتیجه‌گیری

این نمودارها وابستگی‌های سیستم را در سطوح مختلف نشان می‌دهند:
- ✅ Solution Level: وابستگی پروژه‌ها
- ✅ Layer Level: جریان داده بین لایه‌ها
- ✅ Use Case Level: فلوی یک Use Case خاص
- ✅ Feature Level: وابستگی‌های یک Feature
- ✅ Service Level: یکپارچه‌سازی‌های خارجی
- ✅ Data Access Level: EF Core + Dapper
- ✅ Cache Strategy: چگونه Cache کار می‌کند
- ✅ Transaction Boundaries: چالش‌های Transaction

</div>
