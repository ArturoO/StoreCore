using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace StoreCore
{
    class ProductDM
    {

        protected const string connectionString = "Data Source=ARTUROO-PC;Initial Catalog=Store;Integrated Security=True;Pooling=False";

        public static bool Create(Product product)
        {
            using (var client = new SqlConnection(connectionString))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.AppendFormat(
                    "INSERT INTO Products(name, description, price, category) OUTPUT INSERTED.Id"
                    + " VALUES ('{0}', '{1}', {2}, '{3}') ", 
                    product.Name, product.Description, product.Price, product.Category);

                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                var insertedId = int.Parse(cmd.ExecuteScalar().ToString());
                if (insertedId > 0)
                {
                    product.Id = insertedId;
                    return true;
                }
                else
                { 
                    return false;
                }
            }
        }

        public static bool Update(Product product)
        {
            using (var client = new SqlConnection(connectionString))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.AppendFormat(
                    "UPDATE Products"
                    + " SET name='{0}', description='{1}', price={2}, category='{3}'"
                    + " WHERE Id = {4} ", 
                    product.Name, product.Description, product.Price, product.Category, product.Id);
                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                var result = cmd.ExecuteNonQuery();
                if (result > 0)
                    return true;
                else
                    return false;
            }
        }

        public static List<Product> List()
        {
            List<Product> products = new List<Product>();
            using (var client = new SqlConnection(connectionString))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.Append("SELECT Id, name, description, price, category FROM Products");
                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = int.Parse(reader["Id"].ToString());
                        string name = reader["name"].ToString();
                        string description = reader["description"].ToString();
                        decimal price = decimal.Parse(reader["price"].ToString());
                        string category = reader["category"].ToString();

                        Product product = new Product(id, name, description, price, category);
                        products.Add(product);
                    }
                }
            }
            return products;
        }

        public static Product FindById(int Id)
        {
            Product product = new Product();
            using (var client = new SqlConnection(connectionString))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.AppendFormat(
                    "SELECT Id, name, description, price, category"
                    + " FROM Products WHERE Id={0}", 
                    Id);
                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        product.Id = int.Parse(reader["Id"].ToString());
                        product.Name = reader["name"].ToString();
                        product.Description = reader["description"].ToString();
                        product.Price = decimal.Parse(reader["price"].ToString());
                        product.Category = reader["category"].ToString();
                    }
                }
            }
            return product;
        }

        public static bool Delete(int Id)
        {
            using (var client = new SqlConnection(connectionString))
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
