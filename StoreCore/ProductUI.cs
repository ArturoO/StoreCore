using System;
using System.Collections.Generic;
using System.Text;

namespace StoreCore
{
    class ProductUI: IConsoleUI
    { 
        public void registerCommands(Dictionary<string, Action> commandsMap)
        {
            commandsMap.Add("add-product", addProduct);
            commandsMap.Add("list-products", ListProducts);
            commandsMap.Add("show-product", showProduct);
            commandsMap.Add("delete-product", deleteProduct);
            commandsMap.Add("edit-product", editProduct);
        }

        public void ListProducts()
        {
            List<Product> products = ProductDb.List();

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

            Product product = ProductDb.FindById(productId);            

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
            bool result = ProductDb.Delete(productId);
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
            bool result = ProductDb.Create(product);

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
            var result = ProductDb.Update(product);

            //var result = product.Update(product);
            //var result = product.Update();
            
            if (result)
                Console.WriteLine("Product changed.");
            else
                Console.WriteLine("Product not changed.");

        }
    }
}
