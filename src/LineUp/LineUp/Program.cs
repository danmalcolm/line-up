using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LineUp.Commands;
using LineUp.Configuration;

namespace LineUp
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowIntro();
            if (args.Length == 0)
            {
                ShowUsage();
                return;
            }
            string command = args.First().ToLowerInvariant();
            switch (command)
            {
                case "use":
                    new UseCommand().Execute(args.Skip(1).ToArray());
                    break;
                default:
                    ShowUsage();
                    break;
            }
        }

        private static void ShowIntro()
        {
            Console.WriteLine(@"Running LineUp");
        }

        private static void ShowUsage()
        {
            Console.WriteLine("Unable to work out what you're trying to do.");
            Console.WriteLine(@"Example usage: lu use <component> <version>");
        }
    }
}
