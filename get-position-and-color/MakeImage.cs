using System.Drawing;
using System.Windows.Forms;

namespace get_position_and_color
{
    class MakeImage
    {
        public static Bitmap GetImage()
        {
            Bitmap image = Capture();
            image = DrawPointer(image);
            return image;
        }

        private static Bitmap DrawPointer(Bitmap image)
        {
            Color color = image.GetPixel(25, 25);
            color = Color.FromArgb(color.A, 0xFF - color.R, 0xFF - color.G, 0xFF - color.B);
            image.SetPixel(25, 25, color);
            image.SetPixel(25, 26, color);
            image.SetPixel(25, 24, color);
            image.SetPixel(24, 25, color);
            image.SetPixel(26, 25, color);
            return image;
        }

        public static Bitmap Capture()
        {
            Rectangle bounds;
            Size size = new Size(50, 50);
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
}
