using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_GridGame
{
    class Key : Block
    {
        public override void GetPos(int x, int y)
        {
            this.PosX = x;
            this.PosY = y;
        }
        public void Color()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("K");
            Console.ResetColor();
        }
        public Key()
        {
            Color();
        }
    }
}
