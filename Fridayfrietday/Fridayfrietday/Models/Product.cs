using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fridayfrietday.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(40)]
        public string Name { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; } // Navigation property for category
        [Required]
        public double Price { get; set; }
        public bool AllowsSauces { get; set; } // true voor friet, false voor dranken bv
        public string ImageLink { get; set; }
    }
}
