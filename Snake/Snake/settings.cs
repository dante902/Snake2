using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public enum Direction { Up, Down, Left, Right, D, W, S, A };

    class settings
    {
        public static int Width;
        public static int Height;
        public static int Speed1;
        public static int Speed2;
        public static int Speed3;
        public static int Score;
        public static int Points;
        public static bool GameOver;
        public static Direction direction;

        public settings()
        {
            Width = 17;
            Height = 17;
            Speed1 = 30;
            Speed2 = 13;
            Speed3 = 8;
            Score = 0;
            Points = 10;
            GameOver = false;
            direction = Direction.Down;
            
       }
        
    }
}
