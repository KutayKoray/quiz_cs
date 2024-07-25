using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Api
{
    public class Question
    {
        [Key]
        public int soru_id { get; set; }

        [Required] // TYT / AYT
        public string alan_bilgisi { get; set; } = String.Empty;

        [Required] // Türkçe / Tarih / Coğrafya / Fizik / Kimya / Biyoloji / Matematik
        public string soru_dersi { get; set; } = String.Empty;

        [Required]
        public string dogru_cevap { get; set; } = String.Empty;

        public string? ImageFileName { get; set; }
    }
}
