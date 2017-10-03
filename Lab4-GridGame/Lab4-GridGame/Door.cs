using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_GridGame
{
    class Door : Block
    {
        public override void GetPos(int x, int y)
        {
            this.PosY = y;
            this.PosX = x;
        }
        public void Color()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("D");
            Console.ResetColor();
        }
    }
}
