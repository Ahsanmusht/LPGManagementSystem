using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPGManagementSystem.Models
{
    [Table("Purchase")]
    public class Purchase
    {
        [Key]
        public int PurchaseId { get; set; }
        
        [Required]
        public int SupplierId { get; set; }
        
        [Required]
        public DateTime TrDate { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Qty { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        
        [ForeignKey("SupplierId")]
        public virtual Party? Supplier { get; set; }
    }
}