using System;
using System.Collections.Generic;
using System.Text;
using StoreCore.Factory;
using StoreCore.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StoreCore.UserInterface
{
    public class CartUI: ConsoleUI
    {
        override public void registerCommands(Dictionary<string, CommandInfo> commandsMap)
        {
            commandsMap.Add("cart-add", new CommandInfo(new string[] { "client", "admin" }, Add));
            commandsMap.Add("cart-update", new CommandInfo(new string[] { "client", "admin" }, Update));
            commandsMap.Add("cart-remove", new CommandInfo(new string[] { "client", "admin" }, Remove));
            commandsMap.Add("cart-view", new CommandInfo(new string[] { "client", "admin" }, View));
            commandsMap.Add("checkout", new CommandInfo(new string[] { "client", "admin" }, Checkout));
        }

        public void Add()
        {
            var user = UserFactory.GetCurrentUser();

            Console.WriteLine("Please provide product Id.");
            int productId = RequiredIntField();

            using (var context = new StoreContext())
            {
                var Product = context.Products
                    .SingleOrDefault(x => x.Id==productId);
                if(Product==null)
                {
                    Console.WriteLine("Error: Product doesn't exists.");
                    return;
                }

                user.LoadCart();

                var CartProduct = user.Cart
                    .Products
                    .SingleOrDefault(x => x.ProductId == productId);
                if (CartProduct!=null)
                {
                    Console.WriteLine("Error: Product already exists in cart. Use 'cart-update' to change the quantity.");
                    return;
                }

                Console.WriteLine("Please provide quantity.");
                int qty = RequiredIntField();
                if (qty <= 0)
                {
                    Console.WriteLine("Error: Quantity must be a positive number.");
                    return;
                }

                var NewCartProduct = new CartProduct(user.Cart.Id, Product.Id, qty);

                context.CartProducts.Add(NewCartProduct);
                user.Cart.Products.Add(NewCartProduct);                
                user.Cart.UpdateSummary();
                context.Carts.Update(user.Cart);
                
                var result = context.SaveChanges();

                if (result>0)
                    Console.WriteLine("Product added to cart.");
                else
                    Console.WriteLine("Error: Couldn't add product to cart.");
            }

        }

        public void Update()
        {
            var user = UserFactory.GetCurrentUser();
            Console.WriteLine("Please provide product Id.");
            int productId = RequiredIntField();

            using (var context = new StoreContext())
            {
                var Product = context.Products
                    .SingleOrDefault(x => x.Id == productId);
                if (Product == null)
                {
                    Console.WriteLine("Error: Product doesn't exists.");
                    return;
                }
                user.LoadCart();
                var CartProduct = user.Cart
                    .Products
                    .SingleOrDefault(x => x.ProductId == productId);
                if (CartProduct == null)
                {
                    Console.WriteLine("Error: You must first add product to cart.");
                    return;
                }

                Console.WriteLine("Please provide quantity.");
                int qty = RequiredIntField();
                if (qty <= 0)
                {
                    Console.WriteLine("Error: Quantity must be a positive number.");
                    return;
                }

                CartProduct.Qty = qty;
                context.CartProducts.Update(CartProduct);
                user.Cart.UpdateSummary();
                context.Carts.Update(user.Cart);
                var result = context.SaveChanges();

                if (result>0)
                    Console.WriteLine("Cart updated.");
                else
                    Console.WriteLine("Error: Couldn't update cart.");
            }
        }

        public void Remove()
        {
            var user = UserFactory.GetCurrentUser();
            Console.WriteLine("Please provide product Id.");
            int productId = RequiredIntField();

            using (var context = new StoreContext())
            {
                var Product = context.Products
                    .SingleOrDefault(x => x.Id == productId);
                if (Product == null)
                {
                    Console.WriteLine("Error: Product doesn't exists.");
                    return;
                }
                
                user.LoadCart();

                var CartProduct = user.Cart
                    .Products
                    .SingleOrDefault(x => x.ProductId == productId);
                if (CartProduct == null)
                {
                    Console.WriteLine("Error: You must first add product to cart.");
                    return;
                }

                context.CartProducts.Remove(CartProduct);
                user.Cart.Products.Remove(CartProduct);
                user.Cart.UpdateSummary();
                context.Carts.Update(user.Cart);
                var result = context.SaveChanges();

                if (result>0)
                    Console.WriteLine("Product removed from cart.");
                else
                    Console.WriteLine("Error: Couldn't remove product from cart.");
            }
        }

        public void View()
        {
            var User = UserFactory.GetCurrentUser();
            using (var context = new StoreContext())
            {
                User.LoadCart();                
                var Cart = User.Cart;

                Console.WriteLine("-----------------------------------------------------------");
                Console.WriteLine(" Id        | Name      | Price     | Category  | Quantity  ");
                Console.WriteLine("-----------------------------------------------------------");

                if (Cart.Products != null)
                {
                    foreach (var cartProduct in Cart.Products)
                    {
                        Console.WriteLine(String.Format(" {0,-10}| {1,-10}| {2,-10}| {3,-10}| {4,-10}",
                           cartProduct.Product.Id, cartProduct.Product.Name, cartProduct.Product.Price, cartProduct.Product.Category, cartProduct.Qty));
                        Console.WriteLine("-----------------------------------------------------------");
                    }
                }
                Console.WriteLine(String.Format(" Items: {0,49}", Cart.Qty));
                Console.WriteLine(String.Format(" Total: {0,49}", Cart.Price));
                Console.WriteLine("-----------------------------------------------------------");
            }
        }

        public void Checkout()
        {
            var user = UserFactory.GetCurrentUser();

            Console.WriteLine("You're about to make a new order.");
            Console.WriteLine($"Currently you have {user.Cart.Qty} products in your cart, total price is: {user.Cart.Price}.");
            Console.WriteLine("Do you want to proceed? (yes/no)");

            string input = Console.ReadLine();
            if (input == "yes")
            {
                var orderId = user.Cart.Checkout();
                Console.WriteLine($"Order ID:{orderId} has been made.");
            }
            else
            {
                Console.WriteLine("Checkout canceled.");
            }
        }

    }
}
