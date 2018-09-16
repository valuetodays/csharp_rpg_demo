using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace rpg
{
    public partial class Form1 : Form
    {
        public static Player[] player = new Player[3];
        public static Map[] map = new Map[2];
        public static Npc[] npc = new Npc[6];
        public static WMPLib.WindowsMediaPlayer music_player = new WMPLib.WindowsMediaPlayer();
        public Form1()
        {
            InitializeComponent();
        }


        public Bitmap mc_normal;
        public Bitmap mc_event;
        public int mc_mod = 0; // 0-normal, 1-event
        
        private void Form1_Load(object sender, EventArgs e)
        {
            Title.init();
            Message.init();
            
            Define.define(player, map, npc);

            Map.change_map(map, player, npc, 0, 30, 500, 1, music_player);
           /*
            Panel.Button b = new Panel.Button();
            b.click_event += new Panel.Button.Click_event(tryevent);
            b.click();*/
            

            Title.show();
            StatusMenu.init();
            Shop.init();
            
            
            // 光标
            mc_normal = new Bitmap(ResourceContextDeterminer.GetAssetPath("mc_1.png"));
            mc_normal.SetResolution(96, 96);
            mc_event = new Bitmap(ResourceContextDeterminer.GetAssetPath("mc_2.png"));
            mc_event.SetResolution(96, 96);
        }

        private void draw_mouse(Graphics g)
        {
            Point showPoint = stage.PointToClient(Cursor.Position);
            if (mc_mod == 0)
            {
                g.DrawImage(mc_normal, showPoint.X, showPoint.Y);
            }
            else if (mc_mod == 1)
            {
                g.DrawImage(mc_event, showPoint.X, showPoint.Y);
            }
        }
        
        private void stage_MouseMove(object sender, MouseEventArgs e)
        {
            if (Panel.panel != null)
            {
                Panel.mouse_move(e);
            }

            mc_mod = Npc.check_mouse_collision(map, player, npc, new Rectangle(0, 0, stage.Width, stage.Height), e);
        }

        private void stage_MouseClick(object sender, MouseEventArgs e)
        {
            Player.mouse_click(map, player, new Rectangle(0, 0, stage.Width, stage.Height), e);
            Npc.mouse_click(map, player, npc, new Rectangle(0, 0, stage.Width, stage.Width), e);
            if (Panel.panel != null)
            {
                Panel.mouse_click(e);
            }
        }

        private void stage_MouseEnter(object sender, EventArgs eventArgs)
        {
            Cursor.Hide();
        }

        private void stage_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
        }

        private void Draw()
        {
            Graphics g1 = stage.CreateGraphics();
            BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
            BufferedGraphics myBuffer = currentContext.Allocate(g1, this.DisplayRectangle);
            Graphics g = myBuffer.Graphics;
            
            Map.draw(map, player, npc, g, new Rectangle(0, 0, stage.Width, stage.Height));
            if (Panel.panel != null)
            {
                Panel.draw(g);
            }
            draw_mouse(g);
            
            myBuffer.Render();
            myBuffer.Dispose();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
           // Console.WriteLine("form1.keydown");

            //stage.Refresh();
            Player.key_ctrl(player, map, npc, e);
            if (Panel.panel != null)
            {
                Panel.key_ctrl(e);
            }
            
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Player.key_ctrl_up(player, e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("timer1_Tick.");
            Player.timer_logic(player, map);
            
            for (int i = 0; i < npc.Length; i++)
            {
                if (npc[i] != null && npc[i].map == Map.current_map)
                {
                    npc[i].timer_logic(map);
                }
            }
            Draw();
        }

        public void tryevent()
        {
            MessageBox.Show("kkkkk");
        }

    }
}
