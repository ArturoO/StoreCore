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
            User user = UserFactory.GetCurrentUser();

            Console.WriteLine("Please provide product Id.");
            int productId = int.Parse(Console.ReadLine());
            Product product = ProductDM.FindById(productId);
            if (product.Id == 0)
            {
                Console.WriteLine("Error: Product doesn't exists.");
                return;
            }
            if (user.Cart.ProductExists(productId))
            {
                Console.WriteLine("Error: Product already exists in cart. Use 'cart-update' to change the quantity.");
                return;
            }

            Console.WriteLine("Please provide quantity.");
            int qty = int.Parse(Console.ReadLine());
            if(qty<=0)
            {
                Console.WriteLine("Error: Quantity must be a positive number.");
                return;
            }

            bool result = user.Cart.AddProduct(product, qty);
            if (result)
                Console.WriteLine("Product added to cart.");
            else
                Console.WriteLine("Error: Couldn't add product to cart.");
        }

        public void Update()
        {
            User user = UserFactory.GetCurrentUser();
            Console.WriteLine("Please provide product Id.");
            int productId = int.Parse(Console.ReadLine());
            Product product = ProductDM.FindById(productId);
            if (product.Id == 0)
            {
                Console.WriteLine("Error: Product doesn't exists.");
                return;
            }
            if (!user.Cart.ProductExists(productId))
            {
                Console.WriteLine("Error: You must first add product to cart.");
                return;
            }

            Console.WriteLine("Please provide new quantity.");
            int qty = int.Parse(Console.ReadLine());
            if (qty <= 0)
            {
                Console.WriteLine("Error: Quantity must be a positive number.");
                return;
            }

            bool result = user.Cart.UpdateProduct(product, qty);
            if (result)
                Console.WriteLine("Cart updated.");
            else
                Console.WriteLine("Error: Couldn't update cart.");
        }

        public void Remove()
        {
            User user = UserFactory.GetCurrentUser();
            Console.WriteLine("Please provide product Id.");
            int productId = int.Parse(Console.ReadLine());
            Product product = ProductDM.FindById(productId);
            if (product.Id == 0)
            {
                Console.WriteLine("Error: Product doesn't exists.");
                return;
            }
            if (!user.Cart.ProductExists(productId))
            {
                Console.WriteLine("Error: You can't remove product that wasn't added to cart.");
                return;
            }

            bool result = user.Cart.RemoveProduct(product);
            if (result)
                Console.WriteLine("Product removed from cart.");
            else
                Console.WriteLine("Error: Couldn't remove product from cart.");
        }

        public void View()
        {
            User user = UserFactory.GetCurrentUser();
            List<CartProduct> cartProducts = user.Cart.Products;

            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine(" Id        | Name      | Price     | Category  | Quantity  ");
            Console.WriteLine("-----------------------------------------------------------");

            foreach (var cartProduct in cartProducts)
            {
                Console.WriteLine(String.Format(" {0,-10}| {1,-10}| {2,-10}| {3,-10}| {4,-10}",
                   cartProduct.Product.Id, cartProduct.Product.Name, cartProduct.Product.Price, cartProduct.Product.Category, cartProduct.Qty));
                Console.WriteLine("-----------------------------------------------------------");
            }

            Console.WriteLine(String.Format(" Items: {0,49}", user.Cart.Qty));
            Console.WriteLine(String.Format(" Total: {0,49}", user.Cart.Price));
            Console.WriteLine("-----------------------------------------------------------");
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
