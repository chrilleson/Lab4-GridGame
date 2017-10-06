using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_GridGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Messages messages = new Messages();

            messages.PrintStartScreen();
            Map map = new Map();
            map.RunGame();
        }
    }
}
