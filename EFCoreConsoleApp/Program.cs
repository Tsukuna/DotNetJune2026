// See https://aka.ms/new-console-template for more information
using EFCoreConsoleApp;

Console.WriteLine("EF Core Starting...");


//EfCoreAppDbContext db = new EfCoreAppDbContext();

//// List Student
//List<StudentEntity> list = db.Students.ToList();

//foreach (var student in list)
//{
//    Console.WriteLine($"Student Number: {student.StudentNo}, Student Name: {student.StudentName}");
//};

////Update Student
//StudentEntity? updateStudent = db.Students.Where(x => x.StudentId == 7).FirstOrDefault();
//if (updateStudent is null)
//{
//    Console.WriteLine("Student Not Found");
//    return;
//}
//else
//{
//    updateStudent.StudentName = "Hte Min Lu";
//    db.SaveChanges();
//    Console.WriteLine("Update Success");
//}

////Create Student
//StudentEntity createStudent = new StudentEntity
//{
//    StudentName = "Jinny",
//    StudentNo = "STU012",
//    FatherName = "Kinny",
//    PhoneNumber = "098765432",
//    Address = "Mandalay",
//    BirthDate = new DateTime(2004, 3, 7),
//    IsDelete = false
//};

//db.Students.Add(createStudent);
//db.SaveChanges();
//Console.WriteLine("Insert Success");

////Delete Student
//StudentEntity? deleteStudent = db.Students.Where(x => x.StudentId == 18).FirstOrDefault();
//if (deleteStudent is null)
//{
//    Console.WriteLine("Student Not Found");
//    return;
//}
//else
//{
//    db.Students.Remove(deleteStudent);
//    db.SaveChanges();
//    Console.WriteLine("Delete Success");
//}


//// Login with EfCore
//LoginService loginService = new LoginService();
//loginService.Login("john doe", "Password@123");


//EfCore Service
EfCoreService efCoreService = new EfCoreService();
//efCoreService.ListProduct();
//efCoreService.ListStaff();
efCoreService.ListSong();

Console.ReadLine();
