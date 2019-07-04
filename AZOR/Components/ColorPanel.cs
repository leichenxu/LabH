using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AZOR
{
    public partial class ColorPanel
    {
        #region//////////////////////lista de colores/////////////////////////////////////////////////////


        private static Color[] colorList =
         {
                Color.Cyan,Color.Magenta,Color.Yellow,Color.White,
                Color.Red,Color.Green,Color.Blue,Color.Black
        };

        #endregion

        /// <summary>
        /// Panel display picture box.
        /// </summary>
        private Panel panelColour;       
        /// <summary>
        /// Margin between colour and picture box.
        /// </summary>
        private int LR_Interval = 2;
        /// <summary>
        /// Margin between picture box
        /// </summary>
        private int Pic_Interval = 4;
        /// <summary>
        /// Selected colour.
        /// </summary>
        private Color SelectColor;



        /// <summary>
        /// Constuctor.
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="label"></param>
        public ColorPanel(Panel panel)
        {
            panelColour = panel;
            CreatPicture();
        }


        /// <summary>
        /// Create the pickturebox inside the panel.
        /// </summary>
        private void CreatPicture()
        {
            //calculate picture box widht and height
            int width = (panelColour.Width - 8 * Pic_Interval - 2 * LR_Interval - 5) / 8;
            int height = width - 3 + 1;
            //add 8 colours
            for (int j = 0; j < 8; j++)
            {
                //create and adjust
                PictureBox PB = new PictureBox();
                PB.Size = new Size(width, height);
                PB.Location =
                new Point(LR_Interval + Pic_Interval + j *
                (PB.Size.Width + Pic_Interval) - 2, 0);
                PB.BackColor = colorList[j];
                PB.BorderStyle = BorderStyle.Fixed3D;
                PB.Click += new EventHandler(PB_Click);
                PB.Validated += new EventHandler(PB_Validated);
                panelColour.Controls.Add(PB);
            }
        }
        /// <summary>
        /// Draw line when the picture box is validated, and not draw line when this is not validated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PB_Validated(object sender, EventArgs e)
        {
            PictureBox PB = ((PictureBox)sender);
            Graphics graphic = panelColour.CreateGraphics();
            //calulate the points then draw it
            Point[] point = new Point[5];
            point[0] = new Point(PB.Location.X - 1, PB.Location.Y - 1);
            point[1] = new Point(PB.Location.X - 1, PB.Location.Y + PB.Height);
            point[2] = new Point(PB.Location.X + PB.Width, PB.Location.Y + PB.Height);
            point[3] = new Point(PB.Location.X + PB.Width, PB.Location.Y - 1);
            point[4] = new Point(PB.Location.X - 1, PB.Location.Y - 1);
            graphic.DrawLines(new Pen(Color.White), point);


            Pen pen = new Pen(Color.White, 1);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            pen.DashPattern = new float[] { 1, 1 };


            Point[] point2 = new Point[5];
            point2[0] = new Point(PB.Location.X - 3, PB.Location.Y - 3);
            point2[1] = new Point(PB.Location.X - 3, PB.Location.Y + PB.Height + 2);
            point2[2] = new Point(PB.Location.X + PB.Width + 2, PB.Location.Y + PB.Height + 2);
            point2[3] = new Point(PB.Location.X + PB.Width + 2, PB.Location.Y - 3);
            point2[4] = new Point(PB.Location.X - 3, PB.Location.Y - 3);
            graphic.DrawLines(pen, point2);


        }
        /// <summary>
        /// When picture is clicked select it, drawn line and store the colour.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PB_Click(object sender, EventArgs e)
        {
            PictureBox PB = ((PictureBox)sender);
            Graphics graphic = panelColour.CreateGraphics();
            //calulate the points then draw it
            Point[] point = new Point[5];
            point[0] = new Point(PB.Location.X - 1, PB.Location.Y - 1);
            point[1] = new Point(PB.Location.X - 1, PB.Location.Y + PB.Height);
            point[2] = new Point(PB.Location.X + PB.Width, PB.Location.Y + PB.Height);
            point[3] = new Point(PB.Location.X + PB.Width, PB.Location.Y - 1);
            point[4] = new Point(PB.Location.X - 1, PB.Location.Y - 1);
            graphic.DrawLines(new Pen(Color.Black), point);
            Pen pen = new Pen(Color.Black, 1);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            pen.DashPattern = new float[] { 1, 1 };
            Point[] point2 = new Point[5];
            point2[0] = new Point(PB.Location.X - 3, PB.Location.Y - 3);
            point2[1] = new Point(PB.Location.X - 3, PB.Location.Y + PB.Height + 2);
            point2[2] = new Point(PB.Location.X + PB.Width + 2, PB.Location.Y + PB.Height + 2);
            point2[3] = new Point(PB.Location.X + PB.Width + 2, PB.Location.Y - 3);
            point2[4] = new Point(PB.Location.X - 3, PB.Location.Y - 3);
            graphic.DrawLines(pen, point2);
            PB.Focus();
            //store the colour
            SelectColor = PB.BackColor;
        }

        /// <summary>
        /// Get colour+set colour.
        /// </summary>
        public Color GetColor
        {
            get { if (!SelectColor.IsEmpty) return SelectColor; return Color.Black; }
            set { SelectColor = value; }
        }
     
    }
}
