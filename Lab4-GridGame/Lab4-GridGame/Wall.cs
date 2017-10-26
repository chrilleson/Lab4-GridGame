using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_GridGame
{
    public class Wall : Block, IPrint
    {
        public override void GetPos(int y, int x)
        {
            this.PosRow = y;
            this.PosCol = x;
        }
        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write('#');
            Console.ResetColor();
        }
        public Wall()
        {
            Print();
        }
    }
}
