using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectMid
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
                Form2 form2 = new Form2();    //Manage Students
                form2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 CLO = new Form3();
            CLO.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 rubrics = new Form4();
            rubrics.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.Show();
        }
    }
}
