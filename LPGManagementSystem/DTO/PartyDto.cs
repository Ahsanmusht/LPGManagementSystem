namespace LPGManagementSystem.DTO
{
    public class PartyDto
    {
        public int Id { get; set; }
        public int PartyTypeId { get; set; }
        public string PartyTypeName { get; set; } = string.Empty;
        public string PartyName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public decimal DefaultRate { get; set; }
        public decimal Balance { get; set; }
        public string? Remarks { get; set; }
        public bool IsAffectProfitLoss { get; set; }
    }

    public class CreatePartyDto
    {
        public int PartyTypeId { get; set; }
        public string PartyName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public decimal DefaultRate { get; set; }
        public decimal Balance { get; set; }
        public string? Remarks { get; set; }
        public bool IsAffectProfitLoss { get; set; }
    }

    public class UpdatePartyDto
    {
        public int PartyTypeId { get; set; }
        public string PartyName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public decimal DefaultRate { get; set; }
        public decimal Balance { get; set; }
        public string? Remarks { get; set; }
        public bool IsAffectProfitLoss { get; set; }
    }
}