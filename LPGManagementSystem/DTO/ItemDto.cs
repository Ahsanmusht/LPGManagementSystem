namespace LPGManagementSystem.DTO
{
    public class ItemDto
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public decimal Weight { get; set; }
        public decimal AvailableStock { get; set; }
        public decimal CostPrice { get; set; }
        public bool IsPrimary { get; set; }
    }

    public class CreateItemDto
    {
        public string ItemName { get; set; } = string.Empty;
        public decimal Weight { get; set; }
        public decimal AvailableStock { get; set; }
        public decimal CostPrice { get; set; }
        public bool IsPrimary { get; set; }
    }

    public class UpdateItemDto
    {
        public string ItemName { get; set; } = string.Empty;
        public decimal Weight { get; set; }
        public decimal AvailableStock { get; set; }
        public decimal CostPrice { get; set; }
        public bool IsPrimary { get; set; }
    }
}