using System;
using System.Data.SqlClient;

namespace StoreCore
{
    class Program
    {
        static void Main(string[] args)
        {
            configure();
            Console.WriteLine("Welcome to our store!\r\nHow can we help you?");
            Console.WriteLine("Commands: exit, list-products, show-product, add-product, delete-product, edit-product.");

            while (true)
            {
                String input = Console.ReadLine();

                if (input == "add-product")
                    addProduct();

                if (input == "list-products")
                    listProducts();

                if (input == "show-product")
                    showProduct();

                if (input == "delete-product")
                    deleteProduct();

                if (input == "edit-product")
                    editProduct();

                if (input == "exit")
                    break;

                Console.WriteLine("Please specify a command.");
                Console.WriteLine("Commands: exit, list-products, add-product, delete-product, show-product.");
            }

        }

        static void configure()
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
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
            if(result)
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


        static void testDb()
        {
            using (var client = new SqlConnection("Data Source=ARTUROO-PC;Initial Catalog=Store;Integrated Security=True;Pooling=False"))
            {
                client.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Products", client);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader[0]}:{reader[1]}");
                    }
                }

            }
        }

    }
}
