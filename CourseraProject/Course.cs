using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseraProject
{
    public class Course
    {
        public int Id;
        public string CourseName;
        public string OfferedBy;
        public string CourseDescription;
        public string CourseContent;
        public string Rating;
        public string Price;
        public string Tutors;
        public  static List<Course> Courses;


        public override string ToString()
        {
            string str = this.CourseName;
            return str;
        }
        public static int getMaxId()
        {
            string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True";
            string query1 = "select max(Id) as Id from Courses;";
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd1 = new SqlCommand(query1, con);
            con.Open();
            SqlDataReader dr = cmd1.ExecuteReader();
            dr.Read();
            int val = (int)dr["Id"];

            con.Close();
            
cmd1.Dispose();
            return (val);
        }
        public static string getProgressSeq(string CourseId,string UserId)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT CourseSequence,Progress FROM Courses,UserHistoryProgress where Courses.Id = '" + CourseId + "' and UserId ='" + UserId + "' and Courses.Id = CourseId", con);
            con.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();
            string ProgressSeq = "";
            if (dataReader.Read())
            {
                ProgressSeq = (string)dataReader["Progress"];
            }
            con.Close();
            cmd.Dispose();
            dataReader.Close();
            return ProgressSeq;
        }
        public static string getCourseSeq(string CourseId, string UserId)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT CourseSequence,Progress FROM Courses,UserHistoryProgress where Courses.Id = '" + CourseId + "' and UserId ='" + UserId + "' and Courses.Id = CourseId", con);
            con.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();
            string CourseSeq = "";
            if (dataReader.Read())
            {
                CourseSeq = (string)dataReader["CourseSequence"];
            }
            con.Close();
            cmd.Dispose();
            dataReader.Close();
            return CourseSeq;
        }
        public static int UpdateProgressSeq(string ProgressSeq,string CourseId,string UserId)
        {
            SqlConnection con2 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
            string s = "Update UserHistoryProgress set Progress = '" + ProgressSeq + "' where UserId = '" + UserId + "' and CourseId = '" + CourseId + "'";
            string str = s;
            SqlCommand cmd2 = new SqlCommand(str, con2);
            con2.Open();
            int rowsAff = cmd2.ExecuteNonQuery();
            cmd2.Dispose();
            con2.Close();
            return rowsAff;
        }
        public static void UpdateCourseSequence(string seq,int CourseId)
        {
            string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True";
            string query = "update Courses set CourseSequence='" + seq + "' where Id='" + CourseId + "';";

            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            
        }
        public static void InsertCourseEntry(int Id,string cname,string desc,int orgid,string date,int sales,int price)
        {
            string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True";
            string asd = DateTime.Now.ToString("MM-dd-yyyy");
            string query = "insert into Courses(Id,CourseName,CourseDescription,OrganisationId,UploadDate,Sales,Price) values('" + Id + "','" + cname + "','" + desc + "','" + (orgid + 1) + "','" + asd + "',0,'" + price + "');";
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            //SqlDataReader myReader;
            int i = cmd.ExecuteNonQuery();
            string path = @"D:\0_Drive_E\SEM-6\OOSE\WindowsFormsApp1\WindowsFormsApp1\Courses\" + Id.ToString();
            System.IO.Directory.CreateDirectory(path);
            UploadContent up = new UploadContent(Id);
            up.ShowDialog();up.ShowDialog();
        }
        public static List<Course> GetAllCourses()
        {
            if (Courses != null)
            {
                return Courses;
            }
            else
            {
                Courses = new List<Course>();
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("SELECT * FROM Courses", con);
                con.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {

                    Course c = new Course();

                    c.Id = (int)dataReader["Id"];
                    c.CourseName = (string)dataReader["CourseName"];
                    try
                    {
                        c.OfferedBy = (string)dataReader["OfferdBy"];
                    }
                    catch
                    {
                        c.OfferedBy = "No Organization";
                    }

                    c.CourseDescription = (string)dataReader["CourseDEscription"];
                    c.CourseContent = (string)dataReader["CourseSequence"];
                    c.Rating = dataReader["Rating"].ToString();
                    c.Price = dataReader["Price"].ToString();
                    Courses.Add(c);

                }
                dataReader.Close();
                cmd.Dispose();
                con.Close();



                return Courses;
            }
            
        }
        public List<Course> GetAuditedCourse(string UserId)
        {
            List<Course> AuditedCourses = new List<Course>();
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
            string id = UserId;
            string cm = "SELECT * FROM Courses,UserHistoryProgress where CourseId = Courses.Id and isAudited = 1 and UserId = " + id ;
            SqlCommand cmd = new SqlCommand(cm, con);
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

                AuditedCourses.Add(c);

            }
            dataReader.Close();
            cmd.Dispose();
            con.Close();
            return AuditedCourses;
        }
        public List<Course> GetPurchasedCourse(string UserId)
        {
            List<Course> AuditedCourses = new List<Course>();
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT * FROM Courses,UserHistoryProgress where CourseId = Courses.Id and UserId = '" + UserId + "' and PaymentId IS NOT NULL;", con);
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

                AuditedCourses.Add(c);

            }
            dataReader.Close();
            cmd.Dispose();
            con.Close();
            return AuditedCourses;
        }
        public Course DetailedDescription(int index,string CourseName)
        {
            Course SelectedCourse = Courses.ElementAt<Course>(index);

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
            string s = "SELECT Tutor_Name FROM Courses,Course_Tutor,Tutor_Details where CourseName = '" + CourseName + "' and Courses.Id = CourseId and Tutor_Details.Id = TutorId";

            SqlCommand cmd = new SqlCommand(s, con);
            con.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();
            string Tutors = "";
            while (dataReader.Read())
            {
                Tutors += (((string)dataReader["Tutor_Name"]).TrimEnd() + " ");

            }
            SelectedCourse.Tutors = Tutors;
            dataReader.Close();
            cmd.Dispose();
            con.Close();
            return SelectedCourse;
        }
    }
}
