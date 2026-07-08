using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreConsoleApp
{
    [Table("Student")]
    public class StudentEntity
    {
        [Key]
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentNo { get; set; }
        public string FatherName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsDelete { get; set; }
    }
}
