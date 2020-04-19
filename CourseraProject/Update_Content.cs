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
    public partial class Update_Content : Form
    {
        int Id;
        string dataentry = "";
        int textfile = 0;
        int videofile = 0;
        public Update_Content(int Id)
        {
            InitializeComponent();
            this.Id = Id;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True";
            string query = "select * from Courses where Id=" + Id.ToString() + ";";
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        dataentry = reader["CourseSequence"].ToString();
                        dataentry = dataentry.TrimEnd();
                        Console.WriteLine(dataentry);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (dataentry != "")
            {
                dataentry += ",";
            }
            for (int i = 0; i < dataentry.Length; i++)
            {
                if (dataentry[i] == 't')
                {
                    textfile += 1;
                }
                else if (dataentry[i] == 'v')
                {
                    videofile += 1;
                }
            }
            string path = @"D:\0_Drive_E\SEM-6\OOSE\Github_coursera\E-Courses\CourseraProject\Resources\" + Id.ToString();
            OpenFileDialog op1 = new OpenFileDialog();
            if (comboBox1.SelectedIndex == 0)
            {
                op1.Filter = "allfiles|*.pdf";
                path = path + "\\DOC";
                Directory.CreateDirectory(path);
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                op1.Filter = "allfiles|*.mp4";
                path = path + "\\VIDEO";
                Directory.CreateDirectory(path);
            }

            op1.ShowDialog();
            string s = op1.FileName;
            if (comboBox1.SelectedIndex == 0)
            {
                string fpath = path + "\\t" + textfile.ToString() + "_" + op1.SafeFileName;
                File.Copy(s, fpath);
                dataentry = dataentry.TrimEnd() + 't' + textfile.ToString() + ',';
                textfile++;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                string fpath = path + "\\v" + videofile.ToString() + "_" + op1.SafeFileName;
                File.Copy(s, fpath);
                dataentry = dataentry.TrimEnd() + 'v' + videofile.ToString() + ',';
                videofile++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True";
            Console.WriteLine(dataentry);
            string query = "update Courses set CourseSequence='" + dataentry.TrimEnd().Substring(0, dataentry.Length - 1) + "' where Id='" + Id + "';";
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                int i = cmd.ExecuteNonQuery();

                MessageBox.Show("Your Course is successfully updated!\n" +
                    "Thank you for using COURSERA!");
                this.Hide();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
