using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace StoreCore
{
    class Program
    {
        protected static Dictionary<string, Action> commandsMap;

        static void Main(string[] args)
        {
            configure();
            Console.WriteLine("Welcome to our store!\r\nHow can we help you?");
            Console.WriteLine("To display a list of all available commands type: help");

            while (true)
            {
                String input = Console.ReadLine();

                if (input == "exit")
                    break;
                else if (commandsMap.ContainsKey(input))
                    commandsMap[input]();
                else
                    Console.WriteLine("Incorrect command. Type 'help' for a list of commands. Type 'exit' to exit the program.");
            }

        }

        static void configure()
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            commandsMap = new Dictionary<string, Action>();
            ProductUI.registerCommands(commandsMap);
            UserUI.registerCommands(commandsMap);

            commandsMap.Add("help", listCommands);
        }

        static void listCommands()
        {
            List<string> keys = new List<string>(commandsMap.Keys);
            var commands = String.Join(", ", keys.ToArray());
            Console.WriteLine(commands);
        }

    }
}
