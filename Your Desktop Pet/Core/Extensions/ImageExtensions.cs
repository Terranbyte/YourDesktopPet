using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Your_Desktop_Pet
{
    public static class ImageExtensions
    {
        public static Image Resize(this Image source, Size to, InterpolationMode interpMode = InterpolationMode.NearestNeighbor)
        {
            Image resized = new Bitmap(to.Width, to.Height);

            using (Graphics g = Graphics.FromImage(resized))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.DrawImage(source, new Rectangle(0, 0, resized.Width, resized.Height), new Rectangle(0, 0, source.Width, source.Height), GraphicsUnit.Pixel);
                g.Save();
            }

            source.Dispose();

            return resized;
        }
    }
}
