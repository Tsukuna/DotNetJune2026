namespace MiniPOSApplication.Models.Order
{
    public class OrderRequestModel
    {
        public List<OrderItemsRequestModel> Items { get; set; }
    }

    public class OrderItemsRequestModel
    {
        public int ProductId { get; set; }
        public int Qty { get; set; }
    }
}
