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
    public partial class CourseDescription : System.Windows.Forms.Form
    {
        string SelectedCourse;
        string UserId;
        string CourseId;

        public CourseDescription(string UserId,string CourseId,string theValue,string Tutors,string Desc,string Org,string Rating,string Price)
        {

            InitializeComponent();
            this.UserId = UserId;
            this.CourseId = CourseId;
            label1.Text = theValue;
            SelectedCourse = theValue;
            label2.Text += Tutors;
            label3.Text += Desc;
            label4.Text += Org;
            label5.Text += Rating;
            label6.Text += Price;

            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM UserHistoryProgress where CourseId = '" + CourseId + "' and UserId = '" + UserId + "'",connection);
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                button3.Enabled = true;
                dataReader.Read();
                if (dataReader["isAudited"].ToString() == "0")
                {
                    button2.Text = "Purchased";
                    button2.Enabled = false;
                    //if already purchased no need to audit
                    button1.Text = "Audit course";
                    button1.Enabled = false;
                }
                else
                {
                    button1.Text = "Already Audited";
                    button1.Enabled = false;
                }
            }
            else
            {
                button3.Enabled = false;
            }
            command.Dispose();
            connection.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void CourseDescription_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT * FROM UserHistoryProgress where CourseId = '" + CourseId + "' and UserId = '" + UserId + "'", con);
            con.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.HasRows)
            {
                dataReader.Close();
                
                SqlConnection con1 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
                con1.Open();
                SqlCommand cmd2 = new SqlCommand("UPDATE UserHistoryProgress SET isAudited = 1 WHERE CourseId = " + CourseId + " and UserId = " + UserId, con1);
                int rowsAffected = cmd2.ExecuteNonQuery();
                cmd2.Dispose();
                con1.Close();
                
                if (rowsAffected.ToString() == "1")
                {
                    MessageBox.Show("Done");
                }
                else
                {
                    MessageBox.Show("Error");
                }
                this.Close();
            }
            else
            {
                dataReader.Close();
                
                SqlConnection con2 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
                con2.Open();
                SqlCommand cmd3 = new SqlCommand("Insert into UserHistoryProgress(CourseId, UserId, isAudited,isCompleted,Progress) Values("+CourseId+","+UserId+",1,0,'')", con2);

                int rowsAffected = cmd3.ExecuteNonQuery();
                Console.WriteLine(rowsAffected);
                int i = rowsAffected;
                cmd3.Dispose();
                con2.Close();
                if(i.ToString() == "1")
                {
                    MessageBox.Show("Done");
                }
                else
                {
                    MessageBox.Show("Error");
                }
                this.Close();
            }

            cmd.Dispose();
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //now once we click on buy button we set is audited to false
            button2.Enabled = false;
            button1.Enabled = false;

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT * FROM UserHistoryProgress where CourseId = '" + CourseId + "' and UserId = '" + UserId + "'", con);
            con.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.HasRows)
            {
                dataReader.Close();
                
                SqlConnection con1 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
                con1.Open();
                SqlCommand cmd2 = new SqlCommand("UPDATE UserHistoryProgress SET isAudited = 0,PaymentId = 1 WHERE CourseId = " + CourseId + " and UserId = " + UserId, con1);
                int rowsAffected = cmd2.ExecuteNonQuery();
                cmd2.Dispose();
                con1.Close();

                if (rowsAffected.ToString() == "1")
                {
                    MessageBox.Show("Done");
                }
                else
                {
                    MessageBox.Show("Error");
                }
                this.Close();
            }
            else
            {
                dataReader.Close();
                
                SqlConnection con2 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
                con2.Open();
                SqlCommand cmd3 = new SqlCommand("Insert into UserHistoryProgress(CourseId, UserId, isAudited,PaymentId,isCompleted,Progress) Values(" + CourseId + "," + UserId + ",0,1,0,0)", con2);

                int rowsAffected = cmd3.ExecuteNonQuery();
                Console.WriteLine(rowsAffected);
                int i = rowsAffected;
                cmd3.Dispose();
                con2.Close();
                if (i.ToString() == "1")
                {
                    MessageBox.Show("Done");
                }
                else
                {
                    MessageBox.Show("Error");
                }
                this.Close();
            }

            cmd.Dispose();
            con.Close();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            CourseContent cc = new CourseContent(CourseId);
            cc.Show();
        }
    }
}
