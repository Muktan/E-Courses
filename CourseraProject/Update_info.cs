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
    public partial class Update_info : Form
    {
        int Id;
        public Update_info(int Id)
        {
            InitializeComponent();
            this.Id = Id;
            string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True";
            string query1 = "select * from Courses where Id=" + Id.ToString() + ";";
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd1 = new SqlCommand(query1, con);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd1.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        textBox1.Text = reader["CourseName"].ToString();
                        textBox2.Text = reader["CourseDescription"].ToString();
                        textBox3.Text = reader["Price"].ToString();
                        int orgId = int.Parse(reader["OrganisationId"].ToString());
                        string query = "select * from Organization where Id=" + orgId.ToString() + ";";
                        SqlConnection con1 = new SqlConnection(constring);
                        SqlCommand cmd = new SqlCommand(query, con1);
                        con1.Open();
                        using (SqlDataReader r1 = cmd.ExecuteReader())
                        {
                            if (r1.Read())
                            {
                                comboBox1.Text = r1["OrgName"].ToString();
                            }
                        }
                        //    Console.WriteLine(String.Format("{0}", reader["id"]));
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cname = textBox1.Text;
            string desc = textBox2.Text;
            int orgid = comboBox1.SelectedIndex;
            int price = int.Parse(textBox3.Text);
            string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True";
            string query1 = "update Courses set CourseName='" + cname + "',CourseDescription='" + desc + "',price='" + price + "' where Id=" + Id + ";";
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd1 = new SqlCommand(query1, con);
            try
            {
                con.Open();
                int i = cmd1.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Hide();
            Update_Content uc = new Update_Content(Id);
            uc.ShowDialog();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
