namespace Csis.Admission.Application.Common.Helpers;

/// <summary>
/// Helper class for determining request flow.
/// </summary>
public static class RequestFlowHelper
{
    /// <summary>
    /// بررسی فلو درخواست بر اساس وضعیت خانه، وضعیت درخواست، نوع کاربر و فایل های آپلود شده.
    /// </summary>
    /// <param name="requestStatus">وضعیت درخواستی خانه</param>
    /// <param name="houseStatus">وضعیت فعلی خانه</param>
    /// <param name="isStudent">آیا کاربر دانشجو است</param>
    /// <param name="isEmployee">آیا کاربر کارمند است</param>
    /// <param name="isSeniorPersonnel">آیا کاربر کارمند ارشد است</param>
    /// <param name="fileUploadTypes">لیست انواع فایل‌های آپلود شده</param>
    /// <returns>نوع فلو درخواست</returns>
    public static async Task<RequestFlow> DetermineRequestFlowAsync(
        HouseStatus requestStatus,
        HouseStatus? houseStatus,
        bool isStudent,
        bool isEmployee,
        bool isSeniorPersonnel,
        List<DocumentType> fileUploadTypes) {

        RequestFlow? result = null;

        if ( isSeniorPersonnel ) {
            return RequestFlow.DirectRegistration;
        }

        switch ( requestStatus ) {
            // ✔
            case HouseStatus.Supportive:
                if ( houseStatus is HouseStatus.Supportive or HouseStatus.RentalOrMortgage ) {
                    result = RequestFlow.DirectRegistration;
                }

                break;

            case HouseStatus.Private:
                result = RequestFlow.DirectRegistration;
                break;

            case HouseStatus.RentalOrMortgage:
                result = DetermineRentalFlow(houseStatus, isStudent, isEmployee, isSeniorPersonnel,
                    fileUploadTypes);
                break;
        }


        if ( result == null && houseStatus == HouseStatus.Private ) {
            result = DeterminePrivateHouseFlow(isStudent, isEmployee, isSeniorPersonnel, fileUploadTypes);
        }

        if ( result == null ) {
            throw new CommandValidationException("فایل های مورد نظر را وارد کنید");
        }

        return result.Value;
    }

    #region Private Helper Methods

    /// <summary>
    /// تعیین فلو برای درخواست خانه اجاره‌ای
    /// </summary>
    private static RequestFlow? DetermineRentalFlow(HouseStatus? houseStatus, bool isStudent, bool isEmployee,
        bool isSeniorPersonnel, List<DocumentType> fileUploadTypes) {

        var hasLeaseCertificate = fileUploadTypes.Contains(DocumentType.LeaseCertificateFirstPage);
        var hasNonOwnershipProof = fileUploadTypes.Contains(DocumentType.ProofOfNonOwnership);

        switch ( isStudent ) {
            case true when hasNonOwnershipProof && houseStatus == HouseStatus.Private:
                return RequestFlow.StudentToEmployeeToSeniorEmployee;
            case true when hasLeaseCertificate:
                return RequestFlow.StudentToEmployee;
        }

        if ( (isEmployee && hasLeaseCertificate) || (isEmployee && hasNonOwnershipProof) || isSeniorPersonnel ) {
            return RequestFlow.DirectRegistration;
        } 
        else if ( isEmployee && !isSeniorPersonnel && !hasLeaseCertificate && !hasNonOwnershipProof ) {
            throw new CommandValidationException("کارمند محترم، لطفا مدارک اجاره نامه را آپلود نمایید.");
        }

        return null;
    }

    /// <summary>
    /// تعیین فلو برای درخواست خانه شخصی
    /// </summary>
    private static RequestFlow? DeterminePrivateHouseFlow(bool isStudent, bool isEmployee, bool isSeniorPersonnel,
        List<DocumentType> fileUploadTypes) {

        var hasNonOwnershipProof = fileUploadTypes.Contains(DocumentType.ProofOfNonOwnership);

        if ( isStudent && hasNonOwnershipProof ) {
            return RequestFlow.StudentToEmployeeToSeniorEmployee;
        }

        if ( (isEmployee && hasNonOwnershipProof) || isSeniorPersonnel ) {
            return RequestFlow.DirectRegistration;
        }

        if( isEmployee && !hasNonOwnershipProof ) {
            throw new CommandValidationException("کارمند محترم، لطفا مدرک عدم مالکیت را آپلود نمایید.");
        }

        return null;
    }
    #endregion
}

