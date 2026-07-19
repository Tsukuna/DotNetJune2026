using System;
using System.Collections.Generic;

namespace Database.AppDbContextModels;

public partial class Student
{
    public int StudentId { get; set; }

    public string StudentName { get; set; } = null!;

    public string? StudentNo { get; set; }

    public string FatherName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? Address { get; set; }

    public DateTime BirthDate { get; set; }

    public bool IsDelete { get; set; }
}
