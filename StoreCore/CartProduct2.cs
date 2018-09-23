using System;
using System.Collections.Generic;
using System.Text;

namespace StoreCore
{
    class CartProduct2
    {
        public int Id { get; set; } 
        public Cart2 Cart { get; set; }
        public Product2 Product { get; set; }
        public int Qty { get; set; }

    }
}
