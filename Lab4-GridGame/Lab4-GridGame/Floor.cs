using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_GridGame
{
    public class Floor : Block, IColorClass
    {
        public override void GetPos(int x, int y)
        {
            this.PosX = x;
            this.PosY = y;
        }
        public void Color()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write('.');
            Console.ResetColor();
        }

        public Floor()
        {
            Color();
        }
    }
}
