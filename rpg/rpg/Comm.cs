using System;
using System.Collections.Generic;
using System.Text;

namespace rpg
{
    public class Comm
    {
        public static long Time()
        {
            return currentTimeMillis();
        }

        public static long currentTimeMillis()
        {
            TimeSpan ts = DateTime.Now - new DateTime(1979, 1, 1);
            return (long)ts.TotalMilliseconds;
        }

        public static void exit()
        {
            // 这个才能干净地退出程序
            System.Environment.Exit(0);
        }

        public static Comm.Direction opposite_direction(Comm.Direction direction)
        {
            if (direction == Comm.Direction.UP)
            {
                return Comm.Direction.DOWN;
            }
            else if (direction == Comm.Direction.DOWN)
            {
                return Comm.Direction.UP;
            }
            else if (direction == Comm.Direction.LEFT)
            {
                return Comm.Direction.RIGHT;
            }
            else if (direction == Comm.Direction.RIGHT)
            {
                return Comm.Direction.LEFT;
            }
            throw new Exception("获取相反方向时出错！");
        }

        public static bool isNullOrEmptyString(string str)
        {
            return str == null || str.Length == 0;
        }

        public static bool isNotNullOrEmptyString(string str)
        {
            return str != null && str.Length > 0;
        }


        public enum Direction
        {
            UP = 4,
            DOWN = 1,
            RIGHT = 3,
            LEFT = 2,
        }

    }
}
