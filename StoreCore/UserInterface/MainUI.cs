﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoreCore.Factory;
using StoreCore.Entities;

namespace StoreCore.UserInterface
{
    public class MainUI : ConsoleUI
    {
        protected Dictionary<string, ConsoleUI> consoleUIs;
        protected Dictionary<string, CommandInfo> commandsMap;

        public MainUI()
        {
            consoleUIs = new Dictionary<string, ConsoleUI>();
            consoleUIs.Add("MainUI", this);
            consoleUIs.Add("UserUI", new UserUI());
            consoleUIs.Add("ProductUI", new ProductUI());
            consoleUIs.Add("CartUI", new CartUI());
            consoleUIs.Add("OrderUI", new OrderUI());

            commandsMap = new Dictionary<string, CommandInfo>();
            foreach (var consoleUI in consoleUIs)
            {
                consoleUI.Value.registerCommands(commandsMap);
            }
        }

        public void Start()
        {   
            CreateAdminIfNotExists();

            Console.WriteLine("Welcome to our store!\r\nHow can we help you?");
            Console.WriteLine("To display a list of all available commands type: help");

            while (true)
            {
                String command = Console.ReadLine();
                if (command == "exit")
                    break;
                else if (commandsMap.ContainsKey(command))
                {   
                    if(CurrentUserCan(commandsMap[command]))
                        commandsMap[command].callable();
                    else
                        Console.WriteLine("Error: You're not allowed to use this command.");
                }
                else
                    Console.WriteLine("Error: Incorrect command. Type 'help' for a list of commands. Type 'exit' to exit the program.");
            }
        }

        public void CreateAdminIfNotExists()
        {
            using (var context = new StoreContext())
            {
                var Admin = context.Users.SingleOrDefault(x => x.Type == "admin");
                if (Admin == null)
                {
                    UserUI UserUI = new UserUI();
                    UserUI.RegisterAdministrator();
                }
            }
        }

        public bool CurrentUserCan(CommandInfo commandInfo)
        {
            string userType = UserFactory.GetCurrentUserType();
            int result = Array.IndexOf(commandInfo.users, userType);
            if (result >= 0)
                return true;
            else
                return false;
        }

        override public void registerCommands(Dictionary<string, CommandInfo> commandsMap)
        {
            commandsMap.Add("help", new CommandInfo(new string[] { "guest", "client", "admin" }, Help));
        }

        public void Help()
        {            
            string userType = UserFactory.GetCurrentUserType();
            List<string> commandsList = new List<string> { "exit" };
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
