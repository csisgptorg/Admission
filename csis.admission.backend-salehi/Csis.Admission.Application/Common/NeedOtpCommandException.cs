namespace Csis.Admission.Application.Common;

/// <summary>نیازمند تایید کد یکبار مصرف</summary>
public sealed class NeedOtpCommandException : Exception
{
    /// <inheritdoc/>
    public NeedOtpCommandException(int expiresInSeconds, string message) : base(message) {
        ExpiresInSeconds = expiresInSeconds;
    }

    /// <summary>زمان انتظار به ثانیه</summary>
    public int ExpiresInSeconds { get; }
}
