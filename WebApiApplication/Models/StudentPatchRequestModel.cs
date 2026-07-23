namespace WebApiApplication.Models
{
    public class StudentPatchRequestModel
    {
        public string? StudentName { get; set; } = null!;

        public string? StudentNo { get; set; }

        public string? FatherName { get; set; } = null!;

        public string? PhoneNumber { get; set; } = null!;

        public string? Address { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}
