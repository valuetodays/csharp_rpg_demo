using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace rpg
{
    class Task
    {
        public static void story(int i)
        {
            DialogResult dialogResult;
            if (i == 0)
            {
                dialogResult = MessageBox.Show("我是女孩");
            } else if (i == 1)
            {
                dialogResult = MessageBox.Show("我是男孩");
            } else if (i == 2)
            {
                Map.change_map(Form1.map, Form1.player, Form1.npc, 1, 955, 550, 2, Form1.music_player);
            } else if (i == 3)
            {
                Map.change_map(Form1.map, Form1.player, Form1.npc, 0, 45, 550, 3, Form1.music_player);
            }
        }
    }
}
