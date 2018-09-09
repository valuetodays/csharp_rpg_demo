using System;
using System.Collections.Generic;
using System.Text;

namespace rpg
{
    class Comm
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

        public static int opposite_direction(int direction)
        {
            if (direction == Constants.DIRECTION_UP)
            {
                return Constants.DIRECTION_DOWN;
            }
            else if (direction == Constants.DIRECTION_DOWN)
            {
                return Constants.DIRECTION_UP;
            }
            else if (direction == Constants.DIRECTION_LEFT)
            {
                return Constants.DIRECTION_RIGHT;
            }
            else if (direction == Constants.DIRECTION_RIGHT)
            {
                return Constants.DIRECTION_LEFT;
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

    }
}
