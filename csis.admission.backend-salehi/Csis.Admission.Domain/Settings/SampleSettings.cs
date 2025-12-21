namespace Csis.Admission.Domain.Settings;

/// <summary>
/// تنظیمات نمونه
/// </summary>
public sealed class SampleSettings : ISettings<SampleSettings>
{
    /// <summary>
    /// 
    /// </summary>
    public int SamplePropOne { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string SamplePropTwo { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool SamplePropThree { get; set; }

    /// <inheritdoc/>
    public SampleSettings GetDefault() {
        return new() {
            SamplePropOne = 10,
            SamplePropTwo = "Test",
            SamplePropThree = true
        };
    }
}
