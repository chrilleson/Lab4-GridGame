using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_GridGame
{       //TODO: Flytta infyllningen av mapen till en egen funktion utanför game loopen, fixa utskriften av map efter row, column.
    public class Map
    {
        //The max positions of the map
        public const int MaxPosCol = 20;
        public const int MaxPosRow = 10;

        public bool IsGameRunning;
        public string buffer = " ";

        //Instantiates game objects and Player for the map
        Block[,] MapGrid = new Block[MaxRow, MaxCol];
        public Player player = new Player();
        private Key key1 = new Key();
        private Key key2 = new Key();
        private Door door1 = new Door();
        private Door door2 = new Door();
        private Exit exit = new Exit();

        //Function to run the game
        public void RunGame()
        {
            //Standard values
            player.SetPlayerPos(5, 7);
            player.HaveKey = false;
            player.NumberOfKey = 0;
            player.NumberOfTurns = 0;
            key1.PickedUpKey = false;
            key2.PickedUpKey = false;
            door1.DoorOpen = false;
            door2.DoorOpen = false;

            //Places all the keys, doors, and the exit
            
            key1.GetPos(2, 4);
            key2.GetPos(8, 12);
            key3.GetPos(5, 13);
            door1.GetPos(4, 2);
            door2.GetPos(9, 4);
            door3.GetPos(3, 12);
            exit.GetPos(2, 11);
            //Updating the map loop
            IsGameRunning = true;
            while (IsGameRunning != false)
            {
                Console.Clear();

                //Filling mapArray with objects based on Level Design
                for (int row = 0; row < MapGrid.GetLength(0); row++)
                {
                    for (int col = 0; col < MapGrid.GetLength(1); col++)
                    {
                        if (col == 0 || col == MapGrid.GetLength(1) - 1 || row == 0 || row == MapGrid.GetLength(0) - 1 ||
                            col == 2 && row == 3 || col == 2 && row == 2 || col == 12 && row == 3 || col == 12 && row == 2 ||
                            col == 6 && row == 1 || col == 8 && row == 1 || col == 6 && row == 3 || col == 8 && row == 3)
                        {
                            MapGrid[row, col] = new Wall();
                            Console.Write(' ');
                        }
                        else if (col == 4 && row == 2 || col == 10 && row == 2)
                        {
                            MapGrid[row, col] = new Monster();
                            Console.Write(buffer);
                        }
                        else if (col == 8 && row == 2 && door1.DoorOpen == false)
                        {
                            MapGrid[row, col] = new Door();
                            Console.Write(buffer);
                        }
                        else if (col == 13 && row == 2 && door2.DoorOpen == false)
                        {
                            MapGrid[row, col] = new Door();
                            Console.Write(buffer);
                        }
                        else if (col == 7 && row == 3 && key1.PickedUpKey == false)
                        {
                            MapGrid[row, col] = new Key();
                            Console.Write(buffer);
                        }
                        else if (col == 1 && row == 3 && key2.PickedUpKey == false)
                        {
                            MapGrid[row, col] = new Key();
                            Console.Write(buffer);
                        }
                        else if (row == 3 && col == 13)
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
                Console.WriteLine();

                //Checks if the player have any keys and how many
                CurrentKeys();

                //To check if the player inside an monster room

                Console.Write($"you have taken: {player.NumberOfTurns} steps");
                Console.Write($"\nYou have: {player.NumberOfKey} keys.");
                Console.WriteLine();

                //Checks if the player is on a monster-tile
                IsMonsterRoom();

                switch (Input.Key)
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
                    default:
                        break;
                }
            }

            //When the player reaches the exit and exits the map loop, this message plays.
            Console.WriteLine($"\n\n\t\t\tGOOD JOB!");
            Console.WriteLine($"\n\n\t   TOTAL STEPS TAKEN TO REACH EXIT: {player.NumberOfTurns}");
            if (player.NumberOfTurns > 200)
                Console.WriteLine("\t\t\tYOU SUCK");
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
        //Bool to check if the player can step on the next block

        private bool NextStep(int col, int row, char InputKey)
        {            
            if ((MapGrid[player.PosCol - 1, player.PosRow] is Wall && InputKey == 'W') || 
                (MapGrid[player.PosCol, player.PosRow + 1] is Wall && InputKey == 'D') || 
                (MapGrid[player.PosCol, player.PosRow - 1] is Wall && InputKey == 'A') || 
                (MapGrid[player.PosCol + 1, player.PosRow] is Wall && InputKey == 'S'))

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

        public void CurrentKeys()
        {
            if (player.NumberOfKey <= 0)
            {
                player.HaveKey = false;
                player.NumberOfKey = 0;
            }
        }

        //To be able to pick up keys
        public bool KeyIsPickedUp()
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
    }
}