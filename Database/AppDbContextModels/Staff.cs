using System;
using System.Collections.Generic;

namespace Database.AppDbContextModels;

public partial class Staff
{
    public int StaffId { get; set; }

    public string StaffName { get; set; } = null!;

    public string? Address { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string? Gender { get; set; }
}
