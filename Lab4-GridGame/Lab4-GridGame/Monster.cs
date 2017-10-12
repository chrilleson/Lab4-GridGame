using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_GridGame
{
    class Monster : Block
    {
        public override void GetPos(int x, int y)
        {
            this.PosCol = x;
            this.PosRow = y;
        }
        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("M ");
            Console.ResetColor();
        }
        public Monster()
        {
            Print();
        }
    }
}
