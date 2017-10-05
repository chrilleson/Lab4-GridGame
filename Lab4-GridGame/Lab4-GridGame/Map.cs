﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_GridGame
{
    public class Map
    {
        //The max positions of the map
        public static int MaxPosX = 20;
        public static int MaxPosY = 10;

        public bool IsGameRunning;

        public char NextPlayerStep;

        //Creates game objects and the player
        Block[,] MapGrid = new Block[MaxPosY, MaxPosX];
        public Player player = new Player();
        Key key1 = new Key();
        Key key2 = new Key();
        Key key3 = new Key();
        Door door1 = new Door();
        Door door2 = new Door();
        Door door3 = new Door();
        Exit exit = new Exit();

        //Function to run the game
        public void RunGame()
        {
            //Standard values
            player.GetPlayerPos(5, 7);
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
            key2.GetPos(9, 14);
            key3.GetPos(5, 13);
            door1.GetPos(4, 2);
            door2.GetPos(9, 4);
            door3.GetPos(3, 12);
            //Updating the map loop
            IsGameRunning = true;
            while (IsGameRunning == true)
            {
                Console.Clear();

                //Puts all of the diffrent blocks in the map
                for (int y = 0; y < MapGrid.GetLength(0); y++)
                {
                    for (int x = 0; x < MapGrid.GetLength(1); x++)
                    {
                        if (y == 0 || y == MapGrid.GetLength(0) - 1 || x == 0 || x == MapGrid.GetLength(1) - 1
                            || y < 4 && x == 8 || y == 3 && x == 8 || y == 3 && x == 9 || y == 3 && x == 10
                            || y == 3 && x == 11 || y == 3 && x >= 13)
                            MapGrid[y, x] = new Wall();
                        else if (y == 2 && x == 4)
                        {
                            MapGrid[y, x] = new Key();
                        }
                        else if (y == 9 && x == 13)
                        {
                            MapGrid[y, x] = new Key();
                        }
                        else if (y == 5 && x == 13)
                        {
                            MapGrid[y, x] = new Key();
                        }
                        else if (y == 3 && x == 1)
                        {
                            MapGrid[y, x] = new Door();
                        }
                        else if (y == 9 && x == 4)
                        {
                            MapGrid[y, x] = new Door();
                        }
                        else if (y == 3 && x == 12)
                        {
                            MapGrid[y, x] = new Door();
                        }
                        else if (y == player.PosX && x == player.PosY)
                        {
                            player.WritePlayer();
                        }
                        else
                        {
                            MapGrid[y, x] = new Floor();
                        }
                    }
                    Console.WriteLine();
                }

                //Switch to check if the player have pressed W, A, S, D, or Escape. Also checks if the player can move there
                var Input = Console.ReadKey();
                NextPlayerStep = (char)Input.Key;

                switch (Input.Key)
                {
                    case ConsoleKey.W:
                        if (NextStep(player.PosX, player.PosY, NextPlayerStep) == false)
                            break;
                        else
                            player.PosX--;
                        break;
                    case ConsoleKey.A:
                        if (NextStep(player.PosX, player.PosY, NextPlayerStep) == false)
                            break;
                        else
                            player.PosY--;
                        break;
                    case ConsoleKey.S:
                        if (NextStep(player.PosX, player.PosY, NextPlayerStep) == false)
                            break;
                        else
                            player.PosX++;
                        break;
                    case ConsoleKey.D:
                        if (NextStep(player.PosX, player.PosY, NextPlayerStep) == false)
                            break;
                        else
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
        private bool NextStep(int x, int y, char InputKey)
        {
            Block nextBlock = null;
            switch (InputKey)
            {
                case 'W':
                    nextBlock = MapGrid[player.PosY - 1, player.PosX];
                    break;
                case 'A':
                    nextBlock = MapGrid[player.PosY, player.PosX - 1];
                    break;
                case 'S':
                    nextBlock = MapGrid[player.PosY + 1, player.PosX];
                    break;
                case 'D':
                    nextBlock = MapGrid[player.PosY, player.PosX + 1];
                    break;
                default:
                    break;

            }
            if (nextBlock is Wall)
            {
                return false;
            }/*
            else if(nextBlock is Door)
            {
                if (DoorIsOpen())
                    return true;
                else
                    return false;
            }*/
            else
                return true;
        }

        //To be able to open door if you have a key
        public bool DoorIsOpen()
        {
            if
                (MapGrid[player.PosY - 1, player.PosX] == MapGrid[door1.PosY, door1.PosX] ||
                 MapGrid[player.PosY + 1, player.PosX] == MapGrid[door1.PosY, door1.PosX] ||
                 MapGrid[player.PosY, player.PosX - 1] == MapGrid[door1.PosY, door1.PosX] ||
                 MapGrid[player.PosY, player.PosX + 1] == MapGrid[door1.PosY, door1.PosX])
            {
                door1.DoorOpen = true;
            }
            else if
                (MapGrid[player.PosY - 1, player.PosX] == MapGrid[door2.PosY, door2.PosX] ||
                 MapGrid[player.PosY + 1, player.PosX] == MapGrid[door2.PosY, door2.PosX] ||
                 MapGrid[player.PosY, player.PosX - 1] == MapGrid[door2.PosY, door2.PosX] ||
                 MapGrid[player.PosY, player.PosX + 1] == MapGrid[door2.PosY, door2.PosX])
            {
                door2.DoorOpen = true;
            }
            else if
                (MapGrid[player.PosY - 1, player.PosX] == MapGrid[door3.PosY, door3.PosX] ||
                 MapGrid[player.PosY + 1, player.PosX] == MapGrid[door3.PosY, door3.PosX] ||
                 MapGrid[player.PosY, player.PosX - 1] == MapGrid[door3.PosY, door3.PosX] ||
                 MapGrid[player.PosY, player.PosX + 1] == MapGrid[door3.PosY, door3.PosX])
            {
                door3.DoorOpen = true;
            }
            player.NumberOfKey--;
            player.NumberOfTurns++;
            return true;
        }

        //To be able to pick up keys
        public bool KeyIsPickedUp()
        {
            if (MapGrid[player.PosY, player.PosX] == MapGrid[key1.PosY, key1.PosX] && key1.PickedUpKey == false)
            {
                key1.PickedUpKey = true;
                player.HaveKey = true;
                player.NumberOfKey++;
                player.NumberOfTurns++;
                return true;
            }
            else if (MapGrid[player.PosY, player.PosX] == MapGrid[key2.PosY, key2.PosX] && key2.PickedUpKey == false)
            {
                key2.PickedUpKey = true;
                player.HaveKey = true;
                player.NumberOfKey++;
                player.NumberOfTurns++;
                return true;
            }
            else if (MapGrid[player.PosY, player.PosX] == MapGrid[key3.PosY, key3.PosX] && key3.PickedUpKey == false)
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
    }
}
