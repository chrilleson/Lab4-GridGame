using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_GridGame
{
    class Wall : Block
    {
        public override void GetPos(int x, int y)
        {
            this.PosX = x;
            this.PosY = y;
        }
        public Wall()
        {
            Console.Write("#");
        }
    }
}
