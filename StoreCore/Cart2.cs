﻿using System;
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

    }
}
