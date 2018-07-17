using System;
using System.Collections.Generic;
using System.Text;
using StoreCore.Factory;
using StoreCore.DataMapper;
using StoreCore.Entity;

namespace StoreCore.UserInterface
{
    class CartUI: IConsoleUI
    {
        
        public void registerCommands(Dictionary<string, CommandInfo> commandsMap)
        {
            commandsMap.Add("cart-add", new CommandInfo(new string[] { "client", "admin" }, Add));
            commandsMap.Add("cart-update", new CommandInfo(new string[] { "client", "admin" }, Update));
            commandsMap.Add("cart-remove", new CommandInfo(new string[] { "client", "admin" }, Remove));
            commandsMap.Add("cart-view", new CommandInfo(new string[] { "client", "admin" }, View));
            commandsMap.Add("checkout", new CommandInfo(new string[] { "client", "admin" }, Checkout));
        }

        public void Add()
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
            if (result)
                Console.WriteLine("Product added to cart.");
            else
                Console.WriteLine("Error: Couldn't add product to cart.");
        }

        public void Update()
        {

        }

        public void Remove()
        {

        }

        public void View()
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
