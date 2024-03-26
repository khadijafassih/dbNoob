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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            PopulateRegistrationNumbers();
            PopulateAttendanceStatus();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void PopulateAttendanceStatus()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Absent");
            comboBox1.Items.Add("Present");
        }
        private void PopulateRegistrationNumbers()
        {
            string connection = "Data Source=CYBERSPACE;Initial Catalog=ProjectB;Integrated Security=True; Trusted_Connection = True;";
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                try
                {
                    string selectCmdText = "SELECT RegistrationNumber FROM dbo.Student";
                    using (SqlCommand selectCommand = new SqlCommand(selectCmdText, con))
                    {
                        using (SqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            comboBox2.Items.Clear();
                            while (reader.Read())
                            {
                                comboBox2.Items.Add(reader["RegistrationNumber"].ToString());
                            }
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Absent")
            {
                comboBox1.Tag = 2;
            }
            else if (comboBox1.SelectedItem.ToString() == "Present")
            {
                comboBox1.Tag = 1;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string selectedRegistrationNumber = comboBox2.SelectedItem.ToString();
            int statusValue = Convert.ToInt32(comboBox1.Tag);
            DateTime selectedDate = dateTimePicker1.Value;
            string connection = "Data Source=CYBERSPACE;Initial Catalog=ProjectB;Integrated Security=True; Trusted_Connection = True;";
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                try
                {
                    using (SqlTransaction transaction = con.BeginTransaction())
                    {
                         string insertCmdText = "INSERT INTO dbo.ClassAttendance (AttendanceDate) VALUES (@AttendanceDate); SELECT SCOPE_IDENTITY();";
                         using (SqlCommand insertCommand = new SqlCommand(insertCmdText, con, transaction))
                        {
                            insertCommand.Parameters.AddWithValue("@AttendanceDate", selectedDate);
                            int attendanceId = Convert.ToInt32(insertCommand.ExecuteScalar());
                            string updateCmdText = "UPDATE dbo.Student SET Status = @Status WHERE RegistrationNumber = @RegistrationNumber";
                            using (SqlCommand updateCommand = new SqlCommand(updateCmdText, con, transaction))
                            {
                                updateCommand.Parameters.AddWithValue("@Status", statusValue);
                                updateCommand.Parameters.AddWithValue("@RegistrationNumber", selectedRegistrationNumber);
                                int rowsAffected = updateCommand.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    transaction.Commit();
                                    MessageBox.Show("Attendance saved successfully!");
                                }
                                else
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("No records were updated. Make sure the selected registration number exists.");
                                }
                            }
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
}
