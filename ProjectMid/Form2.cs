using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ProjectMid
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            RefreshDataGridView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connection = "Data Source=CYBERSPACE;Initial Catalog=ProjectB;Integrated Security=True; Trusted_Connection = True;";
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            if (IsDuplicateRecord(textBox1.Text, textBox2.Text, textBox4.Text, textBox5.Text, textBox3.Text))
            {
                MessageBox.Show("Record already exists. Please enter unique data.");
                return;
            }
            
         
                try
                {
                    string cmdText = "INSERT INTO dbo.Student (FirstName, LastName, Contact, Email, RegistrationNumber, Status) " +
                                     "VALUES (@FirstName, @LastName, @Contact, @Email, @RegistrationNumber, @Status)";

                    using (SqlCommand command = new SqlCommand(cmdText, con))
                    {
                        command.Parameters.AddWithValue("@FirstName", textBox1.Text);
                        command.Parameters.AddWithValue("@LastName", textBox2.Text);
                        command.Parameters.AddWithValue("@Contact", textBox4.Text);
                        command.Parameters.AddWithValue("@Email", textBox5.Text);
                        command.Parameters.AddWithValue("@RegistrationNumber", textBox3.Text);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data inserted successfully!");
                            RefreshDataGridView();
                            ClearTextBoxes();

                        }
                        else
                        {
                            MessageBox.Show("No rows were affected. Check your input and try again.");
                        }
                    }
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM dbo.Student", con);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error!!" + ex.Message);
                }
                con.Close();
            }
            
        private void button2_Click(object sender, EventArgs e)
        {
            Form7 Attendance = new Form7();
            Attendance.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int studentId = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string connection = "Data Source=CYBERSPACE;Initial Catalog=ProjectB;Integrated Security=True; Trusted_Connection = True;";
                    using (SqlConnection con = new SqlConnection(connection))
                    {
                        con.Open();
                        try
                        {
                            string deleteCmdText = "DELETE FROM dbo.Student WHERE Id = @StudentId";
                            using (SqlCommand deleteCommand = new SqlCommand(deleteCmdText, con))
                            {
                                deleteCommand.Parameters.AddWithValue("@StudentId", studentId);
                                int rowsAffected = deleteCommand.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Record deleted successfully!");
                                    RefreshDataGridView();
                                }
                                else
                                {
                                    MessageBox.Show("No records were deleted. Make sure the selected row exists.");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                        con.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int studentId = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                string updatedFirstName = textBox1.Text;
                string updatedLastName = textBox2.Text;
                string updatedContact = textBox4.Text;
                string updatedEmail = textBox5.Text;
                string updatedRegistrationNumber = textBox3.Text;

                DialogResult result = MessageBox.Show("Are you sure you want to update this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string connection = "Data Source=CYBERSPACE;Initial Catalog=ProjectB;Integrated Security=True; Trusted_Connection = True;";
                    using (SqlConnection con = new SqlConnection(connection))
                    {
                        con.Open();

                        try
                        {
                            string updateCmdText = "UPDATE dbo.Student SET FirstName = @FirstName, LastName = @LastName, " +
                                                   "Contact = @Contact, Email = @Email, RegistrationNumber = @RegistrationNumber, " +
                                                   "Status = @Status WHERE Id = @StudentId";
                            using (SqlCommand updateCommand = new SqlCommand(updateCmdText, con))
                            {
                                updateCommand.Parameters.AddWithValue("@FirstName", updatedFirstName);
                                updateCommand.Parameters.AddWithValue("@LastName", updatedLastName);
                                updateCommand.Parameters.AddWithValue("@Contact", updatedContact);
                                updateCommand.Parameters.AddWithValue("@Email", updatedEmail);
                                updateCommand.Parameters.AddWithValue("@RegistrationNumber", updatedRegistrationNumber);
                                updateCommand.Parameters.AddWithValue("@StudentId", studentId);

                                int rowsAffected = updateCommand.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Record updated successfully!");
                                    RefreshDataGridView();
                                }
                                else
                                {
                                    MessageBox.Show("No records were updated. Make sure the selected row exists.");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                        con.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to update.");
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form3 CLO = new Form3();
            CLO.Show();
        }
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
            textBox1.Text = selectedRow.Cells["FirstName"].Value.ToString();
            textBox2.Text = selectedRow.Cells["LastName"].Value.ToString();
            textBox4.Text = selectedRow.Cells["Contact"].Value.ToString();
            textBox5.Text = selectedRow.Cells["Email"].Value.ToString();
            textBox3.Text = selectedRow.Cells["RegistrationNumber"].Value.ToString();

        }

        //--------------------------------------------------------METHODS----------------------------------------------------
        private void RefreshDataGridView()
        {
            string connection = "Data Source=CYBERSPACE;Initial Catalog=ProjectB;Integrated Security=True; Trusted_Connection = True;";
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM dbo.Student", con);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                con.Close();
            }
        }

        private void ClearTextBoxes()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'projectBDataSet.Lookup' table. You can move, or remove it, as needed.
            this.lookupTableAdapter.Fill(this.projectBDataSet.Lookup);
            // Filter the ComboBox to only display "Active" and "Inactive"
            DataView dv = new DataView(this.projectBDataSet.Lookup);
            dv.RowFilter = "Name IN ('Active', 'Inactive')";
            comboBox1.DataSource = dv.ToTable();
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Name";

        }
        private bool IsDuplicateRecord(string firstName, string lastName, string contact, string email, string regNumber)
        {
            string connectionString = "Data Source=CYBERSPACE;Initial Catalog=ProjectB;Integrated Security=True; Trusted_Connection = True;";
            string query = "SELECT COUNT(*) FROM Student WHERE FirstName = @FirstName AND LastName = @LastName AND Contact = @Contact AND Email = @Email AND RegistrationNumber = @RegNumber";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@Contact", contact);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@RegNumber", regNumber);

                connection.Open();
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        // Function to retrieve the status ID from the selected item in the ComboBox
    

        // Function to retrieve the status ID from the student ID
        private int GetStatusIdFromStudentId(int studentId)
        {
            string connectionString = "Data Source=CYBERSPACE;Initial Catalog=ProjectB;Integrated Security=True; Trusted_Connection = True;";
            string query = "SELECT Status FROM Student WHERE Id = @StudentId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentId", studentId);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    return (int)result;
                }

                else
                {
                    return -1; // Return -1 if no status is found
                }
            }
        }
    }
}
