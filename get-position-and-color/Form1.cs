using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace get_position_and_color
{
    public partial class Form1 : Form
    {
        DataInfo.Data data;
        private Bitmap myImage;
        private bool colorType = true;

        public Form1()
        {
            InitializeComponent();
            KeyDown += new KeyEventHandler(Form1_KeyDown);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            File.WriteAllText("SaveFile.txt", "");
        }

        private void Form1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            int distance = 1;
            if (e.Shift)
            {
                distance = 10;
            }
            if (e.Control)
            {
                distance = 5;
            }
            if (e.KeyCode == Keys.Up)
            { 
                Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y - distance);
                ShowImage();
            }
            if (e.KeyCode == Keys.Down)
            {
                Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y + distance);
                ShowImage();
            }
            if (e.KeyCode == Keys.Left)
            {
                Cursor.Position = new Point(Cursor.Position.X - distance, Cursor.Position.Y);
                ShowImage();
            }
            if (e.KeyCode == Keys.Right)
            {
                Cursor.Position = new Point(Cursor.Position.X + distance, Cursor.Position.Y);
                ShowImage();
            }
            if (e.KeyCode == Keys.C) colorType = !colorType;
            if (e.KeyCode == Keys.S) DataInfo.SaveData(data);
        }

        
        
        private void ShowInformation()
        {
            data = DataInfo.GetInfomation();
            ShowPosition.Text = "x="+ Cursor.Position.X + " y=" + Cursor.Position.Y;
            if (colorType)
            {
                Color arbPixel = ConvertColor.ToColor(data.color);
                ShowColor.Text = ConvertColor.ToString(arbPixel);
            }
            else
            { 
                ShowColor.Text = Convert.ToString(data.color);
            }
        }

        private void ShowImage()
        {
            myImage = MakeImage.GetImage();
            pictureBox1.Image = myImage;
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
            colorType = !colorType;
        }

    }
}
