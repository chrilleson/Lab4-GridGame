using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_GridGame
{
    class Exit : Block
    {
        public override void GetPos(int y, int x)
        {
            this.PosY = y;
            this.PosX = x;
        }
        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("E");
            Console.ResetColor();
        }
        public Exit()
        {
            Print();
        }

    }
}
