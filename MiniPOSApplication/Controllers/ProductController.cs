using Database.AppDbContextModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniPOSApplication.Models.Product;

namespace MiniPOSApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly AppDbContext _db;

        public ProductController()
        {
            _db = new AppDbContext();
        }

        #region GetProducts
        [HttpGet]
        public IActionResult GetProducts()
        {
            List<TblProduct> products = _db.TblProducts.ToList();
            return Ok(products);
        }
        #endregion

        #region GetProductById
        [HttpGet("{id}")]

        public IActionResult GetProductById(int id)
        {
            var product = _db.TblProducts.Where(x => x.IsActive == true && x.ProductId == id).FirstOrDefault();
            return Ok(product);
        }
        #endregion

        #region CreateProduct
        [HttpPost("create")]
        public IActionResult CreateProduct(ProductCreateRequestModel requestDto)
        {
            if(requestDto.ProductName is null)
            {
                return Ok(new ProductCreateResponseModel
                {
                    Message = "Product name is required"
                });
            }

            if (requestDto.Price == 0)
            {
                return Ok(new ProductCreateResponseModel
                {
                    Message = "Product price is required"
                });
            }

            if (requestDto.StockQty == 0)
            {
                return Ok(new ProductCreateResponseModel
                {
                    Message = "Product stock is required"
                });
            }

            TblProduct product = new TblProduct
            {
                ProductName = requestDto.ProductName,
                Price = requestDto.Price,
                StockQty = requestDto.StockQty,
                IsActive = requestDto.IsActive,
                CreatedDate = DateTime.Now,
            };

            _db.TblProducts.Add(product);
            int result = _db.SaveChanges();

            ProductCreateResponseModel response = new ProductCreateResponseModel
            {
                IsSuccess = result > 0 ? true : false,
                Message = result > 0 ? "Create Success" : "Create Fail",
                ProductId = product.ProductId
            };

            return Ok(response);

        }
        #endregion

        #region UpsertProduct
        [HttpPut("upsert/{id}")]
        public IActionResult UpsertProduct(int id, ProductUpdateRequestModel requestDto)
        {
            var productEntity = _db.TblProducts.FirstOrDefault(x => x.ProductId == id);

            bool isUpdated = false;

            if (productEntity is null)
            {

                if (string.IsNullOrEmpty(requestDto.ProductName))
                {
                    return Ok(new ProductCreateResponseModel
                    {
                        Message = "Product name is required"
                    });
                }

                if (!requestDto.Price.HasValue)
                {
                    return Ok(new ProductCreateResponseModel
                    {
                        Message = "Product price is required"
                    });
                }

                if (!requestDto.StockQty.HasValue)
                {
                    return Ok(new ProductCreateResponseModel
                    {
                        Message = "Product stock is required"
                    });
                }

                if (!requestDto.IsActive.HasValue)
                {
                    return Ok(new ProductCreateResponseModel
                    {
                        Message = "IsActive is required"
                    });
                }

                TblProduct product = new TblProduct
                {
                    ProductName = requestDto.ProductName,
                    Price = requestDto.Price.Value,
                    StockQty = requestDto.StockQty.Value,
                    IsActive = requestDto.IsActive.Value,
                    CreatedDate = DateTime.Now,
                };

                _db.TblProducts.Add(product);

                int result = _db.SaveChanges();

                ProductUpdateResponseModel response = new ProductUpdateResponseModel
                {
                    IsSuccess = result > 0 ? true : false,
                    Message = result > 0 ? "Create Success" : "Create Fail"
                };

                return Ok(response);
            }

            else
            {
                if (!string.IsNullOrEmpty(requestDto.ProductName))
                {
                    productEntity.ProductName = requestDto.ProductName;
                    isUpdated = true;
                }

                if (requestDto.Price.HasValue && productEntity.Price != requestDto.Price.Value)
                {
                    productEntity.Price = requestDto.Price.Value;
                    isUpdated = true;
                }


                if (requestDto.StockQty.HasValue && productEntity.StockQty != requestDto.StockQty.Value)
                {
                    productEntity.StockQty = requestDto.StockQty.Value;
                    isUpdated = true;
                }

                if (requestDto.IsActive.HasValue && productEntity.IsActive != requestDto.IsActive.Value)
                {
                    productEntity.IsActive = requestDto.IsActive.Value;
                    isUpdated = true;
                }

                if (!isUpdated)
                {
                    return Ok(new ProductUpdateResponseModel
                    {
                        Message = "Nothing was updated"
                    });
                }

                productEntity.UpdateDate = DateTime.Now;

                int result = _db.SaveChanges();

                ProductUpdateResponseModel response = new ProductUpdateResponseModel
                {
                    IsSuccess = result > 0 ? true : false,
                    Message = result > 0 ? "Update Success" : "Update Fail"
                };

                return Ok(response);
            }
        }
        #endregion


        #region DeleteProduct
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _db.TblProducts.FirstOrDefault(x => x.ProductId == id);
            _db.TblProducts.Remove(product);
            int result = _db.SaveChanges();

            ProductDeleteResponseModel response = new ProductDeleteResponseModel
            {
                IsSuccess = result > 0 ? true : false,
                Message = result > 0 ? "Delete Success" : "Delete Fail",

            };

            return Ok(response);
        }
        #endregion


    }
}
