using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPGManagementSystem.Models
{
    [Table("PartyType")]
    public class PartyType
    {
        [Key]
        public int PartyTypeId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string PartyTypeName { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        
        public virtual ICollection<Party> Parties { get; set; } = new List<Party>();
    }
}