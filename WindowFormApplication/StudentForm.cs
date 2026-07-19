using Database.AppDbContextModels;

namespace WindowFormApplication
{
    public partial class StudentForm : Form
    {

        private readonly AppDbContext _db;
        public StudentForm()
        {
            InitializeComponent();
            _db = new AppDbContext();
        }

        private void StudentForm_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void BindData()
        {
            int studentRowId = 0;

            List<Student> studentList = _db.Students.ToList();

            List<StudentDto> students = new List<StudentDto>();

            foreach (var student in studentList)
            {
                StudentDto studentDto = new StudentDto()
                {
                    RowNo = ++studentRowId,
                    StudentId = student.StudentId,
                    StudentName = student.StudentName,
                    StudentNo = student.StudentNo,
                    FatherName = student.FatherName,
                    PhoneNumber = student.PhoneNumber,
                    BirthDate = student.BirthDate,
                    Address = student.Address
                };

                students.Add(studentDto);
            }

            dgvData.DataSource = students;

            ClearControls();
        }

        public class StudentDto
        {
            public int RowNo { get; set; }
            public int StudentId { get; set; }
            public string StudentName { get; set; }
            public string? StudentNo { get; set; }
            public string FatherName { get; set; }
            public string PhoneNumber { get; set; }
            public DateTime BirthDate { get; set; }
            public string? Address { get; set; }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        private void ClearControls()
        {
            txtName.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtFatherName.Clear();
            txtStudentNo.Clear();
            datePicker.Value = DateTime.Today;
            txtName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _db.Students.Add(new Student
            {
                StudentName = txtName.Text.Trim(),
                FatherName = txtFatherName.Text.Trim(),
                StudentNo = txtStudentNo.Text.Trim(),
                BirthDate = datePicker.Value,
                PhoneNumber = txtPhone.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                IsDelete = true
            });
            _db.SaveChanges();

            BindData();
        }

      
    }
}
