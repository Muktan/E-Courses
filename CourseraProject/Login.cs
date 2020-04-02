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
using CourseraProject;
namespace CourseraProject
{
    public partial class Login : Form
    {
        string UserId;
        public Login()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {//login as Student button clicked

            User CurrentUser = new Student();
            CurrentUser.SetUsernamePassword(textBox1.Text,textBox2.Text);
            bool AuthResult = CurrentUser.AuthenticateUser();
            
            if (AuthResult)
            {
                UserId = CurrentUser.Id;
                this.Hide();
                Home h = new Home(CurrentUser);
                h.Show();
            }
            else
            {
                label2.Text = "Wrong username/Password";
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {//register student
            RegisterStudent r = new RegisterStudent();
            r.Show();
        }
        
        private void button3_Click(object sender, EventArgs e)
        {//Login as tutor
            User CurrentUser = new Tutor();
            CurrentUser.SetUsernamePassword(textBox1.Text, textBox2.Text);
            bool AuthResult = CurrentUser.AuthenticateUser();

            if (AuthResult)
            {
                UserId = CurrentUser.Id;
                this.Hide();
                TutorSection h = new TutorSection(CurrentUser);
                h.Show();
            }
            else
            {
                label2.Text = "Wrong username/Password";
            }
        }
        
        private void button4_Click(object sender, EventArgs e)
        {//register tutor
            TutorRegistration r = new TutorRegistration();
            r.Show();
        }
        
    }
}
