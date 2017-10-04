using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_GridGame
{
    public class Map
    {
        public static int MaxPosX = 10;
        public static int MaxPosY = 15;

        public char PlayerIcon;

        Block[,] MapGrid = new Block[MaxPosX, MaxPosY];
        public Player player = new Player();
        Key key1 = new Key();
        Key key2 = new Key();
        Key key3 = new Key();
        Door door1 = new Door();
        Door door2 = new Door();
        Door door3 = new Door();
        Exit exit = new Exit();
        
        public void RunGame()
        {
            player.GetPlayerPos(5, 7);

            Console.Clear();

            for (int y = 0; y < MapGrid.GetLength(0); y++)
            {
                for(int x = 0; x<MapGrid.GetLength(1); x++)
                {
                    if (y == 0 || y == MapGrid.GetLength(0) - 1 || x == 0 || x == MapGrid.GetLength(1) - 1)
                        MapGrid[y, x] = new Wall();
                    else if (y == player.PosX && x == player.PosY)
                    {
                        player.WritePlayer();
                    }
                    else
                        MapGrid[y, x] = new Floor();
                }
                Console.WriteLine();
            }

            var Input = Console.ReadKey();
            PlayerIcon = (char)Input.Key;

            switch (Input.Key)
            {

                case ConsoleKey.W:
                    player.PosY--;
                    break;
                case ConsoleKey.A:
                    player.PosX--;
                    break;
                case ConsoleKey.S:
                    player.PosY++;
                    break;
                case ConsoleKey.D:
                    player.PosX++;
                    break;
                default:
                    break;
            }
        }
    }
}
