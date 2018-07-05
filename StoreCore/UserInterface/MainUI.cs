using System;
using System.Collections.Generic;
using System.Text;

namespace StoreCore
{
    class MainUI: IConsoleUI
    {
        protected Dictionary<string, IConsoleUI> consoleUIs;
        protected Dictionary<string, Action> commandsMap;

        public MainUI()
        {
            consoleUIs = new Dictionary<string, IConsoleUI>();
            consoleUIs.Add("MainUI", this);
            consoleUIs.Add("UserUI", new UserUI());
            consoleUIs.Add("ProductUI", new ProductUI());

            commandsMap = new Dictionary<string, Action>();
            foreach (var consoleUI in consoleUIs)
            {
                consoleUI.Value.registerCommands(commandsMap);
            }
        }

        public void Start()
        {
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


        public void registerCommands(Dictionary<string, Action> commandsMap)
        {
            commandsMap.Add("help", help);
        }


        public void help()
        {
            List<string> keys = new List<string>(this.commandsMap.Keys);
            var commands = String.Join(", ", keys.ToArray());
            Console.WriteLine(commands);
        }

    }
}
