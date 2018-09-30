using System;
using System.Collections.Generic;
using System.Text;

namespace StoreCore.Entities
{
    class CartProduct
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Qty { get; set; }

        public CartProduct()
        {

        }

        public CartProduct(Cart cart, Product product, int qty)
        {
            CartId = cart.Id;
            Cart = cart;
            ProductId = product.Id;
            Product = product;
            Qty = qty;
        }
        public CartProduct(int cartId, int productId, int qty)
        {
            CartId = cartId;
            ProductId = productId;
            Qty = qty;
        }


    }
}
