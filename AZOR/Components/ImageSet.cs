using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AZOR
{
    class ImageSet
    {
        /// <summary>
        /// Reset the image size for center.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Image ResetImageSize(Image img, Size s)
        {
            //create new image with the size given.
            Image newImage = new Bitmap(s.Width, s.Height);
            //draw a new image with the size
            using (Graphics graphics = Graphics.FromImage((Bitmap)newImage))
            {
                graphics.DrawImage(img, new Rectangle(Point.Empty, s));
            }
            return newImage;
        }
    }
}
