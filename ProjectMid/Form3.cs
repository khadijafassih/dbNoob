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

namespace ProjectMid
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            RefreshDataGridView();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form4 rubrics = new Form4();
            rubrics.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string cloName = textBox1.Text.Trim();
                if (!string.IsNullOrEmpty(cloName))
                {
                    int cloId = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                    DateTime currentDate = DateTime.Now;
                    string connection = "Data Source=CYBERSPACE;Initial Catalog=ProjectB;Integrated Security=True; Trusted_Connection = True;";
                    using (SqlConnection con = new SqlConnection(connection))
                    {
                        try
                        {
                            con.Open(); 
                            string sqlCommand = "UPDATE dbo.Clo SET Name = @Name, DateUpdated = @DateUpdated WHERE Id = @Id";

                            SqlCommand command = new SqlCommand(sqlCommand, con);
                            command.Parameters.AddWithValue("@Id", cloId);
                            command.Parameters.AddWithValue("@Name", cloName);
                            command.Parameters.AddWithValue("@DateUpdated", currentDate);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("CLO updated successfully!");
                                RefreshDataGridView();
                            }
                            else
                            {
                                MessageBox.Show("Failed to update CLO.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                        con.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid CLO name.");
                }
            }
            else
            {
                MessageBox.Show("Please select a row to update.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string cloName = textBox1.Text.Trim();
            if (!string.IsNullOrEmpty(cloName))
            {
                DateTime currentDate = DateTime.Now;
                string connection = "Data Source=CYBERSPACE;Initial Catalog=ProjectB;Integrated Security=True; Trusted_Connection = True;";
                using (SqlConnection con = new SqlConnection(connection))
                {
                    try
                    {
                        con.Open();
                        string sqlCommand = "INSERT INTO dbo.Clo (Name, DateCreated, DateUpdated) VALUES (@Name, @DateCreated, @DateUpdated)";
                        SqlCommand command = new SqlCommand(sqlCommand, con);
                        command.Parameters.AddWithValue("@Name", cloName);
                        command.Parameters.AddWithValue("@DateCreated", currentDate);
                        command.Parameters.AddWithValue("@DateUpdated", currentDate);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("CLO added successfully!");
                            RefreshDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add CLO.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid CLO name.");
            }
        }
    
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int cloId = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                string connection = "Data Source=CYBERSPACE;Initial Catalog=ProjectB;Integrated Security=True; Trusted_Connection = True;";
                using (SqlConnection con = new SqlConnection(connection))
                {                 
                    try
                    {
                        con.Open();
                        string sqlCommand = "DELETE FROM dbo.Clo WHERE Id = @Id";

                        SqlCommand command = new SqlCommand(sqlCommand, con);
                        command.Parameters.AddWithValue("@Id", cloId);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("CLO deleted successfully!");
                            RefreshDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete CLO.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM dbo.Clo", con);
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

    }
}

