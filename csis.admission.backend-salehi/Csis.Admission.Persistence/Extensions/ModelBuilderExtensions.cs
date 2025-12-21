/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Domain.Common;
using System.Reflection;

namespace Csis.Admission.Persistence.Extensions;
internal static class ModelBuilderExtensions
{
    /// <summary>
    /// Auto register DbSets for all entities
    /// </summary>
    /// <param name="modelBuilder"></param>
    public static void RegisterDbSets(this ModelBuilder modelBuilder) {
        foreach ( var type in GetEntityTypes().Distinct() ) {
            modelBuilder.Entity(type);
        }
    }

    private static IEnumerable<Type> GetEntityTypes() {
        foreach ( var type in typeof(IEntity).Assembly.GetTypes().Where(x => x.IsClass && x.IsPublic && !x.IsAbstract) ) {
            if ( typeof(IEntity).IsAssignableFrom(type) ) {
                yield return type;
            }

            var baseType = type.BaseType;
            if ( baseType is not null && baseType.IsGenericType && typeof(IEntity<>).IsAssignableFrom(baseType.GetGenericTypeDefinition()) ) {
                yield return type;
            }

            foreach ( var @interface in type.GetInterfaces() ) {
                if ( @interface.IsGenericType && typeof(IEntity<>).IsAssignableFrom(@interface.GetGenericTypeDefinition()) ) {
                    yield return type;
                }
            }
        }
    }

    /// <summary>
    /// اضافه کردن توضیحات در دیتابیس
    /// </summary>
    /// <param name="builder"></param>
    public static void AddXmlComments(this ModelBuilder builder) {
        try {
            var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{typeof(BaseEntity).Assembly.GetName().Name}.xml");
            var xmlDocument = XmlDocumentHelper.ReadXmlFile(xmlPath);
            var baseEntityAssemblyName = Assembly.GetAssembly(typeof(BaseEntity)).FullName;

            // تایپ انتیتی‌های مدل‌بیلدر
            foreach ( var entityBuilderMetadata in builder.Model.GetEntityTypes() ) {
                // تایپ مربوط به انتیتی در دامین
                var entityType = entityBuilderMetadata.ClrType;

                // پراپرتی‌های انتیتی
                foreach ( var entityProperty in entityType?.GetProperties(BindingFlags.Public | BindingFlags.Instance) ?? [] ) {
                    // پراپرتی متناظر در مدل‌بیلدر
                    var property = entityBuilderMetadata.GetProperties().FirstOrDefault(x => x.Name == entityProperty.Name);

                    if ( property is not null ) {
                        // بدست آوردن توضیحات پراپرتی
                        var propertySummary = xmlDocument.GetPropertySummary(entityType, entityProperty.Name);

                        // اگر اینام است، مقادیر آن به توضیحات اضافه شود
                        if ( entityProperty.PropertyType.IsEnum ) {
                            var enumType = Type.GetType($"{entityProperty.PropertyType.FullName}, {baseEntityAssemblyName}");

                            if ( enumType is not null ) {
                                propertySummary += ":\r\n";

                                // فیلدهای تعریف شده برای اینام
                                var enumFields = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);
                                var enumSummaries = new List<string>();

                                foreach ( var enumField in enumFields ) {
                                    // بدست آوردن توضیحات اینام
                                    var enumSummary = xmlDocument.GetPropertySummary(enumField.FieldType, enumField.Name);

                                    // مقادیر تعریف شده برای اینام
                                    foreach ( var valueAsObject in Enum.GetValues(enumType) ) {
                                        var valueName = Enum.GetName(enumType, valueAsObject);

                                        if ( enumField.Name == valueName ) {
                                            var value = Convert.ChangeType(valueAsObject, Enum.GetUnderlyingType(enumType));
                                            enumSummaries.Add($"{enumSummary} = {value}");
                                            break;
                                        }
                                    }
                                }

                                propertySummary += string.Join(" , \r\n", enumSummaries);
                            }
                        }

                        if ( !string.IsNullOrWhiteSpace(propertySummary) ) {
                            // ست کردن برای ثبت در دیتابیس
                            property.SetComment(propertySummary);
                        }
                    }
                }
            }
        } catch ( Exception ex ) {
            Console.WriteLine($"Error in AddXmlDocsSummaryComments method. {ex.Message}");
        }
    }
}
