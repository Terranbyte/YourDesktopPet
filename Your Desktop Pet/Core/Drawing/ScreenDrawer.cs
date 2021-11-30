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
        private static IntPtr _desktopDC = IntPtr.Zero;

        [DllImport("User32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("User32.dll")]
        private static extern void ReleaseDC(IntPtr dc);

        public static void Instantiate()
        {
            _desktopDC = GetDC(IntPtr.Zero);
            _g = Graphics.FromHdc(_desktopDC);
        }

        public static void Dispose()
        {
            _g.Dispose();
            ReleaseDC(_desktopDC);
        }

        public static void Draw()
        {
            KeyValuePair<string, Rectangle>[] windows = Helpers.DesktopWindows.GetAllWindowBounds(false, false);

            Pen p = new Pen(Color.Red);

            foreach (KeyValuePair<string, Rectangle> window in windows)
            {
                _g.DrawRectangle(p, window.Value);
            }

        }
    }
}
