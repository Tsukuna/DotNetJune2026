using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TestingConsoleApp.DataEntity;

namespace TestingConsoleApp;

internal class AdoNetService
{

    private readonly SqlConnectionStringBuilder sb;

    public AdoNetService()
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
    #region Student List
    public void GetData()
    {
        SqlConnection connection = new SqlConnection(sb.ConnectionString);
        Console.WriteLine("Connection Opening...");
        connection.Open();
        Console.WriteLine("Connection Opened");

        string query = @"SELECT [StudentId]
          ,[StudentName]
          ,[StudentNo]
          ,[FatherName]
          ,[PhoneNumber]
          ,[Address]
          ,[BirthDate]
            FROM [Student]
        ";
        SqlCommand cmd = new SqlCommand(query, connection);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adapter.Fill(dt);

        List<Student> studentList = new List<Student>();

        foreach(DataRow row in dt.Rows)
        {
            Student student = new Student
            {
                StudentId = Convert.ToInt32(row["StudentId"]),
                StudentName = Convert.ToString(row["StudentName"]),
                StudentNo = Convert.ToString(row["StudentNo"]),
                Address = Convert.ToString(row["Address"]),
                FatherName = Convert.ToString(row["FatherName"]),
                BirthDate = Convert.ToDateTime(row["BirthDate"]),
            };
            studentList.Add(student);
        }

        connection.Close();
        Console.WriteLine("Connection Closed");

        foreach (var student in studentList)
        {
            Console.WriteLine($"StudentId: {student.StudentId}, StudentName: {student.StudentName}");
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
                        ('Sophia Taylor',   'STU006', 'Andrew Taylor',   '09611111111', 'Yangon',      '2002-03-18' , 0),
                        ('Ethan Martinez',  'STU007', 'Carlos Martinez', '09622222222', 'Mandalay',    '2001-08-27', 0),
                        ('Olivia Anderson', 'STU008', 'Thomas Anderson', '09633333333', 'Taunggyi',    '2003-11-14', 0),
                        ('Noah Thomas',     'STU009', 'Joseph Thomas',   '09644444444', 'Pathein',     '2002-06-09', 0),
                        ('Ava White',       'STU010', 'Charles White',   '09655555555', 'Pyin Oo Lwin','2001-10-31', 0);";

        SqlConnection connection = new SqlConnection(sb.ConnectionString);
        connection.Open();

        SqlCommand cmd = new SqlCommand(query, connection);
        int result = cmd.ExecuteNonQuery();

        if(result > 0)
        {
            Console.WriteLine("Data inserted successfully");
        }
        else
        {
            Console.WriteLine("Data insertion failed");
        }

        connection.Close();

        Console.ReadLine();


    }
    #endregion


    #region Update
    public void Update()
    {
        string query = @"UPDATE [Student]
                        SET
                            StudentName = 'Sophia Taylor',
                            FatherName = 'Andrew Taylor',
                            PhoneNumber = '09611111111',
                            Address = 'Yangon',
                            BirthDate = '2002-03-18'
                        WHERE SrudentNo = 'STU006';";

        SqlConnection connection = new SqlConnection(sb.ConnectionString);
        connection.Open();

        SqlCommand cmd = new SqlCommand(query,connection);
        int result = cmd.ExecuteNonQuery();

        if(result > 0)
        {
            Console.WriteLine("Data updated");
        }
        else
        {
            Console.WriteLine("Data update failed");
        }

        connection.Close();

        Console.ReadLine();

    }
    #endregion


    #region Delete
    public void Delete()
    {
        string query = @"UPDATE [Student] SET IsDelete = 1 WHERE StudentId = 6";
        SqlConnection connection = new SqlConnection(sb.ConnectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand(query, connection);
        int result = cmd.ExecuteNonQuery();

        if(result > 0)
        {
            Console.WriteLine("Delete Success");
        }
        else
        {
            Console.WriteLine("Delete Fail");
        }

        connection.Close();
        Console.ReadLine();


    }
    #endregion
}
