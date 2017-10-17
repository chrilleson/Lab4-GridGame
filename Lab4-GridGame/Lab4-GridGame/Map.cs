using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_GridGame
{
    //Level of the game
    public class Map
    {
        //X- & Y variables to set map size
        public static int MaxCol = 20;
        public static int MaxRow = 10;

        //Player input variable for visual representation
        public char playerInput;

        public bool GameLoop;
        public string buffer = " ";

        //Instantiates game objects and Player for the map
        Block[,] MapGrid = new Block[MaxRow, MaxCol];
        public Player player = new Player();
        private Key key1 = new Key();
        private Key key2 = new Key();
        private Door door1 = new Door();
        private Door door2 = new Door();
        private Exit exit = new Exit();

        //Function that runs the games map
        public void RunGame()
        {
            //Create map-specific variables and setting player-, key-, and door starting values
            player.SetPlayerPos(2, 7);
            player.HaveKey = false;
            player.NumberOfKey = 0;
            player.NumberOfTurns = 0;
            key1.PickedUpKey = false;
            key2.PickedUpKey = false;
            door1.DoorOpen = false;
            door2.DoorOpen = false;

            //Sets key, door and exit positions on the map
            key1.GetPos(1, 1);
            key2.GetPos(8, 1);
            door1.GetPos(5, 5);
            door2.GetPos(6, 15);
            exit.GetPos(8, 18);

            //Update map loop based on player input
            GameLoop = true;
            while (GameLoop != false)
            {
                Console.Clear();

                //Filling mapArray with objects based on Level Design
                for (int row = 0; row < MapGrid.GetLength(0); row++)
                {
                    for (int col = 0; col < MapGrid.GetLength(1); col++)
                    {
                        if (col == 0 || col == MapGrid.GetLength(1) - 1 || row == 0 || row == MapGrid.GetLength(0) - 1 ||
                            col == 2 && row == 3 || col == 2 && row == 2 || col == 12 && row == 3 || col == 12 && row == 2 ||
                            col == 6 && row == 1 || col == 8 && row == 1 || col == 6 && row == 3 || col == 8 && row == 3 || row == 5 && col != 5 && col != 16 || col == 4 && row == 6 || col == 4 && row == 7 || col == 2 && row == 7 || col == 15 && row >= 3 && row != 6 && row >= 7)
                        {
                            MapGrid[row, col] = new Wall();
                            Console.Write(' ');
                        }
                        else if (col == 4 && row == 2 || col == 10 && row == 2 || col == 2 && row == 8 )
                        {
                            MapGrid[row, col] = new Monster();
                            Console.Write(buffer);
                        }
                        else if (row == 5 && col == 16)
                        {
                            MapGrid[row, col] = new Troll();
                            Console.Write(buffer);
                        }
                        else if (col == 5 && row == 5 && door1.DoorOpen == false)
                        {
                            MapGrid[row, col] = new Door();
                            Console.Write(buffer);
                        }
                        else if (col == 15 && row == 6 && door2.DoorOpen == false)
                        {
                            MapGrid[row, col] = new Door();
                            Console.Write(buffer);
                        }
                        else if (col == 1 && row == 1 && key1.PickedUpKey == false)
                        {
                            MapGrid[row, col] = new Key();
                            Console.Write(buffer);
                        }
                        else if (col == 1 && row == 8 && key2.PickedUpKey == false)
                        {
                            MapGrid[row, col] = new Key();
                            Console.Write(buffer);
                        }
                        else if (row == 8 && col == 18)
                        {
                            MapGrid[row, col] = new Exit();
                            Console.Write(buffer);
                        }
                        else if (row == player.PosRow && col == player.PosCol)
                        {
                            player.WritePlayer();
                            Console.Write(buffer);
                        }
                        else
                        {
                            MapGrid[row, col] = new Floor();
                            Console.Write(buffer);
                        }
                    }
                    Console.WriteLine();
                }

                //Calls function that checks how many keys the Player currently has
                CurrentNumberOfKeys();

                //Prints useful information for the player:
                Console.Write($"KEYS: {player.NumberOfKey}");
                Console.Write($"\t\tSTEPS TAKEN: {player.NumberOfTurns}");
                Console.Write($"\n\tPREVIOUS MOVE: {playerInput}");
                Console.WriteLine();

                //Checks if the player is on a monster-tile
                IsMonsterRoom();
                TrollRoom();
                //Switch statement to register player input and check whether or not the player is able to take a step.
                var input = Console.ReadKey();
                playerInput = (char)input.Key;

                switch (input.Key)
                {
                    case ConsoleKey.W:
                        if (NextStep(player.PosRow, player.PosCol, playerInput) == false)
                            break;
                        else
                        {
                            player.PosRow--;
                            ReachExit(player.PosRow, player.PosCol, playerInput);
                            PickUpKey();
                            player.NumberOfTurns++;
                            break;
                        }

                    case ConsoleKey.S:
                        if (NextStep(player.PosRow, player.PosCol, playerInput) == false)
                            break;
                        else
                        {
                            player.PosRow++;
                            PickUpKey();
                            player.NumberOfTurns++;
                            ReachExit(player.PosRow, player.PosCol, playerInput);
                            break;
                        }

                    case ConsoleKey.A:
                        if (NextStep(player.PosRow, player.PosCol, playerInput) == false)
                            break;
                        else
                        {
                            player.PosCol--;
                            PickUpKey();
                            player.NumberOfTurns++;
                            ReachExit(player.PosRow, player.PosCol, playerInput);
                            break;
                        }

                    case ConsoleKey.D:
                        if (NextStep(player.PosRow, player.PosCol, playerInput) == false)
                            break;
                        else
                        {
                            player.PosCol++;
                            PickUpKey();
                            player.NumberOfTurns++;
                            ReachExit(player.PosRow, player.PosCol, playerInput);
                            break;
                        }
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }

            //When the player reaches the exit and exits the map loop, this message plays.
            Console.WriteLine($"\n\n\t\t\tGOOD JOB!");
            Console.WriteLine($"\n\n\t   TOTAL STEPS TAKEN TO REACH EXIT: {player.NumberOfTurns}");
            if(player.PosCol == 16 && player.PosRow == 5)
                Console.WriteLine("You're greedy, aren't you?");
            Console.ReadKey();
        }

        //Function to handle what happens when the player reaches on the exit-tile.
        public void ReachExit(int y, int x, char keyInput)
        {
            if (MapGrid[y, x] is Exit)
            {
                Console.Clear();
                Console.WriteLine("\n\n\n\t\tYOU REACHED THE EXIT!");
                Console.ReadKey();
                GameLoop = false;
                Console.Clear();
            }
        }

        //Function to return true or false depending on if the player is standing by a wall or locked door and is able to move or not.
        public bool NextStep(int y, int x, char keyInput)
        {
            if ((MapGrid[player.PosRow - 1, player.PosCol] is Wall && keyInput == 'W') ||
                (MapGrid[player.PosRow, player.PosCol + 1] is Wall && keyInput == 'D') ||
                (MapGrid[player.PosRow, player.PosCol - 1] is Wall && keyInput == 'A') ||
                (MapGrid[player.PosRow + 1, player.PosCol] is Wall && keyInput == 'S'))
            {
                return false;
            }
            else if (MapGrid[player.PosRow - 1, player.PosCol] is Door && keyInput == 'W' ||
                    MapGrid[player.PosRow + 1, player.PosCol] is Door && keyInput == 'S' ||
                    MapGrid[player.PosRow, player.PosCol - 1] is Door && keyInput == 'A' ||
                    MapGrid[player.PosRow, player.PosCol + 1] is Door && keyInput == 'D')
            {
                if (IsDoorOpen() == true)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }

        //Function to handle the opening of doors.
        public bool IsDoorOpen()
        {
            if (player.HaveKey)
            {
                if (MapGrid[player.PosRow - 1, player.PosCol] == MapGrid[door1.PosRow, door1.PosCol] ||
                    MapGrid[player.PosRow + 1, player.PosCol] == MapGrid[door1.PosRow, door1.PosCol] ||
                    MapGrid[player.PosRow, player.PosCol - 1] == MapGrid[door1.PosRow, door1.PosCol] ||
                    MapGrid[player.PosRow, player.PosCol + 1] == MapGrid[door1.PosRow, door1.PosCol])
                {
                    door1.DoorOpen = true;
                }
                else if (MapGrid[player.PosRow - 1, player.PosCol] == MapGrid[door2.PosRow, door2.PosCol] ||
                        MapGrid[player.PosRow + 1, player.PosCol] == MapGrid[door2.PosRow, door2.PosCol] ||
                        MapGrid[player.PosRow, player.PosCol - 1] == MapGrid[door2.PosRow, door2.PosCol] ||
                        MapGrid[player.PosRow, player.PosCol + 1] == MapGrid[door2.PosRow, door2.PosCol])
                {
                    door2.DoorOpen = true;
                }
                player.NumberOfKey--;
                player.NumberOfTurns++;
                return true;
            }
            else
                return false;
        }

        //Function that checks the players current number of keys
        public void CurrentNumberOfKeys()
        {
            if (player.NumberOfKey <= 0)
            {
                player.HaveKey = false;
                player.NumberOfKey = 0;
            }
        }

        //Function for picking up keys. 
        public bool PickUpKey()
        {
            if (MapGrid[player.PosRow, player.PosCol] == MapGrid[key1.PosRow, key1.PosCol] && key1.PickedUpKey == false)
            {
                key1.PickedUpKey = true;
                player.HaveKey = true;
                player.NumberOfKey++;
                player.NumberOfTurns++;
                return true;
            }
            else if (MapGrid[player.PosRow, player.PosCol] == MapGrid[key2.PosRow, key2.PosCol] && key2 .PickedUpKey == false)
            {
                key2.PickedUpKey = true;
                player.HaveKey = true;
                player.NumberOfKey++;
                player.NumberOfTurns++;
                return true;
            }
            else
                return false;
        }

        //Function that checks if player is standing in a monster room
        public void IsMonsterRoom()
        {
            if ((MapGrid[player.PosRow, player.PosCol] is Monster && playerInput == 'W') ||
                    (MapGrid[player.PosRow, player.PosCol] is Monster && playerInput == 'D') ||
                    (MapGrid[player.PosRow, player.PosCol] is Monster && playerInput == 'A') ||
                    (MapGrid[player.PosRow, player.PosCol] is Monster && playerInput == 'S'))
            {
                player.NumberOfTurns += 10;
                Console.WriteLine("You enter a room and a monster attacks you. It takes a while to fight it.");
            }
        }
        public void TrollRoom()
        {
            if ((MapGrid[player.PosRow, player.PosCol] is Troll && playerInput == 'W')||
                (MapGrid[player.PosRow, player.PosCol] is Troll && playerInput == 'D') ||
                (MapGrid[player.PosRow, player.PosCol] is Troll && playerInput == 'A') ||
                (MapGrid[player.PosRow, player.PosCol] is Troll && playerInput == 'S')
                )
                {
                player.NumberOfTurns += 60;
                Console.WriteLine("You're greedy, aren't you ? ");
                Console.WriteLine("You entered a room where the Troll is, it takes you a while to fight it.");
            }
        }
    }
}