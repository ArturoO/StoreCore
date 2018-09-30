using System;
using System.Collections.Generic;
using System.Text;

namespace StoreCore.Entities
{
    class Product
    {
        decimal price = 0;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get { return price; } set { if (value >= 0) price = value; } }
        public string Category { get; set; }

        public Product(string name, string description, decimal price, string category)
        {
            Name = name;
            Description = description;
            Price = price;
            Category = category;
        }

        public Product(int id, string name, string description, decimal price, string category)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Category = category;
        }
    }
}
