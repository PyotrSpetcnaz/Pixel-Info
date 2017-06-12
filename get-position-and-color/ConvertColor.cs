using System.Drawing;

namespace get_position_and_color
{
    sealed class ConvertColor
    {
        public static Color ToColor(uint c)
        {
            Color color = Color.FromArgb((int)(c & 0x000000FF),
            (int)(c & 0x0000FF00) >> 8,
            (int)(c & 0x00FF0000) >> 16);
            return color;
        }
        public static string ToString(Color c)
        {
            string s = "R=" + c.R + " G=" + c.G + " B=" + c.B;
            return s;
        }
    }
}
