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
        private Color color;
        private bool colorType = false;

        public Form1()
        {
            InitializeComponent();
            KeyDown += new KeyEventHandler(Form1_KeyDown);
            MouseMove += new MouseEventHandler(Form1_MouseMove);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y - 1);
                ShowImage();
            }
            if (e.KeyCode == Keys.Down)
            {
                Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y + 1);
                ShowImage();
            }
            if (e.KeyCode == Keys.Left)
            {
                Cursor.Position = new Point(Cursor.Position.X - 1, Cursor.Position.Y);
                ShowImage();
            }
            if (e.KeyCode == Keys.Right)
            {
                Cursor.Position = new Point(Cursor.Position.X + 1, Cursor.Position.Y);
                ShowImage();
            }

        }


        
        private void ShowInformation()
        {
            pixel = Win32.GetPixelColor(Cursor.Position.X, Cursor.Position.Y);
            ShowPosition.Text = "x="+ Cursor.Position.X + " y=" + Cursor.Position.Y;
            if (colorType)
            {
                Color arbPixel = Color.FromArgb((int)(pixel & 0x000000FF),
                (int)(pixel & 0x0000FF00) >> 8,
                (int)(pixel & 0x00FF0000) >> 16);
                ShowColor.Text = ConvertColor.ToString(arbPixel);

            }
            else
            { 
                ShowColor.Text = Convert.ToString(pixel);
            }
        }

        private void ShowImage()
        {
            myImage = ScreenCapturer.Capture();
            color = myImage.GetPixel(25, 25);
            color = Color.FromArgb(color.A, 0xFF - color.R, 0xFF - color.G, 0xFF - color.B);
            myImage.SetPixel(25, 25, color);
            myImage.SetPixel(25, 26, color);
            myImage.SetPixel(25, 24, color);
            myImage.SetPixel(24, 25, color);
            myImage.SetPixel(26, 25, color);
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

        private void ShowColor_Click(object sender, EventArgs e)
        {
            if (colorType)
                colorType = false;
            else colorType = true;
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

    public static Bitmap Capture()
    {
        Rectangle bounds;
        Size size = new Size(50,50);
        Point center = new Point(Cursor.Position.X - 25, Cursor.Position.Y - 25);
        bounds = new Rectangle(center, size);
 
        var result = new Bitmap(bounds.Width, bounds.Height);

        using (var g = Graphics.FromImage(result))
        {
            g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
        }

        return result;
    }

}

 class ConvertColor
{ 
    public static string ToString(Color c)
    {
        string s;
        s = "R=" + c.R + " G=" + c.G + " B=" + c.B;
        return s;
    }
}