/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Application.Common;

/// <summary>
/// Events list through the entire application
/// </summary>
public static partial class Events
{
    /// <summary>
    /// Bad request
    /// </summary>
    public const int BadRequest = 400;

    /// <summary>
    /// Unauthorized action attempted
    /// </summary>
    public const int UnauthorizedAction = 403;

    /// <summary>
    /// Resource not found
    /// </summary>
    public const int NotFound = 404;

    /// <summary>
    /// Unexpected exception occurred
    /// </summary>
    public const int UnhandledException = 500;

    /// <summary>
    /// Method not implemented exception occurred
    /// </summary>
    public const int NotImplemented = 501;

    /// <summary>
    /// Service unavailable like request timeout
    /// </summary>
    public const int ServiceUnavailable = 503;

    #region Student data service   
    /// <summary>
    /// Logs related to <seealso cref="IStudentDataService.GetStudentInfoAsync"/>
    /// </summary>
    public const int GetStudentInfo = 1000;

    /// <summary>
    /// Logs related to <seealso cref="IStudentDataService.SearchStudentAsync"/>
    /// </summary>
    public const int SearchStudent = 1001;

    /// <summary>
    /// Logs related to <seealso cref="IStudentDataService.GetStudentWithDependantsAsync"/>
    /// </summary>
    public const int GetStudentWithDependants = 1002;

    /// <summary>
    /// Logs related to <seealso cref="IStudentDataService.GetStudentGroupInfoAsync"/>
    /// </summary>
    public const int GetStudentGroupInfo = 1003;

    /// <summary>
    /// Logs related to <seealso cref="IStudentDataService.GetDependantsGroupInfoAsync"/>
    /// </summary>
    public const int GetDependantsGroupInfo = 1004;

    /// <summary>
    /// Logs related to <seealso cref="IStudentDataService.GetCsisBranchesAsync"/>
    /// </summary>
    public const int GetCsisBranches = 1005;

    /// <summary>
    /// Logs related to <seealso cref="IStudentDataService.AdvancedSearchAsync"/>
    /// </summary>
    public const int AdvancedSearchStudent = 1006;
    #endregion

    #region Employee Data Service
    /// <summary>
    /// Logs related to <seealso cref="IEmployeeDataService.GetEmployeeInfoAsync"/>
    /// </summary>
    public const int GetEmployeeInfo = 1100;

    /// <summary>
    /// Logs related to <seealso cref="IEmployeeDataService.GetAllJobPositionsAsync"/>
    /// </summary>
    public const int GetAllJobPositions = 1101;

    /// <summary>
    /// Logs related to <seealso cref="IEmployeeDataService.GetJobPositionsByPersonnelIdAsync"/>
    /// </summary>
    public const int GetJobPositionsByPersonnelId = 1102;

    /// <summary>
    /// Logs related to <seealso cref="IEmployeeDataService.GetEmployeeContactInfoAsync"/>
    /// </summary>
    public const int GetEmployeeContactInfo = 1103;

    /// <summary>
    /// Logs related to <seealso cref="IEmployeeDataService.GetEmployeesGroupInfoAsync"/>
    /// </summary>
    public const int GetEmployeesGroupInfo = 1104;

    /// <summary>
    /// Logs related to <seealso cref="IEmployeeDataService.GetDependantsGroupInfoAsync"/>
    /// </summary>
    public const int GetEmployeeDependantsGroupInfo = 1105;
    #endregion

    #region Background Services
    /// <summary>
    /// Logs related to send notification background service
    /// </summary>
    public const int SendNotificationBackgroundService = 65_000;
    #endregion
}
