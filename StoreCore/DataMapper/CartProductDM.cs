using System;
using System.Collections.Generic;
using System.Text;
using StoreCore.Entity;
using System.Data.SqlClient;

namespace StoreCore.DataMapper
{
    public class CartProductDM
    {
        protected const string connectionString = "Data Source=ARTUROO-PC;Initial Catalog=Store;Integrated Security=True;Pooling=False";

        public static bool Create(CartProduct cartProduct)
        {
            using (var client = new SqlConnection(connectionString))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.Append(
                    "INSERT INTO CartProducts(cart_id, product_id, qty)"
                    + " VALUES (@cart_id, @product_id, @qty)");
                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                cmd.Parameters.AddWithValue("@cart_id", cartProduct.CartId);
                cmd.Parameters.AddWithValue("@product_id", cartProduct.ProductId);
                cmd.Parameters.AddWithValue("@qty", cartProduct.Qty);

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

        //public static bool Create(Cart cart, Product product, int qty)
        //{
        //    CartProduct cartProduct = new CartProduct();
        //    using (var client = new SqlConnection(connectionString))
        //    {
        //        client.Open();
        //        StringBuilder sbCmd = new StringBuilder();
        //        sbCmd.Append(
        //            "INSERT INTO CartProducts(cart_id, product_id, qty)"
        //            + " VALUES (@cart_id, @product_id, @qty)");
        //        SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
        //        cmd.Parameters.AddWithValue("@cart_id", cart.Id);
        //        cmd.Parameters.AddWithValue("@product_id", product.Id);
        //        cmd.Parameters.AddWithValue("@qty", qty);

        //        var rowsCount = cmd.ExecuteNonQuery();

        //        if (rowsCount > 0)
        //        {
        //            cartProduct.CartId = cart.Id;
        //            cartProduct.ProductId = product.Id;
        //            cartProduct.Qty = qty;
        //            cart.Products.Add(cartProduct);
        //            //cart.Products
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}

        public static bool Update(CartProduct cartProduct)
        {
            //CartProduct cartProduct = new CartProduct();
            using (var client = new SqlConnection(connectionString))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.Append(
                    "UPDATE CartProducts SET qty=@qty" +
                    " WHERE product_id=@product_id" +
                    " AND cart_id=@cart_id");
                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);                
                cmd.Parameters.AddWithValue("@product_id", cartProduct.ProductId);
                cmd.Parameters.AddWithValue("@cart_id", cartProduct.CartId);
                cmd.Parameters.AddWithValue("@qty", cartProduct.Qty);

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

        public static bool Remove(Cart cart, Product product)
        {
            using (var client = new SqlConnection(connectionString))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.Append(
                    "DELETE FROM CartProducts " +
                    " WHERE product_id=@product_id" +
                    " AND cart_id=@cart_id");
                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                cmd.Parameters.AddWithValue("@product_id", product.Id);
                cmd.Parameters.AddWithValue("@cart_id", cart.Id);

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

        public static bool Remove(CartProduct cartProduct)
        {
            using (var client = new SqlConnection(connectionString))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.Append(
                    "DELETE FROM CartProducts " +
                    " WHERE product_id=@product_id" +
                    " AND cart_id=@cart_id");
                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                cmd.Parameters.AddWithValue("@product_id", cartProduct.ProductId);
                cmd.Parameters.AddWithValue("@cart_id", cartProduct.CartId);

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
                        cartProduct.Id = int.Parse(reader["Id"].ToString());
                        cartProduct.CartId = int.Parse(reader["cart_id"].ToString());
                        cartProduct.ProductId = int.Parse(reader["product_id"].ToString());
                        cartProduct.Qty = int.Parse(reader["qty"].ToString());
                        cartProducts.Add(cartProduct);
                    }
                }
            }
            return cartProducts;
        }

        public static int RemoveProducts(Cart cart)
        {
            using (var client = new SqlConnection(connectionString))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.Append(
                    "DELETE FROM CartProducts " +
                    " WHERE cart_id = @cart_id");
                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                cmd.Parameters.AddWithValue("@cart_id", cart.Id);

                var rows = cmd.ExecuteNonQuery();
                return rows;
            }
        }

    }
}
