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
            Id = Course.getMaxId();
            string asd = DateTime.Now.ToString("MM-dd-yyyy");
            Course.InsertCourseEntry((Id+1), cname, desc, (orgid + 1), asd, 0, price);
            this.Hide();
            UploadContent up = new UploadContent(Id);
            string path = @"D:\0_Drive_E\SEM-6\OOSE\WindowsFormsApp1\WindowsFormsApp1\Courses\" + Id.ToString();
            System.IO.Directory.CreateDirectory(path);
            up.ShowDialog();
        }
    }
}
