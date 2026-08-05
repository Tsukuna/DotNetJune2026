using System;
using System.Collections.Generic;

namespace WeddingBookingApplication.Database.AppDbContextModels;

public partial class Vendor
{
    public int VendorId { get; set; }

    public string VendorName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Address { get; set; }

    public string? Description { get; set; }

    public byte Status { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<DecorationPackage> DecorationPackages { get; set; } = new List<DecorationPackage>();

    public virtual ICollection<ServicePackage> ServicePackages { get; set; } = new List<ServicePackage>();

    public virtual ICollection<UserBooking> UserBookings { get; set; } = new List<UserBooking>();

    public virtual ICollection<Venue> Venues { get; set; } = new List<Venue>();
}
