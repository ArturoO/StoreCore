using System;
using System.Collections.Generic;
using System.Text;
using StoreCore.DataMapper;
using StoreCore.Factory;

namespace StoreCore.Entity
{
    class OrderProduct:AEntity
    {
        protected int order_id;
        protected int qty = 0;
        protected string name = "";
        protected string description = "";
        protected decimal price = 0;
        protected string category = "";

        protected Order order = null;

        public int OrderId { get { return order_id; } set { order_id = value; } }
        public int Qty { get { return qty; } set { qty = value; } }
        public string Name { get { return this.name; } set { this.name = value; } }
        public string Description{ get { return this.description; } set { this.description = value; } }

        public decimal Price
        {
            get
            {
                return this.price;
            }
            set
            {
                if (value > 0)
                    this.price = value;
            }
        }

        public string Category
        {
            get
            {
                return this.category;
            }
            set
            {
                this.category = value;
            }
        }

        public Order Order
        {
            get
            {
                return order;
            }
            set
            {
                order = value;
            }
        }

        public OrderProduct() { }

        public OrderProduct(Order order, Product product, int qty)
        {
            Order = order;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            Category = product.Category;
            Qty = qty;
        }

        public OrderProduct(Order order, string name, string description, decimal price, string category, int qty)
        {
            Order = order;
            Name = name;
            Description = description;
            Price = price;
            Category = category;
            Qty = qty;
        }

        public OrderProduct(int order_id, string name, string description, decimal price, string category, int qty)
        {
            OrderId = order_id;
            Name = name;
            Description = description;
            Price = price;
            Category = category;
            Qty = qty;
        }

    }
}
