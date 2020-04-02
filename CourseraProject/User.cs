using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseraProject
{
    public abstract class  User
    {
        public string Id;
        public string Username;
        public string Password;
        public abstract bool AuthenticateUser();
        public abstract void SetUsernamePassword(string Username, string Password);
        public abstract bool RegisterUser();
        
    }

    public class Student : User
    {
        public new string Id;
        public new string Username;
        public new string Password;
        public string Email;
        public string MobileNumber;
        public string Address;
        public string Country;
        public string DOB;


        public override void SetUsernamePassword(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }
        public override bool AuthenticateUser()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT * FROM User_Details where Username='" + this.Username + "' and Password = '" + this.Password + "'", con);
            con.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();
            bool AuthResult = dataReader.HasRows;
            if (AuthResult)
            {
                dataReader.Read();
                this.Id = dataReader["Id"].ToString();
            }
            dataReader.Close();
            cmd.Dispose();
            con.Close();
            return AuthResult;
        }
        public override bool RegisterUser()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("Insert into User_Details(Username,Password,Email,MobileNumber,Address,Country,DOB) values('" + Username + "','" + Password + "','" + Email + "','" + MobileNumber + "','" + Address + "','" + Country + "','" + DOB + "' )", con);
            con.Open();

            int aff = cmd.ExecuteNonQuery();
            if (aff > 0)
            {
                return true;
            }
            else
            {
                return false;

            }
            cmd.Dispose();
            con.Close();
        }
        public void SetDetails(string Username, string Password, string Email, string MobileNumber, string Address, string Country, string DOB)
        {
            this.Address = Address;
            this.Country = Country;
            this.DOB = DOB;
            this.Email = Email;
            this.MobileNumber = MobileNumber;
            this.Password = Password;
            this.Username = Username;

        }
        public bool hasUserAdoptedSelectedCourse(Course SelectedCourse)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM UserHistoryProgress where CourseId = '" + SelectedCourse.Id + "' and UserId = '" + this.Id + "'", connection);
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }
            dataReader.Close();
            command.Dispose();
            connection.Close();
        }
        public bool isAudited(Course SelectedCourse)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM UserHistoryProgress where CourseId = '" + SelectedCourse.Id + "' and UserId = '" + this.Id + "'", connection);
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                dataReader.Read();
                if (dataReader["isAudited"].ToString() == "0")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            dataReader.Close();
            command.Dispose();
            connection.Close();
        }
        public string auditCourse(Course SelectedCourse)
        {
            SqlConnection con1 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
            con1.Open();
            SqlCommand cmd2 = new SqlCommand("UPDATE UserHistoryProgress SET isAudited = 1 WHERE CourseId = " + SelectedCourse.Id + " and UserId = " + this.Id, con1);

            int rowsAffected = cmd2.ExecuteNonQuery();
            cmd2.Dispose();
            con1.Close();
            return rowsAffected.ToString();
        }
        public string insertAuditEntry(Course SelectedCourse)
        {
            SqlConnection con2 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
            con2.Open();
            SqlCommand cmd3 = new SqlCommand("Insert into UserHistoryProgress(CourseId, UserId, isAudited,isCompleted,Progress) Values(" + SelectedCourse.Id + "," + this.Id + ",1,0,'')", con2);

            int rowsAffected = cmd3.ExecuteNonQuery();
            cmd3.Dispose();
            con2.Close();
            return rowsAffected.ToString();

        }
        public string updatePaymentEntry(Course SelectedCourse)
        {
            SqlConnection con1 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
            con1.Open();
            SqlCommand cmd2 = new SqlCommand("UPDATE UserHistoryProgress SET isAudited = 0,PaymentId = 1 WHERE CourseId = " + SelectedCourse.Id + " and UserId = " + this.Id, con1);
            int rowsAffected = cmd2.ExecuteNonQuery();
            cmd2.Dispose();
            con1.Close();
            return rowsAffected.ToString();
        }
        public string insertPaymentEntry(Course SelectedCourse)
        {
            SqlConnection con2 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
            con2.Open();
            SqlCommand cmd3 = new SqlCommand("Insert into UserHistoryProgress(CourseId, UserId, isAudited,PaymentId,isCompleted,Progress) Values(" + SelectedCourse.Id + "," + this.Id + ",0,1,0,0)", con2);

            int rowsAffected = cmd3.ExecuteNonQuery();
            cmd3.Dispose();
            con2.Close();
            return rowsAffected.ToString();
        }
    }
    public class Tutor : User
    {
        public new string Id;
        public new string Username;
        public new string Password;
        public string Email;
        public string MobileNumber;
        public string Address;
        public string Country;
        public string DOB;
        public string OrgId;

        public override void SetUsernamePassword(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }
        public override bool AuthenticateUser()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT * FROM Tutor_Details where Tutor_Name='" + this.Username + "' and Password = '" + this.Password + "'", con);
            con.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();
            bool AuthResult = dataReader.HasRows;
            if (AuthResult)
            {
                dataReader.Read();
                this.Id = dataReader["Id"].ToString();
            }
            dataReader.Close();
            cmd.Dispose();
            con.Close();
            return AuthResult;
        }
        public override bool RegisterUser()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("Insert into Tutor_Details(Tutor_Name,Password,Email,MobileNumber,Address,Country,DOB,OrganisationId) values('" + Username+ "','" + Password+ "','" + Email+ "','" + MobileNumber+ "','" + Address+ "','" + Country+ "','" + DOB+ "','" + OrgId+ "' )", con);
            con.Open();

            int aff = cmd.ExecuteNonQuery();
            if (aff > 0)
            {
                return true;
                
            }
            else
            {
                return false;
            }
            cmd.Dispose();
            con.Close();
        }
        public void SetDetails(string Username,string Password,string Email,string MobileNumber,string Address,string Country,string DOB,string OrgId)
        {
            this.Address = Address;
            this.Country = Country;
            this.DOB = DOB;
            this.Email = Email;
            this.MobileNumber = MobileNumber;
            this.Password = Password;
            this.Username = Username;
            this.OrgId = OrgId;
        }
    }

}
