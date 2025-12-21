//using Csis.Admission.Domain.Common;
//using Csis.Admission.Domain.Enums;

//namespace Csis.Admission.Domain.Entities;

///// <summary>
///// موجودیت طلبه
///// </summary>
//public sealed class Student : SoftDeletedBaseEntity, IFilterable, IAuditable
//{
//    private List<CaseExtensionReason> _caseExtensionReasons;
//    private List<CaseBlockReason> _caseBlockReasons;

//    ///// <summary>
//    ///// شناسه طلبه
//    ///// </summary>
//    //public int Codm { get; set; }

//    /// <summary>
//    /// شناسه کاربر
//    /// </summary>
//    public int? PersonId { get; set; }

//    /// <summary>
//    /// شعبه
//    /// </summary>
//    public byte? Branch { get; set; }

//    /// <summary>
//    /// نمایندگی
//    /// </summary>
//    public byte? Agency { get; set; }

//    /// <summary>
//    /// وضعیت
//    /// </summary>
//    public byte Status { get; set; }

//    /// <summary>
//    /// تاریخ ایجاد پرونده
//    /// </summary>
//    public int? CaseCreateDate { get; set; }

//    /// <summary>
//    /// تاریخ انقضا
//    /// </summary>
//    public int? CaseExpireDate { get; set; }

//    /// <summary>
//    /// تاریخ مسدودی پرونده
//    /// </summary>
//    public int? CaseBlockDate { get; set; }

//    /// <summary>
//    /// 
//    /// </summary>
//    public bool IsNotStudent { get; set; }

//    /// <summary>
//    /// 
//    /// </summary>
//    public int? SeveralSurvivingWifeId { get; set; }

//    /// <summary>
//    /// 
//    /// </summary>
//    public byte? ApprovalCenter { get; set; }

//    /// <summary>
//    /// 
//    /// </summary>
//    public long? NumberInApprovalCenter { get; set; }

//    /// <summary>
//    /// 
//    /// </summary>
//    public int? CommissionRequestId { get; set; }

//    /// <summary>
//    /// دلایل انسداد پرونده
//    /// مقدار فقط از طریق متد تغییر کند
//    /// </summary>
//    public List<CaseBlockReason> CaseBlockReasons {
//        get {
//            _caseBlockReasons ??= [];
//            return _caseBlockReasons;
//        }

//        private set => _caseBlockReasons = value;
//    }

//    /// <summary>
//    /// دلایل تمدید پرونده
//    /// مقدار فقط از طریق متد تغییر کند
//    /// </summary>
//    public List<CaseExtensionReason> CaseExtensionReasons {
//        get {
//            _caseExtensionReasons ??= [];
//            return _caseExtensionReasons;
//        }

//        private set => _caseExtensionReasons = value;
//    }

//    /// <summary>
//    /// کاربر
//    /// </summary>
//    public Person Person { get; private set; }

//    public ICollection<BlockedService> BlockedServices { get; private set; } = [];

//    public ICollection<StudentDependant> StudentDependents { get; private set; } = [];

//    public ICollection<StudentEducation> StudentEducations { get; private set; } = [];

//    /// <summary>
//    /// اسناد
//    /// </summary>
//    public List<Document> Documents { get; private set; } = [];

//    #region IAuditable
//    /// <inheritdoc/>
//    public Guid? TempId { get; set; }

//    /// <inheritdoc/>
//    public DataSource? AuditDataSource { get; set; }

//    /// <inheritdoc/>
//    public int? AuditRequestId { get; set; }

//    /// <inheritdoc/>
//    public int? AuditPersonId { get; set; }
//    #endregion

//    /// <summary>
//    /// تنظیم دلایل انسداد پرونده
//    /// </summary>
//    /// <param name="blockReasons"></param>
//    public void SetCaseBlockReasons(List<CaseBlockReason> blockReasons) {
//        CaseBlockReasons = new(blockReasons);
//    }

//    /// <summary>
//    /// تنظیم دلایل تمدید پرونده
//    /// </summary>
//    /// <param name="caseExtensionReasons"></param>
//    public void SetCaseExtensionReasons(List<CaseExtensionReason> caseExtensionReasons) {
//        CaseExtensionReasons = new(caseExtensionReasons);
//    }

//    /// <inheritdoc/>
//    public string[] GetFilterableFields() {
//        return [];
//    }
//}
