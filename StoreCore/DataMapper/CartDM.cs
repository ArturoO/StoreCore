﻿using System;
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

        public static bool AddProduct(Cart cart, Product product, int qty)
        {
            using (var client = new SqlConnection(connectionString))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.Append(
                    "INSERT INTO CartProducts(cart_id, product_id, qty)"
                    + " VALUES (@cart_id, @product_id, @qty)");
                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                cmd.Parameters.AddWithValue("@cart_id", cart.Id);
                cmd.Parameters.AddWithValue("@product_id", product.Id);
                cmd.Parameters.AddWithValue("@qty", qty);

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
                    "SELECT cp.* FROM CartProducts AS cp" +
                    " LEFT JOIN Cartc AS c ON(c.Id = cp.cart_id)" +
                    " WHERE c.user_id = @user_id");
                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                cmd.Parameters.AddWithValue("@user_id", cart.Id);

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
