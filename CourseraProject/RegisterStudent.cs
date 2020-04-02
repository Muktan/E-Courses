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

namespace CourseraProject
{
    public partial class RegisterStudent : Form
    {
        public RegisterStudent()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {//register student button clicked

            Student CurrentUser = new Student();
            CurrentUser.SetDetails(textBox1.Text,textBox2.Text,textBox3.Text,textBox4.Text,textBox5.Text,textBox6.Text,textBox7.Text);
            bool registered = CurrentUser.RegisterUser();
            if (registered)
            {
                MessageBox.Show("Registered");
                this.Close();
            }
            else
            {
                MessageBox.Show("Not Registered");
                this.Close();
            }
            
        }
    }
}
