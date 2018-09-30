using System;
using System.Collections.Generic;
using System.Text;

namespace StoreCore.Entities
{
    class OrderProduct
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int Qty { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

        public OrderProduct()
        {

        }

        public OrderProduct(CartProduct cartProduct)
        {
            Name = cartProduct.Product.Name;
            Description = cartProduct.Product.Description;
            Price = cartProduct.Product.Price;
            Category = cartProduct.Product.Category;
            Qty = cartProduct.Qty;
        }

    }
}
