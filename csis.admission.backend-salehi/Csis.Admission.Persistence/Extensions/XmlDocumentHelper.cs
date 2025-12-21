/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using System.Xml.Linq;

namespace Csis.Admission.Persistence.Extensions;

internal static class XmlDocumentHelper
{
    public static XDocument ReadXmlFile(string xmlPath) {
        if ( !File.Exists(xmlPath) ) {
            throw new FileNotFoundException($"XML documentation file not found at path: {xmlPath}");
        }

        return XDocument.Load(xmlPath);
    }

    public static string GetPropertySummary(this XDocument xmlDocument, Type type, string propertyName) {
        do {
            var memberName = $"{(type.IsEnum ? 'F' : 'P')}:{type.FullName}.{propertyName}";
            var memberElement = xmlDocument.Descendants("member")
                                       .FirstOrDefault(e => e.Attribute("name")?.Value == memberName);

            if ( memberElement is not null ) {
                var summaryElement = memberElement.Element("summary");
                if ( summaryElement is not null ) {
                    return summaryElement.Value.Trim();
                }
            }

            // اگر پراپرتی پیدا نشد، کلاس پایه هم جستجو شود
            type = type.BaseType;
        }
        while ( type is not null );

        return string.Empty;
    }
}
