using System;
using System.Collections.Generic;

namespace WeddingBookingApplication.Database.AppDbContextModels;

public partial class BookingDecoration
{
    public int BookingDecorationId { get; set; }

    public int BookingId { get; set; }

    public int DecorationPackageId { get; set; }

    public virtual UserBooking Booking { get; set; } = null!;

    public virtual DecorationPackage DecorationPackage { get; set; } = null!;
}
