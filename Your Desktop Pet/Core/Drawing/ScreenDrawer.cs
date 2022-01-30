using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Your_Desktop_Pet.Core.Lua;

namespace Your_Desktop_Pet.Core.Drawing
{
    static class ScreenDrawer
    {
        private static IntPtr _hdc;
        private static Graphics g;

        private const int DCX_WINDOW = 0x00000001;
        private const int DCX_CACHE = 0x00000002;
        private const int DCX_LOCKWINDOWUPDATE = 0x00000400;

        [DllImport("user32.dll")]
        private static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetDCEx(IntPtr hwnd, IntPtr hrgn, uint flags);

        public static void InitDraw()
        {
            _hdc = GetDCEx(GetDesktopWindow(),
                                 IntPtr.Zero,
                                 DCX_WINDOW | DCX_CACHE | DCX_LOCKWINDOWUPDATE);

            g = Graphics.FromHdc(_hdc);
        }

        public static void DrawRectangleWireframe(Table rect, Table color)
        {
            g.DrawRectangle(new Pen(LuaHelper.TableToRGB(color)), LuaHelper.TableToRect(rect));
        }

        public static void DrawRectangle(Table rect, Table color)
        {
            g.FillRectangle(new SolidBrush(LuaHelper.TableToRGB(color)), LuaHelper.TableToRect(rect));
        }

        public static void DrawEllipseWireframe(Table circle, Table color)
        {
            g.DrawEllipse(new Pen(LuaHelper.TableToRGB(color)), LuaHelper.TableToRect(circle));
        }

        public static void DrawCircle(Table circle, Table color)
        {
            g.FillEllipse(new SolidBrush(LuaHelper.TableToRGB(color)), LuaHelper.CircleToRect(LuaHelper.TableToRect(circle)));
        }

        public static void TerminateDraw()
        {
            if (_hdc != IntPtr.Zero)
            {
                g.Dispose();
                _hdc = IntPtr.Zero;
            }
        }
    }
}
