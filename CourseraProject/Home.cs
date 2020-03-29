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
        //creating one user
        string UserId = "1";



        public Home()
        {
            InitializeComponent();
        }




        public List<Course> GetAllCourses()
        {
            if (Courses == null)
            {
                Courses = new List<Course>();
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("SELECT * FROM Courses", con);
                con.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {

                    Course c = new Course();
                    
                    c.Id = (int)dataReader.GetValue(0);
                    c.CourseName = (string)dataReader.GetValue(1);
                    c.OfferedBy = (string)dataReader.GetValue(2);
                    c.CourseDescription = (string)dataReader.GetValue(3);
                    c.CourseContent = (string)dataReader.GetValue(4);

                    Courses.Add(c);

                }
                dataReader.Close();
                cmd.Dispose();
                con.Close();
                
            }
            return Courses;
        }




        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'courseraDataSet1.Courses' table. You can move, or remove it, as needed.
            

        }







        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            List<Course> cs = GetCourses("ALL");
            int x = 250;
            int y = 150;
            foreach (var item in cs)
            {
                
                listBox1.Items.Add(item.ToString());
            }

        }



        public string TheValue
        {
            get;
            set;
        }




        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            TheValue = listBox1.Text;
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
            string s = "SELECT * FROM Courses,Course_Tutor,Tutor_Details where CourseName = '"+TheValue+"' and Courses.Id = CourseId and Tutor_Details.Id = TutorId";

            SqlCommand cmd = new SqlCommand(s, con);
            con.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();
            string Tutors = "";
            while (dataReader.Read())
            {
                Tutors += (((string)dataReader["Tutor_Name"]).TrimEnd()+" ");

            }
            dataReader.Close();
            s = "SELECT Id,CourseDescription,Rating,Price FROM Courses where CourseName = '"+TheValue+"'";
            cmd = new SqlCommand(s, con);
            dataReader = cmd.ExecuteReader();
            string Desc = "";
            string Rating = "";
            string Price = "";
            string CourseId = "";
            while (dataReader.Read())
            {
                Desc += (((string)dataReader["CourseDescription"]).TrimEnd() + " ");
                Rating += ((dataReader["Rating"].ToString()).TrimEnd() + " ");
                Price += ((dataReader["Price"].ToString()).TrimEnd() + " ");
                CourseId += ((dataReader["Id"].ToString()).TrimEnd() + " ");
            }


            dataReader.Close();
            //SELECT* FROM Courses,Organization where Organization.Id = OrganisationId and CourseName = 'Tafl'
            s = "SELECT * FROM Courses,Organization where Organization.Id = OrganisationId and CourseName = '"+TheValue+"'";
            cmd = new SqlCommand(s, con);
            dataReader = cmd.ExecuteReader();
            string Org = "";
            while (dataReader.Read())
            {
                Org += (((string)dataReader["OrgName"]).TrimEnd() + " ");
            }








            dataReader.Close();

            cmd.Dispose();
            con.Close();
            // thevalue is the name of the course
            CourseDescription cd = new CourseDescription(UserId,CourseId,TheValue,Tutors,Desc,Org,Rating,Price);
            
            cd.Show();

        }





        public List<Course> GetCourses(string type)
        {

            Courses = new List<Course>();
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT * FROM Courses", con);
            if (type == "AUDITED")
            {
                cmd = new SqlCommand("SELECT * FROM Courses,UserHistoryProgress where CourseId = Courses.Id and UserId = 1 and isAudited = 1", con);
            }
            else if (type == "PURCHASED")
            {
                cmd = new SqlCommand("SELECT * FROM Courses,UserHistoryProgress where CourseId = Courses.Id and UserId = 1 and PaymentId IS NOT NULL;", con);
            }
            con.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {

                Course c = new Course();

                c.Id = (int)dataReader.GetValue(0);
                c.CourseName = (string)dataReader.GetValue(1);
                try
                {
                    c.OfferedBy = (string)dataReader.GetValue(2);
                }
                catch
                {
                    c.OfferedBy = "No Organization";
                }
                
                c.CourseDescription = (string)dataReader.GetValue(3);
                c.CourseContent = (string)dataReader.GetValue(4);

                Courses.Add(c);

            }
            dataReader.Close();
            cmd.Dispose();
            con.Close();


            
            return Courses;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            List<Course> cs = GetCourses("AUDITED");
            int x = 250;
            int y = 150;
            foreach (var item in cs)
            {

                listBox1.Items.Add(item.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            List<Course> cs = GetCourses("PURCHASED");
            int x = 250;
            int y = 150;
            foreach (var item in cs)
            {

                listBox1.Items.Add(item.ToString());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TutorSection ts = new TutorSection();
            ts.Show();
        }
    }




    public class Course
    {
        public int Id;
        public string CourseName;
        public string OfferedBy;
        public string CourseDescription;
        public string CourseContent;
        
        override
        public string ToString()
        {
            string str = this.CourseName;
            return str;
        }
    }



}
