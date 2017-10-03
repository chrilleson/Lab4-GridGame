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
        Player player = new Player();
        Key key1 = new Key();
        Key key2 = new Key();
        Key key3 = new Key();
        Door door1 = new Door();
        Door door2 = new Door();
        Door door3 = new Door();
        Exit exit = new Exit();
        
        public void GameRun()
        {
            for (int x = 0; x < MapGrid.Length; x++)
            {
                for(int y = 0; y<MapGrid.Length; y++)
                {
                    MapGrid[x, y] = new Wall();
                }
            }

            Console.ReadKey();
        }

    }
}
