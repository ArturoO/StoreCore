using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using StoreCore.Factory;
using Microsoft.EntityFrameworkCore;

namespace StoreCore.UserInterface
{
    public class UserUI : ConsoleUI
    {
        override public void registerCommands(Dictionary<string, CommandInfo> commandsMap)
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
            string email = RequiredTextField();
            Console.WriteLine("Please provide username.");
            String username = RequiredTextField();
            Console.WriteLine("Please provide password.");
            string password = RequiredPasswordField();
            Console.WriteLine("Please provide user gender.");
            String gender = Console.ReadLine();
            Console.WriteLine("Please provide user age.");
            int age = RequiredIntField();

            User user = new User(firstName, lastName, gender, age, username, "client", email);
            user.HashPassword(password);

            using (var context = new StoreContext())
            {
                context.Users.Add(user);
                var result = context.SaveChanges();
                if (result==1)
                {
                    var Cart = new Cart(user);
                    context.Carts.Add(Cart);
                    context.SaveChanges();
                    
                    Console.WriteLine($"User added, ID: {user.Id}.");
                }   
                else
                    Console.WriteLine("User not added.");
            }
        }

        public void RegisterAdministrator()
        {
            while (true)
            {
                Console.WriteLine("This store needs an administrator, please provide your details.");
                Console.WriteLine("Please provide first name.");
                String firstName = Console.ReadLine();
                Console.WriteLine("Please provide last name.");
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
                int age;
                int.TryParse(Console.ReadLine(), out age);
                
                User user = new User(firstName, lastName, gender, age, username, "admin", email);
                user.HashPassword(password);

                using (var context = new StoreContext())
                {
                    context.Users.Add(user);
                    var result = context.SaveChanges();
                    if (result == 1)
                    {
                        var Cart = new Cart(user);
                        context.Carts.Add(Cart);
                        context.SaveChanges();

                        Console.WriteLine($"Administrator added, ID: {user.Id}.\r\n");
                    }
                    else
                        Console.WriteLine("Administrator not added.\r\n");

                }
            }
            //bool result = UserDM.Register(user);
        }

        public void Login()
        {
            Console.WriteLine("Please provide username.");
            String username = Console.ReadLine();
            Console.WriteLine("Please provide password.");
            string password = RequiredPasswordField();
            
            using (var context = new StoreContext())
            {
                var User = context.Users
                    .Include(x => x.Cart)
                    .ThenInclude(c => c.Products)
                    .ThenInclude(p => p.Product)
                    .SingleOrDefault(x => x.Username == username);

                if(User==null)
                {
                    Console.WriteLine("Error: credentials incorrect.");
                    return;
                }

                var result = User.ValidatePassword(password);

                if (result)
                {
                    UserFactory.SetCurrentUser(User);
                    Console.WriteLine("Logged in.");
                }
                else
                    Console.WriteLine("Error: credentials incorrect.");
            }

        }

        public void Logout()
        {
            UserFactory.SetCurrentUserAsGuest();
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
            using (var context = new StoreContext())
            {
                var Users = context.Users.ToList();
                Console.WriteLine("------------------------------------------------------------------------------------------------------");
                Console.WriteLine(" Id   | Username   | First name | Last name  | Email                    | Gender     | Age | Type     ");
                Console.WriteLine("------------------------------------------------------------------------------------------------------");

                foreach (var User in Users)
                {
                    Console.WriteLine(String.Format(" {0,-5}| {1,-11}| {2,-11}| {3,-11}| {4,-25}| {5,-11}| {6,-4}| {7,-9}",
                      User.Id, User.Username, User.FirstName, User.LastName, User.Email, User.Gender, User.Age, User.Type));
                    Console.WriteLine("------------------------------------------------------------------------------------------------------");
                }
            }
        }

    }
}
