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
            //image.SetPixel(25, 25, Invert(color));
            color = image.GetPixel(26, 26);
            image.SetPixel(26, 26, Invert(color));
            color = image.GetPixel(24,24);
            image.SetPixel(24, 24, Invert(color));
            color = image.GetPixel(24,26);
            image.SetPixel(24, 26, Invert(color));
            color = image.GetPixel(26,24);
            image.SetPixel(26, 24, Invert(color));
            return image;
        }

        private static Color Invert(Color c)
        {
            c = Color.FromArgb(c.A, 0xFF - c.R, 0xFF - c.G, 0xFF - c.B);
            return c;
        }

        private static Bitmap Capture()
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
