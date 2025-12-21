using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csis.Admission.Domain.Enums;
/// <summary>
/// نسبت خانوادگی شخص
/// </summary>
public enum PersonRelation
{
    /// <summary>
    /// پدر
    /// </summary>
    Father = 1,
    /// <summary>
    /// مادر
    /// </summary>
    Mother = 2,
    /// <summary>
    /// فرزند
    /// </summary>
    Child = 3,
}
