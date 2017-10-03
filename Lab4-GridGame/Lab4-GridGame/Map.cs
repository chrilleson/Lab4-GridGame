using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_GridGame
{
    public class Map
    {
        public static int MaxPosX = 15;
        public static int MaxPosY = 10;

        public char PlayerIcon;

        Block[,] MapGrid = new Block[MaxPosX, MaxPosY];

        public void GameRun()
        {
            Console.WriteLine("Hello Game");
            Console.ReadKey();
        }

    }
}
