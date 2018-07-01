using System;
using System.Collections.Generic;
using System.Text;

namespace StoreCore
{
    class ProductUI
    { 
        public static void registerCommands(Dictionary<string, Action> commandsMap)
        {
            commandsMap.Add("add-product", addProduct);
            commandsMap.Add("list-products", listProducts);
            commandsMap.Add("show-product", showProduct);
            commandsMap.Add("delete-product", deleteProduct);
            commandsMap.Add("edit-product", editProduct);
        }

        static void listProducts()
        {
            Product.list();
        }

        static void showProduct()
        {
            Console.WriteLine("Please provide product id.");
            int productId = int.Parse(Console.ReadLine());
            Product.show(productId);
        }

        static void deleteProduct()
        {
            Console.WriteLine("Please provide product id.");
            int productId = int.Parse(Console.ReadLine());
            bool result = Product.delete(productId);
            if (result)
                Console.WriteLine("Product deleted.");
            else
                Console.WriteLine("Product not deleted.");
        }

        static void addProduct()
        {
            Console.WriteLine("Please provide product name.");
            String productName = Console.ReadLine();
            Console.WriteLine("Please provide product description.");
            String productDescription = Console.ReadLine();
            Console.WriteLine("Please provide product price.");
            decimal productPrice = Decimal.Parse(Console.ReadLine());
            Console.WriteLine("Please provide product category.");
            String productCategory = Console.ReadLine();

            bool result = Product.add(productName, productDescription, productPrice, productCategory);
            if (result)
                Console.WriteLine("Product added.");
            else
                Console.WriteLine("Product not added.");

        }

        static void editProduct()
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

            bool result = Product.edit(productId, productName, productDescription, productPrice, productCategory);
            if (result)
                Console.WriteLine("Product changed.");
            else
                Console.WriteLine("Product not changed.");

        }
    }
}
