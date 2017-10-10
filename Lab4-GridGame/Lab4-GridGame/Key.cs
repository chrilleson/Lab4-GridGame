using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_GridGame
{
    class Key : Block
    {
        public bool PickedUpKey { get; set; }

        public override void GetPos(int y, int x)
        {
            this.PosY = y;
            this.PosX = x;
        }
        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write('K');
            Console.ResetColor();
        }
        public Key()
        {
            Print();
        }
    }
}
