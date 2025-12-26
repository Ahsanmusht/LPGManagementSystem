using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPGManagementSystem.Models
{
    [Table("Invoice")]
    public class Invoice
    {
        [Key]
        [StringLength(50)]
        public string Id { get; set; } = string.Empty;
        
        [Required]
        public DateTime Date { get; set; }
        
        [Required]
        public int CustomerId { get; set; }
        
        [StringLength(50)]
        public string? GatePassNo { get; set; }
        
        public int TotalQty { get; set; } = 0;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalKg { get; set; } = 0;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; } = 0;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal OtherCharges { get; set; } = 0;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal GrandTotal { get; set; } = 0;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal CostPrice { get; set; } = 0;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        
        [ForeignKey("CustomerId")]
        public virtual Party? Customer { get; set; }
        
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
    }
}