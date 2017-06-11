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
        private Bitmap myImage;

        public Form1()
        {
            InitializeComponent();

            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);

        }

        private void Form1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y - 1);
            if (e.KeyCode == Keys.Down)
                Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y + 1);
            if (e.KeyCode == Keys.Left)
                Cursor.Position = new Point(Cursor.Position.X - 1, Cursor.Position.Y);
            if (e.KeyCode == Keys.Right)
                Cursor.Position = new Point(Cursor.Position.X + 1, Cursor.Position.Y);
        }
        
        private void ShowInformation()
        {
            pixel = Win32.GetPixelColor(Cursor.Position.X, Cursor.Position.Y);
            ShowPosition.Text = Cursor.Position.X + ":" + Cursor.Position.Y;
            ShowColor.Text = Convert.ToString(pixel);

        }

        private void ShowImage()
        {
            myImage = ScreenCapturer.Capture();
            pictureBox1.Image = myImage;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            ShowInformation();
            

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            ShowImage();
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

class ScreenCapturer
{

    public enum CaptureMode
    {
        Screen,
        Window
    }

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    public static Bitmap Capture()
    {
        CaptureMode screenCaptureMode = CaptureMode.Screen;
        Rectangle bounds;

        if (screenCaptureMode == CaptureMode.Screen)
        {
            bounds = Screen.GetBounds(Point.Empty);
        }
        else
        {
            var handle = GetForegroundWindow();
            var rect = new Rect();
            GetWindowRect(handle, ref rect);

            bounds = new Rectangle(rect.Left, rect.Top, rect.Right, rect.Bottom);
            //CursorPosition = new Point(Cursor.Position.X - rect.Left, Cursor.Position.Y - rect.Top);
        }

        var result = new Bitmap(bounds.Width, bounds.Height);

        using (var g = Graphics.FromImage(result))
        {
            g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
        }

        return result;
    }

}
