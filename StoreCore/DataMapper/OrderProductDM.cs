using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using StoreCore.Entity;

namespace StoreCore.DataMapper
{
    class OrderProductDM
    {
        protected const string connectionString = "Data Source=ARTUROO-PC;Initial Catalog=Store;Integrated Security=True;Pooling=False";


        public static bool Create(OrderProduct product)
        {
            using (var client = new SqlConnection(connectionString))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.Append(
                    "INSERT INTO OrderProducts(order_id, qty, name, description, price, category) OUTPUT INSERTED.Id"
                    + " VALUES (@order_id, @qty, @name, @description, @price, @category)");
                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                cmd.Parameters.AddWithValue("@order_id", product.Order.Id);
                cmd.Parameters.AddWithValue("@qty", product.Qty);
                cmd.Parameters.AddWithValue("@name", product.Name);
                cmd.Parameters.AddWithValue("@description", product.Description);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@category", product.Category);

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

        public static bool AddProducts(Order order)
        {
            foreach(OrderProduct product in order.Products)
            {
                Create(product);
            }
            return true;
        }

        public static List<OrderProduct> ListProducts(Order order)
        {
            List<OrderProduct> products = new List<OrderProduct>();

            return products;

        }

    }
}
