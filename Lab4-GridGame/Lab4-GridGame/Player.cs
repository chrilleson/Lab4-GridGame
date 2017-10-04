﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_GridGame
{
    public class Player : IColorClass
    {
        public int PosX { get; set; }
        public int PosY { get; set; }

        public void GetPlayerPos(int x, int y)
        {
            this.PosX = x;
            this.PosY = y;
        }

        public void Color()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("@");
            Console.ResetColor();
        }
        public void WritePlayer()
        {
            Color();
        }
    }
}
