using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseraProject
{
    public partial class EditCourse : Form
    {
        public EditCourse()
        {
            InitializeComponent();
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT * FROM Courses", con);
            con.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                string s = dataReader["CourseName"].ToString();
                comboBox1.Items.Add(s.TrimEnd());
            }
            dataReader.Close();
            cmd.Dispose();
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int selectedIndex = comboBox1.SelectedIndex;
            string path = @"D:\0_Drive_E\SEM-6\OOSE\Github_coursera\E-Courses\CourseraProject\Resources\" + selectedIndex;

            string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True";
            string query1 = "delete from Courses where Id=" + selectedIndex + ";";
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd1 = new SqlCommand(query1, con);
            try
            {
                con.Open();
                int i = cmd1.ExecuteNonQuery();
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
                MessageBox.Show("Your Course is successfully removed!\n" +
                    "Thank you for using COURSERA!");
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int selectedIndex = comboBox1.SelectedIndex;
            
            Update_info u = new Update_info(selectedIndex+1);
            
            u.Show();
        }
    }
}
