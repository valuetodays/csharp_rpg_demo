using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace rpg
{
    public class Player
    {
        public int x = 50;
        public int y = 50;
        public int face = 1;
        public int anm_frame = 0;
        public long last_walk_time = 0;
        public long walk_interval = 100;
        public int speed = 20;
        public Bitmap bitmap;
        public static int current_player = 0;
        public int is_active = 0;
        public int x_offset = -120;
        public int y_offset = -220;
        public int collision_ray = 50;

        public Player()
        {
            bitmap = new Bitmap(ResourceContextDeterminer.GetAssetPath("r1.png"));
            bitmap.SetResolution(96, 96);
        }

        public static void key_ctrl(Player[] player, Map[] map, Npc[] npc, KeyEventArgs e)
        {
            Player p = player[current_player];

            if (e.KeyCode == Keys.Tab)
            {
                key_change_player(player);
            }

            if (e.KeyCode == Keys.Up)
            {
                walk(player, map, Comm.Direction.UP);
            }
            else if (e.KeyCode == Keys.Down)
            {
                walk(player, map, Comm.Direction.DOWN);
            }
            else if (e.KeyCode == Keys.Left)
            {
                walk(player, map, Comm.Direction.LEFT);
            }
            else if (e.KeyCode == Keys.Right)
            {
                walk(player, map, Comm.Direction.RIGHT);
            }
            npc_collision(player, map, npc, e);
        }

        public static void set_pos(Player[] player, int x, int y, int face)
        {
            player[current_player].x = x;
            player[current_player].y = y;
            player[current_player].face = face;
        }

        public static void draw(Player[] player, Graphics g, int map_sx, int map_sy)
        {
            Player p = player[current_player];

            Rectangle crazycoderRgl = new Rectangle(p.bitmap.Width / 4 * (p.anm_frame % 4),
                p.bitmap.Height / 4 * (p.face - 1),
                p.bitmap.Width / 4,
                p.bitmap.Height / 4);
            Bitmap bitmap0 = p.bitmap.Clone(crazycoderRgl, p.bitmap.PixelFormat);
            g.DrawImage(bitmap0, map_sx + p.x + p.x_offset, map_sy + p.y + p.y_offset);
        }


        public static void key_change_player(Player[] player)
        {
            for (int i = current_player+1; i < player.Length; i++)
            {
                if (player[i].is_active == 1)
                {
                    set_player(player, current_player, i);
                    return;
                }
            }

            for (int i = 0; i < current_player; i++)
            {
                if (player[i].is_active == 1)
                {
                    set_player(player, current_player, i);
                }
            }
        }

        private static void set_player(Player[] player, int oldIndex, int newIndex)
        {
            current_player = newIndex;
            player[newIndex].x = player[oldIndex].x;
            player[newIndex].y = player[oldIndex].y;
            player[newIndex].face = player[oldIndex].face;
        }

        public static void key_ctrl_up(Player[] player, KeyEventArgs e)
        {
            Player p = player[current_player];
            p.anm_frame = 0;
            p.last_walk_time = 0;
        }

        public static int get_pos_x(Player[] player)
        {
            return player[current_player].x;
        }

        public static int get_pos_y(Player[] player)
        {
            return player[current_player].y;
        }

        public static int get_pos_face(Player[] player)
        {
            return player[current_player].face;
        }

        public static void walk(Player[] player, Map[] map, Comm.Direction direction)
        {
            Player p = player[current_player];
            p.face = (int)direction;
            if (Comm.Time() - p.last_walk_time <= p.walk_interval)
            {
                return;
            }

            if (direction == Comm.Direction.UP && Map.can_through(map, p.x, p.y - p.speed))
            {
                p.y = p.y - p.speed;
            } else if (direction == Comm.Direction.DOWN && Map.can_through(map, p.x, p.y + p.speed))
            {
                p.y = p.y + p.speed;
            } else if (direction == Comm.Direction.LEFT && Map.can_through(map, p.x - p.speed, p.y))
            {
                p.x = p.x - p.speed;
            } else if (direction == Comm.Direction.RIGHT && Map.can_through(map, p.x + p.speed, p.y))
            {
                p.x = p.x + p.speed;
            }

            p.anm_frame++;
            if (p.anm_frame >= int.MaxValue)
            {
                p.anm_frame = 0;
            }
            p.last_walk_time = Comm.Time();
        }

        public static Point get_collision_point(Player[] player)
        {
            Player p = player[current_player];
            int collision_x = 0;
            int collision_y = 0;
            collision_x = p.x;
            collision_y = p.y;

            if (p.face == (int)Comm.Direction.UP)
            {
                collision_y = p.y - p.collision_ray;
            } else if (p.face == (int)Comm.Direction.DOWN) 
            {
                collision_y = p.y + p.collision_ray;
            } else if (p.face == (int)Comm.Direction.LEFT)
            {
                collision_x = p.x - p.collision_ray;
            } else if (p.face == (int)Comm.Direction.RIGHT)
            {
                collision_x = p.x + p.collision_ray;
            }
            return new Point(collision_x, collision_y);
        }

        public static void npc_collision(Player[] player, Map[] map, Npc[] npc, KeyEventArgs e)        {
            Player p = player[current_player];
            Point p1 = new Point(p.x, p.y);
            Point p2 = get_collision_point(player);

            for (int i = 0; i < npc.Length; i++)
            {
                Npc n = npc[i];
                Console.WriteLine(">>" + i);
                if (n == null)
                {
                    continue;
                }
                if (n.map != Map.current_map)
                {
                    continue;
                }
                
                if (n.is_line_collision(p1, p2))
                {
                    if (n.collision_type == Npc.Collision_type.ENTER)
                    {
                        
                        Task.story(i);
                        break;
                    } else if (n.collision_type == Npc.Collision_type.KEY)
                    {
                        if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
                        {
                            Task.story(i);
                            break;
                        }
                    }
                }
            }
        }
    }
}
