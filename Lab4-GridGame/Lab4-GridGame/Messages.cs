using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_GridGame
{
    class Messages
    {
        public void PrintStartScreen()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t\t\t\t    A simple game by Christoffer and Knut");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\t\t\t\t\t\t    || ");
            Console.WriteLine("\t\t\t\t\t\t   //\\\\ ");
            Console.WriteLine("\t\t\t\t\t    ~~~~~///--\\\\\\~~~~~");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\t\t\t\t\t\t Have fun!");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\t\t\t\t\t    ~~~~~\\\\\\--///~~~~~");
            Console.WriteLine("\t\t\t\t\t\t   \\\\//");
            Console.WriteLine("\t\t\t\t\t\t    || ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t\t\t\t      Press any key to start the game. ");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
