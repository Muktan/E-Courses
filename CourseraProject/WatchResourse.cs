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
    public partial class WatchResourse : Form
    {
        string UserId = "1";
        string CourseId ="";
        string item;
        public WatchResourse(string fullpath,string type,string CourseId,string item)
        {
            this.Size = new Size(750,500);
            this.CourseId = CourseId;
            this.item = item;
            if (type == "VIDEO")
            {
               
                AxWMPLib.AxWindowsMediaPlayer mp = new AxWMPLib.AxWindowsMediaPlayer();
                
                ((System.ComponentModel.ISupportInitialize)(mp)).BeginInit();
                mp.Name = "wmPlayer";
                mp.Enabled = true;
                mp.Dock = System.Windows.Forms.DockStyle.None;
                
                
                this.Controls.Add(mp);
                ((System.ComponentModel.ISupportInitialize)(mp)).EndInit();

                // After initialization you can customize the Media Player
                //mp.uiMode = "none";
                mp.Location = new Point(12, 32);
                mp.Margin = new Padding(12);
                //776, 426
                mp.Size = new Size(700,400);
                mp.URL = fullpath;
                mp.Ctlcontrols.play();
                
            }
            else
            {
                AxAcroPDFLib.AxAcroPDF dr = new AxAcroPDFLib.AxAcroPDF();
                //dr.CreateControl();
                //dr.LoadFile(fullpath);
                //dr.src = fullpath;
                //dr.Location = new Point(12, 12);
                //dr.Size = new Size(776, 426);
                //dr.Visible = true;
                //dr.Show();
                //this.Controls.Add(dr);
                ((System.ComponentModel.ISupportInitialize)(dr)).BeginInit();
                dr.Dock = System.Windows.Forms.DockStyle.None;
                this.Controls.Add(dr);
                ((System.ComponentModel.ISupportInitialize)(dr)).EndInit();
                dr.Width = this.Width;
                dr.Height = this.Height;
                dr.Location = new Point(12,42);
                dr.LoadFile(fullpath);
                dr.Show();
            }
            
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT Progress FROM UserHistoryProgress where CourseId = '"+CourseId+"' and UserId = '"+UserId+"'", con);
            con.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();
            string ProgressSeq = "";
            while (dataReader.Read())
            {
                ProgressSeq += dataReader["Progress"];
            }
            ProgressSeq = ProgressSeq.TrimEnd();
            string[] progArr = ProgressSeq.TrimEnd().Split(',');
            dataReader.Close();
            cmd.Dispose();
            con.Close();
            if (!progArr.Contains(item))
            {

                if (ProgressSeq.TrimEnd() == "")
                {
                    ProgressSeq = item;
                }
                else
                {
                    ProgressSeq += "," + item;
                }
                
                SqlConnection con2 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Coursera.mdf;Initial Catalog=CourseraNew;Integrated Security=True");
                string s = "Update UserHistoryProgress set Progress = '" + ProgressSeq + "' where UserId = '" + UserId + "' and CourseId = '" + CourseId + "'";
                string str = s;
                SqlCommand cmd2 = new SqlCommand(str, con);
                con.Open();
                int rowsAff = cmd2.ExecuteNonQuery();
                if (rowsAff == 1)
                {
                    MessageBox.Show("Done");
                }
                else
                {
                    MessageBox.Show("Not Done");
                }
                cmd.Dispose();
                con.Close();
            }
            else
            {
                MessageBox.Show("Already completed");
            }
            button1.Enabled = false;


        }
    }
}
