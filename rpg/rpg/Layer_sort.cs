using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace rpg
{
    class Layer_sort
    {
        public int y;
        public int index;
        public int type; // 0是主角，1是npc
    }

    public class Layer_sort_comparer : System.Collections.IComparer
    {
        public int Compare(object obj1, object obj2)
        {
            return ((Layer_sort)obj1).y - ((Layer_sort)obj2).y;
        }
    }
}
