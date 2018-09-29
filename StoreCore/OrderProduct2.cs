using System;
using System.Collections.Generic;
using System.Text;

namespace StoreCore
{
    class OrderProduct2
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order2 Order { get; set; }
        public int Qty { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

        public OrderProduct2()
        {

        }

        public OrderProduct2(CartProduct2 cartProduct)
        {
            Name = cartProduct.Product.Name;
            Description = cartProduct.Product.Description;
            Price = cartProduct.Product.Price;
            Category = cartProduct.Product.Category;
            Qty = cartProduct.Qty;
        }

    }
}
