using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Your_Desktop_Pet.Core.Drawing
{
    static class ScreenDrawer
    {
        private static Graphics _g;
        private static IntPtr desktopDC = IntPtr.Zero;

        [DllImport("User32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("User32.dll")]
        private static extern void ReleaseDC(IntPtr dc);

        public static void Instantiate()
        {
            
        }

        public static void Dispose()
        {
            
        }

        public static void Draw()
        {
            desktopDC = GetDC(IntPtr.Zero);
            _g = Graphics.FromHdc(desktopDC);

            _g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, 64, 64));

            _g.Dispose();
            ReleaseDC(desktopDC);
        }
    }
}
