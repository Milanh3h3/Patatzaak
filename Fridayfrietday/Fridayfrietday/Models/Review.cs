using System.ComponentModel.DataAnnotations;

namespace Fridayfrietday.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(40)]
        public string Name { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }
        public decimal Stars { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
