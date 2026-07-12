using Database.AppDbContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreConsoleApp
{
    public class EfCoreService
    {
        AppDbContext db = new AppDbContext();

        public void ListProduct()
        {

            List<Product> products = db.Products.Where(x => x.IsActive == true).ToList();

            foreach(var product in products)
            {
                Console.WriteLine($"ProductID: {product.ProductId}, ProductName: {product.ProductName}");
            }

        }

        //public void ListStaff()
        //{
        //    List<Staff> staff = db.Staff.ToList();

        //    foreach (var lst in staff)
        //    {
        //        Console.WriteLine($"StaffName: {lst.StaffName}, PhoneNumber: {lst.PhoneNumber}");
        //    }
        //}

        public void ListSong()
        {

            List<Song> songs = db.Songs.ToList();

            foreach (var song in songs)
            {
                Console.WriteLine($"ProductID: {song.SongId}, ProductName: {song.SongName}, ArtistName: {song.ArtistName}");
            }

        }

    }
}
