using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace rpg
{
    public class Panel
    {
        public static Panel panel = null;

        public int x;
        public int y;

        public string bitmap_path;
        public Bitmap bitmap;

        public Button[] button;
        public int default_button = 0;
        public int cancel_button = -1;
        public int current_button = 0;

        public void set(int x0, int y0, string path, int default_button0, int cancel_button0)
        {
            x = x0;
            y = y0;
            bitmap_path = path;
            default_button = default_button0;
            cancel_button = cancel_button0;
        }

        public void init()
        {
            if (Comm.isNotNullOrEmptyString(bitmap_path))
            {
                bitmap = new Bitmap(bitmap_path);
                bitmap.SetResolution(96, 96);
            }

            if (button != null)
            {
                for (int i = 0; i < button.Length; i++)
                {
                    if (button[i] != null)
                    {
                        button[i].load();
                    }
                }
            }
        }

        public void show()
        {
            panel = this;
            current_button = default_button;
            set_button_status(Button.Status.SELECT);
        }

        public void hide()
        {
            panel = null;
        }

        public void set_button_status(Button.Status status)
        {
            if (button != null)
            {
                for (int i = 0; i < button.Length; i++)
                {
                    if (button[i] != null)
                    {
                        button[i].status = Button.Status.NORMAL;
                    }
                }

                if (button[current_button] != null)
                {
                    button[current_button].status = status;
                }
            }
        }

        public delegate void Draw_event(Graphics g, int x_offset, int y_offset);

        public event Draw_event draw_event;

        public delegate void Drawbg_event(Graphics g, int x_offset, int y_offset);

        public event Drawbg_event drawbg_event;

        public void draw_me(Graphics g)
        {
            if (drawbg_event != null)
            {
                drawbg_event(g, this.x, this.y);
            }

            if (bitmap != null)
            {
                g.DrawImage(bitmap, x, y);
            }

            if (draw_event != null)
            {
                draw_event(g, this.x, this.y);
            }

            if (button != null)
            {
                for (int i = 0; i < button.Length; i++)
                {
                    if (button[i] != null) 
                    {
                        button[i].draw(g, x, y);
                    }
                }
            }
            
        }

        public static void draw(Graphics g)
        {
            if (panel != null)
            {
                panel.draw_me(g);
            }
        }

        public static void key_ctrl(KeyEventArgs e)
        {
            if (panel != null)
            {
                panel.key_ctrl_me(e);
            }
            
        }
        
        public void key_ctrl_me(KeyEventArgs e)
        {
            if (button == null)
            {
                return;
            }

            Button btn = button[current_button];
            if (btn == null)
            {
                return;
            }

            int newIndex = -1;
            if (e.KeyCode == Keys.Up)
            {
                newIndex = btn.key_ctrl.up;
            } else if (e.KeyCode == Keys.Down)
            {
                newIndex = btn.key_ctrl.down;
            } else if (e.KeyCode == Keys.Left)
            {
                newIndex = btn.key_ctrl.left;
            } else if (e.KeyCode == Keys.Right)
            {
                newIndex = btn.key_ctrl.right;
            }

            if (newIndex >= 0 && newIndex < button.Length && button[newIndex] != null)
            {
                current_button = newIndex;
                set_button_status(Button.Status.SELECT);
            }

            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                set_button_status(Button.Status.PRESS);
                btn.click();
            } else if (e.KeyCode == Keys.Escape)
            {
                if (cancel_button >= 0 && cancel_button < button.Length)
                {
                    button[cancel_button].click();
                }
            }
        }
           
        

        public class Button
        {
            public int x = 0;
            public int y = 0;
            public int w = 0;
            public int h = 0;

            public string b_normal_path;
            public string b_select_path;
            public string b_press_path;
            private Bitmap b_normal;
            private Bitmap b_select;
            private Bitmap b_press;
            
            public enum Status
            {
                NORMAL = 1,
                SELECT = 2,
                PRESS = 3,
            }

            public Status status = Status.NORMAL;

            public class Key_ctrl
            {
                public int up = -1;
                public int down = -1;
                public int left = -1;
                public int right = -1;
            }

            public Key_ctrl key_ctrl = new Key_ctrl();

            public void set(int x0, int y0, int w0, int h0, 
                string normal_path, string select_path, string press_path,
                int key_up, int key_down, int key_left, int key_right)
            {
                x = x0;
                y = y0;
                w = w0;
                h = h0;
                b_normal_path = normal_path;
                b_select_path = select_path;
                b_press_path = press_path;
                key_ctrl.up = key_up;
                key_ctrl.down = key_down;
                key_ctrl.left = key_left;
                key_ctrl.right = key_right;
            }

            public void load()
            {
                if (Comm.isNotNullOrEmptyString(b_normal_path))
                {
                    b_normal = new Bitmap(b_normal_path);
                    b_normal.SetResolution(96, 96);
                    if (w <= 0)
                    {
                        w = b_normal.Width;
                    }

                    if (h <= 0)
                    {
                        h = b_normal.Height;
                    }
                }

                if (Comm.isNotNullOrEmptyString(b_select_path))
                {
                    b_select = new Bitmap(b_select_path);
                    b_select.SetResolution(96, 96);
                }
                
                if (Comm.isNotNullOrEmptyString(b_press_path))
                {
                    b_press = new Bitmap(b_press_path);
                    b_press.SetResolution(96, 96);
                }
            }

            public void draw(Graphics g, int x_offset, int y_offset)
            {
                if (status == Status.NORMAL && b_normal != null)
                {
                    g.DrawImage(b_normal, x_offset + x, y_offset + y);
                }

                if (status == Status.SELECT && b_select != null)
                {
                    g.DrawImage(b_select, x_offset + x, y_offset + y);
                }
                
                if (status == Status.PRESS && b_press != null)
                {
                    g.DrawImage(b_press, x_offset + x, y_offset + y);
                }
            }

            public delegate void Click_event();

            public event Click_event click_event;

            public void click()
            {
                if (click_event != null)
                {
                    click_event();
                }
            }


        }
    }
}