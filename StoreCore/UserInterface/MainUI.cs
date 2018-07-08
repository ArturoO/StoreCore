using System;
using System.Collections.Generic;
using System.Text;
using StoreCore.Factory;

namespace StoreCore
{
    class MainUI: IConsoleUI
    {
        protected Dictionary<string, IConsoleUI> consoleUIs;
        protected Dictionary<string, CommandInfo> commandsMap;

        public MainUI()
        {
            consoleUIs = new Dictionary<string, IConsoleUI>();
            consoleUIs.Add("MainUI", this);
            consoleUIs.Add("UserUI", new UserUI());
            consoleUIs.Add("ProductUI", new ProductUI());

            commandsMap = new Dictionary<string, CommandInfo>();
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
                    string userType = UserFactory.GetCurrentUserType();
                    int result = Array.IndexOf(commandsMap[input].users, userType);
                    if(result>=0)
                        commandsMap[input].callable();
                    else
                        Console.WriteLine("Error: You're not allowed to use this command.");
                }
                else
                    Console.WriteLine("Error: Incorrect command. Type 'help' for a list of commands. Type 'exit' to exit the program.");
            }
        }


        public void registerCommands(Dictionary<string, CommandInfo> commandsMap)
        {
            commandsMap.Add("help", new CommandInfo(new string[] { "guest", "client", "admin" }, Help));
        }


        public void Help()
        {            
            string userType = UserFactory.GetCurrentUserType();
            List<string> commandsList = new List<string>();
            foreach (var commandMap in this.commandsMap)
            {
                if (Array.IndexOf(commandMap.Value.users, userType) >= 0)
                    commandsList.Add(commandMap.Key);
            }

            var commands = String.Join(", ", commandsList.ToArray());
            Console.WriteLine(commands);
        }

    }
}
