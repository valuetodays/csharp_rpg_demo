using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace rpg
{
    class Task
    {
        public static Player.Status player_last_status = Player.Status.WALK;
        
        public static void story(int i)
        {
            if (Player.status != Player.Status.TASK)
            {
                player_last_status = Player.status;
            }

            Player.status = Player.Status.TASK;
            
            
            
            DialogResult dialogResult;
            if (i == 0)
            {
                Message.show("主角", "夏山如碧，绿树成荫，总会令人怡然笛自乐。此地山青水秀，我十分喜欢。我们便相约好了，闲暇时，便来此地，沏茶共饮。",
                    ResourceContextDeterminer.GetAssetPath("face1_1.png"), Message.Face.LEFT);
                block();
                Message.show("女孩", "嗯，说好了，一言为定。可是怎么感觉你这句话是山寨自哪里的。",
                    ResourceContextDeterminer.GetAssetPath("face3_2.png"), Message.Face.RIGHT);
                block();
                Message.show("主角", "(被发现了TAT)", 
                    ResourceContextDeterminer.GetAssetPath("face2_1.png"), Message.Face.LEFT);
                block();
            } else if (i == 1)
            {
//                Message.showtip("遇到一个男孩。");
//                block();
//                dialogResult = MessageBox.Show("我是男孩");
                Shop.show(new int[]{0, 1, 2, 3, -1, -1, -1, -1});
                block();
            } else if (i == 2)
            {
                Map.change_map(Form1.map, Form1.player, Form1.npc, 1, 955, 550, 2, Form1.music_player);
            } else if (i == 3)
            {
                Map.change_map(Form1.map, Form1.player, Form1.npc, 0, 45, 550, 3, Form1.music_player);
            } else if (i == 4)
            {
                Form1.npc[4].play_anm(0);
            } else if (i == 5)
            {
                dialogResult = MessageBox.Show("我会走路！");
            }

            Player.status = player_last_status;
        }

        public static void block()
        {
            while (Player.status == Player.Status.PANEL)
            {
                Application.DoEvents();
            }
        }
    }
}
