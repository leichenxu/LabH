using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AZOR
{
    public class CircularArrowDraw
    {
        private Control controlToPaint;
        private Timer timerDrawCircular= new Timer();
        private int startAngleDrawOne = 90;
        private int startAngleDrawTwo = 270;
        private readonly Color penColor;
        /// <summary>
        /// The angule chosed.
        /// </summary>
        private readonly int angle = 190;
        public CircularArrowDraw(Control control,Color color)
        {
            control.ForeColor = color;
            control.Paint += PaintCircularImage;
            controlToPaint = control;
            //set the interval
            TimerDrawCircular.Interval = 100;
            penColor = color;
            TimerDrawCircular.Tick += TimerDraw_Tick;
            TimerDrawCircular.Start();
        }
        /// <summary>
        /// Start again the draw animation.
        /// </summary>
        public void StartAgain()
        {            
            controlToPaint.ForeColor = penColor;
            controlToPaint.Paint += PaintCircularImage;
            TimerDrawCircular.Start();
        }
        /// <summary>
        /// When end stop the animation and repaint.
        /// </summary>
        public void EndDraw()
        {
            controlToPaint.Paint -= PaintCircularImage;
            controlToPaint.ForeColor = Color.White;
            controlToPaint.Invalidate();
        }
        public Timer TimerDrawCircular { get => timerDrawCircular; set => timerDrawCircular = value; }

        /// <summary>
        /// Paint it when called.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void PaintCircularImage(object sender, EventArgs eventArgs)
        {
            Control c = sender as Control;
            PaintEventArgs e = eventArgs as PaintEventArgs;
            //color and size of pen
            Pen p = new Pen(penColor, 3)
            {
                //the line chosed
                EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor
            };
            //round location and radius
            int cx = c.ClientSize.Width / 2;
            int cy = c.ClientSize.Height / 2;
            int r = Convert.ToInt32(0.2 * cx);
            e.Graphics.DrawArc(p, cx * 2 - r * 2, r, r, r, startAngleDrawOne, angle);
            e.Graphics.DrawArc(p, cx * 2 - r * 2, r, r, r, startAngleDrawTwo, angle);            
            //timer1.Start();
            /*PaintEventArgs e = eventA as PaintEventArgs;
            Pen p = new Pen(Color.Blue,10);
            p.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            int cx=pictureBox1.ClientSize.Width / 2;
            int cy = pictureBox1.ClientSize.Height / 2;
            int r = Convert.ToInt32(0.8*cx);            
            e.Graphics.DrawArc(p, cx - r, cy - r, 2 * r, 2 * r, startAngleDrawOne, angle);
            e.Graphics.DrawArc(p, cx - r, cy - r, 2 * r, 2 * r, startAngleDrawTwo, angle);
            string a = Convert.ToSingle(200 * (angle) / 360).ToString("f1");
            e.Graphics.DrawString(a, new Font("arial", 14, FontStyle.Bold), Brushes.White, cx - 20, cy - 10);
            timer1.Start();*/
        }

        private void TimerDraw_Tick(object sender, EventArgs e)
        {
            Control c = sender as Control;
            startAngleDrawOne += 10;
            startAngleDrawTwo += 10;
            //draw
            controlToPaint.Invalidate();
        }
    }
}
