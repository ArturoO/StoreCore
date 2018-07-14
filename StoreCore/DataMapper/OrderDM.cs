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




    }
}
