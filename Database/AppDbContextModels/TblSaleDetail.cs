using System;
using System.Collections.Generic;

namespace Database.AppDbContextModels;

public partial class TblSaleDetail
{
    public int SaleDetailId { get; set; }

    public int SaleId { get; set; }

    public int ProductId { get; set; }

    public int Qty { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal SubTotal { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual TblProduct Product { get; set; } = null!;

    public virtual TblSale Sale { get; set; } = null!;
}
