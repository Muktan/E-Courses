using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseraProject
{
    class FacadeVideoPlayer
    {
        public static void AddPlayerAndPlay(Form form,string fullpath)
        {
            AxWMPLib.AxWindowsMediaPlayer mp = new AxWMPLib.AxWindowsMediaPlayer();

            ((System.ComponentModel.ISupportInitialize)(mp)).BeginInit();
            mp.Name = "wmPlayer";
            mp.Enabled = true;
            mp.Dock = System.Windows.Forms.DockStyle.None;


            form.Controls.Add(mp);
            ((System.ComponentModel.ISupportInitialize)(mp)).EndInit();

            // After initialization you can customize the Media Player
            //mp.uiMode = "none";
            mp.Location = new Point(12, 32);
            mp.Margin = new Padding(12);
            //776, 426
            mp.Size = new Size(700, 400);
            mp.URL = fullpath;
            mp.Ctlcontrols.play();
        }
    }
}
