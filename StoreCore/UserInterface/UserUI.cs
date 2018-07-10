using StoreCore.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreCore
{
    class UserUI: IConsoleUI
    {
        public void registerCommands(Dictionary<string, CommandInfo> commandsMap)
        {
            commandsMap.Add("register", new CommandInfo(new string[] { "guest" }, Register));
            commandsMap.Add("login", new CommandInfo(new string[] { "guest" }, Login));
            commandsMap.Add("logout", new CommandInfo(new string[] {"client", "admin" }, Logout));
            commandsMap.Add("list-users", new CommandInfo(new string[] { "admin" }, ListUsers));
        }

        public void Register()
        {
            Console.WriteLine("Please provide user first name.");
            String firstName = Console.ReadLine();
            Console.WriteLine("Please provide user last name.");
            String lastName = Console.ReadLine();
            Console.WriteLine("Please provide email.");
            String email = Console.ReadLine();
            Console.WriteLine("Please provide username.");
            String username = Console.ReadLine();            
            Console.WriteLine("Please provide password.");
            String password = Console.ReadLine();
            Console.WriteLine("Please provide user gender.");
            String gender = Console.ReadLine();
            Console.WriteLine("Please provide user age.");
            int age = int.Parse(Console.ReadLine());

            User user = new User(firstName, lastName, gender, age, username, "client", email);            
            user.HashPassword(password);

            bool result = UserDM.Register(user);

            if (result)
                Console.WriteLine($"User added, ID: {user.Id}.");
            else
                Console.WriteLine("User not added.");
        }

        public void Login()
        {
            Console.WriteLine("Please provide username.");
            String username = Console.ReadLine();
            Console.WriteLine("Please provide password.");
            String password = Console.ReadLine();
            
            User user = UserDM.FindByUsername(username);
            var result = user.ValidatePassword(password);

            if (result)
            {
                UserFactory.SetCurrentUser(user);
                Console.WriteLine("Logged in.");
            }
            else
                Console.WriteLine("Error: credentials incorrect.");
        }

        public void Logout()
        {
            UserFactory.SetCurrentUserAsGuest();
            //UserFactory.SetCurrentUser(null);
            Console.WriteLine("You were logged out.");
        }

        //public void addUser()
        //{
        //    Console.WriteLine("Please provide user first name.");
        //    String firstName = Console.ReadLine();
        //    Console.WriteLine("Please provide user last name.");
        //    String lastName = Console.ReadLine();
        //    Console.WriteLine("Please provide user gender.");
        //    String gender = Console.ReadLine();
        //    Console.WriteLine("Please provide user age.");
        //    int age = int.Parse(Console.ReadLine());

        //    User user = new User(firstName, lastName, gender, age);
        //    bool result = UserDM.Create(user);

        //    if (result)
        //        Console.WriteLine($"User added, ID: {user.Id}.");
        //    else
        //        Console.WriteLine("User not added.");
        //}

        public void ListUsers()
        {
            List<User> users = UserDM.list();

            //email, type, username
            Console.WriteLine("------------------------------------------------------------------------------------------------------");
            Console.WriteLine(" Id   | Username   | First name | Last name  | Email                    | Gender     | Age | Type     ");
            Console.WriteLine("------------------------------------------------------------------------------------------------------");

            foreach (var user in users)
            {
                Console.WriteLine(String.Format(" {0,-5}| {1,-11}| {2,-11}| {3,-11}| {4,-25}| {5,-11}| {6,-4}| {7,-9}",
                  user.Id, user.Username, user.FirstName, user.LastName, user.Email, user.Gender, user.Age, user.Type));
                Console.WriteLine("------------------------------------------------------------------------------------------------------");
            }
        }
    }
}
