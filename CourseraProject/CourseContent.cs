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
    public partial class CourseContent : Form
    {
        string UserId = "1";
        public CourseContent(string CourseId)
        {
            InitializeComponent();
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT CourseSequence,Progress FROM Courses,UserHistoryProgress where Courses.Id = '" + CourseId+"' and Courses.Id = CourseId", con);
            con.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();
            string CourseSeq ="";
            string ProgressSeq = "";
            while (dataReader.Read())
            {
                CourseSeq += dataReader["CourseSequence"];
                ProgressSeq += dataReader["Progress"];
            }
            string[] arr = CourseSeq.TrimEnd().Split(',');

            string[] progArr = ProgressSeq.TrimEnd().Split(',');
            int CourseLen = arr.Length;
            int ProgLen;
            if (ProgressSeq.TrimEnd() == "")
            {
                ProgLen = 0;
            }
            else
            {
                ProgLen = progArr.Length;
            }
            float ProgPer;
            ProgPer = ((float)ProgLen / (float)CourseLen);
            ProgPer = ProgPer * (float)100;
            label1.Text += ((int)ProgPer).ToString() + "%";
            int x = 20;
            int y = 50;
            string init;
            init = @"D:\0_Drive_E\SEM-6\OOSE\Github_coursera\E-Courses\CourseraProject\Resources";
            foreach (var item in arr)
            {
                try
                {
                    Label l = new Label();
                    Button b = new Button();
                    l.Width = 550;
                    l.Location = new Point(x, y);
                    //_______________________________________________________________

                    
                    b.Name = item;
                    b.Location = new Point(x + 600, y);
                    
                    //________________________________________________________________

                    string path;
                    string fullpath;

                    //_________________________________________________________________
                    if (item.StartsWith("v"))
                    {
                        b.Text = "Watch";
                        path = CourseId.TrimEnd() + "\\" + "VIDEO";
                        fullpath = init + "\\" + path;
                    }
                    else
                    {
                        b.Text = "Read";
                        path = CourseId.TrimEnd() + "\\" + "DOC";
                        fullpath = init + "\\" + path;
                    }
                    

                    
                    DirectoryInfo d = new DirectoryInfo(fullpath);//Assuming Test is your Folder
                    
                    FileInfo[] Files;
                    if (item.StartsWith("v"))
                    {
                        Files = d.GetFiles("*.mp4"); //Getting Text files
                    }
                    else
                    {
                        Files = d.GetFiles("*.pdf"); //Getting Text files
                    }
                    
                    string str = "";
                    foreach (FileInfo file in Files)
                    {
                        if (file.Name.StartsWith(item))
                        {
                            str += file.Name;
                        }
                        
                    }
                    b.Click += (sender, e) =>
                    {
                        if (item.StartsWith("v"))
                        {
                            WatchResourse wr = new WatchResourse(fullpath + "\\" + str,"VIDEO",CourseId,item);
                            wr.Show();
                        }
                        else
                        {
                            WatchResourse wr = new WatchResourse(fullpath + "\\" + str, "DOC",CourseId,item);
                            wr.Show();
                        }
                        
                        
                    };
                    l.Text = str.Substring(3, str.Length - 7);
                    y += 30;
                    
                    this.Controls.Add(l);
                    this.Controls.Add(b);
                }
                catch
                {
                    continue;
                }
            }
            dataReader.Close();
            cmd.Dispose();
            con.Close();

        }

    }
}
