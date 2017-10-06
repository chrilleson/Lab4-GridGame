using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_GridGame
{
    class Messages : Map
    {
        public void PrintStepsAndKeys()
        {
            Console.Write($"you have taken: {player.NumberOfTurns} steps");
            Console.Write($"\nYou have: {player.NumberOfKey} keys.");
            Console.WriteLine();
        }
        public void PrintStartScreen()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t\t\t   A simple game by Christoffer and Knut");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\t\t\t\t\tHave fun!");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
