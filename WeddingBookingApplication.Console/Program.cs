using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeddingBookingApplication.Domain.Models.Decoration;
using WeddingBookingApplication.Domain.Models.Service;
using WeddingBookingApplication.Domain.Models.Vendor;
using WeddingBookingApplication.Domain.Models.Venue;

namespace WeddingBookingApplication.ConsoleApp;

public class Program
{
    private static readonly HttpClient client = new HttpClient();
    private static readonly string BaseUrl = "https://localhost:7277/";

    public static async Task Main(string[] args)
    {
        Console.Title = "Wedding Booking System";
        
        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("    WEDDING BOOKING SYSTEM MAIN MENU    ");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Manage Vendors");
            Console.WriteLine("2. Manage Venues");
            Console.WriteLine("3. Manage Decoration Packages");
            Console.WriteLine("4. Manage Service Packages");
            Console.WriteLine("0. Exit");
            Console.WriteLine("========================================");
            Console.Write("Select option: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    await ManageVendors();
                    break;
                case "2":
                    await ManageVenues();
                    break;
                case "3":
                    await ManageDecorationPackages();
                    break;
                case "4":
                    await ManageServicePackages();
                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Press any key to try again.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    #region Vendor Management
    private static async Task ManageVendors()
    {
        bool back = false;
        while (!back)
        {
            Console.Clear();
            Console.WriteLine("--- VENDOR MANAGEMENT ---");
            Console.WriteLine("1. List All Active Vendors");
            Console.WriteLine("2. Get Vendor By ID");
            Console.WriteLine("3. Create New Vendor");
            Console.WriteLine("4. Update Vendor");
            Console.WriteLine("5. Delete Vendor");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("Select option: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    await ListVendors();
                    break;
                case "2":
                    await GetVendorById();
                    break;
                case "3":
                    await CreateVendor();
                    break;
                case "4":
                    await UpdateVendor();
                    break;
                case "5":
                    await DeleteVendor();
                    break;
                case "0":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Press any key...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private static async Task ListVendors()
    {
        Console.Clear();
        Console.WriteLine("--- ACTIVE VENDORS ---");
        try
        {
            var response = await client.GetAsync(BaseUrl + "api/Vendor");
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<VendorResponseModel>>(jsonString);
                
                if (list == null || list.Count == 0)
                {
                    Console.WriteLine("No active vendors found.");
                }
                else
                {
                    foreach (var vendor in list)
                    {
                        Console.WriteLine($"ID: {vendor.VendorId} | Name: {vendor.VendorName} | Email: {vendor.Email} | Phone: {vendor.Phone} | Status: {vendor.Status}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Failed to fetch vendors. Status Code: " + response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private static async Task GetVendorById()
    {
        Console.Clear();
        Console.WriteLine("--- GET VENDOR BY ID ---");
        Console.Write("Enter Vendor ID: ");
        string idStr = Console.ReadLine() ?? "";
        int id = Convert.ToInt32(idStr);
        
        try
        {
            var response = await client.GetAsync(BaseUrl + "api/Vendor/" + id);
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                var v = JsonConvert.DeserializeObject<VendorResponseModel>(jsonString);
                
                if (v != null)
                {
                    Console.WriteLine($"ID: {v.VendorId}");
                    Console.WriteLine($"Name: {v.VendorName}");
                    Console.WriteLine($"Email: {v.Email}");
                    Console.WriteLine($"Phone: {v.Phone}");
                    Console.WriteLine($"Address: {v.Address}");
                    Console.WriteLine($"Description: {v.Description}");
                    Console.WriteLine($"Status: {v.Status}");
                    Console.WriteLine($"Created Date: {v.CreatedDate}");
                }
            }
            else
            {
                Console.WriteLine("Vendor not found. Status Code: " + response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private static async Task CreateVendor()
    {
        Console.Clear();
        Console.WriteLine("--- CREATE VENDOR ---");
        
        var requestModel = new VendorCreateRequestModel();
        Console.Write("Enter Vendor Name: ");
        requestModel.VendorName = Console.ReadLine() ?? "";
        Console.Write("Enter Email: ");
        requestModel.Email = Console.ReadLine() ?? "";
        Console.Write("Enter Phone: ");
        requestModel.Phone = Console.ReadLine() ?? "";
        Console.Write("Enter Address: ");
        requestModel.Address = Console.ReadLine();
        Console.Write("Enter Description: ");
        requestModel.Description = Console.ReadLine();

        Console.Write("Enter Status (1 = Active): ");
        byte.TryParse(Console.ReadLine(), out byte status);
        requestModel.Status = status;

        try
        {
            string jsonBody = JsonConvert.SerializeObject(requestModel);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            
            var response = await client.PostAsync(BaseUrl + "api/Vendor", content);
            string responseString = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<VendorCreateResponseModel>(responseString);

            if (res != null)
            {
                Console.WriteLine($"Success: {res.IsSuccess} | Message: {res.Message} | VendorId: {res.VendorId}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private static async Task UpdateVendor()
    {
        Console.Clear();
        Console.WriteLine("--- UPDATE VENDOR ---");
        Console.Write("Enter Vendor ID to update: ");
        string idStr = Console.ReadLine() ?? "";
        int id = Convert.ToInt32(idStr);

        try
        {
            var getResponse = await client.GetAsync(BaseUrl + "api/Vendor/" + id);
            if (!getResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Vendor not found.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                return;
            }

            string existingJson = await getResponse.Content.ReadAsStringAsync();
            var existing = JsonConvert.DeserializeObject<VendorResponseModel>(existingJson);

            if (existing == null) return;

            var requestModel = new VendorUpdateRequestModel();
            
            Console.Write($"Enter Name [{existing.VendorName}]: ");
            string name = Console.ReadLine() ?? "";
            requestModel.VendorName = string.IsNullOrWhiteSpace(name) ? existing.VendorName : name;

            Console.Write($"Enter Email [{existing.Email}]: ");
            string email = Console.ReadLine() ?? "";
            requestModel.Email = string.IsNullOrWhiteSpace(email) ? existing.Email : email;

            Console.Write($"Enter Phone [{existing.Phone}]: ");
            string phone = Console.ReadLine() ?? "";
            requestModel.Phone = string.IsNullOrWhiteSpace(phone) ? existing.Phone : phone;

            Console.Write($"Enter Address [{existing.Address}]: ");
            string address = Console.ReadLine() ?? "";
            requestModel.Address = string.IsNullOrWhiteSpace(address) ? existing.Address : address;

            Console.Write($"Enter Description [{existing.Description}]: ");
            string desc = Console.ReadLine() ?? "";
            requestModel.Description = string.IsNullOrWhiteSpace(desc) ? existing.Description : desc;

            Console.Write($"Enter Status [{existing.Status}]: ");
            string statusStr = Console.ReadLine() ?? "";
            requestModel.Status = string.IsNullOrWhiteSpace(statusStr) ? existing.Status : byte.Parse(statusStr);

            string jsonBody = JsonConvert.SerializeObject(requestModel);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(BaseUrl + "api/Vendor/" + id, content);
            string responseString = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<VendorUpdateResponseModel>(responseString);

            if (res != null)
            {
                Console.WriteLine($"Success: {res.IsSuccess} | Message: {res.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private static async Task DeleteVendor()
    {
        Console.Clear();
        Console.WriteLine("--- DELETE VENDOR ---");
        Console.Write("Enter Vendor ID to delete: ");
        string idStr = Console.ReadLine() ?? "";
        int id = Convert.ToInt32(idStr);

        try
        {
            var response = await client.DeleteAsync(BaseUrl + "api/Vendor/" + id);
            string responseString = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<VendorDeleteResponseModel>(responseString);

            if (res != null)
            {
                Console.WriteLine($"Success: {res.IsSuccess} | Message: {res.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
    #endregion

    #region Venue Management
    private static async Task ManageVenues()
    {
        bool back = false;
        while (!back)
        {
            Console.Clear();
            Console.WriteLine("--- VENUE MANAGEMENT ---");
            Console.WriteLine("1. List All Active Venues");
            Console.WriteLine("2. Get Venue By ID");
            Console.WriteLine("3. Create New Venue");
            Console.WriteLine("4. Update Venue");
            Console.WriteLine("5. Delete Venue");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("Select option: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    await ListVenues();
                    break;
                case "2":
                    await GetVenueById();
                    break;
                case "3":
                    await CreateVenue();
                    break;
                case "4":
                    await UpdateVenue();
                    break;
                case "5":
                    await DeleteVenue();
                    break;
                case "0":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Press any key...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private static async Task ListVenues()
    {
        Console.Clear();
        Console.WriteLine("--- ACTIVE VENUES ---");
        try
        {
            var response = await client.GetAsync(BaseUrl + "api/Venue");
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<VenueResponseModel>>(jsonString);
                
                if (list == null || list.Count == 0)
                {
                    Console.WriteLine("No active venues found.");
                }
                else
                {
                    foreach (var v in list)
                    {
                        Console.WriteLine($"ID: {v.VenueId} | Name: {v.VenueName} | Location: {v.Location} | Capacity: {v.Capacity} | Price: {v.Price:C}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Failed. Status Code: " + response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private static async Task GetVenueById()
    {
        Console.Clear();
        Console.WriteLine("--- GET VENUE BY ID ---");
        Console.Write("Enter Venue ID: ");
        string idStr = Console.ReadLine() ?? "";
        int id = Convert.ToInt32(idStr);

        try
        {
            var response = await client.GetAsync(BaseUrl + "api/Venue/" + id);
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                var v = JsonConvert.DeserializeObject<VenueResponseModel>(jsonString);
                
                if (v != null)
                {
                    Console.WriteLine($"ID: {v.VenueId}");
                    Console.WriteLine($"Vendor ID: {v.VendorId}");
                    Console.WriteLine($"Name: {v.VenueName}");
                    Console.WriteLine($"Location: {v.Location}");
                    Console.WriteLine($"Capacity: {v.Capacity}");
                    Console.WriteLine($"Price: {v.Price:C}");
                    Console.WriteLine($"Description: {v.Description}");
                    Console.WriteLine($"Is Active: {v.IsActive}");
                    Console.WriteLine($"Created Date: {v.CreatedDate}");
                }
            }
            else
            {
                Console.WriteLine("Venue not found. Status Code: " + response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private static async Task CreateVenue()
    {
        Console.Clear();
        Console.WriteLine("--- CREATE VENUE ---");
        
        var requestModel = new VenueCreateRequestModel();
        Console.Write("Enter Venue Name: ");
        requestModel.VenueName = Console.ReadLine() ?? "";
        Console.Write("Enter Location: ");
        requestModel.Location = Console.ReadLine() ?? "";
        Console.Write("Enter Capacity: ");
        int.TryParse(Console.ReadLine(), out int capacity);
        requestModel.Capacity = capacity;
        Console.Write("Enter Price: ");
        decimal.TryParse(Console.ReadLine(), out decimal price);
        requestModel.Price = price;
        Console.Write("Enter Description: ");
        requestModel.Description = Console.ReadLine();
        Console.Write("Is Active? (true/false): ");
        bool.TryParse(Console.ReadLine(), out bool isActive);
        requestModel.IsActive = isActive;

        try
        {
            string jsonBody = JsonConvert.SerializeObject(requestModel);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            
            var response = await client.PostAsync(BaseUrl + "api/Venue", content);
            string responseString = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<VenueCreateResponseModel>(responseString);

            if (res != null)
            {
                Console.WriteLine($"Success: {res.IsSuccess} | Message: {res.Message} | VenueId: {res.VenueId}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private static async Task UpdateVenue()
    {
        Console.Clear();
        Console.WriteLine("--- UPDATE VENUE ---");
        Console.Write("Enter Venue ID to update: ");
        string idStr = Console.ReadLine() ?? "";
        int id = Convert.ToInt32(idStr);


        try
        {
            var getResponse = await client.GetAsync(BaseUrl + "api/Venue/" + id);
            if (!getResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Venue not found.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                return;
            }

            string existingJson = await getResponse.Content.ReadAsStringAsync();
            var existing = JsonConvert.DeserializeObject<VenueResponseModel>(existingJson);

            if (existing == null) return;

            var requestModel = new VenueUpdateRequestModel();
            
            Console.Write($"Enter Name [{existing.VenueName}]: ");
            string name = Console.ReadLine() ?? "";
            requestModel.VenueName = string.IsNullOrWhiteSpace(name) ? existing.VenueName : name;

            Console.Write($"Enter Location [{existing.Location}]: ");
            string loc = Console.ReadLine() ?? "";
            requestModel.Location = string.IsNullOrWhiteSpace(loc) ? existing.Location : loc;

            Console.Write($"Enter Capacity [{existing.Capacity}]: ");
            string cap = Console.ReadLine() ?? "";
            requestModel.Capacity = string.IsNullOrWhiteSpace(cap) ? existing.Capacity : int.Parse(cap);

            Console.Write($"Enter Price [{existing.Price}]: ");
            string priceStr = Console.ReadLine() ?? "";
            requestModel.Price = string.IsNullOrWhiteSpace(priceStr) ? existing.Price : decimal.Parse(priceStr);

            Console.Write($"Enter Description [{existing.Description}]: ");
            string desc = Console.ReadLine() ?? "";
            requestModel.Description = string.IsNullOrWhiteSpace(desc) ? existing.Description : desc;

            Console.Write($"Enter Is Active [{existing.IsActive}]: ");
            string activeStr = Console.ReadLine() ?? "";
            requestModel.IsActive = string.IsNullOrWhiteSpace(activeStr) ? existing.IsActive : bool.Parse(activeStr);

            string jsonBody = JsonConvert.SerializeObject(requestModel);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(BaseUrl + "api/Venue/" + id, content);
            string responseString = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<VenueUpdateResponseModel>(responseString);

            if (res != null)
            {
                Console.WriteLine($"Success: {res.IsSuccess} | Message: {res.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private static async Task DeleteVenue()
    {
        Console.Clear();
        Console.WriteLine("--- DELETE VENUE ---");
        Console.Write("Enter Venue ID to delete: ");
        string idStr = Console.ReadLine() ?? "";
        int id = Convert.ToInt32(idStr);

        try
        {
            var response = await client.DeleteAsync(BaseUrl + "api/Venue/" + id);
            string responseString = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<VenueDeleteResponseModel>(responseString);

            if (res != null)
            {
                Console.WriteLine($"Success: {res.IsSuccess} | Message: {res.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
    #endregion

    #region Decoration Package Management
    private static async Task ManageDecorationPackages()
    {
        bool back = false;
        while (!back)
        {
            Console.Clear();
            Console.WriteLine("--- DECORATION PACKAGE MANAGEMENT ---");
            Console.WriteLine("1. List All Active Decoration Packages");
            Console.WriteLine("2. Get Decoration Package By ID");
            Console.WriteLine("3. Create New Decoration Package");
            Console.WriteLine("4. Update Decoration Package");
            Console.WriteLine("5. Delete Decoration Package");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("Select option: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    await ListDecorationPackages();
                    break;
                case "2":
                    await GetDecorationPackageById();
                    break;
                case "3":
                    await CreateDecorationPackage();
                    break;
                case "4":
                    await UpdateDecorationPackage();
                    break;
                case "5":
                    await DeleteDecorationPackage();
                    break;
                case "0":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Press any key...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private static async Task ListDecorationPackages()
    {
        Console.Clear();
        Console.WriteLine("--- ACTIVE DECORATION PACKAGES ---");
        try
        {
            var response = await client.GetAsync(BaseUrl + "api/DecorationPackage");
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<DecorationResponseModel>>(jsonString);
                
                if (list == null || list.Count == 0)
                {
                    Console.WriteLine("No active decoration packages found.");
                }
                else
                {
                    foreach (var decorationPackage in list)
                    {
                        Console.WriteLine($"ID: {decorationPackage.DecorationPackageId} | VendorID: {decorationPackage.VendorId} | Name: {decorationPackage.PackageName} | Price: {decorationPackage.Price:C} | Active: {decorationPackage.IsActive}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Failed. Status Code: " + response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private static async Task GetDecorationPackageById()
    {
        Console.Clear();
        Console.WriteLine("--- GET DECORATION PACKAGE BY ID ---");
        Console.Write("Enter Decoration Package ID: ");
        string idStr = Console.ReadLine() ?? "";
        int id = Convert.ToInt32(idStr);


        try
        {
            var response = await client.GetAsync(BaseUrl + "api/DecorationPackage/" + id);
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                var decorationPackage = JsonConvert.DeserializeObject<DecorationResponseModel>(jsonString);
                
                if (decorationPackage != null)
                {
                    Console.WriteLine($"ID: {decorationPackage.DecorationPackageId}");
                    Console.WriteLine($"Vendor ID: {decorationPackage.VendorId}");
                    Console.WriteLine($"Package Name: {decorationPackage.PackageName}");
                    Console.WriteLine($"Price: {decorationPackage.Price:C}");
                    Console.WriteLine($"Description: {decorationPackage.Description}");
                    Console.WriteLine($"Is Active: {decorationPackage.IsActive}");
                    Console.WriteLine($"Created Date: {decorationPackage.CreatedDate}");
                }
            }
            else
            {
                Console.WriteLine("Package not found. Status Code: " + response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private static async Task CreateDecorationPackage()
    {
        Console.Clear();
        Console.WriteLine("--- CREATE DECORATION PACKAGE ---");
        
        var requestModel = new DecorationCreateRequestModel();
        Console.Write("Enter Vendor ID: ");
        int.TryParse(Console.ReadLine(), out int vendorId);
        requestModel.VendorId = vendorId;
        Console.Write("Enter Package Name: ");
        requestModel.PackageName = Console.ReadLine() ?? "";
        Console.Write("Enter Price: ");
        decimal.TryParse(Console.ReadLine(), out decimal price);
        requestModel.Price = price;
        Console.Write("Enter Description: ");
        requestModel.Description = Console.ReadLine();
        Console.Write("Is Active? (true/false): ");
        bool.TryParse(Console.ReadLine(), out bool isActive);
        requestModel.IsActive = isActive;

        try
        {
            string jsonBody = JsonConvert.SerializeObject(requestModel);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            
            var response = await client.PostAsync(BaseUrl + "api/DecorationPackage", content);
            string responseString = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<DecorationCreateResponseModel>(responseString);

            if (res != null)
            {
                Console.WriteLine($"Success: {res.IsSuccess} | Message: {res.Message} | PackageId: {res.DecorationPackageId}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private static async Task UpdateDecorationPackage()
    {
        Console.Clear();
        Console.WriteLine("--- UPDATE DECORATION PACKAGE ---");
        Console.Write("Enter Decoration Package ID to update: ");
        string idStr = Console.ReadLine() ?? "";
        int id = Convert.ToInt32(idStr);


        try
        {
            var getResponse = await client.GetAsync(BaseUrl + "api/DecorationPackage/" + id);
            if (!getResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Package not found.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                return;
            }

            string existingJson = await getResponse.Content.ReadAsStringAsync();
            var existing = JsonConvert.DeserializeObject<DecorationResponseModel>(existingJson);

            if (existing == null) return;

            var requestModel = new DecorationUpdateRequestModel();
            
            Console.Write($"Enter Vendor ID [{existing.VendorId}]: ");
            string vendorStr = Console.ReadLine() ?? "";
            requestModel.VendorId = string.IsNullOrWhiteSpace(vendorStr) ? existing.VendorId : int.Parse(vendorStr);

            Console.Write($"Enter Package Name [{existing.PackageName}]: ");
            string name = Console.ReadLine() ?? "";
            requestModel.PackageName = string.IsNullOrWhiteSpace(name) ? existing.PackageName : name;

            Console.Write($"Enter Price [{existing.Price}]: ");
            string priceStr = Console.ReadLine() ?? "";
            requestModel.Price = string.IsNullOrWhiteSpace(priceStr) ? existing.Price : decimal.Parse(priceStr);

            Console.Write($"Enter Description [{existing.Description}]: ");
            string desc = Console.ReadLine() ?? "";
            requestModel.Description = string.IsNullOrWhiteSpace(desc) ? existing.Description : desc;

            Console.Write($"Enter Is Active [{existing.IsActive}]: ");
            string activeStr = Console.ReadLine() ?? "";
            requestModel.IsActive = string.IsNullOrWhiteSpace(activeStr) ? existing.IsActive : bool.Parse(activeStr);

            string jsonBody = JsonConvert.SerializeObject(requestModel);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(BaseUrl + "api/DecorationPackage/" + id, content);
            string responseString = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<DecorationUpdateResponseModel>(responseString);

            if (res != null)
            {
                Console.WriteLine($"Success: {res.IsSuccess} | Message: {res.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private static async Task DeleteDecorationPackage()
    {
        Console.Clear();
        Console.WriteLine("--- DELETE DECORATION PACKAGE ---");
        Console.Write("Enter Decoration Package ID to delete: ");
        string idStr = Console.ReadLine() ?? "";
        int id = Convert.ToInt32(idStr);


        try
        {
            var response = await client.DeleteAsync(BaseUrl + "api/DecorationPackage/" + id);
            string responseString = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<DecorationDeleteResponseModel>(responseString);

            if (res != null)
            {
                Console.WriteLine($"Success: {res.IsSuccess} | Message: {res.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
    #endregion

    #region Service Package Management
    private static async Task ManageServicePackages()
    {
        bool back = false;
        while (!back)
        {
            Console.Clear();
            Console.WriteLine("--- SERVICE PACKAGE MANAGEMENT ---");
            Console.WriteLine("1. List All Active Service Packages");
            Console.WriteLine("2. Get Service Package By ID");
            Console.WriteLine("3. Create New Service Package");
            Console.WriteLine("4. Update Service Package");
            Console.WriteLine("5. Delete Service Package");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("Select option: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    await ListServicePackages();
                    break;
                case "2":
                    await GetServicePackageById();
                    break;
                case "3":
                    await CreateServicePackage();
                    break;
                case "4":
                    await UpdateServicePackage();
                    break;
                case "5":
                    await DeleteServicePackage();
                    break;
                case "0":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Press any key...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private static async Task ListServicePackages()
    {
        Console.Clear();
        Console.WriteLine("--- ACTIVE SERVICE PACKAGES ---");
        try
        {
            var response = await client.GetAsync(BaseUrl + "api/ServicePackage");
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<ServiceResponseModel>>(jsonString);
                
                if (list == null || list.Count == 0)
                {
                    Console.WriteLine("No active service packages found.");
                }
                else
                {
                    foreach (var servicePackage in list)
                    {
                        Console.WriteLine($"ID: {servicePackage.ServicePackageId} | VendorID: {servicePackage.VendorId} | Name: {servicePackage.PackageName} | Price: {servicePackage.Price:C} | Active: {servicePackage.IsActive}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Failed. Status Code: " + response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private static async Task GetServicePackageById()
    {
        Console.Clear();
        Console.WriteLine("--- GET SERVICE PACKAGE BY ID ---");
        Console.Write("Enter Service Package ID: ");
        string idStr = Console.ReadLine() ?? "";
        int id = Convert.ToInt32(idStr);


        try
        {
            var response = await client.GetAsync(BaseUrl + "api/ServicePackage/" + id);
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                var servicePackage = JsonConvert.DeserializeObject<ServiceResponseModel>(jsonString);
                
                if (servicePackage != null)
                {
                    Console.WriteLine($"ID: {servicePackage.ServicePackageId}");
                    Console.WriteLine($"Vendor ID: {servicePackage.VendorId}");
                    Console.WriteLine($"Package Name: {servicePackage.PackageName}");
                    Console.WriteLine($"Price: {servicePackage.Price:C}");
                    Console.WriteLine($"Description: {servicePackage.Description}");
                    Console.WriteLine($"Is Active: {servicePackage.IsActive}");
                    Console.WriteLine($"Created Date: {servicePackage.CreatedDate}");
                }
            }
            else
            {
                Console.WriteLine("Package not found. Status Code: " + response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private static async Task CreateServicePackage()
    {
        Console.Clear();
        Console.WriteLine("--- CREATE SERVICE PACKAGE ---");
        
        var requestModel = new ServiceCreateRequestModel();
        Console.Write("Enter Vendor ID: ");
        int.TryParse(Console.ReadLine(), out int vendorId);
        requestModel.VendorId = vendorId;
        Console.Write("Enter Package Name: ");
        requestModel.PackageName = Console.ReadLine() ?? "";
        Console.Write("Enter Price: ");
        decimal.TryParse(Console.ReadLine(), out decimal price);
        requestModel.Price = price;
        Console.Write("Enter Description: ");
        requestModel.Description = Console.ReadLine();
        Console.Write("Is Active? (true/false): ");
        bool.TryParse(Console.ReadLine(), out bool isActive);
        requestModel.IsActive = isActive;

        try
        {
            string jsonBody = JsonConvert.SerializeObject(requestModel);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            
            var response = await client.PostAsync(BaseUrl + "api/ServicePackage", content);
            string responseString = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<ServiceCreateResponseModel>(responseString);

            if (res != null)
            {
                Console.WriteLine($"Success: {res.IsSuccess} | Message: {res.Message} | PackageId: {res.ServicePackageId}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private static async Task UpdateServicePackage()
    {
        Console.Clear();
        Console.WriteLine("--- UPDATE SERVICE PACKAGE ---");
        Console.Write("Enter Service Package ID to update: ");
        string idStr = Console.ReadLine() ?? "";
        int id = Convert.ToInt32(idStr);


        try
        {
            var getResponse = await client.GetAsync(BaseUrl + "api/ServicePackage/" + id);
            if (!getResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Package not found.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                return;
            }

            string existingJson = await getResponse.Content.ReadAsStringAsync();
            var existing = JsonConvert.DeserializeObject<ServiceResponseModel>(existingJson);

            if (existing == null) return;

            var requestModel = new ServiceUpdateRequestModel();
            
            Console.Write($"Enter Vendor ID [{existing.VendorId}]: ");
            string vendorStr = Console.ReadLine() ?? "";
            requestModel.VendorId = string.IsNullOrWhiteSpace(vendorStr) ? existing.VendorId : int.Parse(vendorStr);

            Console.Write($"Enter Package Name [{existing.PackageName}]: ");
            string name = Console.ReadLine() ?? "";
            requestModel.PackageName = string.IsNullOrWhiteSpace(name) ? existing.PackageName : name;

            Console.Write($"Enter Price [{existing.Price}]: ");
            string priceStr = Console.ReadLine() ?? "";
            requestModel.Price = string.IsNullOrWhiteSpace(priceStr) ? existing.Price : decimal.Parse(priceStr);

            Console.Write($"Enter Description [{existing.Description}]: ");
            string desc = Console.ReadLine() ?? "";
            requestModel.Description = string.IsNullOrWhiteSpace(desc) ? existing.Description : desc;

            Console.Write($"Enter Is Active [{existing.IsActive}]: ");
            string activeStr = Console.ReadLine() ?? "";
            requestModel.IsActive = string.IsNullOrWhiteSpace(activeStr) ? existing.IsActive : bool.Parse(activeStr);

            string jsonBody = JsonConvert.SerializeObject(requestModel);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(BaseUrl + "api/ServicePackage/" + id, content);
            string responseString = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<ServiceUpdateResponseModel>(responseString);

            if (res != null)
            {
                Console.WriteLine($"Success: {res.IsSuccess} | Message: {res.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private static async Task DeleteServicePackage()
    {
        Console.Clear();
        Console.WriteLine("--- DELETE SERVICE PACKAGE ---");
        Console.Write("Enter Service Package ID to delete: ");
        string idStr = Console.ReadLine() ?? "";
        int id = Convert.ToInt32(idStr);

        try
        {
            var response = await client.DeleteAsync(BaseUrl + "api/ServicePackage/" + id);
            string responseString = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<ServiceDeleteResponseModel>(responseString);

            if (res != null)
            {
                Console.WriteLine($"Success: {res.IsSuccess} | Message: {res.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
    #endregion
}
