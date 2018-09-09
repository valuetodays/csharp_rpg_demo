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

        public Animation[] anm;
        public int anm_frame = 0;
        public int current_anm = -1;
        public long last_anm_time = 0;

        public void load()
        {
            if (Comm.isNotNullOrEmptyString(bitmap_path))
            {
                bitmap = new Bitmap(bitmap_path);
                bitmap.SetResolution(96, 96);
            }

            if (anm != null)
            {
                for (int i = 0; i < anm.Length; i++)
                {
                    anm[i].load();
                }
            }
        }

        public void unload()
        {
            if (bitmap != null)
            {
                bitmap = null;
            }

            if (anm != null)
            {
                for (int i = 0; i < anm.Length; i++)
                {
                    anm[i].unload();
                }
            }
        }

        public void draw(Graphics g, int map_sx, int map_sy)
        {
            if (!visible)
            {
                return;
            }
            if (current_anm < 0) // 绘制角色
            {
                if (bitmap != null)
                {
                    g.DrawImage(bitmap, map_sx + x + x_offset, map_sy + y + y_offset);
                }
            } else // 绘制动画
            {
                draw_anm(g, map_sx, map_sy);
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

        public void draw_anm(Graphics g, int map_sx, int map_sy)
        {
            if (anm == null 
                || current_anm >= anm.Length
                || anm[current_anm] == null
                || anm[current_anm].bitmap_path == null
                )
            {
                current_anm = -1;
                anm_frame = 0;
                last_anm_time = 0;
                return;
            }

            anm[current_anm].draw(g, anm_frame, map_sx + x + x_offset, map_sy + y + y_offset);

            if (Comm.Time() - last_anm_time >= Animation.REATE)
            {
                anm_frame++;
                last_anm_time = Comm.Time();
                if (anm_frame / anm[current_anm].anm_rate >= anm[current_anm].max_frame)
                {
                    current_anm = -1;
                    anm_frame = 0;
                    last_anm_time = 0;
                }
            }
        }

        public void play_anm(int index) {
            current_anm = index;
            anm_frame = 0;
        }

        public enum Collision_type
        {
            KEY = 1,
            ENTER = 2,
        }
    }
}
