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
        public WatchResourse(string fullpath,string type,string CourseId,string UserId,string item)
        {
            this.Size = new Size(750,500);
            this.CourseId = CourseId;
            this.UserId = UserId;
            this.item = item;
            if (type == "VIDEO")
            {

                FacadeVideoPlayer.AddPlayerAndPlay(this,fullpath);                
            }
            else
            {
                FacadeDocumentViewer.AddDocumentViewerAndView(this,fullpath);
                
            }
            
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
 
            string ProgressSeq = Course.getProgressSeq(CourseId, UserId);
            string[] progArr = ProgressSeq.TrimEnd().Split(',');
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
                int rowsAff = Course.UpdateProgressSeq(ProgressSeq,CourseId,UserId);               
                
                if (rowsAff == 1)
                {
                    MessageBox.Show("Done");
                }
                else
                {
                    MessageBox.Show("Not Done");
                }
                
            }
            else
            {
                MessageBox.Show("Already completed");
            }
            button1.Enabled = false;


        }
    }
}
