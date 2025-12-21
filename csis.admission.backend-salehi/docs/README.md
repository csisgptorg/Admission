<div dir="rtl">

# گزارش کشف اولیه سامانه پذیرش (Admission)

این گزارش فقط برداشت اولیه از ساختار و نقاط ورودی کد است تا مرحله بعدی (مستندسازی فایل‌به‌فایل و استخراج Use Case) بر پایه آن اجرا شود.

## فهرست اجمالی پروژه‌ها و لایه‌ها
- **Solution**: `Csis.Admission.sln` شامل لایه‌های Core، Infrastructure، Presentation و مجموعه تست‌هاست. پروژه‌های اصلی: `Csis.Admission.Domain` (دامنه)، `Csis.Admission.Application` (منطق کاربردی)، `Csis.Admission.Persistence` (داده)، `Csis.Admission.Services` (سرویس‌های کمکی/HealthCheck) و `Csis.Admission.WebApi` (ورودی HTTP).【F:Csis.Admission.sln†L5-L88】
- **WebApi**: پروژه `Csis.Admission.WebApi` با هدف تولید مستندات XML و وابستگی به Serilog، Swagger و Digest Authentication برای محافظت از مسیرهای Swagger/HealthCheck. ارجاع به لایه‌های Application، Persistence و Services دارد.【F:Csis.Admission.WebApi/Csis.Admission.WebApi.csproj†L1-L84】
- **Application**: لایه CQRS/میدیاتر با تولید فایل داک، ارجاع به دامنه، AutoMapper، MediatR، FluentValidation و کتابخانه‌های تصویری/فایل. شامل فولدر `Features` بسیار گسترده برای Commands/Queries/Validators است.【F:Csis.Admission.Application/Csis.Admission.Application.csproj†L13-L69】
- **Persistence**: لایه داده با EF Core (SQL Server/InMemory)، BulkExtensions و Dapper؛ ارجاع به Application دارد و برای اینترفیس‌های داده/کانفیگ‌ها استفاده می‌شود.【F:Csis.Admission.Persistence/Csis.Admission.Persistence.csproj†L12-L44】
- **Services**: لایه سرویس/سلامت با HealthChecks برای SQL/Redis/Elasticsearch و کش Redis؛ وابسته به Persistence.【F:Csis.Admission.Services/Csis.Admission.Services.csproj†L17-L39】

## نقاط ورودی و پیکربندی کلی
- **Program.cs**: مقداردهی GlobalOptions (حالت توسعه، سرویس‌های پس‌زمینه، آپلود فایل)، تنظیم Serilog با Elastic sink، ثبت DI برای Application/Services/Persistence، فعال‌سازی Swagger و Digest Authentication، تعریف CORS، و اجرای Seeder در صورت فعال بودن گزینه `DatabaseOptions.RunSeeders`.【F:Csis.Admission.WebApi/Program.cs†L29-L200】
- **پیکربندی (appsettings.json)**: شامل گزینه‌های Cache، CORS، DatabaseOptions (ConnectionStrings/Pooling/Seeders)، DigestAuthenticationOptions (کاربران swagger/health)، ElasticSearchOptions، سرویس‌های بیرونی (Employee/Student/File/Identity Server)، Redis، Serilog و SwaggerOptions. این گزینه‌ها در Program.cs خوانده و برای ساخت سرویس‌ها به‌کار می‌روند.【F:Csis.Admission.WebApi/appsettings.json†L2-L108】

## الگوها و تکنولوژی‌های تشخیص‌داده‌شده
- **CQRS/MediatR + FluentValidation** در لایه Application (وابستگی‌های بسته).【F:Csis.Admission.Application/Csis.Admission.Application.csproj†L43-L95】
- **AutoMapper** برای نگاشت DTOها و استفاده از `MapperProvider.Initialize` در Program.cs برای تزریق Mapper عمومی.【F:Csis.Admission.WebApi/Program.cs†L145-L156】
- **EF Core + Dapper** در Persistence با گزینه InMemory و BulkExtensions برای کارایی درج/به‌روزرسانی انبوه.【F:Csis.Admission.Persistence/Csis.Admission.Persistence.csproj†L22-L36】
- **Serilog + Elastic Stack** برای لاگ و ارسال به Elastic Data Stream در زمان اجرا.【F:Csis.Admission.WebApi/Program.cs†L49-L80】
- **Swagger با Digest Authentication** برای محافظت از رابط اسناد و HealthCheck.【F:Csis.Admission.WebApi/Program.cs†L93-L199】
- **HealthChecks/Redis** در لایه Services برای رصد دیتابیس، Elasticsearch و Redis.【F:Csis.Admission.Services/Csis.Admission.Services.csproj†L17-L39】

## نمای کلی نقاط ورودی HTTP
- ساختار کنترلرها شامل سه خوشه اصلی است: `Other`, `Students`, `NonIranian`, `NonStudents`, `ImamJamaat` و مدیریت گزارش/پروفایل. تعداد زیاد کنترلرها (ده‌ها فایل) نشان‌دهنده پوشش گسترده Use Caseهای دانشجویی/درخواست/اطلاعات جانبی است. مسیرهای عمومی (`.Public.cs`) برای دسترسی بدون احراز هویت طراحی شده‌اند.【e689dc†L1-L92】

## نقشه اولیه Featureها
- فولدر `Csis.Admission.Application/Features` شامل ده‌ها زیرحوزه (مثلاً `Students`, `Addresses`, `BankAccounts`, `Documents`, `Protests`, `Auth`, `Settings`, `ReportBuilders`, `BlockServices` و ...) با زیرشاخه‌های `Commands`, `Queries`, `Validators`, `Dtos`. این ساختار نشان‌دهنده معماری استاندارد CQRS با Validator و DTO برای هر Use Case است.【57936a†L1-L143】

## نقاط داده و کانفیگ مهم
- **DatabaseOptions**: پشتیبانی از SQL Server و گزینه `UseInMemoryDatabase` برای تست، کنترل لاگ و Pooling تا 1024 کانکشن.【F:Csis.Admission.WebApi/appsettings.json†L20-L30】
- **ElasticSearchOptions**: فعال بودن پیش‌فرض و نیاز به لیست نودها؛ در صورت نبود لیست در زمان اجرا خطا ایجاد می‌شود (Program.cs).【F:Csis.Admission.WebApi/appsettings.json†L46-L49】【F:Csis.Admission.WebApi/Program.cs†L65-L79】
- **GlobalOptions**: پرچم‌های توسعه، سرویس‌های پس‌زمینه و آپلود فایل که مسیر اجرای برنامه را تعیین می‌کنند.【F:Csis.Admission.WebApi/appsettings.json†L60-L64】【F:Csis.Admission.WebApi/Program.cs†L34-L46】
- **DigestAuthenticationOptions**: کاربران جداگانه برای Swagger و Health؛ Realm برابر `csis.ir`.【F:Csis.Admission.WebApi/appsettings.json†L31-L45】【F:Csis.Admission.WebApi/Program.cs†L102-L117】

## اقدام بعدی
- بر اساس این نقشه، مرحله بعدی شامل تولید مستندات فایل‌به‌فایل (`/docs/files/**`) و ایندکس‌های UseCases، Features، RequestFlows، DataAccess و ConfigMap خواهد بود. هر سند با پوسته RTL و لینک‌دهی متقابل تهیه می‌شود.

</div>
