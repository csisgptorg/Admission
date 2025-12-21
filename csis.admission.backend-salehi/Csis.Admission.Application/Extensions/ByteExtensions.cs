namespace Csis.Admission.Application.Extensions;

/// <summary>
/// ByteExtensions
/// </summary>
public static class ByteExtensions
{
    /// <summary>
    /// ByteToBase64String
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string ByteToBase64String(this byte[] input) {

        if ( !input.Any() )return null;
        var base64 = Convert.ToBase64String(input, 0, input.Length);
        base64= "data:image/jpg;base64," + base64;
        return base64;
    }
}
