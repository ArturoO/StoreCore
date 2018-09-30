using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using StoreCore.Entities;

namespace StoreCore.UserInterface
{
    public class ProductUI : ConsoleUI
    {

        override public void registerCommands(Dictionary<string, CommandInfo> commandsMap)
        {
            commandsMap.Add("add-product", new CommandInfo(new string[] { "admin" }, addProduct));
            commandsMap.Add("list-products", new CommandInfo(new string[] { "guest", "client", "admin" }, ListProducts));
            commandsMap.Add("show-product", new CommandInfo(new string[] { "guest", "client", "admin" }, showProduct));
            commandsMap.Add("delete-product", new CommandInfo(new string[] { "admin" }, deleteProduct));
            commandsMap.Add("edit-product", new CommandInfo(new string[] { "admin" }, editProduct));
        }

        public void ListProducts()
        {
            using(var context = new StoreContext())
            {
                var Products = context.Products.ToList();

                Console.WriteLine("-------------------------------------------");
                Console.WriteLine(" Id    | Name      | Price     | Category  ");
                Console.WriteLine("-------------------------------------------");
                foreach (var Product in Products)
                {
                    Console.WriteLine(String.Format(" {0,-6}| {1,-10}| {2,-10}| {3,-10}",
                        Product.Id, Product.Name, Product.Price, Product.Category));
                    Console.WriteLine("-------------------------------------------");
                }
            }
        }

        public void showProduct()
        {
            Console.WriteLine("Please provide product id.");
            int productId = RequiredIntField();

            using (var context = new StoreContext())
            {
                var Product = context.Products.Single(x => x.Id == productId);

                Console.WriteLine($"Id: {Product.Id}");
                Console.WriteLine($"Name: {Product.Name}");
                Console.WriteLine($"Price: {Product.Price}");
                Console.WriteLine($"Category: {Product.Category}");
                Console.WriteLine($"Description:\r\n{Product.Description}");
            }
        }

        public void deleteProduct()
        {
            Console.WriteLine("Please provide product id.");
            int productId = RequiredIntField();

            using (var context = new StoreContext())
            {
                var Product = context.Products.Single(x => x.Id == productId);
                context.Products.Remove(Product);
                var result = context.SaveChanges();
                if (result == 1)
                    Console.WriteLine("Product deleted.");
                else
                    Console.WriteLine("Product not deleted.");
            }
        }

        public void addProduct()
        {
            Console.WriteLine("Please provide product name.");
            String productName = RequiredTextField();
            Console.WriteLine("Please provide product description.");
            String productDescription = Console.ReadLine();
            Console.WriteLine("Please provide product price.");
            decimal productPrice = RequiredDecimalField();
            Console.WriteLine("Please provide product category.");
            String productCategory = Console.ReadLine();

            Product product = new Product(productName, productDescription, productPrice, productCategory);

            using (var context = new StoreContext())
            {
                context.Products.Add(product);
                var result = context.SaveChanges();
                if (result==1)
                    Console.WriteLine($"Product added, Id: {product.Id} .");
                else
                    Console.WriteLine("Product not added.");
            }
        }

        public void editProduct()
        {
            Console.WriteLine("Please provide product Id.");
            int productId;
            int.TryParse(Console.ReadLine(), out productId);

            using (var context = new StoreContext())
            {
                var Product = context.Products.SingleOrDefault(x => x.Id == productId);
                if (Product == null)
                {
                    Console.WriteLine("Error: Product doesn't exists.");
                    return;
                }

                string[] productFields = new string[] { "name", "description", "price", "category" };

                while (true)
                {
                    Console.WriteLine("Which field do you want to update? " +
                        "(Fields: " + string.Join(", ", productFields) + "). " +
                        "Type 'update' to update product. Type 'cancel' to skip update.");
                    String input = Console.ReadLine();
                    String value;

                    if (input == "update")
                        break;
                    if (input == "cancel")
                        return;

                    switch (input)
                    {
                        case "name":
                            Console.WriteLine("Old value:");
                            Console.WriteLine(Product.Name);
                            Console.WriteLine("Specify new value:");
                            value = RequiredTextField();
                            Product.Name = value;
                            break;
                        case "description":
                            Console.WriteLine("Old value:");
                            Console.WriteLine(Product.Description);
                            Console.WriteLine("Specify new value:");
                            value = Console.ReadLine();
                            Product.Description = value;
                            break;
                        case "price":
                            Console.WriteLine("Old value:");
                            Console.WriteLine(Product.Price);
                            Console.WriteLine("Specify new value:");
                            decimal price = RequiredDecimalField();
                            Product.Price = price;
                            break;
                        case "category":
                            Console.WriteLine("Old value:");
                            Console.WriteLine(Product.Category);
                            Console.WriteLine("Specify new value:");
                            value = Console.ReadLine();
                            Product.Category = value;
                            break;
                        default:
                            Console.WriteLine("Error: Field doesn't exist, provide valid field");
                            break;
                    }
                }

                var result = context.SaveChanges();

                if (result==1)
                    Console.WriteLine("Product changed.");
                else
                    Console.WriteLine("Product not changed.");

            }
        }
    }
}
