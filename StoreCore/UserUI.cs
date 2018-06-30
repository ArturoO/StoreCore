using System;
using System.Collections.Generic;
using System.Text;

namespace StoreCore
{
    class UserUI
    {
        public static void registerCommands(ref Dictionary<string, Action> commandsMap)
        {
            commandsMap.Add("add-user", addUser);
            commandsMap.Add("list-users", listUsers);
        }

        public static void addUser()
        {
            Console.WriteLine("Please provide user first name.");
            String userFirstName = Console.ReadLine();
            Console.WriteLine("Please provide user last name.");
            String userLastName = Console.ReadLine();
            Console.WriteLine("Please provide user gender.");
            String userGender = Console.ReadLine();
            Console.WriteLine("Please provide user age.");
            int userAge = int.Parse(Console.ReadLine());

            bool result = User.add(userFirstName, userLastName, userGender, userAge);
            if (result)
                Console.WriteLine("User added.");
            else
                Console.WriteLine("User not added.");
        }

        static void listUsers()
        {
            User.list();
        }

    }
}
