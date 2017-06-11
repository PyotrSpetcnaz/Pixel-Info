using System.Drawing;

namespace get_position_and_color
{
    class ConvertColor
    {
        public static string ToString(Color c)
        {
            string s;
            s = "R=" + c.R + " G=" + c.G + " B=" + c.B;
            return s;
        }
    }
}
