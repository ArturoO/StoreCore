using System;
using System.Collections.Generic;
using System.Text;

namespace StoreCore
{
    class UserUI: IConsoleUI
    {

        protected UserDM userDM;

        public UserUI()
        {
            userDM = new UserDM();
        }

        public void registerCommands(Dictionary<string, Action> commandsMap)
        {
            commandsMap.Add("add-user", addUser);
            commandsMap.Add("list-users", listUsers);
        }

        public void addUser()
        {
            Console.WriteLine("Please provide user first name.");
            String firstName = Console.ReadLine();
            Console.WriteLine("Please provide user last name.");
            String lastName = Console.ReadLine();
            Console.WriteLine("Please provide user gender.");
            String gender = Console.ReadLine();
            Console.WriteLine("Please provide user age.");
            int age = int.Parse(Console.ReadLine());

            User user = new User(firstName, lastName, gender, age);
            bool result = userDM.Create(user);

            if (result)
                Console.WriteLine($"User added, ID: {user.Id}.");
            else
                Console.WriteLine("User not added.");
        }

        public void listUsers()
        {
            List<User> users = userDM.list();

            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine(" Id    | First name    | Last name     | Gender    | Age       ");
            Console.WriteLine("---------------------------------------------------------------");

            foreach (var user in users)
            {
                Console.WriteLine(String.Format(" {0,-6}| {1,-14}| {2,-14}| {3,-10}| {4,-10}",
                  user.Id, user.FirstName, user.LastName, user.Gender, user.Age));
                Console.WriteLine("---------------------------------------------------------------");
            }
        }
    }
}
