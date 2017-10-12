using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_GridGame
{
    public class Door : Block, IPrint
    {
        public bool DoorOpen { get; set; }

        public override void GetPos(int x, int y)
        {
            this.PosRow = y;
            this.PosCol = x;
        }
        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("D");
            Console.ResetColor();
        }

        public Door()
        {
            Print();
        }
    }
}
