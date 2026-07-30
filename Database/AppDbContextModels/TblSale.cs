using System;
using System.Collections.Generic;

namespace Database.AppDbContextModels;

public partial class TblSale
{
    public int SaleId { get; set; }

    public DateTime SaleDate { get; set; }

    public string VoucherNumber { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<TblSaleDetail> TblSaleDetails { get; set; } = new List<TblSaleDetail>();
}
