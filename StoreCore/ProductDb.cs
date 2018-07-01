using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace StoreCore
{
    class ProductDb
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

        public static List<Product> list()
        {
            List<Product> products = new List<Product>();
            using (var client = new SqlConnection("Data Source=ARTUROO-PC;Initial Catalog=Store;Integrated Security=True;Pooling=False"))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.AppendLine("SELECT * FROM Products");
                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = int.Parse(reader[0].ToString());
                        string name = reader[1].ToString();
                        string description = reader[2].ToString();
                        decimal price = decimal.Parse(reader[3].ToString());
                        string category = reader[4].ToString();

                        products.Add(new Product(id, name, description, price, category));
                    }
                }
            }
            return products;
        }

        public static Product show(int Id)
        {
            Product product = new Product();
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
                        product.Id = int.Parse(reader[0].ToString());
                        product.Name = reader[1].ToString();
                        product.Description = reader[2].ToString();
                        product.Price = decimal.Parse(reader[3].ToString());
                        product.Category = reader[4].ToString();
                    }
                }
            }
            return product;
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
