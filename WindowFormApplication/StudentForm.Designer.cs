namespace WindowFormApplication
{
    partial class StudentForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvData = new DataGridView();
            RowNo = new DataGridViewTextBoxColumn();
            colStudentNo = new DataGridViewTextBoxColumn();
            colFatherName = new DataGridViewTextBoxColumn();
            colBirthDate = new DataGridViewTextBoxColumn();
            colStudentId = new DataGridViewTextBoxColumn();
            colStudentName = new DataGridViewTextBoxColumn();
            colPhoneNum = new DataGridViewTextBoxColumn();
            colAddress = new DataGridViewTextBoxColumn();
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtAddress = new TextBox();
            btnCancel = new Button();
            btnSave = new Button();
            txtName = new TextBox();
            txtPhone = new TextBox();
            txtFatherName = new TextBox();
            label5 = new Label();
            txtStudentNo = new TextBox();
            label6 = new Label();
            label7 = new Label();
            datePicker = new DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            SuspendLayout();
            // 
            // dgvData
            // 
            dgvData.AllowUserToAddRows = false;
            dgvData.AllowUserToDeleteRows = false;
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvData.Columns.AddRange(new DataGridViewColumn[] { RowNo, colStudentNo, colFatherName, colBirthDate, colStudentId, colStudentName, colPhoneNum, colAddress });
            dgvData.Dock = DockStyle.Bottom;
            dgvData.Location = new Point(0, 253);
            dgvData.Name = "dgvData";
            dgvData.ReadOnly = true;
            dgvData.RowHeadersWidth = 51;
            dgvData.Size = new Size(944, 245);
            dgvData.TabIndex = 9;
            // 
            // RowNo
            // 
            RowNo.DataPropertyName = "RowNo";
            RowNo.HeaderText = "Row Number";
            RowNo.MinimumWidth = 6;
            RowNo.Name = "RowNo";
            RowNo.ReadOnly = true;
            // 
            // colStudentNo
            // 
            colStudentNo.DataPropertyName = "StudentNo";
            colStudentNo.HeaderText = "Student No";
            colStudentNo.MinimumWidth = 6;
            colStudentNo.Name = "colStudentNo";
            colStudentNo.ReadOnly = true;
            // 
            // colFatherName
            // 
            colFatherName.DataPropertyName = "FatherName";
            colFatherName.HeaderText = "Father Name";
            colFatherName.MinimumWidth = 6;
            colFatherName.Name = "colFatherName";
            colFatherName.ReadOnly = true;
            // 
            // colBirthDate
            // 
            colBirthDate.DataPropertyName = "BirthDate";
            colBirthDate.HeaderText = "Birth Date";
            colBirthDate.MinimumWidth = 6;
            colBirthDate.Name = "colBirthDate";
            colBirthDate.ReadOnly = true;
            // 
            // colStudentId
            // 
            colStudentId.DataPropertyName = "StudentId";
            colStudentId.HeaderText = "Student Id";
            colStudentId.MinimumWidth = 6;
            colStudentId.Name = "colStudentId";
            colStudentId.ReadOnly = true;
            // 
            // colStudentName
            // 
            colStudentName.DataPropertyName = "StudentName";
            colStudentName.HeaderText = "Student Name";
            colStudentName.MinimumWidth = 6;
            colStudentName.Name = "colStudentName";
            colStudentName.ReadOnly = true;
            // 
            // colPhoneNum
            // 
            colPhoneNum.DataPropertyName = "PhoneNumber";
            colPhoneNum.HeaderText = "Phone Number";
            colPhoneNum.MinimumWidth = 6;
            colPhoneNum.Name = "colPhoneNum";
            colPhoneNum.ReadOnly = true;
            // 
            // colAddress
            // 
            colAddress.DataPropertyName = "Address";
            colAddress.HeaderText = "Address";
            colAddress.MinimumWidth = 6;
            colAddress.Name = "colAddress";
            colAddress.ReadOnly = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 52);
            label1.Name = "label1";
            label1.Size = new Size(107, 20);
            label1.TabIndex = 1;
            label1.Text = "Student Name:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(30, 103);
            label3.Name = "label3";
            label3.Size = new Size(111, 20);
            label3.TabIndex = 3;
            label3.Text = "Phone Number:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(30, 148);
            label4.Name = "label4";
            label4.Size = new Size(65, 20);
            label4.TabIndex = 5;
            label4.Text = "Address:";
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(158, 148);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(169, 27);
            txtAddress.TabIndex = 6;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(30, 197);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(126, 29);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "&Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(201, 197);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(126, 29);
            btnSave.TabIndex = 8;
            btnSave.Text = "&Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // txtName
            // 
            txtName.Location = new Point(158, 52);
            txtName.Name = "txtName";
            txtName.Size = new Size(169, 27);
            txtName.TabIndex = 2;
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(158, 103);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(169, 27);
            txtPhone.TabIndex = 4;
            // 
            // txtFatherName
            // 
            txtFatherName.Location = new Point(556, 52);
            txtFatherName.Name = "txtFatherName";
            txtFatherName.Size = new Size(169, 27);
            txtFatherName.TabIndex = 11;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(428, 52);
            label5.Name = "label5";
            label5.Size = new Size(96, 20);
            label5.TabIndex = 10;
            label5.Text = "Father Name:";
            // 
            // txtStudentNo
            // 
            txtStudentNo.Location = new Point(556, 103);
            txtStudentNo.Name = "txtStudentNo";
            txtStudentNo.Size = new Size(169, 27);
            txtStudentNo.TabIndex = 13;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(428, 103);
            label6.Name = "label6";
            label6.Size = new Size(87, 20);
            label6.TabIndex = 12;
            label6.Text = "Student No:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(428, 148);
            label7.Name = "label7";
            label7.Size = new Size(79, 20);
            label7.TabIndex = 14;
            label7.Text = "Birth Date:";
            // 
            // datePicker
            // 
            datePicker.Location = new Point(556, 148);
            datePicker.Name = "datePicker";
            datePicker.Size = new Size(250, 27);
            datePicker.TabIndex = 15;
            // 
            // StudentForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(944, 498);
            Controls.Add(datePicker);
            Controls.Add(label7);
            Controls.Add(txtStudentNo);
            Controls.Add(label6);
            Controls.Add(txtFatherName);
            Controls.Add(label5);
            Controls.Add(txtPhone);
            Controls.Add(txtName);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            Controls.Add(txtAddress);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(dgvData);
            Name = "StudentForm";
            Text = "Form1";
            Load += StudentForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvData;
        private Label label2;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label1;
        private TextBox textBox3;
        private Label label3;
        private Label label4;
        private TextBox textBox4;
        private TextBox txtAddress;
        private Button button1;
        private Button btnSave;
        private TextBox txtName;
        private TextBox txtFatherName;
        private TextBox txtPhone;
        private Button btnCancel;
        private DataGridViewTextBoxColumn RowNo;
        private DataGridViewTextBoxColumn colStudentNo;
        private DataGridViewTextBoxColumn colFatherName;
        private DataGridViewTextBoxColumn colBirthDate;
        private DataGridViewTextBoxColumn colStudentId;
        private DataGridViewTextBoxColumn colStudentName;
        private DataGridViewTextBoxColumn colPhoneNum;
        private DataGridViewTextBoxColumn colAddress;
        private TextBox textBox5;
        private Label label5;
        private TextBox txtStudentNo;
        private Label label6;
        private Label label7;
        private DateTimePicker datePicker;
    }
}
