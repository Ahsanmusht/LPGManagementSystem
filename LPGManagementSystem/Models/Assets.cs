using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPGManagementSystem.Models
{
    [Table("Assets")]
    public class Assets
    {
        [Key]
        public int AssetsId { get; set; }
        
        [Required]
        public DateTime PurchaseDate { get; set; }
        
        [Required]
        [StringLength(200)]
        public string AssetsName { get; set; } = string.Empty;
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        [StringLength(200)]
        public string? BrandModel { get; set; }
        
        [StringLength(100)]
        public string? SerialNo { get; set; }
        
        public bool IsInActive { get; set; } = false;
        
        public DateTime? InActiveDate { get; set; }
        
        public string? InActiveRemarks { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}