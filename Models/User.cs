using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Api
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Surname { get; set; } = string.Empty;
        
        [Required]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public bool is_student { get; set; }

        [Required]
        public bool is_teacher { get; set; }

        public int Total_questions { get; set; }

        public int Correct_answers { get; set; }

        public int Wrong_answers { get; set; }

        public List<int> Wrong_questions_Ids { get; set; } = new List<int>();

        public int Score { get; set; }

    }
}
