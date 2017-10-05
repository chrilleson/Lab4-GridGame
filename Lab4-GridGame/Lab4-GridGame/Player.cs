using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_GridGame
{
    public class Player : IColorClass
    {
        //Player positions
        public int PosX { get; set; }
        public int PosY { get; set; }

        //check if the player have keys
        public bool HaveKey { get; set; }
        public int NumberOfKey { get; set; }
        
        //counts the number of steps that the player have taken
        public int NumberOfTurns { get; set; }

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
