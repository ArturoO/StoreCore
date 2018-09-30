using System;
using System.Collections.Generic;
using System.Text;

namespace StoreCore.Entities
{
    class Cart
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<CartProduct> Products { get; set; }

        public Cart()
        {
            Products = new List<CartProduct>();            
        }

        public Cart(User user)
        {
            Products = new List<CartProduct>();
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
            var order = new Order();
            order.UserId = UserId;
            order.Qty = Qty;
            order.Total = Price;
            order.DateTime = DateTime.Now;            
            order.Status = "new";
            foreach (var cartProduct in Products)
            {
                var orderProduct = new OrderProduct(cartProduct);
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
