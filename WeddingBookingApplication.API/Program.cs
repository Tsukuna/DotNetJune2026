using Database.AppDbContextModels;
using Microsoft.EntityFrameworkCore;
using WeddingBookingApplication.Domain.Features.Decoration;
using WeddingBookingApplication.Domain.Features.ServicePackage;
using WeddingBookingApplication.Domain.Features.Vendor;
using WeddingBookingApplication.Domain.Features.Venue;

var builder = WebApplication.CreateBuilder(args);

// ── Database ────────────────────────────────────────────────────────────────
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ── Domain Services ─────────────────────────────────────────────────────────
builder.Services.AddScoped<VendorService>();
builder.Services.AddScoped<VenueService>();
builder.Services.AddScoped<DecorationService>();
builder.Services.AddScoped<ServicePackageService>();

// ── ASP.NET Core ─────────────────────────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ── Pipeline ─────────────────────────────────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
