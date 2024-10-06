using System.ComponentModel.DataAnnotations;

namespace Fridayfrietday.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(40, ErrorMessage = "Name cannot exceed 40 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(300, ErrorMessage = "Description cannot exceed 300 characters")]
        public string Description { get; set; }

        [Range(1, 5, ErrorMessage = "Stars must be between 1 and 5")]
        public decimal Stars { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
