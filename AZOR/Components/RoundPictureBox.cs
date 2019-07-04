using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AZOR
{ 
    public class RoundPictureBox:PictureBox
    {
        public RoundPictureBox(Color backColor)
        {
            this.BackColor = backColor;
        }
        protected override void OnPaint(PaintEventArgs pe)
        {           
            GraphicsPath g = new GraphicsPath();
            g.AddEllipse(0,0,ClientSize.Width,ClientSize.Height/2);
            Region = new System.Drawing.Region(g);
            base.OnPaint(pe);
        }
    }
}
