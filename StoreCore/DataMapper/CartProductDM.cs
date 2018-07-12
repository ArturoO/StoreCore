using System;
using System.Collections.Generic;
using System.Text;
using StoreCore.Entity;
using System.Data.SqlClient;

namespace StoreCore.DataMapper
{
    class CartProductDM
    {
        protected const string connectionString = "Data Source=ARTUROO-PC;Initial Catalog=Store;Integrated Security=True;Pooling=False";

        public static List<CartProduct> ListProducts(Cart cart)
        {
            List<CartProduct> cartProducts = new List<CartProduct>();

            using (var client = new SqlConnection(connectionString))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.Append(
                    "SELECT * FROM CartProducts " +
                    " WHERE cart_id = @cart_id");
                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                cmd.Parameters.AddWithValue("@cart_id", cart.Id);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CartProduct cartProduct = new CartProduct();
                        cartProduct.CartId = int.Parse(reader["cart_id"].ToString());
                        cartProduct.ProductId = int.Parse(reader["product_id"].ToString());
                        cartProduct.Qty = int.Parse(reader["qty"].ToString());
                        cartProducts.Add(cartProduct);
                    }
                }
            }
            return cartProducts;
        }

    }
}
