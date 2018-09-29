using System;
using System.Collections.Generic;
using System.Text;

namespace StoreCore
{
    class Order2
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User2 User { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Total { get; set; }
        public int Qty { get; set; }
        public string Status { get; set; }
        public ICollection<OrderProduct2> Products { get; set; }

        public Order2()
        {
            Products = new List<OrderProduct2>();
        }

    }
}
