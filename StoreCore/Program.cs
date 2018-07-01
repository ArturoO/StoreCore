using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace StoreCore
{
    class Program
    {

        protected static Dictionary<string, IConsoleUI> consoleUIs;
        protected static Dictionary<string, Action> commandsMap;

        static void Main(string[] args)
        {
            config();
            Console.WriteLine("Welcome to our store!\r\nHow can we help you?");
            Console.WriteLine("To display a list of all available commands type: help");

            while (true)
            {
                String input = Console.ReadLine();

                if (input == "exit")
                    break;
                else if (commandsMap.ContainsKey(input))
                {
                    commandsMap[input]();
                }
                else
                    Console.WriteLine("Incorrect command. Type 'help' for a list of commands. Type 'exit' to exit the program.");
            }

        }

        static void config()
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            Program.consoleUIs = new Dictionary<string, IConsoleUI>();
            Program.consoleUIs.Add("UserUI", new UserUI());
            Program.consoleUIs.Add("ProductUI", new ProductUI());

            Program.commandsMap = new Dictionary<string, Action>();
            foreach (var consoleUI in consoleUIs)
            {
                consoleUI.Value.registerCommands(Program.commandsMap);
            }

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
