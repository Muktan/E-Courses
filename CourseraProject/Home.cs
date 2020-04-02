using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseraProject
{
    public partial class Home : System.Windows.Forms.Form
    {
        private List<Course> Courses;
        User CurrentUser;
        string i;
        public Home(User CurrentUser)
        {
            this.i = ((Student)CurrentUser).Id;
            this.CurrentUser = ((Student)CurrentUser);
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {//Get List Of all Courses button clicked
            listBox1.Items.Clear();
            List<Course> cs = Course.GetAllCourses();
            foreach (var item in cs)
            {
                listBox1.Items.Add(item.ToString());
            }
        }
        
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CourseName = listBox1.Text.TrimEnd();
            int index = listBox1.SelectedIndex;
            Course SelectedCourse = new Course();
            SelectedCourse= SelectedCourse.DetailedDescription(index,CourseName);
            
            CourseDescription cd = new CourseDescription(CurrentUser,SelectedCourse);
            
            cd.Show();

        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Course c = new Course();
            List<Course> cs = c.GetAuditedCourse(this.i);
            foreach (var item in cs)
            {
                listBox1.Items.Add(item.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Course c = new Course();
            
            List<Course> cs = c.GetPurchasedCourse(this.i);
            foreach (var item in cs)
            {
                listBox1.Items.Add(item.ToString());
            }
        }

    }

}
