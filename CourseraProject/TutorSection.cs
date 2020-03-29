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
        public TutorSection(string UserId)
        {
            this.UserId = UserId;
            InitializeComponent();
        }
        

        private void button1_Click_1(object sender, EventArgs e)
        {

            UploadForm f2 = new UploadForm();
            f2.ShowDialog();
        }
    }
}
