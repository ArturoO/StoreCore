using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using StoreCore.Entity;

namespace StoreCore.DataMapper
{
    class OrderDM
    {
        protected const string connectionString = "Data Source=ARTUROO-PC;Initial Catalog=Store;Integrated Security=True;Pooling=False";

        public static bool Create(Order order)
        {
            using (var client = new SqlConnection(connectionString))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.Append(
                    "INSERT INTO Orders(user_id, total, qty) OUTPUT INSERTED.Id"
                    + " VALUES (@user_id, @total, @qty)");
                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                cmd.Parameters.AddWithValue("@user_id", order.UserId);
                cmd.Parameters.AddWithValue("@total", order.Total);
                cmd.Parameters.AddWithValue("@qty", order.Qty);

                var insertedId = int.Parse(cmd.ExecuteScalar().ToString());
                if (insertedId > 0)
                {
                    order.Id = insertedId;
                    return true;
                }
                else
                { 
                    return false;
                }
            }
        }

        public static List<Order> ListByUser(User user)
        {
            List<Order> orders = new List<Order>();

            using (var client = new SqlConnection(connectionString))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.AppendLine("SELECT * FROM Orders" +
                    " WHERE user_id = @user_id" +
                    " ORDER BY date_time DESC");
                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                cmd.Parameters.AddWithValue("@user_id", user.Id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = int.Parse(reader["Id"].ToString());
                        int user_id = int.Parse(reader["user_id"].ToString());
                        DateTime date_time = DateTime.Parse(reader["date_time"].ToString());
                        decimal total = decimal.Parse(reader["total"].ToString());
                        int qty = int.Parse(reader["qty"].ToString());
                        string status = reader["status"].ToString();
                        
                        Order order = new Order(id, user_id, date_time, total, qty, status);
                        orders.Add(order);
                    }
                }
            }
            return orders;
        }

        public static Order FindById(int Id)
        {
            Order order = new Order();

            using (var client = new SqlConnection(connectionString))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.AppendLine("SELECT * FROM Orders" +
                    " WHERE Id = @Id");
                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                cmd.Parameters.AddWithValue("@Id", Id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = int.Parse(reader["Id"].ToString());
                        int user_id = int.Parse(reader["user_id"].ToString());
                        DateTime date_time = DateTime.Parse(reader["date_time"].ToString());
                        decimal total = decimal.Parse(reader["total"].ToString());
                        int qty = int.Parse(reader["qty"].ToString());
                        string status = reader["status"].ToString();

                        order = new Order(id, user_id, date_time, total, qty, status);
                    }
                }
            }

            return order;
        }


    }
}
