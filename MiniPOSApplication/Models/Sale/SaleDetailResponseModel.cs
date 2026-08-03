namespace MiniPOSApplication.Models.Sale
{

    public class SaleResponseModel
    {
        public int SaleId { get; set; }
        public string VoucherNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }

        public List<SaleDetailResponseModel> SaleDetailList { get; set;  }

    }
    public class SaleDetailResponseModel
    {
        public int SaleDetailId{ get; set; }
        public int ProductId{ get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }
    }
}
