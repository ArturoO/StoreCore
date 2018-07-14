using StoreCore.Factory;
using System;
using System.Collections.Generic;
using System.Text;
using StoreCore.DataMapper;
using StoreCore.Entity;

namespace StoreCore.UserInterface
{
    class UserUI: IConsoleUI
    {
        public void registerCommands(Dictionary<string, CommandInfo> commandsMap)
        {
            commandsMap.Add("register", new CommandInfo(new string[] { "guest" }, Register));
            commandsMap.Add("login", new CommandInfo(new string[] { "guest" }, Login));
            commandsMap.Add("logout", new CommandInfo(new string[] {"client", "admin" }, Logout));
            commandsMap.Add("list-users", new CommandInfo(new string[] { "admin" }, ListUsers));
            commandsMap.Add("add-to-cart", new CommandInfo(new string[] { "client", "admin" }, AddToCart));
            commandsMap.Add("view-cart", new CommandInfo(new string[] { "client", "admin" }, ViewCart));
            commandsMap.Add("checkout", new CommandInfo(new string[] { "client", "admin" }, Checkout));
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
            List<User> users = UserDM.List();

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

        public void AddToCart()
        {
            Console.WriteLine("Please provide product Id.");
            int productId = int.Parse(Console.ReadLine());
            Console.WriteLine("Please provide quantity.");
            int qty = int.Parse(Console.ReadLine());

            Product product = ProductDM.FindById(productId);
            if (product.Id == 0)
            { 
                Console.WriteLine("Error: Product doesn't exists.");
                return;
            }

            User user = UserFactory.GetCurrentUser();
            bool result = user.AddToCart(product, qty);
            if(result)
                Console.WriteLine("Product added to cart.");
            else
                Console.WriteLine("Error: Couldn't add product to cart.");
        }

        public void ViewCart()
        {
            User user = UserFactory.GetCurrentUser();
            List<CartProduct> cartProducts = user.Cart.Products;

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine(" Name      | Price     | Category  | Quantity  ");
            Console.WriteLine("-----------------------------------------------");

            foreach (var cartProduct in cartProducts)
            {
                Console.WriteLine(String.Format(" {0,-10}| {1,-10}| {2,-10}| {3,-10}",
                   cartProduct.Product.Name, cartProduct.Product.Price, cartProduct.Product.Category, cartProduct.Qty));
                Console.WriteLine("-----------------------------------------------");
            }

            Console.WriteLine(String.Format(" Items: {0,39}", user.Cart.Qty));
            Console.WriteLine(String.Format(" Total: {0,39}", user.Cart.Price));
            Console.WriteLine("-----------------------------------------------");
        }

        public void Checkout()
        {
            User user = UserFactory.GetCurrentUser();

            Console.WriteLine("You're about to make a new order.");
            Console.WriteLine($"Currently you have {user.Cart.Qty} products in your cart, total price is: {user.Cart.Price}.");
            Console.WriteLine("Do you want to proceed? (yes/no)");

            string input = Console.ReadLine();
            if (input == "yes")
            {
                user.Cart.Checkout();
                Console.WriteLine("Order has been made.");
            }
            else
            {
                Console.WriteLine("Checkout canceled.");
            }

        }

    }
}
