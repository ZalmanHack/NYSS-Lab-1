using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1
{
    public static class ShowAs
    {
        private static void CustomShow(string info, ConsoleColor FC, ConsoleColor BC = ConsoleColor.Black)
        {
            var oldFColor = Console.ForegroundColor;
            var oldBColor = Console.BackgroundColor;
            Console.ForegroundColor = FC;
            Console.BackgroundColor = BC;
            Console.Write(info);
            Console.ForegroundColor = oldFColor;
            Console.BackgroundColor = oldBColor;
        }

        public static void Error(string info)
        {
            CustomShow(info, ConsoleColor.Red);
        }

        public static void Improve(string info)
        {
            CustomShow(info, ConsoleColor.Green);
        }
    }
}
