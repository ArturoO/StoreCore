using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace StoreCore
{
    class Program
    {

        protected static Dictionary<string, Action> commandsMap;

        static void Main(string[] args)
        {
            configure();
            Console.WriteLine("Welcome to our store!\r\nHow can we help you?");
            Console.WriteLine("Commands: exit, list-products, show-product, add-product, delete-product, edit-product.");

            while (true)
            {
                String input = Console.ReadLine();

                if (input == "exit")
                    break;
                else if (commandsMap.ContainsKey(input))
                    commandsMap[input]();
                else
                    Console.WriteLine("Incorrect command.");
                
                Console.WriteLine("Please specify a command.");
                Console.WriteLine("Commands: exit, list-products, add-product, delete-product, show-product.");
            }

        }

        static void configure()
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            commandsMap = new Dictionary<string, Action>();
            ProductUI.registerCommands(ref commandsMap);
        }

    }
}
