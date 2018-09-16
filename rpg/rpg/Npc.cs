using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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

        public Npc_type npc_type = Npc_type.NORMAL;
        public Comm.Direction face = Comm.Direction.DOWN;
        public int walk_frame = 0;
        public long last_walk_time = 0;
        public long walk_interval = 80;
        public int speed = 40;
        public Comm.Direction idle_walk_direction = Comm.Direction.DOWN;
        public int idle_walk_time = 0;
        public int idle_walk_time_now = 0;
        
        // 鼠标碰撞区域 mouse_click
        public int mc_xoffset = 0;
        public int mc_yoffset = -30;
        public int mc_w = 100;
        public int mc_h = 150;
        public static int mc_distance_x = 300;
        public static int mc_distance_y = 200;
        

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
            
            //鼠标碰撞区域
            if (bitmap != null)
            {
                if (npc_type == Npc_type.NORMAL)
                {
                    if (mc_w == 0)
                    {
                        mc_w = bitmap.Width;
                    }

                    if (mc_h == 0)
                    {
                        mc_h = bitmap.Height;
                    }
                } else if (npc_type == Npc_type.CHARACTER)
                {
                    if (mc_w == 0)
                    {
                        mc_w = bitmap.Width / 4;
                    }

                    if (mc_h == 0)
                    {
                        mc_h = bitmap.Height / 4;
                    }
                }
            }
            else
            {
                if (mc_w == 0)
                {
                    mc_w = region_x;
                }

                if (mc_h == 0)
                {
                    mc_h = region_y;
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
                if (npc_type == Npc_type.NORMAL)
                {
                    if (bitmap != null)
                    {
                        g.DrawImage(bitmap, map_sx + x + x_offset, map_sy + y + y_offset);
                    }
                } else if (npc_type == Npc_type.CHARACTER)
                {
                    draw_character(g, map_sx, map_sy);
                }

            } else // 绘制动画
            {
                draw_anm(g, map_sx, map_sy);
            }

        }

        public void walk(Map[] map, Comm.Direction direction, bool isblock)
        {
            face = direction;
            if (Comm.Time() - last_walk_time <= walk_interval)
            {
                return;
            }

            if (direction == Comm.Direction.UP && (!isblock || Map.can_through(map, x,  y - speed)))
            {
                y -= speed;
            } else if (direction == Comm.Direction.DOWN && (!isblock || Map.can_through(map, x, y+ speed))) {
                y += speed;
            } else if (direction == Comm.Direction.LEFT && (!isblock || Map.can_through(map, x - speed, y)))
            {
                x -= speed;
            } else if (direction == Comm.Direction.RIGHT && (!isblock || Map.can_through(map, x+speed, y)))
            {
                x += speed;
            }

            walk_frame++;
            if (walk_frame >= int.MaxValue)
            {
                walk_frame = 0;
            }
            last_walk_time = Comm.Time();
        }

        public void stop_walk()
        {
            walk_frame = 0;
            last_walk_time = 0;
        }

        public void timer_logic(Map[] map)
        {
            if (npc_type == Npc_type.CHARACTER && idle_walk_time != 0)
            {
                Comm.Direction direction;
                if (idle_walk_time_now >= 0)
                {
                    direction = idle_walk_direction;
                } else
                {
                    direction = Comm.opposite_direction(idle_walk_direction);
                }

                walk(map, direction, true);

                if (idle_walk_time_now >= 0)
                {
                    idle_walk_time_now++;
                    if (idle_walk_time_now > idle_walk_time)
                    {
                        idle_walk_time_now = -1;
                    }
                } else if (idle_walk_time_now < 0)
                {
                    idle_walk_time_now--;
                    if ( idle_walk_time_now < -idle_walk_time)
                    {
                        idle_walk_time_now = 1;
                    }
                }

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


        public void draw_character(Graphics g, int map_sx, int map_sy)
        {
            Rectangle rect = new Rectangle(
                bitmap.Width / 4 * (walk_frame % 4),
                bitmap.Height / 4 * ((int)face - 1),
                bitmap.Width / 4,
                bitmap.Height / 4    
                );
            Bitmap bitmap0 = bitmap.Clone(rect, bitmap.PixelFormat);
            g.DrawImage(bitmap0, map_sx + x + x_offset, map_sy + y + y_offset);
        }

        public bool is_mouse_collision(int collision_x, int collision_y)
        {
            // 有图
            if (bitmap != null)
            {
                if (npc_type == Npc_type.NORMAL)
                {
                    int center_x = x + x_offset + bitmap.Width / 2;
                    int center_y = y + y_offset + bitmap.Height / 2;
                    Rectangle rect = new Rectangle(center_x - mc_w /2, center_y-mc_h/2, mc_w, mc_h);
                    return rect.Contains(collision_x, collision_y);
                } else if (npc_type == Npc_type.CHARACTER)
                {
                    int center_x = x + x_offset + bitmap.Width  / 4 / 2;
                    int center_y = y + y_offset + bitmap.Height / 4 / 2;
                    Rectangle rect = new Rectangle(center_x - mc_w /2, center_y-mc_h/2, mc_w, mc_h);
                    return rect.Contains(collision_x, collision_y);
                }

                return false;
            }
            else // 无图
            {
                Rectangle rect = new Rectangle(x - mc_w/2, y - mc_h/2, mc_w, mc_h);
                return rect.Contains(collision_x, collision_y);
            }
        }

        public bool check_me_distance(Npc n, int player_x, int player_y)
        {
            Rectangle rectangle = new Rectangle(n.x - mc_distance_x/2, n.y - mc_distance_y/2,mc_distance_x, mc_distance_y);
            return rectangle.Contains(player_x, player_y);
        }

        public static void mouse_click(Map[] map, Player[] player, Npc[] npc, Rectangle stage, MouseEventArgs e)
        {
            if (Player.status != Player.Status.WALK)
            {
                return;
            }

            if (npc == null)
            {
                return;
            }

            for (int i = 0; i < npc.Length; i++)
            {
                if (npc[i] == null || npc[i].map != Map.current_map)
                {
                    continue;
                }

                int collision_x = e.X - Map.get_map_sx(map, player, stage);
                int collision_y = e.Y - Map.get_map_sy(map, player, stage);
                if (!npc[i].is_mouse_collision(collision_x, collision_y))
                {
                    continue;
                }

                if (!npc[i].check_me_distance(npc[i], Player.get_pos_x(player), Player.get_pos_y(player)))
                {
                    Player.stop_walk(player);
                    Message.showtip("请走近些");
                    Task.block();
                    continue;
                }
                Player.stop_walk(player);
                Task.story(i);
            }
        }



        public enum Collision_type
        {
            KEY = 1,
            ENTER = 2,
        }

        public enum Npc_type
        {
            NORMAL = 0,
            CHARACTER = 1,
        }

    }


}
