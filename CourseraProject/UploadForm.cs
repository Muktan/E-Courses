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
    public partial class UploadForm : Form
    {
        public UploadForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cname = textBox1.Text;
            string desc = textBox2.Text;
            int orgid = 0;
            int price = int.Parse(textBox3.Text);
            string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True";
            int Id = 0;
            string query1 = "select max(Id) from Courses;";
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd1 = new SqlCommand(query1, con);
            try
            {
                con.Open();
                Id = (int)cmd1.ExecuteScalar() + 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "okokok");
            }
            string asd = DateTime.Now.ToString("MM-dd-yyyy");

            string query = "insert into Courses(Id,CourseName,CourseDescription,OrganisationId,UploadDate,Sales,Price) values('" + Id + "','" + cname + "','" + desc + "','" + (orgid + 1) + "','" + asd + "',0,'" + price + "');";
            SqlCommand cmd = new SqlCommand(query, con);
            //SqlDataReader myReader;
            try
            {
                //con.Open();
                int i = cmd.ExecuteNonQuery();
                //myReader = cmd.ExecuteReader();
                //Console.WriteLine(i);
                //MessageBox.Show("Saved");
                string path = @"D:\0_Drive_E\SEM-6\OOSE\WindowsFormsApp1\WindowsFormsApp1\Courses\" + Id.ToString();
                System.IO.Directory.CreateDirectory(path);
                this.Hide();
                UploadContent up = new UploadContent(Id);
                up.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
