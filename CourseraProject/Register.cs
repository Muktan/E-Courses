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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("Insert into User_Details(Username,Password,Email,MobileNumber,Address,Country,DOB) values('"+textBox1.Text+"','"+textBox2.Text+"','"+textBox3.Text+"','"+textBox4.Text+"','"+textBox5.Text+"','"+textBox6.Text+"','"+textBox7.Text+"' )", con);
            con.Open();

            int aff = cmd.ExecuteNonQuery();
            if (aff>0)
            {
                MessageBox.Show("Registered");
                this.Close();
            }
            else
            {
                MessageBox.Show("Not Registered");
                this.Close();
            }
            cmd.Dispose();
            con.Close();
        }
    }
}
