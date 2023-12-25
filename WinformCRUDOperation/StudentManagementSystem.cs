using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformCRUDOperation
{
    public partial class StudentManagementSystem : Form
    {
        //Instantitate our Database Access Helper
        private MySQLDataAccess mySQLDataAccess = new MySQLDataAccess();
        public StudentManagementSystem()
        {
            InitializeComponent();
        }

        private void StudentManagementSystem_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            //We want to display all the student in a datagridview from myssql table called tblstudent
            string query = "SELECT * FROM tblstudent";
            dgvStudentData.DataSource = mySQLDataAccess.GetData(query);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //obtain data from textboxes (inputs)
            //perform validation
            //execute our query (INSERT / UPDATE / DElETE)
            if (String.IsNullOrWhiteSpace(txtFirstname.Text))
            {
                MessageBox.Show($"Please enter a valid First Name.", "Adding Student Prompt (Add)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstname.Focus();
            }
            else if (String.IsNullOrWhiteSpace(txtLastname.Text))
            {
                MessageBox.Show($"Please enter a valid Last Name.", "Adding Student Prompt (Add)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastname.Focus();
            }
            else if (String.IsNullOrWhiteSpace(txtAge.Text))
            {
                MessageBox.Show($"Please enter a valid Age.", "Adding Student Prompt (Add)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAge.Focus();
            }
            else if (String.IsNullOrWhiteSpace(txtLastname.Text))
            {
                MessageBox.Show($"Please enter a valid Grade.", "Adding Student Prompt (Add)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGrade.Focus();
            }
            else
            {
                try
                {
                    //Your SPECIFIC QUERY HERE
                    string query = $"INSERT INTO tblStudent (first_name, last_name, age, grade) VALUES ('" + txtFirstname.Text + "','" + txtLastname.Text + "','" + txtAge.Text + "','" + txtGrade.Text + "')";

                    //Execute our Query using our mySQLDataAccess.ExecuteQuery
                    mySQLDataAccess.ExecuteQuery(query);

                    LoadData();
                    MessageBox.Show($"Student with Last Name: {txtLastname.Text} has been successfully added to database! ", "Database Connection Prompt (Add Button)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Something went wrong, check your connection to DB. \nError: {ex.Message}", "Database Connection Prompt (Add Button)", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }
        }

        private void txtAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtGrade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtFirstname.Text))
            {
                MessageBox.Show($"Please enter a valid First Name.", "Adding Student Prompt (Add)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstname.Focus();
            }
            else if (String.IsNullOrWhiteSpace(txtLastname.Text))
            {
                MessageBox.Show($"Please enter a valid Last Name.", "Adding Student Prompt (Add)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastname.Focus();
            }
            else if (String.IsNullOrWhiteSpace(txtAge.Text))
            {
                MessageBox.Show($"Please enter a valid Age.", "Adding Student Prompt (Add)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAge.Focus();
            }
            else if (String.IsNullOrWhiteSpace(txtLastname.Text))
            {
                MessageBox.Show($"Please enter a valid Grade.", "Adding Student Prompt (Add)", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtGrade.Focus();
            }
            else
            {
                try
                {
                    int selectedIndex = dgvStudentData.SelectedCells[0].RowIndex;

                    int student_id = Convert.ToInt32(dgvStudentData.Rows[selectedIndex].Cells["student_id"].Value);

                    string query = $"UPDATE tblStudent SET first_name = '{txtFirstname.Text}',last_name = '{txtLastname.Text}',age = '{txtAge.Text}',grade = '{txtGrade.Text}' WHERE student_id = '{student_id}'";

                    mySQLDataAccess.ExecuteQuery(query);

                    LoadData();
                    MessageBox.Show($"Student with ID: {student_id} has been successfully updated in our database! ", "Database Connection Prompt (Update Button)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Something went wrong, check your connection to DB. \nError: {ex.Message}", "Database Connection Prompt (Add Button)", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvStudentData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvStudentData.Rows[e.RowIndex];
                txtFirstname.Text = Convert.ToString(selectedRow.Cells["first_name"].Value);
                txtLastname.Text = Convert.ToString(selectedRow.Cells["last_name"].Value);
                txtAge.Text = Convert.ToString(selectedRow.Cells["age"].Value);
                txtGrade.Text = Convert.ToString(selectedRow.Cells["grade"].Value);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = dgvStudentData.SelectedCells[0].RowIndex;

                int student_id = Convert.ToInt32(dgvStudentData.Rows[selectedIndex].Cells["student_id"].Value);

                string query = $"DELETE FROM tblStudent WHERE student_id = '{student_id}'";

                mySQLDataAccess.ExecuteQuery(query);

                LoadData();
                MessageBox.Show($"Student with ID: {student_id} has been deleted from our database! ", "Database Connection Prompt (Delete Button)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ResetFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Something went wrong, check your connection to DB. \nError: {ex.Message}", "Database Connection Prompt (Add Button)", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
           ResetFields();
        }

        private void ResetFields()
        {
            txtFirstname.ResetText();
            txtLastname.ResetText();
            txtAge.ResetText();
            txtGrade.ResetText();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string query = $"SELECT * FROM tblStudent WHERE student_id LIKE '{txtSearch.Text}%' OR Last_name LIKE '{txtSearch.Text}%' OR first_name LIKE '{txtSearch.Text}%' OR age LIKE '{txtSearch.Text}%' OR grade LIKE '{txtSearch.Text}%'";

                dgvStudentData.DataSource = mySQLDataAccess.GetData(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Something went wrong, check your connection to DB. \nError: {ex.Message}", "Database Connection Prompt (Add Button)", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }
    }
}
