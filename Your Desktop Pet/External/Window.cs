using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

// Modified version of this post:
// https://social.msdn.microsoft.com/Forums/vstudio/en-US/78289886-f3c1-405b-aaa1-722a23690245/how-to-check-if-a-window-is-partially-or-completely-obscured-by-other-windows?forum=netfxbcl

namespace Your_Desktop_Pet.External
{
    static class Window
    {
        /// <returns>Returns true if the window partially obscured by something</returns>
        public static bool IsIntersecting(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero)
                throw new InvalidOperationException("The handle passed does not exist yet");
            if (!IsWindowVisible(hWnd))
                return false;

            HashSet<IntPtr> visited = new HashSet<IntPtr> { hWnd };

            // The set is used to make calling GetWindow in a loop stable by checking if we have already
            //  visited the window returned by GetWindow. This avoids the possibility of an infinate loop.

            RectInt thisRect;
            GetWindowRect(hWnd, out thisRect);

            while ((hWnd = GetWindow(hWnd, GW_HWNDPREV)) != IntPtr.Zero && !visited.Contains(hWnd))
            {
                visited.Add(hWnd);
                RectInt testRect, intersection;
                // Window is garateed to be visible
                if (GetWindowRect(hWnd, out testRect) && IntersectRect(out intersection, ref thisRect, ref testRect))
                    return true;
            }

            return false;
        }

        /// <returns>Returns true if the window completely obscured by something</returns>
        public static bool IsOverlapping(IntPtr hWnd, WINDOWINFO info)
        {
            if (hWnd == IntPtr.Zero)
                throw new InvalidOperationException("The handle passed does not exist yet");
            if (!IsWindowVisible(hWnd))
                return false;

            HashSet<IntPtr> visited = new HashSet<IntPtr> { hWnd };

            // The set is used to make calling GetWindow in a loop stable by checking if we have alreadya
            //  visited the window returned by GetWindow. This avoids the possibility of an infinate loop.

            RectInt thisRect;
            GetWindowRect(hWnd, out thisRect);

            while ((hWnd = GetWindow(hWnd, GW_HWNDPREV)) != IntPtr.Zero && !visited.Contains(hWnd))
            {
                visited.Add(hWnd);
                RectInt testRect;

                if (!IsWindowVisible(hWnd))
                    continue;

                if (!GetWindowRect(hWnd, out testRect) ||
                    (IntersectRect(out _, ref thisRect, ref testRect) && (info.dwStyle & WS_MAXIMIZE) == WS_MAXIMIZE) ||
                    OverlappingRect(thisRect, testRect)
                    )
                    return true;
            }

            return false;
        }

        private static bool OverlappingRect(RectInt bottomRect, RectInt topRect)
        {
            return topRect.Top < bottomRect.Top &&
                topRect.Left < bottomRect.Left &&
                topRect.Right > bottomRect.Right &&
                topRect.Bottom > bottomRect.Bottom;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindow(IntPtr hWnd, int uCmd);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, [Out] out RectInt lpRect);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IntersectRect([Out] out RectInt lprcDst, [In] ref RectInt lprcSrc1, [In] ref RectInt lprcSrc2);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindowVisible(IntPtr hWnd);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);

        private const int GW_HWNDPREV = 3;
        private const uint WS_MAXIMIZE = (uint)0x01000000L;
        private const uint WS_MINIMIZE = (uint)0x20000000L;
    }
}
