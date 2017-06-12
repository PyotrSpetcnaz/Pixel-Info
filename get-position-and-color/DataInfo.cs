using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System;

namespace get_position_and_color
{
    public class DataInfo
    {
        
        static Int32 dataCounter = 1;
        public struct Data
        {
            public uint color;
            public Point coordinates;
        }

        private static Data SetInformation()
        {
            Data data;
            data.color = Win32.GetPixelColor(Cursor.Position.X, Cursor.Position.Y);
            data.coordinates = new Point(Cursor.Position.X, Cursor.Position.Y);
            return data;
        }

        public static Data GetInfomation()
        {
            Data data = SetInformation();
            return data;
        }
        
        public static void SaveData(Data data)
        {
            string[] s = ToString(data);
            using(StreamWriter file = new StreamWriter("SaveFile.txt", true))
            {
                file.WriteLine(dataCounter);
            }
            dataCounter++;
            using (StreamWriter file = new StreamWriter("SaveFile.txt", true))
            {
            for (int i = 0; i < 3; i++)
                {
                   file.WriteLine(s[i]);
                }
            }
        }

        public static string[] ToString(Data data)
        {
            string[] s ={"Position: " + data.coordinates,
                        "RGB:      " + ConvertColor.ToString(ConvertColor.ToColor(data.color)),
                        "uint:     " + data.color};
            return s;
        }
    }
}
