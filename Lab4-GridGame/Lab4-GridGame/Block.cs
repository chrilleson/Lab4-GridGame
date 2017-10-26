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
        public int PosCol { get; set; }
        public int PosRow { get; set; }

        public abstract void GetPos(int y, int x);
    }
}
