using System;
using System.Collections.Generic;
using System.Text;
using StoreCore.DataMapper;
using StoreCore.Entity;

namespace StoreCore.UserInterface
{
    class ProductUI: IConsoleUI
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
            Console.WriteLine("Please provide product name.");
            String productName = Console.ReadLine();
            Console.WriteLine("Please provide product description.");
            String productDescription = Console.ReadLine();
            Console.WriteLine("Please provide product price.");
            decimal productPrice = Decimal.Parse(Console.ReadLine());
            Console.WriteLine("Please provide product category.");
            String productCategory = Console.ReadLine();

            Product product = new Product(productId, productName, productDescription, productPrice, productCategory);
            var result = ProductDM.Update(product);

            if (result)
                Console.WriteLine("Product changed.");
            else
                Console.WriteLine("Product not changed.");

        }
    }
}
