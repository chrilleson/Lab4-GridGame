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

        public char NextPlayerStep;

        //Creates game objects and the player
        Block[,] MapGrid = new Block[MaxPosRow, MaxPosCol];
        public Player player = new Player();
        private Key key1 = new Key();
        private Key key2 = new Key();
        private Key key3 = new Key();
        private Door door1 = new Door();
        private Door door2 = new Door();
        private Door door3 = new Door();
        private Exit exit = new Exit();
        Messages messages = new Messages();

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
            key3.PickedUpKey = false;
            door1.DoorOpen = false;
            door2.DoorOpen = false;
            door3.DoorOpen = false;

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

                //Puts all of the diffrent blocks in the map
                for (int row = 0; row < MaxPosRow; row++)
                {
                    for (int col = 0; col < MaxPosCol; col++)
                    {
                        if (row == 0 || row == MaxPosRow - 1 || col == 0 || col == MaxPosCol - 1
                            || row < 4 && col == 8 || row == 3 && col == 8 || row == 3 && col == 9 || row == 3 && col == 10
                            || row == 3 && col == 11 || row == 3 && col >= 13)
                        {
                            MapGrid[row, col] = new Wall();
                            Console.Write(' ');
                        }
                        else if (row == 4 && col == 5)
                        {
                            MapGrid[row, col] = new Monster();
                        }
                        else if (row == 2 && col == 4 && key1.PickedUpKey == false)
                        {
                            MapGrid[row, col] = new Key();
                            Console.Write(buffer);
                        }
                        else if (row == 8 && col == 12 && key2.PickedUpKey == false)
                        {
                            MapGrid[row, col] = new Key();
                            Console.Write(buffer);
                        }
                        else if (row == 5 && col == 13 && key3.PickedUpKey == false)
                        {
                            MapGrid[row, col] = new Key();
                            Console.Write(buffer);
                        }
                        else if (row == 3 && col == 1 && door1.DoorOpen==false)
                        {
                            MapGrid[row, col] = new Door();
                            Console.Write(buffer);
                        }
                        else if (row == 7 && col == 4 && door2.DoorOpen==false)
                        {
                            MapGrid[row, col] = new Door();
                            Console.Write(buffer);
                        }
                        else if (row == 3 && col == 12 && door3.DoorOpen==false)
                        {
                            MapGrid[row, col] = new Door();
                            Console.Write(buffer);
                        }
                        else if(row == 2 && col == 10)
                        {
                            MapGrid[row, col] = new Exit();
                            Console.Write(buffer);
                        }
                        else if (row == player.PosCol && col == player.PosRow)
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

                //Switch to check if the player have pressed W, A, S, D, or Escape. Also checks if the player can move there
                var Input = Console.ReadKey();
                NextPlayerStep = (char)Input.Key;

                switch (Input.Key)
                {
                    case ConsoleKey.W:
                        if (NextStep(player.PosCol, player.PosRow, NextPlayerStep) == false)
                            break;
                        else
                            player.PosCol--;
                        KeyIsPickedUp();
                            player.NumberOfTurns++;
                        
                        break;
                    case ConsoleKey.A:
                        if (NextStep(player.PosCol, player.PosRow, NextPlayerStep) == false)
                            break;
                        else
                            player.PosRow--;
                            player.NumberOfTurns++;
                        KeyIsPickedUp();
                        break;
                    case ConsoleKey.S:
                        if (NextStep(player.PosCol, player.PosRow, NextPlayerStep) == false)
                            break;
                        else
                            player.PosCol++;
                            player.NumberOfTurns++;
                            KeyIsPickedUp();

                        break;
                    case ConsoleKey.D:
                        if (NextStep(player.PosCol, player.PosRow, NextPlayerStep) == false)
                            break;
                        else
                            player.PosRow++;
                            player.NumberOfTurns++;
                            KeyIsPickedUp();
                        break;
                    case ConsoleKey.Escape:
                        IsGameRunning = false;
                        break;
                    default:
                        break;
                }
            }
        }
        //Bool to check if the player can step on the next block

        private bool NextStep(int col, int row, char InputKey)
        {            
            if ((MapGrid[player.PosCol - 1, player.PosRow] is Wall && InputKey == 'W') || 
                (MapGrid[player.PosCol, player.PosRow + 1] is Wall && InputKey == 'D') || 
                (MapGrid[player.PosCol, player.PosRow - 1] is Wall && InputKey == 'A') || 
                (MapGrid[player.PosCol + 1, player.PosRow] is Wall && InputKey == 'S'))
            {
                return false;
            }
            else if (MapGrid[player.PosRow-1, player.PosCol] is Door && InputKey == 'W' ||
                MapGrid[player.PosRow + 1, player.PosCol] is Door && InputKey == 'S' ||
                MapGrid[player.PosRow, player.PosCol - 1] is Door && InputKey == 'A' ||
                MapGrid[player.PosRow, player.PosCol + 1] is Door && InputKey == 'D')
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
                    MapGrid[player.PosRow, player.PosCol -+1] == MapGrid[door1.PosRow, door1.PosCol])
                {
                    door1.DoorOpen = true;
                }
                else if(MapGrid[player.PosRow-1, player.PosCol] == MapGrid[door2.PosRow, door2.PosCol] ||
                    MapGrid[player.PosRow + 1, player.PosCol] == MapGrid[door2.PosRow, door2.PosCol] ||
                    MapGrid[player.PosRow, player.PosCol - 1] == MapGrid[door2.PosRow, door2.PosCol] ||
                    MapGrid[player.PosRow, player.PosCol + 1] == MapGrid[door2.PosRow, door2.PosCol])
                {
                    door2.DoorOpen = true;
                }
                else if (MapGrid[player.PosRow - 1, player.PosCol] == MapGrid[door3.PosRow, door2.PosCol] ||
                    MapGrid[player.PosRow + 1, player.PosCol] == MapGrid[door3.PosRow, door2.PosCol] ||
                    MapGrid[player.PosRow, player.PosCol - 1] == MapGrid[door3.PosRow, door2.PosCol] ||
                    MapGrid[player.PosRow, player.PosCol + 1] == MapGrid[door3.PosRow, door2.PosCol])
                {
                    door3.DoorOpen = true;
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
            else if (MapGrid[player.PosRow, player.PosCol] == MapGrid[key2.PosRow, key2.PosCol] && key2.PickedUpKey == false)
            {
                key2.PickedUpKey = true;
                player.HaveKey = true;
                player.NumberOfKey++;
                player.NumberOfTurns++;
                return true;
            }
            else if (MapGrid[player.PosRow, player.PosCol] == MapGrid[key3.PosRow, key3.PosCol] && key3.PickedUpKey == false)
            {
                key3.PickedUpKey = true;
                player.HaveKey = true;
                player.NumberOfKey++;
                player.NumberOfTurns++;
                return true;
            }
            else
                return false;
        }
        // If you're walking in a monster room.
        public void InsideMonsterRoom()
        {
            if ((MapGrid[player.PosRow, player.PosCol] is Monster && NextPlayerStep == 'W') ||
                (MapGrid[player.PosRow, player.PosCol] is Monster && NextPlayerStep == 'D') ||
                (MapGrid[player.PosRow, player.PosCol] is Monster && NextPlayerStep == 'A') ||
                (MapGrid[player.PosRow, player.PosCol] is Monster && NextPlayerStep == 'S'))
            {
                player.NumberOfTurns += 10;
            }
        }


    }
}
