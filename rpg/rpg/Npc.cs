using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace rpg
{
    public class Npc
    {
        public int map = -1;
        public int x = 0;
        public int y = 0;
        public int x_offset = -120;
        public int y_offset = -220;

        public string bitmap_path;
        public Bitmap bitmap;
        bool visible = true;
        // 碰撞区域
        public int region_x = 60;
        public int region_y = 60;
        public Collision_type collision_type = Collision_type.KEY;

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

        public void draw(Graphics g, int map_sx, int map_sy)
        {
            if (!visible)
            {
                return;
            }

            if (bitmap != null)
            {
                g.DrawImage(bitmap, map_sx + x + x_offset, map_sy + y + y_offset);
            }
        }

        public bool is_collision(int collision_x, int collision_y)
        {
            Rectangle rect = new Rectangle(x - region_x/2, y - region_y/2, region_x, region_y);
            return rect.Contains(new Point(collision_x, collision_y));
        }

        public bool is_line_collision(Point p1, Point p2)
        {
            if (is_collision(p2.X, p2.Y))
            {
                return true;
            }

            int px = p1.X + (p2.X - p1.X) / 2;
            int py = p1.Y + (p2.Y - p2.Y) / 2;
            if (is_collision(px, py)) 
            {
                return true;
            }

            px = p2.X - (p2.X - p1.X) / 4;
            py = p2.Y - (p2.Y - p1.Y) / 4;
            if (is_collision(px, py))
            {
                return true;
            }

            return false;
        }

        public enum Collision_type
        {
            KEY = 1,
            ENTER = 2,
        }
    }
}
