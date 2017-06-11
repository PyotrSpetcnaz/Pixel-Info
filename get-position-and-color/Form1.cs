using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;
using System.Windows.Input;

namespace get_position_and_color
{
    public partial class Form1 : Form
    {
        private uint pixel;

        public Form1()
        {
            InitializeComponent();
        }

        private void ShowInformation()
        {
            pixel = Win32.GetPixelColor(Cursor.Position.X, Cursor.Position.Y);
            ShowPosition.Text = Cursor.Position.X + ":" + Cursor.Position.Y;
            ShowColor.Text = Convert.ToString(pixel);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ShowInformation();
        }
    }

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
