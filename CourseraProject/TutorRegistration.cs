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
    public partial class TutorRegistration : Form
    {
        public TutorRegistration()
        {
            InitializeComponent();

            //load organisation lists
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT * FROM Organization", con);
            con.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();
            while(dataReader.Read())
            {
                string s= dataReader["OrgName"].ToString();
                comboBox1.Items.Add(s);
            }
            dataReader.Close();
            cmd.Dispose();
            con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {//register the user
            Tutor tutor = new Tutor();
            tutor.SetDetails(textBox1.Text,textBox2.Text,textBox3.Text,textBox4.Text,textBox5.Text,textBox6.Text,textBox7.Text,(comboBox1.SelectedIndex + 1).ToString());
            bool registered = tutor.RegisterUser();
            if (registered)
            {
                MessageBox.Show("Registered");
                this.Close();
            }
            else
            {
                this.Close();
                MessageBox.Show("Not Registered");

            }
        }
    }
}
