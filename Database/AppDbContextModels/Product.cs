using System;
using System.Collections.Generic;

namespace Database.AppDbContextModels;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string ProductType { get; set; } = null!;

    public string Category { get; set; } = null!;

    public bool IsActive { get; set; }
}
