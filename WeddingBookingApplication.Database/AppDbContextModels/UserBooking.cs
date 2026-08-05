using System;
using System.Collections.Generic;

namespace WeddingBookingApplication.Database.AppDbContextModels;

public partial class UserBooking
{
    public int BookingId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string CustomerPhone { get; set; } = null!;

    public string? CustomerEmail { get; set; }

    public int VendorId { get; set; }

    public int VenueId { get; set; }

    public DateOnly BookingDate { get; set; }

    public int GuestCount { get; set; }

    public decimal TotalAmount { get; set; }

    public byte Status { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<BookingDecoration> BookingDecorations { get; set; } = new List<BookingDecoration>();

    public virtual ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>();

    public virtual Vendor Vendor { get; set; } = null!;

    public virtual Venue Venue { get; set; } = null!;
}
