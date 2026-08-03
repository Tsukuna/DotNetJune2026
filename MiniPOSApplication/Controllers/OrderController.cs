using Database.AppDbContextModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniPOSApplication.Models.Order;

namespace MiniPOSApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly AppDbContext _db;

        public OrderController()
        {
            _db = new AppDbContext();
        }

        [HttpPost("create")]
        public IActionResult CreateOrder(OrderRequestModel requestDto)
        {
            if(requestDto.Items == null || !requestDto.Items.Any())
            {
                return BadRequest(new OrderResponseModel
                {
                    IsSuccess = false,
                    Message = "ProductId and Qty are required"
                });

            }

            decimal totalAmount = 0;
            var saleDetails = new List<TblSaleDetail>();
            var responseItems = new List<OrderItemsResponseModel>();

            foreach (var item in requestDto.Items) 
            {
                var existItem = _db.TblProducts.FirstOrDefault(x => x.ProductId == item.ProductId);

                if(existItem == null)
                {
                    return BadRequest(new OrderResponseModel
                    {
                        IsSuccess = false,
                        Message = $"Product with {item.ProductId} Not Exist"
                    });
                }

                if(existItem.StockQty < item.Qty)
                {

                    return BadRequest(new OrderResponseModel
                    {
                        IsSuccess = false,
                        Message = $"Product '{existItem.ProductName}' has only {existItem.StockQty} in stock."
                    });
                }

                existItem.StockQty -= item.Qty;
                var subTotal = item.Qty * existItem.Price;
                totalAmount += subTotal;

                saleDetails.Add(new TblSaleDetail
                {
                    ProductId = item.ProductId,
                    Qty = item.Qty,
                    UnitPrice = existItem.Price,
                    SubTotal = subTotal,
                    CreatedDate = DateTime.Now,
                    IsActive = true
                });

                responseItems.Add(new OrderItemsResponseModel
                {
                    ProductId = item.ProductId,
                    Qty = item.Qty
                });

           
            }

            TblSale sale = new TblSale
            {
                SaleDate = DateTime.Now,
                VoucherNumber = "VCH",
                TotalAmount = totalAmount,
                CreatedDate = DateTime.Now,
                TblSaleDetails = saleDetails
            };

            _db.TblSales.Add(sale);
            _db.SaveChanges();

            sale.VoucherNumber = $"VCH+{DateTime.Now:yyyyMMdd}+{sale.SaleId}";
            _db.SaveChanges();
            

            return Ok(new OrderResponseModel
            {
                IsSuccess = true,
                Message = "Order created successfully.",
                SaleId = sale.SaleId,
                VoucherNumber = sale.VoucherNumber,
                TotalAmount = sale.TotalAmount,
                CreatedDate = sale.CreatedDate,
                Items = responseItems
            });
        }
    }
}
