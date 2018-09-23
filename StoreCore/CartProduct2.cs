using System;
using System.Collections.Generic;
using System.Text;

namespace StoreCore
{
    class CartProduct2
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public Cart2 Cart { get; set; }
        public int ProductId { get; set; }
        public Product2 Product { get; set; }
        public int Qty { get; set; }

        public CartProduct2()
        {

        }

        public CartProduct2(Cart2 cart, Product2 product, int qty)
        {
            CartId = cart.Id;
            Cart = cart;
            ProductId = product.Id;
            Product = product;
            Qty = qty;
        }
        public CartProduct2(int cartId, int productId, int qty)
        {
            CartId = cartId;
            ProductId = productId;
            Qty = qty;
        }


    }
}
