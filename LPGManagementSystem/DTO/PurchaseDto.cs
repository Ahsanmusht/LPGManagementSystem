namespace LPGManagementSystem.DTO
{
    public class PurchaseDto
    {
        public int PurchaseId { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; } = string.Empty;
        public DateTime TrDate { get; set; }
        public decimal Price { get; set; }
        public decimal Qty { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class CreatePurchaseDto
    {
        public int SupplierId { get; set; }
        public DateTime TrDate { get; set; }
        public decimal Price { get; set; }
        public decimal Qty { get; set; }
    }

    public class UpdatePurchaseDto
    {
        public int SupplierId { get; set; }
        public DateTime TrDate { get; set; }
        public decimal Price { get; set; }
        public decimal Qty { get; set; }
    }
}