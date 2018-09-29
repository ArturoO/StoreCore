using System;
using System.Collections.Generic;
using System.Text;

namespace StoreCore
{
    class Cart2
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public int UserId { get; set; }
        public User2 User { get; set; }
        public ICollection<CartProduct2> Products { get; set; }

        public Cart2()
        {
            Products = new List<CartProduct2>();            
        }

        public Cart2(User2 user)
        {
            Products = new List<CartProduct2>();
            Price = 0;
            Qty = 0;
            UserId = user.Id;
            User = user;
        }

        public void UpdateSummary()
        {
            Price = 0;
            Qty = 0;      
            foreach (var product in Products)
            {
                Price += product.Qty * product.Product.Price;
                Qty += product.Qty;                
            }            
        }

        public int Checkout()
        {
            var order = new Order2();
            order.UserId = UserId;
            order.Qty = Qty;
            order.Total = Price;
            order.DateTime = DateTime.Now;            
            order.Status = "new";
            foreach (var cartProduct in Products)
            {
                var orderProduct = new OrderProduct2(cartProduct);
                order.Products.Add(orderProduct);
            }

            using (var context = new StoreContext())
            {
                context.Orders.Add(order);
                context.SaveChanges();
            }
            Clear();
            return order.Id;
        }

        public void Clear()
        {
            using (var context = new StoreContext())
            {
                foreach(var cartProduct in Products)
                    context.CartProducts.Remove(cartProduct);
                Products.Clear();
                UpdateSummary();
                context.Carts.Update(this);
                context.SaveChanges();
            }
        }

    }
}
