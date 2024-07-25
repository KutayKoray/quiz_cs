namespace WebApplication1.Api;

public class FormData
{
    public string AlanBilgisi { get; set; } = string.Empty;
    public string SoruTuru { get; set; } = string.Empty;
    public string SoruDersi { get; set; } = string.Empty;
    public IFormFile DosyaYukle { get; set; } = null!;
}
