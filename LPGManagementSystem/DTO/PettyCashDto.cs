namespace LPGManagementSystem.DTO
{
    public class PettyCashDto
    {
        public int Id { get; set; }
        public int PartyId { get; set; }
        public string PartyName { get; set; } = string.Empty;
        public string PartyTypeName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public int PaymentType { get; set; }
        public string PaymentTypeName { get; set; } = string.Empty;
        public string? Remarks { get; set; }
        public DateTime TransactionDate { get; set; }
    }

    public class CreatePettyCashDto
    {
        public int PartyId { get; set; }
        public decimal Amount { get; set; }
        public int PaymentType { get; set; } // 1 for IN, -1 for OUT
        public string? Remarks { get; set; }
    }

    public class UpdatePettyCashDto
    {
        public int PartyId { get; set; }
        public decimal Amount { get; set; }
        public int PaymentType { get; set; }
        public string? Remarks { get; set; }
    }
}
