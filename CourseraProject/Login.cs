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
    public partial class Login : Form
    {
        string UserId;
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT * FROM User_Details where Username='"+textBox1.Text+"' and Password = '"+textBox2.Text+"'" , con);
            con.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();
            
            if (dataReader.HasRows)
            {
                dataReader.Read();
                UserId = dataReader["Id"].ToString();
                this.Hide();
                Home h = new Home(UserId);
                h.Show();
            }
            else
            {
                label2.Text = "Wrong username/Password";
            }
            dataReader.Close();
            cmd.Dispose();
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Register r = new Register();
            r.Show();
        }
    }
}
