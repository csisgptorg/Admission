using Csis.Admission.Application.Common.Configuration;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Features.CaseFilings.Dtos;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography;

namespace Csis.Admission.Application.Features.CaseFilings.Commands;

/// <summary>
/// تولید و دریافت کد کپچا
/// </summary>
public sealed record GenerateCaptchaCommand : IRequest<CaptchaDto>;

internal sealed class GenerateCaptchaCommandHandler(IMemoryCacheService memoryCacheService)
    : IRequestHandler<GenerateCaptchaCommand, CaptchaDto>
{
    /// <summary>
    /// Handle
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<CaptchaDto> Handle(GenerateCaptchaCommand request, CancellationToken cancellationToken) {
        var random = new Random();
        var captchaCode = random.Next(1000, 9999).ToString(); // Generate a 4-digit number

        var persianCaptchaCode = PersianNumber.GET_Number_To_PersianString(captchaCode);

        var tokenBytes = new byte[32];
        using ( var rng = RandomNumberGenerator.Create() ) {
            rng.GetBytes(tokenBytes);
        }

        var token = Convert.ToHexString(tokenBytes);
#pragma warning disable CA1416 // Validate platform compatibility
        using var bitmap = new Bitmap(600, 60);
        using var graphics = Graphics.FromImage(bitmap);
        graphics.Clear(Color.White);

        var font = new Font(new FontFamily("Arial"), 28, FontStyle.Bold | FontStyle.Italic);

        var brush = new SolidBrush(Color.Black);

        // Add random lines for noise
        for ( var i = 0; i < 100; i++ ) {
            var x1 = random.Next(0, bitmap.Width);
            var y1 = random.Next(0, bitmap.Height);
            var x2 = random.Next(0, bitmap.Width);
            var y2 = random.Next(0, bitmap.Height);
            graphics.DrawLine(new Pen(Color.Gray, 2), x1, y1, x2, y2);
        }

        var words = persianCaptchaCode.Split(' ').Where(c => c != "").ToArray();
        // Apply distortion by shifting characters randomly
        float xDistortion = 500;
        for ( var i = 0; i < words.Length; i++ ) {
            float y = 10 + random.Next(-10, 10); // Random slight y shift
            graphics.DrawString(words[i].ToString(), font, brush, xDistortion, y);
            xDistortion -= 70;
        }

        // Add random noise dots
        for ( var i = 0; i < 1000; i++ ) {
            var x = random.Next(0, bitmap.Width);
            var y = random.Next(0, bitmap.Height);
            bitmap.SetPixel(x, y, Color.Gray);
        }

        using var memoryStream = new MemoryStream();
        bitmap.Save(memoryStream, ImageFormat.Png);
        var imageBytes = memoryStream.ToArray();
        var base64Image = Convert.ToBase64String(imageBytes);
        base64Image = $"data:image/png;base64,{base64Image}";

        memoryCacheService.Set(token, captchaCode, new CacheOptions {
            AbsoluteExpirationSeconds = 120,
        });

        return new CaptchaDto {
            ImageBase64 = base64Image,
            Token = token,
        };
    }
}
