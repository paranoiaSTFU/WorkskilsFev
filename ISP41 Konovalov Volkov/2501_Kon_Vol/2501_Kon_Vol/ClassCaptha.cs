using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace _2501_Kon_Vol
{
    public class ClassCaptha
    {
        public static string CreateTXT()
        {
            string alf = "1234567890qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
            Random rnd = new Random();
            string res = "";
            
            for (int i = 0; i < 5; i++)
            {
                res += alf[rnd.Next(alf.Length)];
            }
            return res;
        }
        public Bitmap CreateIMG(int Width, int Height, string txt)
        {
            Random rnd = new Random();

            Bitmap result = new Bitmap(Width,Height);

            int Xpos = rnd.Next(0, Width - 50);
            int Ypos = rnd.Next(15, Height - 15);

            System.Drawing.Brush[] colors = { System.Drawing.Brushes.Black, System.Drawing.Brushes.Purple, System.Drawing.Brushes.Pink };

            Graphics g = Graphics.FromImage((Image)result);

            g.Clear(System.Drawing.Color.Gray);

            g.DrawString(txt, new Font("Arial", 16), colors[rnd.Next(colors.Length)], new PointF(Xpos,Ypos));

            g.DrawLine(Pens.Green,new System.Drawing.Point(0,0),new System.Drawing.Point(Width-1, Height-1));
            g.DrawLine(Pens.Green, new System.Drawing.Point(0, Height-1), new System.Drawing.Point(Width - 1, 0));

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (rnd.Next() % 20 == 0)
                        result.SetPixel(i, j, System.Drawing.Color.Red);
                }
            }


            return result;
        } 

        public static ImageSource ConvertBitMapToIMG(Bitmap a)
        {
            var headly = a.GetHbitmap();
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(headly, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                /*
            ImageSourceConverter c = new ImageSourceConverter();
            return (ImageSource)c.ConvertFrom(a);
        */
        }

        public static string CapchaLabel()
        {
            return "";
        }
    }
}
