using System;
using System.Runtime.InteropServices;

namespace get_position_and_color
{
    sealed class Win32
    {
        #region DLL
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hwnd, IntPtr hDC);

        [DllImport("gdi32.dll")]
        public static extern uint GetPixel(IntPtr hDC, int x, int y);
        #endregion

        static public uint GetPixelColor(int x, int y)
        {
            IntPtr hDC = GetDC(IntPtr.Zero);
            uint pixel = GetPixel(hDC, x, y);
            ReleaseDC(IntPtr.Zero, hDC);
            return pixel;
        }
    }
}
