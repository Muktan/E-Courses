using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseraProject
{
    public partial class TutorSection : Form
    {
        string UserId;
        User CurrentUser;
        public TutorSection(User CurrentUser)
        {
            this.UserId = CurrentUser.Id;
            this.CurrentUser = CurrentUser;
            InitializeComponent();
        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            UploadForm f2 = new UploadForm();
            f2.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditCourse ec = new EditCourse();
            ec.Show();

        }
    }
}
