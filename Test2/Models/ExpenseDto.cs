using System.ComponentModel.DataAnnotations;

namespace Test2.Models
{
    public class ExpenseDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public decimal Price { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string Email { get; set; }

        public IFormFile? ImageFile { get; set; }

        [Required]
        public string Category { get; set; }
    }
}
