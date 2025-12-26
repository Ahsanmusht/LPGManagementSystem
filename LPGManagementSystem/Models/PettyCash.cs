using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPGManagementSystem.Models
{
    [Table("PettyCash")]
    public class PettyCash
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int PartyId { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        
        [Required]
        public int PaymentType { get; set; } // 1 for IN, -1 for OUT
        
        public string? Remarks { get; set; }
        
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        [ForeignKey("PartyId")]
        public virtual Party? Party { get; set; }
    }
}