using System.Drawing;

namespace rpg
{
    public class Panel
    {

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