using System;
using System.Collections.Generic;

namespace WeddingBookingApplication.Database.AppDbContextModels;

public partial class BookingService
{
    public int BookingServiceId { get; set; }

    public int BookingId { get; set; }

    public int ServicePackageId { get; set; }

    public virtual UserBooking Booking { get; set; } = null!;

    public virtual ServicePackage ServicePackage { get; set; } = null!;
}
