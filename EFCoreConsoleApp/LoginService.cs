using Database.AppDbContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreConsoleApp
{
    public class LoginService
    {
        AppDbContext db = new AppDbContext();
        public void Login(string username, string password)
        {
            User? existUser = db.Users.Where(x => x.Username == username && x.IsActive).FirstOrDefault();

            if(existUser is null)
            {
                Console.WriteLine("User Not Found");
                return;
            }

            if (existUser.Password == password)
            {
                Console.WriteLine("Login Success");
            }
            else
            {
                Console.WriteLine("Incorrect Password");
            }


        }
    }
}
