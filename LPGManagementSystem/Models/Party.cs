using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPGManagementSystem.Models
{
    [Table("Party")]
    public class Party
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int PartyTypeId { get; set; }
        
        [Required]
        [StringLength(200)]
        public string PartyName { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string? Phone { get; set; }
        
        public string? Address { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal DefaultRate { get; set; } = 0;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; } = 0;
        
        public string? Remarks { get; set; }
        
        public bool IsAffectProfitLoss { get; set; } = false;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        
        [ForeignKey("PartyTypeId")]
        public virtual PartyType? PartyType { get; set; }
        
        public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
        public virtual ICollection<PettyCash> PettyCashes { get; set; } = new List<PettyCash>();
    }
}