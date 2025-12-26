using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPGManagementSystem.Models
{
    [Table("Item")]
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        
        [Required]
        [StringLength(200)]
        public string ItemName { get; set; } = string.Empty;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Weight { get; set; } = 0;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal AvailableStock { get; set; } = 0;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal CostPrice { get; set; } = 0;
        
        public bool IsPrimary { get; set; } = false;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
    }
}
