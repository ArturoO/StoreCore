using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace StoreCore
{
    class Product
    {


        public static bool add(string name, string description, decimal price, string category)
        {
            using (var client = new SqlConnection("Data Source=ARTUROO-PC;Initial Catalog=Store;Integrated Security=True;Pooling=False"))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.AppendLine("INSERT INTO Products(name, description, price, category) ");
                sbCmd.AppendFormat("VALUES ('{0}', '{1}', {2}, '{3}') ", name, description, price, category);

                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                    return true;
                else
                    return false;                
            }
        }

        public static bool edit(int Id, string name, string description, decimal price, string category)
        {
            using (var client = new SqlConnection("Data Source=ARTUROO-PC;Initial Catalog=Store;Integrated Security=True;Pooling=False"))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.Append("UPDATE Products ");
                sbCmd.AppendFormat("SET name='{0}', description='{1}', price={2}, category='{3}' ", name, description, price, category);
                sbCmd.AppendFormat("WHERE Id = {0} ", Id);
                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                    return true;
                else
                    return false;
            }
        }

        public static void list()
        {
            using (var client = new SqlConnection("Data Source=ARTUROO-PC;Initial Catalog=Store;Integrated Security=True;Pooling=False"))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.AppendLine("SELECT Id, name, price, category FROM Products");
                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                using (var reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine(" Id    | Name      | Price     | Category  ");
                    Console.WriteLine("-------------------------------------------");

                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format(" {0,-6}| {1,-10}| {2,-10}| {3,-10}", 
                            reader[0], reader[1], reader[2], reader[3]));
                        Console.WriteLine("-------------------------------------------");
                    }
                }
            }
        }

        public static void show(int Id)
        {
            using (var client = new SqlConnection("Data Source=ARTUROO-PC;Initial Catalog=Store;Integrated Security=True;Pooling=False"))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.AppendFormat("SELECT * FROM Products WHERE Id={0}", Id);
                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Id: {reader[0]}");
                        Console.WriteLine($"Name: {reader[1]}");
                        Console.WriteLine($"Price: {reader[3]}");
                        Console.WriteLine($"Category: {reader[4]}");
                        Console.WriteLine($"Description:\r\n{reader[2]}");
                    }
                }
            }
        }

        public static bool delete(int Id)
        {
            using (var client = new SqlConnection("Data Source=ARTUROO-PC;Initial Catalog=Store;Integrated Security=True;Pooling=False"))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.AppendFormat("DELETE FROM Products WHERE Id = {0}", Id);
                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                    return true;
                else
                    return false;
            }
        }

    }
}
