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
        string SelectedCourseName;
        string UserId;
        string CourseId;
        User CurrentUser;
        Course SelectedCourse;
        public CourseDescription(User CurrentUser,Course SelectedCourse)
        {

            InitializeComponent();
            this.UserId = CurrentUser.Id;
            this.CourseId = SelectedCourse.Id.ToString();
            label1.Text = SelectedCourse.CourseName;
            SelectedCourseName = SelectedCourse.CourseName;
            label2.Text += "Muktan";
            label3.Text += "DDU";
            label4.Text += SelectedCourse.CourseDescription;
            label5.Text += "4.4";
            label6.Text += SelectedCourse.Price;
            this.CurrentUser = CurrentUser;
            this.SelectedCourse = SelectedCourse;
            bool UserAdoptedSelectedCourse = ((Student)CurrentUser).hasUserAdoptedSelectedCourse(SelectedCourse);
            bool Audited = ((Student)CurrentUser).isAudited(SelectedCourse);
            if (UserAdoptedSelectedCourse)
            {
                button3.Enabled = true;
                if (Audited)
                {
                    button2.Text = "Purchased";
                    button2.Enabled = false;
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
            bool UserAdoptedSelectedCourse = ((Student)this.CurrentUser).hasUserAdoptedSelectedCourse(SelectedCourse);
            if (UserAdoptedSelectedCourse)
            {
                string rowsAffected = ((Student)CurrentUser).auditCourse(SelectedCourse);
                if (rowsAffected == "1")
                {
                    MessageBox.Show("Updated Successfully");
                }
                else
                {
                    MessageBox.Show("Updated Unsuccessfully");
                }
                this.Close();
            }
            else
            {
                string rowsAffected = ((Student)CurrentUser).insertAuditEntry(SelectedCourse);
                if (rowsAffected == "1")
                {
                    MessageBox.Show("Updated Successfully");
                }
                else
                {
                    MessageBox.Show("Updated Unsuccessfully");
                }
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //now once we click on buy button we set is audited to false
            button2.Enabled = false;
            button1.Enabled = false;
            bool UserAdoptedSelectedCourse = ((Student)CurrentUser).hasUserAdoptedSelectedCourse(SelectedCourse);
            if (UserAdoptedSelectedCourse)
            {
                
                string paymentDone = ((Student)CurrentUser).updatePaymentEntry(SelectedCourse);
                if (paymentDone == "1")
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
                string rowsAffected = ((Student)CurrentUser).insertPaymentEntry(SelectedCourse);
                
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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            CourseContent cc = new CourseContent(CourseId,((Student)CurrentUser).Id);
            cc.Show();
        }
    }
}
