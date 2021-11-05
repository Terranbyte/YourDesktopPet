using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

[StructLayout(LayoutKind.Sequential)]
public struct RectInt
{
    public int Left;        // x position of upper-left corner
    public int Top;         // y position of upper-left corner
    public int Right;       // x position of lower-right corner
    public int Bottom;      // y position of lower-right corner

    public Rectangle toRectangle()
    {
        return Rectangle.FromLTRB(Left, Top, Right, Bottom);
    }
}

[StructLayout(LayoutKind.Sequential)]
struct WINDOWINFO
{
    public uint cbSize;
    public RectInt rcWindow;
    public RectInt rcClient;
    public uint dwStyle;
    public uint dwExStyle;
    public uint dwWindowStatus;
    public uint cxWindowBorders;
    public uint cyWindowBorders;
    public ushort atomWindowType;
    public ushort wCreatorVersion;

    public WINDOWINFO(Boolean? filler) : this()   // Allows automatic initialization of "cbSize" with "new WINDOWINFO(null/true/false)".
    {
        cbSize = (UInt32)(Marshal.SizeOf(typeof(WINDOWINFO)));
    }
}

namespace Your_Desktop_Pet.Core.Helpers
{
    static class DesktopWindows
    {
        private static readonly string[] classBlacklist = new string[]
        {
            "Windows.UI.Core.CoreWindow",
            "ApplicationFrameWindow",
            "Progman",
            "HwndWrapper",
            "ParsecMinFrameRate"
        };
        private static List<KeyValuePair<string, Rectangle>> bounds = new List<KeyValuePair<string, Rectangle>>();
        private static bool rmo = true;
        private static bool rmm = true;

        private delegate bool EnumDelegate(IntPtr hWnd, int lParam);

        [DllImport("user32.dll", EntryPoint = "EnumDesktopWindows",
         ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool _EnumDesktopWindows(IntPtr hDesktop,
        EnumDelegate lpEnumCallbackFunction, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(HandleRef hWnd, out RectInt lpRect);

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("dwmapi.dll")]
        private static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out IntPtr pvAttribute, int cbAttribute);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);

        private const uint WS_MAXIMIZE = (uint)0x01000000L;
        private const uint WS_MINIMIZE = (uint)0x20000000L;

        private static bool EnumWindowsProc(IntPtr hWnd, int lParam)
        {
            try
            {
                // Skip if not visible
                if (!IsWindowVisible(hWnd))
                    return true;

                WINDOWINFO info = new WINDOWINFO();
                info.cbSize = (uint)Marshal.SizeOf(info);
                GetWindowInfo(hWnd, ref info);

                if ((rmm && (info.dwStyle & WS_MAXIMIZE) == WS_MAXIMIZE) || (info.dwStyle & WS_MINIMIZE) == WS_MINIMIZE)
                    return true;

                if (rmo && (info.dwStyle & WS_MAXIMIZE) != WS_MAXIMIZE && External.Window.IsOverlapped(hWnd))
                    return true;

                RectInt r = new RectInt();
                object o = new object();
                GetWindowRect(new HandleRef(o, hWnd), out r);

                if (Math.Abs(r.Left) + Math.Abs(r.Top) + Math.Abs(r.Right) + Math.Abs(r.Bottom) != 0)
                {
                    int length = GetWindowTextLength(hWnd);
                    if (length == 0) return true;

                    StringBuilder ClassName = new StringBuilder(256);
                    //Get the window class name
                    GetClassName(hWnd, ClassName, ClassName.Capacity);

                    if (CheckClassBlacklist(ClassName.ToString()))
                        return true;

                    StringBuilder builder = new StringBuilder(length);
                    GetWindowText(hWnd, builder, length + 1);

                    bounds.Add(new KeyValuePair<string, Rectangle>(builder.ToString(), r.toRectangle()));
                }
                return true;
            }
            catch
            {
                // Something went wrong
                return false;
            }
        }

        /// <returns>Returns true if a classname matches a blacklist entry</returns>
        private static bool CheckClassBlacklist(string className)
        {
            foreach (string s in classBlacklist)
            {
                if (className.Contains(s))
                    return true;
            }

            return false;
        }

        public static KeyValuePair<string, Rectangle>[] GetAllWindowBounds(bool removeOverlap, bool removeMaximize)
        {
            IntPtr hDesktop = IntPtr.Zero;
            bounds.Clear();

            rmo = removeOverlap;
            rmm = removeMaximize;

            bool ok = _EnumDesktopWindows(hDesktop, EnumWindowsProc, IntPtr.Zero);

            if (ok)
                return bounds.ToArray();
            else
            {
                Helpers.Log.WriteLine("PetAPI", "Error! Couldn't retrie window bounds.");
                return new KeyValuePair<string, Rectangle>[0];
            }
        }
    }
}
