using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SpendTracker.Models
{
    public class Expenses
    {
        [Key]
        public int Id { get; set; }
        public decimal Expense { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}
