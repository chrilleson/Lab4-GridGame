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

        public bool IsGameRunning;
        public string Buffer = " ";

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
            
            IsGameRunning = true;
            while(IsGameRunning==true)
            {
                Console.Clear();

                for (int x = 0; x < MapGrid.GetLength(0); x++)
                {
                    for (int y = 0; y < MapGrid.GetLength(1); y++)
                    {
                        if (x == 0 || x == MapGrid.GetLength(0) - 1 || y == 0 || y == MapGrid.GetLength(1) - 1)
                            MapGrid[x, y] = new Wall();
                        else if (x == player.PosX && y == player.PosY)
                        {
                            player.WritePlayer();
                            //Console.Write(Buffer);
                        }
                        else
                        {
                            MapGrid[x, y] = new Floor();
                            //Console.Write(Buffer);
                        }
                    }
                    Console.WriteLine();
                }

                var Input = Console.ReadKey();
                PlayerIcon = (char)Input.Key;

                switch (Input.Key)
                {

                    case ConsoleKey.W:
                        player.PosX--;
                        break;
                    case ConsoleKey.A:
                        player.PosY--;
                        break;
                    case ConsoleKey.S:
                        player.PosX++;
                        break;
                    case ConsoleKey.D:
                        player.PosY++;
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
