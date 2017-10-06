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
            /*
            key1.GetPos(2, 4);
            key2.GetPos(9, 14);
            key3.GetPos(5, 13);
            door1.GetPos(4, 2);
            door2.GetPos(9, 4);
            door3.GetPos(3, 12);*/
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
                            MapGrid[row, col] = new Wall();
                        else if (row == 2 && col == 4)
                        {
                            MapGrid[row, col] = new Key();
                        }
                        else if (row == 9 && col == 13)
                        {
                            MapGrid[row, col] = new Key();
                        }
                        else if (row == 5 && col == 13)
                        {
                            MapGrid[row, col] = new Key();
                        }
                        else if (row == 3 && col == 1 )
                        {
                            MapGrid[row, col] = new Door();
                        }
                        else if (row == 9 && col == 4 )
                        {
                            MapGrid[row, col] = new Door();
                        }
                        else if (row == 3 && col == 12)
                        {
                            MapGrid[row, col] = new Door();
                        }
                        else if (row == player.PosCol && col == player.PosRow)
                        {
                            player.WritePlayer();
                        }
                        else
                        {
                            MapGrid[row, col] = new Floor();
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();

                //To check if the player inside an monster room
                InsideMonsterRoom();

                messages.PrintStepsAndKeys();
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
                            player.NumberOfTurns++;
                        
                        break;
                    case ConsoleKey.A:
                        if (NextStep(player.PosCol, player.PosRow, NextPlayerStep) == false)
                            break;
                        else
                            player.PosRow--;
                            player.NumberOfTurns++;
                        
                        break;
                    case ConsoleKey.S:
                        if (NextStep(player.PosCol, player.PosRow, NextPlayerStep) == false)
                            break;
                        else
                            player.PosCol++;
                            player.NumberOfTurns++;
                        
                        break;
                    case ConsoleKey.D:
                        if (NextStep(player.PosCol, player.PosRow, NextPlayerStep) == false)
                            break;
                        else
                            player.PosRow++;
                            player.NumberOfTurns++;
                        
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
            Block nextBlock = null;
            switch (InputKey)
            {
                case 'W':
                    nextBlock = MapGrid[player.PosRow - 1, player.PosCol];
                    break;
                case 'A':
                    nextBlock = MapGrid[player.PosRow, player.PosCol - 1];
                    break;
                case 'S':
                    nextBlock = MapGrid[player.PosRow + 1, player.PosCol];
                    break;
                case 'D':
                    nextBlock = MapGrid[player.PosRow, player.PosCol + 1];
                    break;
                default:
                    break;

            }
            if (nextBlock is Wall)
            {
                return false;
            }
            else if (nextBlock is Door door)
            {
                if (door.DoorOpen)
                    return true;
                else if (player.NumberOfKey > 0)
                {
                    door.DoorOpen = true;
                    player.NumberOfKey--;
                    return true;
                }
                else
                    return false;
            }
            else
                return true;
        }


        //To be able to pick up keys
        public bool KeyIsPickedUp()
        {
            if (MapGrid[player.PosRow, player.PosCol] == MapGrid[key1.PosY, key1.PosX] && key1.PickedUpKey == false)
            {
                key1.PickedUpKey = true;
                player.HaveKey = true;
                player.NumberOfKey++;
                player.NumberOfTurns++;
                return true;
            }
            else if (MapGrid[player.PosRow, player.PosCol] == MapGrid[key2.PosY, key2.PosX] && key2.PickedUpKey == false)
            {
                key2.PickedUpKey = true;
                player.HaveKey = true;
                player.NumberOfKey++;
                player.NumberOfTurns++;
                return true;
            }
            else if (MapGrid[player.PosRow, player.PosCol] == MapGrid[key3.PosY, key3.PosX] && key3.PickedUpKey == false)
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
                player.NumberOfTurns += 35;
            }
        }


    }
}
