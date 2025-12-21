using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csis.Admission.Domain.Enums;

/// <summary>
/// وضعیت غیر طلبه
/// </summary>
public enum NonStudentStatus : byte
{
    /// <summary>
    /// فعال
    /// </summary>
    Active = 1,

    /// <summary>
    /// غیرفعال
    /// </summary>
    Disabled = 2,

    /// <summary>
    /// مسدود
    /// </summary>
    Blocked = 2,
}
