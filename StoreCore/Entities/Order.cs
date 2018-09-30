using System;
using System.Collections.Generic;
using System.Text;

namespace StoreCore.Entities
{
    class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Total { get; set; }
        public int Qty { get; set; }
        public string Status { get; set; }
        public ICollection<OrderProduct> Products { get; set; }

        public Order()
        {
            Products = new List<OrderProduct>();
        }

    }
}
