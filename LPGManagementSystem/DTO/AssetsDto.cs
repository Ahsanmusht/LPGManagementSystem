namespace LPGManagementSystem.DTO
{
    public class AssetsDto
    {
        public int AssetsId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string AssetsName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? BrandModel { get; set; }
        public string? SerialNo { get; set; }
        public bool IsInActive { get; set; }
        public DateTime? InActiveDate { get; set; }
        public string? InActiveRemarks { get; set; }
    }

    public class CreateAssetsDto
    {
        public DateTime PurchaseDate { get; set; }
        public string AssetsName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? BrandModel { get; set; }
        public string? SerialNo { get; set; }
    }

    public class UpdateAssetsDto
    {
        public DateTime PurchaseDate { get; set; }
        public string AssetsName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? BrandModel { get; set; }
        public string? SerialNo { get; set; }
        public bool IsInActive { get; set; }
        public DateTime? InActiveDate { get; set; }
        public string? InActiveRemarks { get; set; }
    }
}