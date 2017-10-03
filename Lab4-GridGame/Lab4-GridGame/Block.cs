using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_GridGame
{
    //Positions on every block.
    public abstract class Block
    {
        public int PosX { get; set; }
        public int PosY { get; set; }

        public abstract void GetPos(int x, int y);
    }
}
