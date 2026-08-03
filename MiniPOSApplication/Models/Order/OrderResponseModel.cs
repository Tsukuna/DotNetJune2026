namespace MiniPOSApplication.Models.Order
{
    public class OrderResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public int SaleId { get; set; }
        public string VoucherNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemsResponseModel> Items { get; set; }

    }

    public class OrderItemsResponseModel
    {
        public int ProductId { get; set; }
        public int Qty { get; set; }
    }
}
