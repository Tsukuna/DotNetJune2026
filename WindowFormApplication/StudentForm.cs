using Database.AppDbContextModels;
using System.Net;
using static WindowFormApplication.StudentForm;

namespace WindowFormApplication
{
    public partial class StudentForm : Form
    {

        private readonly AppDbContext _db;

        int editId = 0;
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
           if(editId == 0)
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

            }
            else
            {
                var student = _db.Students.Where(x => x.StudentId == editId).FirstOrDefault();
                if (student is null) return;

                student.StudentName = txtName.Text.Trim();
                student.FatherName = txtFatherName.Text.Trim();
                student.StudentNo = txtStudentNo.Text.Trim();
                student.BirthDate = datePicker.Value;
                student.PhoneNumber= txtPhone.Text.Trim();
                student.Address   = txtAddress.Text.Trim();

                _db.SaveChanges();

            }

            BindData();

        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == -1) return;

            if(e.ColumnIndex == 0) //edit
            {
                var studentId = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells[nameof(colStudentId)].Value);
                var student = _db.Students.Where(x => x.StudentId == studentId).FirstOrDefault();

                if (student is null) return;

                txtName.Text = student.StudentName;
                txtFatherName.Text = student.FatherName;
                txtStudentNo.Text = student.StudentNo;
                datePicker.Value = student.BirthDate;
                txtPhone.Text = student.PhoneNumber;
                txtAddress.Text = student.Address;

                editId = student.StudentId;

            }
            else if(e.ColumnIndex == 1) //delete
            {
                var result = MessageBox.Show("Are u sure to delete", "Confirm",MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    var studentId = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells[nameof(colStudentId)].Value);
                    var student = _db.Students.Where(x => x.StudentId == studentId).FirstOrDefault();

                    if (student is null) return;

                    _db.Students.Remove(student);
                    _db.SaveChanges();
                    BindData();
                }
            }
            else
            {
                Console.WriteLine("Please enter valid action...");
            }

        }
    }
}
