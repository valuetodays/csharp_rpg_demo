using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace rpg
{
    public class Animation
    {
        public static long REATE = 100;
        public string bitmap_path;
        public Bitmap bitmap;
        public int row = 1;
        public int col = 1;
        public int max_frame = 1;
        public int anm_rate;

        public void load()
        {
            if (Comm.isNotNullOrEmptyString(bitmap_path))
            {
                bitmap = new Bitmap(bitmap_path);
                bitmap.SetResolution(96, 96);
            }
        }

        public void unload()
        {
            if (bitmap != null)
            {
                bitmap = null;
            }
        }

        public Bitmap get_bitmap (int frame)
        {
            if (bitmap == null)
            {
                return null;
            }

            if (frame > max_frame)
            {
                return null;
            }

            Rectangle rect = new Rectangle(
                bitmap.Width / row * (frame % row),
                bitmap.Height / col * (frame / row),
                bitmap.Width / row,
                bitmap.Height / col
                );
            return bitmap.Clone(rect, bitmap.PixelFormat);
        }

        public void draw(Graphics g, int frame, int x, int y)
        {
            Bitmap bitmap = get_bitmap(frame/anm_rate);
            if (bitmap != null)
            {
                g.DrawImage(bitmap, x, y);
            }
        }
    }
}
