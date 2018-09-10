using System.Drawing;
using System.Windows.Forms;

namespace rpg
{
    public static class Title
    {
        public static Panel title = new Panel();
        public static Panel confirm = new Panel();
        public static string title_music = "2.mp3";
        
        public static Bitmap bg_1 = new Bitmap(ResourceContextDeterminer.GetAssetPath("T_bg1.png"));
        public static Bitmap bg_2 = new Bitmap(ResourceContextDeterminer.GetAssetPath("T_bg2.png"));
        public static Bitmap bg_3 = new Bitmap(ResourceContextDeterminer.GetAssetPath("T_bg3.png"));
        public static Bitmap bg_font = new Bitmap(ResourceContextDeterminer.GetAssetPath("T_logo.png"));
        public static long last_change_bg_time = 0;
        public static int bg_now = 2;


        public static void init()
        {
            Panel.Button btn_new = new Panel.Button();
            btn_new.set(325, 350, 0, 0, 
                ResourceContextDeterminer.GetAssetPath("T_start_1.png"), 
                ResourceContextDeterminer.GetAssetPath("T_start_2.png"), 
                ResourceContextDeterminer.GetAssetPath("T_start_2.png"),
                2, 1, -1, -1);
            btn_new.click_event += new Panel.Button.Click_event(newgame);
            
            Panel.Button btn_load = new Panel.Button();
            btn_load.set(325, 400, 0, 0, 
                ResourceContextDeterminer.GetAssetPath("T_load_1.png"), 
                ResourceContextDeterminer.GetAssetPath("T_load_2.png"), 
                ResourceContextDeterminer.GetAssetPath("T_load_2.png"),
                0, 2, -1, -1);
            btn_load.click_event += new Panel.Button.Click_event(loadgame);

            Panel.Button btn_exit = new Panel.Button();
            btn_exit.set(325, 450, 0, 0, 
                ResourceContextDeterminer.GetAssetPath("T_exit_1.png"), 
                ResourceContextDeterminer.GetAssetPath("T_exit_2.png"), 
                ResourceContextDeterminer.GetAssetPath("T_exit_2.png"),
                1, 0, -1, -1);
            btn_exit.click_event += new Panel.Button.Click_event(exitgame);

            title.button = new Panel.Button[3];
            title.button[0] = btn_new;
            title.button[1] = btn_load;
            title.button[2] = btn_exit;
            title.set(0, 0, ResourceContextDeterminer.GetAssetPath("T_bg1.png"), 0, -1);
            title.init();
            
            bg_1.SetResolution(96, 96);
            bg_2.SetResolution(96, 96);
            bg_3.SetResolution(96, 96);
            bg_font.SetResolution(96, 96);
            
            title.draw_event += new Panel.Draw_event(drawtitle);
            
            
            Panel.Button btn_yes = new Panel.Button();
            btn_yes.set(42, 60, 0, 0, 
                ResourceContextDeterminer.GetAssetPath("confirm_yes_1.png"), 
                ResourceContextDeterminer.GetAssetPath("confirm_yes_2.png"), 
                ResourceContextDeterminer.GetAssetPath("confirm_yes_2.png"),
                -1, 1, -1, -1);
            btn_yes.click_event += new Panel.Button.Click_event(confirm_yes);
            
            Panel.Button btn_no = new Panel.Button();
            btn_no.set(42, 100, 0, 0, 
                ResourceContextDeterminer.GetAssetPath("confirm_no_1.png"),
                ResourceContextDeterminer.GetAssetPath("confirm_no_2.png"), 
                ResourceContextDeterminer.GetAssetPath("confirm_no_2.png"),
                0, -1, -1, -1);
            btn_no.click_event += new Panel.Button.Click_event(confirm_no);
            
            confirm.button = new Panel.Button[2];
            confirm.button[0] = btn_yes;
            confirm.button[1] = btn_no;
            confirm.set(283, 250, ResourceContextDeterminer.GetAssetPath("confirm_bg.png"), 0, 1);
            confirm.init();
            confirm.drawbg_event += new Panel.Drawbg_event(drawconfirm);
        }

        public static void drawconfirm(Graphics g, int x_offset, int y_offset)
        {
            title.draw_me(g);
        }

        public static void show()
        {
            Form1.music_player.URL = title_music;
            title.show();
        }

        public static void newgame()
        {
            Map.change_map(Form1.map, Form1.player, Form1.npc, 0, 800, 400, 1, Form1.music_player);
            title.hide();
        }
        public static void loadgame()
        {
            MessageBox.Show("LoadGame");
        }  
        public static void exitgame()
        {
            confirm.show();
        }

        public static void drawtitle(Graphics g, int x_offset, int y_offset)
        {
            if (bg_now == 0)
            {
                g.DrawImage(bg_1, 0, 0);
            } else if (bg_now == 1)
            {
                g.DrawImage(bg_2, 0, 0);
            } else if (bg_now == 2)
            {
                g.DrawImage(bg_3, 0, 0);
            }
            
            g.DrawImage(bg_font, 260, 80);

            if (Comm.Time() - last_change_bg_time > 5000)
            {
                bg_now++;
                if (bg_now > 2)
                {
                    bg_now = 0;
                }
                last_change_bg_time = Comm.Time();
            }
           
        }

        public static void confirm_yes()
        {
            Application.Exit();
        }
        public static void confirm_no()
        {
            title.show();
        }
        
    }
}