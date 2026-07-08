using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreConsoleApp
{
    public class EfCoreAppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder
                {
                    DataSource = "LAPTOP-1LF20QJ8\\SQLEXPRESS",
                    InitialCatalog = "DotNetJune2026",
                    UserID = "sa",
                    Password = "sasa@123",
                    TrustServerCertificate = true
                };
                optionsBuilder.UseSqlServer(sb.ConnectionString);
            }
        }
        public DbSet<StudentEntity> Students { get; set; }
    }
}
