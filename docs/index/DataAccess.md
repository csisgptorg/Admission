<div dir="rtl">

# مستندات Data Access - EF Core + Dapper

## مقدمه

این سند شامل تحلیل موشکافانه **لایه Data Access** سیستم پذیرش است که از دو رویکرد استفاده می‌کند:
1. **Entity Framework Core** - برای عملیات CRUD و Tracking
2. **Dapper** - برای Stored Procedures و Queries پیچیده

---

## خلاصه آماری

| متریک | مقدار |
|------|------|
| تعداد DbContext | 1 (`AppDbContext`) |
| تعداد Entities | ~108 |
| تعداد Configurations | 92 |
| تعداد Migrations | 3 (Init migration) |
| تعداد Stored Procedures | 90 |
| تعداد Custom Repositories | 13 |
| Default Schema | `stu` |
| Connection Pooling | ✅ (Max: 1024) |
| Decimal Precision | (18, 2) |
| Bulk Operations | ✅ (EFCore.BulkExtensions) |

---

## بخش 1: Entity Framework Core

### 1.1. AppDbContext

#### تعریف

```csharp
public sealed class AppDbContext : DbContext
{
    // Constructor with DI
    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        ICurrentUserService currentUserService,
        IDateTimeService dateTimeService
    ) : base(options)
    
    // Bulk Save Methods
    public void BulkSave()
    public async Task BulkSaveAsync(CancellationToken ct)
    
    // Model Configuration
    protected override void OnModelCreating(ModelBuilder builder)
    protected override void ConfigureConventions(ModelConfigurationBuilder config)
}
```

#### مسیر فایل
`/Csis.Admission.Persistence/AppDbContext.cs`

---

#### 1.1.1. Model Configuration

**OnModelCreating**:
```csharp
protected override void OnModelCreating(ModelBuilder builder)
{
    base.OnModelCreating(builder);
    
    // ✅ ثبت خودکار DbSet ها
    builder.RegisterDbSets();
    
    // ✅ تعیین Schema پیش‌فرض
    builder.HasDefaultSchema(Constants.Db.DefaultSchema); // "stu"
    
    // ✅ اعمال خودکار Configurations
    builder.ApplyConfigurationsFromAssembly(
        typeof(BaseEntityConfiguration<>).Assembly
    );
    
    // ✅ افزودن XML Comments (فقط در Development)
    if (GlobalOptions.IsDevelopment) {
        builder.AddXmlComments();
    }
}
```

**ConfigureConventions**:
```csharp
protected override void ConfigureConventions(
    ModelConfigurationBuilder configurationBuilder
)
{
    base.ConfigureConventions(configurationBuilder);
    
    // ✅ همه Decimal ها → Precision (18, 2)
    configurationBuilder.Properties<decimal>()
        .HavePrecision(18, 2);
}
```

---

#### 1.1.2. Bulk Operations

استفاده از **EFCore.BulkExtensions** برای عملیات دسته‌ای با کارایی بالا:

```csharp
public void BulkSave()
{
    ChangeTracker.DetectChanges();
    
    // ✅ تنظیم خودکار CreatedBy, UpdatedBy, Timestamps
    ChangeTracker.SetBaseEntityProperties(
        currentUserService.GetUserIdAsync().Result,
        currentUserService.GetDelegatedUserIdAsync().Result,
        dateTimeService.Now
    );
    
    // ✅ Bulk Save (بدون تریگرها، سریع‌تر از SaveChanges)
    this.BulkSaveChanges();
}
```

**مزایا**:
- ⚡ **Performance**: تا 100 برابر سریع‌تر از SaveChanges معمولی
- ✅ **Bulk Insert/Update/Delete**: یک Query به جای N Query
- ⚠️ **محدودیت**: Interceptors و Triggers اجرا نمی‌شوند

**استفاده**:
- عملیات Import/Export
- Seed Data
- Migration Data

---

### 1.2. Entity Configurations

تمامی Entity ها با استفاده از **IEntityTypeConfiguration** پیکربندی شده‌اند.

#### 1.2.1. ساختار Configurations

```
Configurations/
├── BaseEntityConfiguration.cs           # پایه همه Config ها
├── SoftDeletedBaseEntityConfiguration.cs
├── AuditableEntityConfiguration.cs
├── AuditableSoftDeletedEntityConfiguration.cs
└── Domain/
    ├── PersonConfiguration.cs
    ├── AddressConfiguration.cs
    ├── StudentSummaryConfiguration.cs
    └── ... (92 configurations)
```

---

#### 1.2.2. BaseEntityConfiguration

**نقش**: پایه برای همه Entity Configuration ها

**ویژگی‌ها**:
```csharp
internal class BaseEntityConfiguration<T, TKey> : IEntityTypeConfiguration<T>
    where T : class, IEntity<TKey>
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        // ✅ Primary Key
        builder.HasKey(e => e.Id);
        
        // ✅ نام جدول → Pluralize (Person → People)
        builder.ToTable(builder.Metadata.GetTableName().Pluralize());
        
        // ✅ فیلدهای استاندارد
        builder.Property(e => e.Description)
            .IsRequired(false)
            .HasMaxLength(2000);
            
        builder.Property(e => e.CreatedOn).IsRequired();
        builder.Property(e => e.UpdatedOn).IsRequired(false);
        builder.Property(e => e.CreatedById).IsRequired(false);
        builder.Property(e => e.LastUpdatedById).IsRequired(false);
    }
}
```

**استفاده**: تمام Entity ها از این پایه ارث‌بری می‌کنند.

---

#### 1.2.3. SoftDeletedBaseEntityConfiguration

**نقش**: برای Entity هایی که نیاز به Soft Delete دارند

```csharp
internal class SoftDeletedBaseEntityConfiguration<T> : BaseEntityConfiguration<T>
    where T : class, ISoftDeletedEntity
{
    public override void Configure(EntityTypeBuilder<T> builder)
    {
        base.Configure(builder);
        
        // ✅ Soft Delete Flag
        builder.Property(e => e.Deleted)
            .IsRequired()
            .HasDefaultValue(false);
        
        // ✅ Query Filter → پیش‌فرض حذف شده‌ها را نشان نده
        builder.HasQueryFilter(e => !e.Deleted);
    }
}
```

**تاثیر Query Filter**:
```csharp
// این query خودکار فقط رکوردهای Deleted=false را می‌گیرد
var students = await dbContext.Students.ToListAsync();

// برای شامل شدن Deleted ها:
var allStudents = await dbContext.Students
    .IgnoreQueryFilters()
    .ToListAsync();
```

---

#### 1.2.4. نمونه Configuration: PersonConfiguration

**مسیر**: `/Configurations/Domain/PersonConfiguration.cs`

```csharp
internal sealed class PersonConfiguration 
    : SoftDeletedBaseEntityConfiguration<Person>
{
    public override void Configure(EntityTypeBuilder<Person> builder)
    {
        base.Configure(builder);
        
        // ✅ Primary Key با نام سفارشی
        builder.HasKey(e => e.Id).HasName("PK_Person");
        
        // ✅ Properties با Comment های فارسی
        builder.Property(e => e.Id).HasComment("شناسه فرد");
        
        builder.Property(e => e.BankAccountNumber)
            .HasMaxLength(13)
            .IsUnicode(false)
            .HasComment("شماره حساب");
        
        builder.Property(e => e.ShebaNumber)
            .HasMaxLength(26)
            .IsUnicode(false)
            .HasComment("شماره شبا");
        
        builder.Property(e => e.NationalCode)
            .HasMaxLength(10)
            .IsUnicode(false)
            .IsFixedLength()       // CHAR(10) در SQL
            .HasComment("کد ملی");
        
        builder.Property(e => e.Mobile)
            .HasMaxLength(11)
            .IsUnicode(false)
            .HasComment("تلفن همراه");
        
        // ✅ Self-Referencing Relationships
        builder.HasOne(x => x.FatherPerson)
            .WithMany()
            .HasForeignKey(x => x.FatherPersonId)
            .IsRequired(false);
        
        builder.HasOne(x => x.MotherPerson)
            .WithMany()
            .HasForeignKey(x => x.MotherPersonId)
            .IsRequired(false);
    }
}
```

**نکات مهم**:
- 📝 **Comments**: تمام ستون‌ها دارای توضیحات فارسی
- 🔗 **Self-Referencing**: پدر و مادر از همین جدول Person
- 🔤 **Unicode/Non-Unicode**: کدملی، موبایل، شماره حساب → varchar
- 📏 **MaxLength**: محدودیت طول رشته‌ها

---

#### 1.2.5. نمونه Configuration: AddressConfiguration

```csharp
internal sealed class AddressConfiguration 
    : AuditableSoftDeletedEntityConfiguration<Address>
{
    public override void Configure(EntityTypeBuilder<Address> builder)
    {
        base.Configure(builder);
        
        // ✅ نام جدول سفارشی (Legacy)
        builder.ToTable("TbAddress");
        
        // ✅ Default Value برای Soft Delete
        builder.Property(x => x.Deleted).HasDefaultValue(false);
        
        // ✅ نام ستون‌های سفارشی (Legacy compatibility)
        builder.Property(x => x.ProvinceId).HasColumnName("Province");
        builder.Property(x => x.CityId).HasColumnName("City");
        builder.Property(x => x.PortionId).HasColumnName("Portion");
        builder.Property(x => x.TownId).HasColumnName("Town");
        builder.Property(x => x.RuralId).HasColumnName("Rural");
        builder.Property(x => x.Township).HasColumnName("Dorp");
    }
}
```

**نکته**: این configuration با دیتابیس قدیمی سازگاری دارد (`TbAddress`, `Dorp`)

---

### 1.3. Relationships & Foreign Keys

#### انواع روابط در سیستم:

##### 1.3.1. One-to-Many
```csharp
// Person → Addresses (یک فرد، چند آدرس)
builder.HasMany(x => x.Addresses)
    .WithOne(x => x.Person)
    .HasForeignKey(x => x.PersonId)
    .OnDelete(DeleteBehavior.Cascade);
```

##### 1.3.2. One-to-One
```csharp
// Student → StudentCase
builder.HasOne(x => x.StudentCase)
    .WithOne(x => x.Student)
    .HasForeignKey<StudentCase>(x => x.StudentId)
    .OnDelete(DeleteBehavior.Restrict);
```

##### 1.3.3. Many-to-Many (جدول میانی)
```csharp
// Student <-> CulturalActivity
builder.HasMany(x => x.CulturalActivities)
    .WithMany(x => x.Students)
    .UsingEntity<StudentCulturalActivity>(
        j => j.HasOne(x => x.CulturalActivity)
              .WithMany()
              .HasForeignKey(x => x.CulturalActivityId),
        j => j.HasOne(x => x.Student)
              .WithMany()
              .HasForeignKey(x => x.StudentId)
    );
```

---

### 1.4. Delete Behaviors

| DeleteBehavior | رفتار | استفاده |
|---------------|-------|---------|
| **Cascade** | حذف کاسکیدی - فرزندان هم حذف می‌شوند | آدرس‌ها، تلفن‌ها |
| **Restrict** | جلوگیری از حذف اگر فرزند دارد | پرونده اصلی |
| **SetNull** | FK فرزندان null می‌شود | Optional relationships |
| **NoAction** | هیچ کاری انجام نده (خطا در SQL) | - |

**استراتژی در پروژه**:
- Entity های اصلی: `Restrict` یا Soft Delete
- Entity های فرعی: `Cascade`

---

### 1.5. Indexes

#### Index Strategy:

```csharp
// Unique Index
builder.HasIndex(e => e.NationalCode)
    .IsUnique()
    .HasFilter("[Deleted] = 0");  // فقط غیرحذف‌شده‌ها

// Composite Index
builder.HasIndex(e => new { e.StudentId, e.DependentId })
    .IsUnique();

// Filtered Index
builder.HasIndex(e => e.Email)
    .HasFilter("[Email] IS NOT NULL");
```

**نکته**: Indexها در Migration تعریف شده‌اند.

---

### 1.6. Value Converters

برای تبدیل انواع داده:

```csharp
// Enum to String
builder.Property(e => e.Gender)
    .HasConversion<string>();

// Custom Converter
builder.Property(e => e.Permissions)
    .HasConversion(
        v => JsonSerializer.Serialize(v, null),
        v => JsonSerializer.Deserialize<List<Permission>>(v, null)
    );
```

---

### 1.7. Migrations

#### لیست Migrations:

| Migration | تاریخ | توضیحات |
|-----------|------|---------|
| `20251203072127_Init` | 2024-12-03 | Initial Migration - ایجاد تمام جداول |

**فایل‌ها**:
- `20251203072127_Init.cs` - کد Migration
- `20251203072127_Init.Designer.cs` - Metadata
- `AppDbContextModelSnapshot.cs` - وضعیت فعلی Model

#### ساختار Migration:

```csharp
public partial class Init : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // ایجاد جداول
        // ایجاد FK ها
        // ایجاد Index ها
    }
    
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        // Rollback
    }
}
```

**نکات مهم**:
- ⚠️ تعداد Migration کم است (فقط 1) → احتمالاً دیتابیس از قبل وجود داشته
- ⚠️ هیچ Data Migration نیست
- ✅ Schema اولیه کامل است

---

### 1.8. Connection & Pooling

#### تنظیمات Connection:

```json
{
  "DatabaseOptions": {
    "ConnectionStrings": {
      "SqlServer": "Data Source=.\\MSSQLSERVER2017;Initial Catalog=[DBNAME];User ID=[USERNAME];Password=[PASSWORD];"
    },
    "UseInMemoryDatabase": false,
    "EnableLogging": false,
    "EnableSensitiveDataLogging": false,
    "EnablePooling": true,
    "MaxPoolSize": 1024,
    "RunSeeders": false
  }
}
```

#### Connection Pooling:

```csharp
services.AddDbContextPool<AppDbContext>(
    (serviceProvider, options) => {
        options.UseSqlServer(
            connectionString,
            sqlOptions => {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null
                );
                sqlOptions.CommandTimeout(180); // 3 minutes
            }
        );
    },
    poolSize: databaseOptions.MaxPoolSize // 1024
);
```

**مزایا**:
- ⚡ **Performance**: کاهش Overhead ایجاد Connection
- 📈 **Scalability**: تا 1024 connection همزمان
- 🔄 **Reuse**: استفاده مجدد از Connection ها

**ریسک‌ها**:
- ⚠️ **MaxPoolSize بالا (1024)**: ممکن است منابع SQL Server تمام شود
- ⚠️ **Interceptors باید Transient باشند** (نه Scoped)

---

### 1.9. Interceptors

#### AuditLogSaveChangesInterceptor

**نقش**: ثبت خودکار Audit Log برای تغییرات

```csharp
public class AuditLogSaveChangesInterceptor : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken ct
    )
    {
        var dbContext = eventData.Context;
        
        // ✅ پیدا کردن تغییرات
        var entries = dbContext.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified || 
                        e.State == EntityState.Added ||
                        e.State == EntityState.Deleted);
        
        foreach (var entry in entries)
        {
            // ✅ ایجاد AdmissionAuditLog
            var auditLog = new AdmissionAuditLog
            {
                EntityName = entry.Entity.GetType().Name,
                Action = entry.State.ToString(),
                Changes = JsonSerializer.Serialize(entry.CurrentValues),
                UserId = currentUserService.GetUserId(),
                Timestamp = DateTime.Now
            };
            
            dbContext.Set<AdmissionAuditLog>().Add(auditLog);
        }
        
        return await base.SavingChangesAsync(eventData, result, ct);
    }
}
```

**ثبت در DI**:
```csharp
// اگر Pooling فعال است → Transient
services.AddTransient<AuditLogSaveChangesInterceptor>();

// اگر Pooling غیرفعال است → Scoped
services.AddScoped<AuditLogSaveChangesInterceptor>();
```

---

### 1.10. Performance Considerations

#### 1.10.1. Tracking vs NoTracking

```csharp
// ✅ Query Only → NoTracking (سریع‌تر)
var students = await dbContext.Students
    .AsNoTracking()
    .ToListAsync();

// ✅ Update/Delete → Tracking
var student = await dbContext.Students
    .FirstOrDefaultAsync(x => x.Id == id);
student.Name = "New Name";
await dbContext.SaveChangesAsync();
```

**قانون پروژه**:
- Repository: پیش‌فرض **NoTracking** برای Query ها
- متدهای `AsTracking`: فقط برای Update/Delete

---

#### 1.10.2. Eager vs Lazy Loading

```csharp
// ❌ Lazy Loading → N+1 Problem
var students = await dbContext.Students.ToListAsync();
foreach (var s in students) {
    var address = s.Address; // N queries!
}

// ✅ Eager Loading → 1 Query with JOIN
var students = await dbContext.Students
    .Include(s => s.Address)
    .ToListAsync();
```

**استراتژی پروژه**: Lazy Loading غیرفعال است → **باید Explicit Include** استفاده شود.

---

#### 1.10.3. Select Projections

```csharp
// ❌ بد: همه ستون‌ها
var students = await dbContext.Students.ToListAsync();

// ✅ خوب: فقط ستون‌های مورد نیاز
var students = await dbContext.Students
    .Select(s => new { s.Id, s.Name, s.NationalCode })
    .ToListAsync();
```

---

#### 1.10.4. Split Queries

```csharp
// برای Join های بزرگ
var students = await dbContext.Students
    .Include(s => s.Addresses)
    .Include(s => s.Dependents)
    .Include(s => s.Employments)
    .AsSplitQuery()  // ✅ چند Query به جای یک JOIN بزرگ
    .ToListAsync();
```

---

#### 1.10.5. Compiled Queries

برای Query های تکراری:

```csharp
private static readonly Func<AppDbContext, int, Task<Student>> 
    _compiledQuery = EF.CompileAsyncQuery(
        (AppDbContext db, int id) => 
            db.Students.FirstOrDefault(s => s.Id == id)
    );

// استفاده
var student = await _compiledQuery(dbContext, studentId);
```

---

## بخش 2: Dapper

### 2.1. AppDapperContext

**نقش**: اجرای Stored Procedures با Dapper

**مسیر**: `/Persistence/AppDapperContext.cs`

```csharp
public class AppDapperContext
{
    private readonly DatabaseOptions _databaseOptions;
    private readonly ILogger<AppDapperContext> _logger;
    private readonly int _commandTimeout = 180; // 3 minutes
    
    public AppDapperContext(
        ILogger<AppDapperContext> logger,
        IOptions<DatabaseOptions> databaseOptions
    ) {
        _logger = logger;
        _databaseOptions = databaseOptions.Value;
    }
    
    private IDbConnection CreateConnection() 
        => new SqlConnection(_databaseOptions.ConnectionStrings.SqlServer);
}
```

---

### 2.2. Stored Procedure Methods

#### 2.2.1. ExecuteQuery (SQL مستقیم)

```csharp
public async Task<dynamic[]> ExecuteQuery(string query)
{
    using var connection = CreateConnection();
    var entities = await connection.QueryAsync(
        query, 
        commandTimeout: _commandTimeout
    );
    return entities.ToArray();
}
```

**استفاده**:
```csharp
var result = await dapper.ExecuteQuery(
    "SELECT * FROM stu.Students WHERE Codm = 12345678"
);
```

⚠️ **خطر SQL Injection** - باید Parameterize شود!

---

#### 2.2.2. ExecuteProcedureToList

```csharp
public async Task<List<TEntity>> ExecuteProcedureToList<TEntity>(
    ProcedureName procedure, 
    object param = null
)
{
    var procedureName = "stu." + procedure.ToString();
    using var connection = CreateConnection();
    
    // ✅ Log
    var queryLog = $"EXEC {procedureName} {SqlParametersToString(param)}";
    _logger.LogInformation(procedureName);
    
    // ✅ Execute SP
    var entities = await connection.QueryAsync<TEntity>(
        procedureName, 
        param, 
        commandType: CommandType.StoredProcedure,
        commandTimeout: _commandTimeout
    );
    
    return entities.ToList();
}
```

**استفاده**:
```csharp
var students = await dapper.ExecuteProcedureToList<StudentDto>(
    ProcedureName.GetStudentInfoV4,
    new { Codm = 12345678 }
);
```

---

#### 2.2.3. ExecuteProcedureSingleOrDefault

```csharp
public async Task<TEntity> ExecuteProcedureSingleOrDefault<TEntity>(
    ProcedureName procedure,
    object param = null
)
{
    var procedureName = "stu." + procedure.ToString();
    using var connection = CreateConnection();
    
    var entity = await connection.QuerySingleOrDefaultAsync<TEntity>(
        procedureName,
        param,
        commandType: CommandType.StoredProcedure,
        commandTimeout: _commandTimeout
    );
    
    // ✅ بررسی خطا در خروجی SP
    if (entity is ProcedureResultDto result) {
        result.ThrowIfUnsuccessful();
    }
    
    return entity;
}
```

**استفاده**:
```csharp
var commission = await dapper.ExecuteProcedureSingleOrDefault<CommissionDto>(
    ProcedureName.GetStudentCommission,
    new { Codm = 12345678 }
);
```

---

#### 2.2.4. ExecuteProcedure (بدون خروجی)

```csharp
public async Task ExecuteProcedure(
    ProcedureName procedure,
    object param = null
)
{
    var procedureName = "stu." + procedure.ToString();
    using var connection = CreateConnection();
    
    await connection.QueryAsync(
        procedureName,
        param,
        commandType: CommandType.StoredProcedure,
        commandTimeout: _commandTimeout
    );
}
```

**استفاده**: برای SP هایی که فقط Insert/Update/Delete انجام می‌دهند.

---

### 2.3. Stored Procedures Enum

**مسیر**: `/Application/Enums/ProcedureName.cs`

**تعداد**: ~90 SP

**دسته‌بندی**:

#### 2.3.1. Query SPs (Select)

| SP Name | توضیحات | خروجی |
|---------|---------|-------|
| `GetStudentInfoV4` | اطلاعات جامع دانشجو | StudentDto |
| `GetStudentCaseInfoV4` | اطلاعات پرونده | CaseDto |
| `GetStudentPictureV4` | تصویر دانشجو | byte[] |
| `GetAddressV4` | آدرس | AddressDto |
| `GetPhoneV4` | تلفن | PhoneDto |
| `GetDependentInfoV4` | اطلاعات تکفل | DependentDto |
| `GetStudentCommission` | کمیسیون دانشجو | CommissionDto |
| `GetDependentCommission` | کمیسیون تکفل | CommissionDto |
| `GetStudentAuditLog` | سوابق ممیزی دانشجو | List<AuditLogDto> |
| `GetTargetedScoreInfoV4` | امتیاز هدفمندی | ScoreDto |
| `GetStudentBlockedService` | خدمات مسدود | List<BlockedServiceDto> |
| `GetFamilyMobileV4` | موبایل خانواده | List<MobileDto> |
| `GetFamilyBankAccountNumberV4` | حساب خانواده | List<BankAccountDto> |
| `GetShahriehData` | شهریه | ShahriehDto |
| `GetDataForPayRunByCodm` | داده پرداخت | PayRunDto |

---

#### 2.3.2. Command SPs (Insert/Update/Delete)

| SP Name | توضیحات | پارامترها |
|---------|---------|-----------|
| `SetNewStudent` | ثبت دانشجوی جدید | StudentInfo |
| `SetNewDependent` | ثبت تکفل | DependentInfo |
| `SetStudentBirthCertInfo` | بروز شناسنامه دانشجو | BirthCertInfo |
| `SetDependentBirthCertInfo` | بروز شناسنامه تکفل | BirthCertInfo |
| `SetStudentPictureV4` | ثبت تصویر | Image, Codm |
| `SetStudentMobileV4` | ثبت موبایل دانشجو | Mobile, Codm |
| `SetDependentMobileV4` | ثبت موبایل تکفل | Mobile, DependentId |
| `SetStudentBankAccountNumberV4` | ثبت حساب دانشجو | BankAccount, Codm |
| `SetDependentBankAccountNumberV4` | ثبت حساب تکفل | BankAccount |
| `SetStudentCaseValidityDate` | تمدید پرونده | Codm, Date |
| `SetStudentCaseValidityDateAuto` | تمدید خودکار | - |
| `SetStudentBlocked` | مسدودی دانشجو | Codm, Reason |
| `SetStudentUnBlocked` | رفع مسدودی | Codm |
| `DeActiveDependentV4` | غیرفعال تکفل | DependentId |
| `SetDependentActive` | فعال تکفل | DependentId |

---

#### 2.3.3. Family Event SPs

| SP Name | توضیحات |
|---------|---------|
| `SetStudentSisterMarriage` | ازدواج طلبه خواهر |
| `SetStudentDivorceV4` | طلاق سرپرست |
| `SetDependentChildMarriage` | ازدواج فرزند |
| `SetDependentSpouseMarriage` | ازدواج همسر بیوه |
| `SetDependentSpouseDivorce` | طلاق همسر |
| `SetDependentChildDivorce` | طلاق فرزند |

---

#### 2.3.4. Integration/Validation SPs

| SP Name | توضیحات | External API |
|---------|---------|--------------|
| `ValidateStudentStatusForRegisterationV4` | اعتبارسنجی حوزه | Seminary Center |
| `CheckAddressApproveV4` | بررسی تایید آدرس | - |
| `CheckDuplicateBankAccountNumberV4` | بررسی تکراری حساب | - |
| `ValidateReligiousRoleV4` | اعتبارسنجی نقش‌آفرینی | - |

---

#### 2.3.5. Utility SPs

| SP Name | توضیحات |
|---------|---------|
| `GenerateOtpCode` | تولید کد OTP |
| `PrepareTestData` | آماده‌سازی داده تست |
| `GenerateUniqueCode` | تولید کد یکتا |
| `UpdateBranchAndAgency` | بروز شعبه |
| `GetTableRecordCountV4` | تعداد رکوردها |

---

### 2.4. Mapping و Parameterization

#### 2.4.1. Anonymous Objects

```csharp
var result = await dapper.ExecuteProcedureToList<StudentDto>(
    ProcedureName.GetStudentInfoV4,
    new {
        Codm = 12345678,
        IncludeDeleted = false
    }
);
```

Dapper خودکار پارامترها را Map می‌کند.

---

#### 2.4.2. Dynamic Parameters

```csharp
var parameters = new DynamicParameters();
parameters.Add("@Codm", 12345678);
parameters.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

await connection.ExecuteAsync(
    "stu.SetNewStudent",
    parameters,
    commandType: CommandType.StoredProcedure
);

int newCodm = parameters.Get<int>("@Result");
```

---

#### 2.4.3. SQL Injection Prevention

✅ **امن**:
```csharp
await dapper.ExecuteProcedureToList<StudentDto>(
    ProcedureName.GetStudentInfoV4,
    new { Codm = userInput }  // Parameterized
);
```

❌ **ناامن**:
```csharp
await dapper.ExecuteQuery(
    $"SELECT * FROM Students WHERE Codm = {userInput}"  // SQL Injection!
);
```

**قانون**: همیشه از Parameterization استفاده کنید.

---

### 2.5. Transaction Boundary

#### مشکل: Dapper و EF در یک Transaction

```csharp
using var transaction = await dbContext.Database.BeginTransactionAsync();

try {
    // ✅ EF Operation
    var student = new Student { ... };
    dbContext.Students.Add(student);
    await dbContext.SaveChangesAsync();
    
    // ❌ Dapper در Transaction جدا است!
    await dapper.ExecuteProcedure(
        ProcedureName.SetStudentPictureV4,
        new { Codm = student.Codm, Picture = ... }
    );
    
    await transaction.CommitAsync();
}
catch {
    await transaction.RollbackAsync();
}
```

**راه حل**: استفاده از Connection مشترک

```csharp
var connection = dbContext.Database.GetDbConnection();
using var transaction = await connection.BeginTransactionAsync();

dbContext.Database.UseTransaction(transaction as DbTransaction);

// حالا هم EF و هم Dapper در یک Transaction هستند
```

---

### 2.6. Risks و Pitfalls

#### ⚠️ ریسک 1: Command Timeout طولانی (180s)

```csharp
private readonly int _commandTimeout = 180; // 3 minutes
```

**تاثیر**: Query های کند ممکن است منابع را مسدود کنند.

**راه حل**: Optimize کردن SP ها، Index گذاری

---

#### ⚠️ ریسک 2: ExecuteQuery با SQL مستقیم

```csharp
public async Task<dynamic[]> ExecuteQuery(string query)
```

**خطر**: SQL Injection اگر Input سانیتایز نشود.

**راه حل**: استفاده از SP ها به جای Query مستقیم

---

#### ⚠️ ریسک 3: عدم Caching

Dapper نتایج را Cache نمی‌کند → هر بار Query به Database

**راه حل**: استفاده از `IMemoryCacheService` در Repository

---

## بخش 3: Repository Pattern

### 3.1. Generic Repository

**مسیر**: `/Repositories/Repository.cs`

```csharp
internal class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    protected readonly AppDbContext _dbContext;
    protected readonly IMemoryCacheService _cache;
    protected readonly ICacheKeyService<TKey> _cacheKeyService;
    
    // Properties
    protected IQueryable<TEntity> QueryNoTracking 
        => _dbContext.Set<TEntity>().AsNoTracking();
    
    protected IQueryable<TEntity> QueryTracking 
        => _dbContext.Set<TEntity>().AsTracking();
    
    protected DbSet<TEntity> DbSet 
        => _dbContext.Set<TEntity>();
}
```

---

### 3.2. Methods

#### GetById (با Cache)

```csharp
public async Task<TEntity> GetByIdAsync(
    TKey id,
    bool includeDeleted = false,
    bool ignoreCache = false,
    CancellationToken ct = default
)
{
    if (ignoreCache) {
        return await AddDeletedFilter(QueryNoTracking, includeDeleted)
            .FirstOrDefaultAsync(x => x.Id.Equals(id), ct);
    }
    
    // ✅ استفاده از Cache
    return await _cache.GetOrSetAsync(
        _cacheKeyService.GetEntityKey<TEntity>(id),
        async () => {
            return await AddDeletedFilter(QueryNoTracking, includeDeleted)
                .FirstOrDefaultAsync(x => x.Id.Equals(id), ct);
        },
        GetCacheOptions(),
        ct
    );
}
```

---

### 3.3. Custom Repositories

| Repository | نقش |
|-----------|------|
| `StudentRepository` | عملیات پیچیده دانشجو |
| `PersonRepository` | مدیریت افراد |
| `AdmissionAuditLogRepository` | لاگ‌های ممیزی |
| `StudentMobileRepository` | موبایل دانشجو |
| `StudentBankAccountRepository` | حساب بانکی |
| `StudentDependentRepository` | تکفل |
| `BasicDataRepository` | داده‌های پایه |
| `QueryBuilderRepository` | Query Builder پویا |

---

### 3.4. StudentRepository (نمونه)

**مسیر**: `/Repositories/Students/StudentRepository.cs`

```csharp
internal sealed class StudentRepository : IStudentRepository
{
    private readonly AppDapperContext _dapper;
    private readonly IMapper _mapper;
    
    public async Task<StudentDto> GetStudentInfoByCodm(int codm)
    {
        var students = await _dapper.ExecuteProcedureToList<StudentDto>(
            ProcedureName.GetStudentInfoV4,
            new { Codm = codm }
        );
        return students.FirstOrDefault();
    }
    
    public async Task UpdateStudentBirthCertInfo(
        UpdateStudentBirthCertInfoRepoCommand command
    )
    {
        await _dapper.ExecuteProcedure(
            ProcedureName.SetStudentBirthCertInfo,
            new {
                command.Codm,
                command.NationalCode,
                command.BirthDate,
                command.Religion
            }
        );
    }
}
```

---

## خلاصه و نتیجه‌گیری

### ✅ نقاط قوت

1. **Dual Approach**: EF Core برای CRUD، Dapper برای SP ها
2. **Pooling**: تا 1024 connection همزمان
3. **Bulk Operations**: عملیات دسته‌ای سریع
4. **Soft Delete**: حذف منطقی
5. **Audit Logging**: Interceptor خودکار
6. **Caching**: کش در Repository
7. **Clean Configuration**: تفکیک Configurations

### ⚠️ نقاط ضعف و ریسک‌ها

1. **Command Timeout بالا (180s)**: خطر مسدود شدن
2. **ExecuteQuery ناامن**: خطر SQL Injection
3. **تعداد Migration کم**: احتمال مشکل در Production
4. **MaxPoolSize خیلی بالا (1024)**: فشار به SQL Server
5. **عدم Transaction مشترک**: EF و Dapper جدا کار می‌کنند

### 📊 آمار نهایی

- ✅ 92 Configuration
- ✅ 108 Entity
- ✅ 90 Stored Procedure
- ✅ 13 Custom Repository
- ✅ Interceptor برای Audit
- ✅ Connection Pooling

---

**لینک به مستندات مرتبط**:
- `/docs/files/Csis.Admission.Persistence/AppDbContext.md`
- `/docs/files/Csis.Admission.Persistence/AppDapperContext.md`
- `/docs/files/Csis.Admission.Persistence/Configurations/...`

</div>
