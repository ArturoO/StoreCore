using System;
using System.Collections.Generic;
using System.Text;
using StoreCore.DataMapper;
using StoreCore.Factory;

namespace StoreCore.Entity
{
    class Order:AEntity
    {
        protected int user_id;
        protected DateTime date_time;
        protected decimal total;
        protected int qty;
        protected string status;

        protected User user = null;
        protected List<OrderProduct> products = null;

        public int UserId { get { return user_id; } set { user_id = value; } }
        public DateTime DateTime { get { return date_time; } set { date_time = value; } }
        public decimal Total { get { return total; } set { total = value; } }
        public int Qty { get { return qty; } set { qty = value; } }
        public string Status { get { return status; } set { status = value; } }

        public User User
        {
            get
            {
                if (user == null)
                    user = UserDM.FindById(UserId);
                return user;
            }
            set
            {
                user = value;
            }
        }

        public List<OrderProduct> Products
        {
            get
            {
                if (products == null)
                    products = OrderProductDM.ListProducts(this);
                return products;
            }
            set
            {
                products = value;
            }
        }

        public void AddProduct(OrderProduct orderProduct)
        {
            Products.Add(orderProduct);            
        }

        public static bool Checkout(Cart cart)
        {
            Order order = new Order();
            order.UserId = cart.UserId;
            order.Qty = cart.Qty;
            order.Total = cart.Price;
            foreach (CartProduct cartProduct in cart.Products)
            {
                Product product = new Product(cartProduct.ProductId, cartProduct.Name
                    , cartProduct.Description, cartProduct.Price, cartProduct.Category);
                order.AddProduct(new OrderProduct(order, product, cartProduct.Qty));
            }

            OrderDM.Create(order);
            OrderProductDM.AddProducts(order);

            return true;
        }


    }
}
