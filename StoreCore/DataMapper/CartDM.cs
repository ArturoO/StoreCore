using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using StoreCore.Entity;

namespace StoreCore.DataMapper
{
    class CartDM
    {

        protected const string connectionString = "Data Source=ARTUROO-PC;Initial Catalog=Store;Integrated Security=True;Pooling=False";

        public static bool Create(User user)
        {
            Cart cart = new Cart();
            using (var client = new SqlConnection(connectionString))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.Append(
                    "INSERT INTO Cart(user_id, qty) OUTPUT INSERTED.Id"
                    + " VALUES (@user_id, @qty)");
                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                cmd.Parameters.AddWithValue("@user_id", user.Id);
                cmd.Parameters.AddWithValue("@qty", 0);

                var insertedId = int.Parse(cmd.ExecuteScalar().ToString());
                if (insertedId > 0)
                {
                    cart.Id = insertedId;
                    cart.UserId = user.Id;
                    user.Cart = cart;
                    return true;                    
                }
                else
                {
                    return false;
                }
            }
        }

        public static Cart FindByUser(User user)
        {
            Cart cart = new Cart();
            using (var client = new SqlConnection(connectionString))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.Append(
                    "SELECT *"
                    + " FROM Cart WHERE user_id=@user_id");
                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                cmd.Parameters.AddWithValue("@user_id", user.Id);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cart.Id = int.Parse(reader["Id"].ToString());
                        cart.UserId = int.Parse(reader["user_id"].ToString());
                        cart.Price = decimal.Parse(reader["price"].ToString());
                        cart.Qty = int.Parse(reader["qty"].ToString());                        
                    }
                }
            }
            return cart;
        }

        public static bool UpdateSummary(Cart cart)
        {
            decimal price = 0;
            int qty = 0;
            List<CartProduct> products = cart.Products;
            foreach(CartProduct product in products)
            {
                qty += product.Qty;
                price += product.Qty*product.Product.Price;                
            }
            cart.Qty = qty;
            cart.Price = price;
            
            using (var client = new SqlConnection(connectionString))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.Append("UPDATE Cart SET price = @price, qty = @qty WHERE Id=@id");
                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                cmd.Parameters.AddWithValue("@price", price);                
                cmd.Parameters.AddWithValue("@qty", qty);
                cmd.Parameters.AddWithValue("@id", cart.Id);

                var rowsCount = cmd.ExecuteNonQuery();

                if (rowsCount > 0)
                {
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
