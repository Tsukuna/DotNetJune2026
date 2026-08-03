using Database.AppDbContextModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniPOSApplication.Models.Sale;

namespace MiniPOSApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly AppDbContext _db;
        public SaleController()
        {
            _db = new AppDbContext();
        }

        [HttpGet()]
        public IActionResult GetSaleList()
        {
            var list = _db.TblSales.ToList();
            return Ok(list);
        }

        [HttpGet("detail/{id}")]
        public IActionResult GetSaleDetail(int id)
        {

            var sale = _db.TblSales.Include(x => x.TblSaleDetails).FirstOrDefault(x => x.SaleId == id);

            if(sale == null)
            {
                return BadRequest("Sale not found");
            }

            List<SaleDetailResponseModel> saleDetails = sale.TblSaleDetails.Where(x => x.IsActive)
                .Select(d => new SaleDetailResponseModel
                {
                    SaleDetailId = d.SaleDetailId,
                    ProductId = d.ProductId,
                    ProductName = d.Product != null ? d.Product.ProductName : string.Empty,
                    Qty = d.Qty,
                    UnitPrice = d.UnitPrice,
                    SubTotal = d.SubTotal
                })
                .ToList();

            return Ok(new SaleResponseModel
            {
                SaleId = sale.SaleId,
                VoucherNumber = sale.VoucherNumber,
                SaleDate = sale.SaleDate,
                TotalAmount = sale.TotalAmount,
                SaleDetailList = saleDetails
            });

        }
    }
}
