using System.Reflection;
using System.Collections;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Application.Common.Models.QueryBuilders;

/// <summary>گزاش ساز</summary>
public class ReportBuilderModel
{
    /// <summary>جدول</summary>
    public class Table
    {
        /// <inheritdoc/>
        public Table() { }

        /// <inheritdoc/>
        public Table(Type tableType) {
            var tableAttribute = tableType.GetCustomAttribute<QueryBuilderAttribute>();
            Name = tableType.Name;
            Label = tableAttribute?.Label;
            Tab = tableAttribute.Tab.ToString();
            Columns = tableType.GetProperties().Select(property => new Column(property)).ToArray();
        }

        /// <summary>نام</summary>
        public string Name { get; set; }
        /// <summary>لیبل</summary>
        public string Label { get; }
        /// <summary>تب</summary>
        public string Tab { get; }
        /// <summary>ستون ها</summary>
        public Column[] Columns { get; set; }
    }

    /// <summary>ستون</summary>
    public class Column
    {
        /// <inheritdoc/>
        public Column() { }

        /// <inheritdoc/>
        public Column(PropertyInfo property) {
            var attribute = property.GetCustomAttribute<QueryBuilderAttribute>();
            Name = property.Name;
            Label = attribute?.Label ?? property.Name;
            Type = GetType(property);
            Source = new SourceType(property, attribute);
            DependentSource = new DependentSourceType(attribute?.DependentColumn, attribute?.DependentApi,attribute?.DependentLabel);
            Operators = attribute?.Operators;
        }

        /// <inheritdoc/>
        public Column(string name, string label, ColumnType type) {
            Name = name;
            Label = label;
            Type = type.ToString();
        }

        /// <summary>نام</summary>
        public string Name { get; set; }
        /// <summary>لیبل</summary>
        public string Label { get; }
        /// <summary>نوع</summary>
        public string Type { get; }
        /// <summary>منبع</summary>
        public SourceType Source { get; }
        /// <summary>ستون وابسته</summary>
        public DependentSourceType DependentSource { get; }
        /// <summary>عملگر</summary>
        public string[] Operators { get; }

        /// <summary>منبع ستون</summary>
        public class SourceType
        {
            /// <inheritdoc/>
            public SourceType(PropertyInfo property, QueryBuilderAttribute attribute) {
                Api = attribute.Source == ColumnSourceType.Api ? ReportBuilderTables.ApiSources[property.Name.Replace("Id", "")] : null;

                var propertyType = property.GetUnderlyingType();
                Enum = propertyType.IsEnum ? propertyType.Name : null;

                //Data = Enum != null ? EnumHelper.GetEnumKeyValuePairs(propertyType) : null;
            }

            /// <summary>ای پی آی</summary>
            public string Api { get; }
            /// <summary>اینام</summary>
            public string Enum { get; }
            /// <summary>دیتا</summary>
            public IEnumerable Data { get; }
        }


        /// <summary>منبع وابستگی</summary>
        public record DependentSourceType(string Column, string Api,string Label);

        /// <summary>دریافت نوع ستون</summary>
        static string GetType(PropertyInfo property) {
            var attribute = property.GetCustomAttribute<QueryBuilderAttribute>();
            var columnType = attribute?.Type ?? property.GetUnderlyingType();

            if ( property.Name.ToLower() == "codm" ) {

            }

            var mapTypes = new Dictionary<Type, ColumnType> {
                [typeof(long)] = ColumnType.Numeric,
                [typeof(int)] = ColumnType.Numeric,
                [typeof(short)] = ColumnType.Numeric,
                [typeof(byte)] = ColumnType.Numeric,
                [typeof(bool)] = ColumnType.Boolean,
                [typeof(string)] = ColumnType.String,
                [typeof(double)] = ColumnType.Decimal,
                [typeof(float)] = ColumnType.Decimal,
                [typeof(decimal)] = ColumnType.Decimal,
                [typeof(DateTime)] = ColumnType.Date,
            };

            mapTypes.TryGetValue(columnType, out var resultType);
            if ( columnType.IsEnum || !string.IsNullOrWhiteSpace(attribute?.RelationTable) ) {
                resultType = ColumnType.List;
            }

            return resultType.ToString().ToLower();
        }
    }
}

/// <summary>گزاش ساز</summary>
public static class ReportBuilderTables
{
    /// <summary>جداول</summary>
    private static ReportBuilderModel.Table[] ـtables { get; set; }

    /// <summary>لیست جداول</summary>
    public static ReportBuilderModel.Table[] GetAll() {
        if ( ـtables == null ) {
            ـtables = Assembly.GetAssembly(typeof(IQueryBuilderRepository)).GetTypes()
                .Where(x => x.IsClass && typeof(IQueryBuilderTable).IsAssignableFrom(x))
                .Select(x => new ReportBuilderModel.Table(x)).ToArray();
        }
        return ـtables;
    }

    /// <summary>سورس های ای پی آی</summary>
    public static readonly Dictionary<string, string> ApiSources = new(){
        { nameof(Province), "/private/provinces" },
        { nameof(Agency), "/private/agencies?branchId=" },
        { nameof(Branch), "/private/branches" },
        { nameof(Country), "/private/countries" },
        { nameof(City), "/private/cities?provinceId=" },
        { nameof(Portion), "/private/portions?cityId=" },
        { nameof(Town), "/private/towns?portionId=" },
        { nameof(Rural), "/private/rurals?townId=" },
        { nameof(EliteType), "/private/elite-types" },
        { nameof(EliteLevel), "/private/elite-levels" },
        { nameof(ExcellentEducationYear), "/private/excellent-education-years" },
        { nameof(ExcellentEducationLevel), "/private/excellent-education-levels" },
        { nameof(EducationYear), "/private/education-years" },
        { nameof(School), "/private/schools" },
    };
}
