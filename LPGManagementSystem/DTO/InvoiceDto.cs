namespace LPGManagementSystem.DTO
{
    public class InvoiceDto
    {
        public string Id { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string? CustomerPhone { get; set; }
        public string? GatePassNo { get; set; }
        public int TotalQty { get; set; }
        public decimal TotalKg { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal OtherCharges { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal CostPrice { get; set; }
        public List<InvoiceItemDto> Items { get; set; } = new();
    }

    public class InvoiceItemDto
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public int Qty { get; set; }
        public decimal TotalKg { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
    }

    public class CreateInvoiceDto
    {
        public string? Id { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public string? GatePassNo { get; set; }
        public decimal OtherCharges { get; set; }
        public List<CreateInvoiceItemDto> Items { get; set; } = new();
    }

    public class CreateInvoiceItemDto
    {
        public int ItemId { get; set; }
        public int Qty { get; set; }
        public decimal TotalKg { get; set; }
        public decimal Price { get; set; }
    }

    public class UpdateInvoiceDto
    {
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public string? GatePassNo { get; set; }
        public decimal OtherCharges { get; set; }
        public List<CreateInvoiceItemDto> Items { get; set; } = new();
    }
}
