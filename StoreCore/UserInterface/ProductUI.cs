using System;
using System.Collections.Generic;
using System.Text;
using StoreCore.DataMapper;
using StoreCore.Entity;
using System.Linq;

namespace StoreCore.UserInterface
{
    public class ProductUI : IConsoleUI
    {
        
        public void registerCommands(Dictionary<string, CommandInfo> commandsMap)
        {
            commandsMap.Add("add-product", new CommandInfo(new string[] { "admin" }, addProduct));
            commandsMap.Add("list-products", new CommandInfo(new string[] { "guest", "client", "admin" }, ListProducts));
            commandsMap.Add("show-product", new CommandInfo(new string[] { "guest", "client", "admin" }, showProduct));
            commandsMap.Add("delete-product", new CommandInfo(new string[] { "admin" }, deleteProduct));
            commandsMap.Add("edit-product", new CommandInfo(new string[] { "admin" }, editProduct));
        }

        public void ListProducts()
        {
            List<Product> products = ProductDM.List();

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine(" Id    | Name      | Price     | Category  ");
            Console.WriteLine("-------------------------------------------");
            foreach (var product in products)
            {
                Console.WriteLine(String.Format(" {0,-6}| {1,-10}| {2,-10}| {3,-10}",
                product.Id, product.Name, product.Price, product.Category));
                Console.WriteLine("-------------------------------------------");
            }
        }

        public void showProduct()
        {
            Console.WriteLine("Please provide product id.");
            int productId = int.Parse(Console.ReadLine());

            Product product = ProductDM.FindById(productId);            

            Console.WriteLine($"Id: {product.Id}");
            Console.WriteLine($"Name: {product.Name}");
            Console.WriteLine($"Price: {product.Price}");
            Console.WriteLine($"Category: {product.Category}");
            Console.WriteLine($"Description:\r\n{product.Description}");
        }

        public void deleteProduct()
        {
            Console.WriteLine("Please provide product id.");
            int productId = int.Parse(Console.ReadLine());
            bool result = ProductDM.Delete(productId);
            if (result)
                Console.WriteLine("Product deleted.");
            else
                Console.WriteLine("Product not deleted.");
        }

        public void addProduct()
        {
            Console.WriteLine("Please provide product name.");
            String productName = Console.ReadLine();
            Console.WriteLine("Please provide product description.");
            String productDescription = Console.ReadLine();
            Console.WriteLine("Please provide product price.");
            decimal productPrice = Decimal.Parse(Console.ReadLine());
            Console.WriteLine("Please provide product category.");
            String productCategory = Console.ReadLine();

            Product product = new Product(productName, productDescription, productPrice, productCategory);
            bool result = ProductDM.Create(product);

            if (result)
                Console.WriteLine($"Product added, Id: {product.Id} .");
            else
                Console.WriteLine("Product not added.");

        }

        public void editProduct()
        {
            Console.WriteLine("Please provide product Id.");
            int productId = int.Parse(Console.ReadLine());
            Product product = ProductDM.FindById(productId);
            if (product.Id == 0)
            {
                Console.WriteLine("Error: Product doesn't exists.");
                return;
            }

            string[] productFields = new string[] { "name", "description", "price", "category" };
            
            while(true)
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

                //if (Array.con(productFields, input);
                switch (input)
                {
                    case "name":
                        Console.WriteLine("Old value:");
                        Console.WriteLine(product.Name);
                        Console.WriteLine("Specify new value:");
                        value = Console.ReadLine();
                        product.Name = value;
                        break;
                    case "description":
                        Console.WriteLine("Old value:");
                        Console.WriteLine(product.Description);
                        Console.WriteLine("Specify new value:");
                        value = Console.ReadLine();
                        product.Description= value;
                        break;
                    case "price":
                        Console.WriteLine("Old value:");
                        Console.WriteLine(product.Price);
                        Console.WriteLine("Specify new value:");
                        value = Console.ReadLine();
                        product.Price = decimal.Parse(value);
                        break;
                    case "category":
                        Console.WriteLine("Old value:");
                        Console.WriteLine(product.Category);
                        Console.WriteLine("Specify new value:");
                        value = Console.ReadLine();
                        product.Category = value;
                        break;
                    default:
                        Console.WriteLine("Error: Field doesn't exist, provide valid field");
                        break;
                }
            }
           
            var result = ProductDM.Update(product);

            if (result)
                Console.WriteLine("Product changed.");
            else
                Console.WriteLine("Product not changed.");

        }
    }
}
