namespace MiniPOSApplication.Models.Product
{
    public class ProductUpdateRequestModel
    {
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public int? StockQty { get; set; }
        public bool? IsActive { get; set; }
    }
}
