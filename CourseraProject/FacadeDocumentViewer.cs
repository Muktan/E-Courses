using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseraProject
{
    class FacadeDocumentViewer
    {
        public static void AddDocumentViewerAndView(Form form,string fullpath)
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
            form.Controls.Add(dr);
            ((System.ComponentModel.ISupportInitialize)(dr)).EndInit();
            dr.Width = form.Width;
            dr.Height = form.Height;
            dr.Location = new System.Drawing.Point(12, 42);
            dr.LoadFile(fullpath);
            dr.Show();
        }
    }
}
