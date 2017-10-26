using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_GridGame
{
    class Exit : Block, IPrint
    {
        public override void GetPos(int y, int x)
        {
            this.PosCol = x;
            this.PosRow = y;
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
