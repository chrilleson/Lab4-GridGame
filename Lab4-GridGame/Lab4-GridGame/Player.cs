using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_GridGame
{
    public class Player : IPrint
    {
        //Player positions
        public int PosCol { get; set; }
        public int PosRow { get; set; }

        //check if the player have keys
        public bool HaveKey { get; set; }
        public int NumberOfKey { get; set; }
        
        //counts the number of steps that the player have taken
        public int NumberOfTurns { get; set; }

        public void SetPlayerPos(int y, int x)
        {
            this.PosRow = y;
            this.PosCol = x;
        }

        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("@");
            Console.ResetColor();
        }
        public void WritePlayer()
        {
            Print();
        }
    }
}
