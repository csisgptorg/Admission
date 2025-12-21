namespace Csis.Admission.Application.Features.Students.Dtos;

/// <summary>تعداد رکوردهای طلبه در بخش های مختلف</summary>
public record StudentRecordCountDto
{
    /// <summary></summary>
    public int HasPreach { get; set; }

    /// <summary></summary>
    public int HasTeach { get; set; }

    /// <summary></summary>
    public int HasResearch { get; set; }

    /// <summary></summary>
    public int HasCulturalActivity { get; set; }

    /// <summary></summary>
    public int HasUniversityEducation { get; set; }

    /// <summary></summary>
    public int HasExcellent { get; set; }

    /// <summary></summary>
    public int HasElite { get; set; }

    /// <summary></summary>
    public int HasProtest { get; set; }

    /// <summary></summary>
    public int HasCommission { get; set; }

    /// <summary></summary>
    public int HasMemorizer { get; set; }

    /// <summary></summary>
    public int HasSoldierStudent { get; set; }

    /// <summary></summary>
    public int HasBlockedService { get; set; }

    /// <summary></summary>
    public int HasFriend { get; set; }

    /// <summary></summary>
    public int HasRequest { get; set; }
}

