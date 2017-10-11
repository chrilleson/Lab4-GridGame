using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_GridGame
{
    public class Floor : Block, IPrint
    {
        public override void GetPos(int y, int x)
        {
            this.PosY = y;
            this.PosX = x;
        }
        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write('.');
            Console.ResetColor();
        }

        public Floor()
        {
            Print();
        }
    }
}
