/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using System.Reflection;

namespace Csis.Admission.Application.Common;

/// <summary>
/// Relation info between entities
/// </summary>
/// <param name="Type">The type of navigation used for relation</param>
/// <param name="ForeignKey">The foreign key property used in relation</param>
public sealed record EntityRelation(Type Type, PropertyInfo ForeignKey);
