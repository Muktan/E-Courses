using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseraProject
{
    public partial class UploadContent : Form
    {
        int Id;
        string dataentry = "";
        int textfile = 0;
        int videofile = 0;
        public UploadContent(int Id)
        {
            this.Id = Id;
            InitializeComponent();
            //comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string path = @"D:\0_Drive_E\SEM-6\OOSE\CourseraProject\CourseraProject\Resources\" + Id.ToString();
            OpenFileDialog op1 = new OpenFileDialog();
            if (comboBox1.SelectedIndex == 0)
                op1.Filter = "allfiles|*.pdf";
            else if (comboBox1.SelectedIndex == 1)
                op1.Filter = "allfiles|*.mp4";
            op1.ShowDialog();
            string s = op1.FileName;
            if (comboBox1.SelectedIndex == 0)
            {
                //op1.Filter= "allfiles|*.txt"; 
                File.Copy(s, path + "\\DOC\\t" + textfile.ToString() + "_"+op1.FileName+".pdf");
                dataentry += 't' + textfile.ToString() + ',';
                textfile++;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                //op1.Filter = "allfiles|*.mp4";
                File.Copy(s, path + "\\VIDEO\\v" + videofile.ToString() +"_" + op1.FileName + ".mp4");
                dataentry += 'v' + videofile.ToString() + ',';
                videofile++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Course.UpdateCourseSequence(dataentry.Substring(0, dataentry.Length - 1),Id);
            MessageBox.Show("Your Course is successfully uploaded!\n" +
                    "Thank you for using COURSERA!");
            this.Hide();
        }

        private void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string path = @"D:\0_Drive_E\SEM-6\OOSE\Github_coursera\E-Courses\CourseraProject\Resources\" + Id.ToString();
            //string path = @"D:\0_Drive_E\SEM-6\OOSE\CourseraProject\CourseraProject\Resources";
            OpenFileDialog op1 = new OpenFileDialog();
            if (comboBox1.SelectedIndex == 0) {
                op1.Filter = "allfiles|*.pdf";
                path = path + "\\DOC";
                Directory.CreateDirectory(path);
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                op1.Filter = "allfiles|*.mp4";
                path = path + "\\VIDEO";
                Directory.CreateDirectory(path);
            }
                
            op1.ShowDialog();
            string s = op1.FileName;
            if (comboBox1.SelectedIndex == 0)
            {
                string fpath = path + "\\t" + textfile.ToString() + "_" + op1.SafeFileName;
                File.Copy(s, fpath);
                dataentry += 't' + textfile.ToString() + ',';
                textfile++;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                string fpath = path + "\\v" + videofile.ToString() + "_" + op1.SafeFileName;
                File.Copy(s, fpath);
                dataentry += 'v' + videofile.ToString() + ',';
                videofile++;
            }
        }
    }
}
