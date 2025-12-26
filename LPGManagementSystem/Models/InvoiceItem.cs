using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPGManagementSystem.Models
{
    [Table("InvoiceItem")]
    public class InvoiceItem
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string InvoiceId { get; set; } = string.Empty;
        
        [Required]
        public int ItemId { get; set; }
        
        public int Qty { get; set; } = 0;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalKg { get; set; } = 0;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } = 0;
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        [ForeignKey("InvoiceId")]
        public virtual Invoice? Invoice { get; set; }
        
        [ForeignKey("ItemId")]
        public virtual Item? Item { get; set; }
    }
}