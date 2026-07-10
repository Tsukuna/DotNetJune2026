using Dapper;
using Database.AppDbContextModels;
using Microsoft.Data.SqlClient;
using System.Data;

namespace TestingConsoleApp
{
    internal class DapperService
    {
        private readonly SqlConnectionStringBuilder sb;

        public DapperService()
        {
            sb = new SqlConnectionStringBuilder
            {
                DataSource = "LAPTOP-1LF20QJ8\\SQLEXPRESS",
                InitialCatalog = "DotNetJune2026",
                UserID = "sa",
                Password = "sasa@123",
                TrustServerCertificate = true
            };
        }

        #region Read Data
        public void GetData()
        {
            string query = @"SELECT [StudentId]
          ,[StudentName]
          ,[StudentNo]
          ,[FatherName]
          ,[PhoneNumber]
          ,[Address]
          ,[BirthDate]
            FROM [Student]";

            using (IDbConnection db = new SqlConnection(sb.ConnectionString))
            {
                db.Open();
                List<Student> students = db.Query<Student>(query).ToList();
                foreach (var student in students)
                {
                    Console.WriteLine($"StudentId: {student.StudentId}, StudentName: {student.StudentName}");
                }
            }


        }
        #endregion

        #region Create
        public void Create()
        {
            string query = @"INSERT INTO [Student]
                        (
                            [StudentName],
                            [StudentNo],
                            [FatherName],
                            [PhoneNumber],
                            [Address],
                            [BirthDate],
                            [IsDelete]
                        )
                        VALUES
                        ('Adam Ken', 'STU011', 'Andrew Ken', '09611111112', 'Yangon', '2002-03-19' , 0);";

            using (IDbConnection db = new SqlConnection(sb.ConnectionString))
            {
                db.Open();
                int result = db.Execute(query);
                if (result > 0)
                {
                    Console.WriteLine("Create Success");
                }
                else
                {
                    Console.WriteLine("Create Fail");
                }

            }
        }
        #endregion

        #region Update
        public void Update()
        {
            string query = @"UPDATE [Student]
                        SET
                            StudentName = 'Sophia',
                            FatherName = 'Andrew Taylor',
                            PhoneNumber = '09611111111',
                            Address = 'Yangon',
                            BirthDate = '2002-03-18'
                        WHERE StudentNo = 'STU006';";

            using (IDbConnection db = new SqlConnection(sb.ConnectionString))
            {
                db.Open();
                int result = db.Execute(query);
                if (result > 0)
                {
                    Console.WriteLine("Update Success");
                }
                else
                {
                    Console.WriteLine("Update Fail");
                }

            }
        }
        #endregion

        #region Delete
        public void Delete()
        {
            string query = @"UPDATE [Student] SET IsDelete = 1 WHERE StudentId = 6";

            using (IDbConnection db = new SqlConnection(sb.ConnectionString))
            {
                db.Open();
                int result = db.Execute(query);
                if (result > 0)
                {
                    Console.WriteLine("Delete Success");
                }
                else
                {
                    Console.WriteLine("Delete Fail");
                }

            }

        }
        #endregion
    }
}
