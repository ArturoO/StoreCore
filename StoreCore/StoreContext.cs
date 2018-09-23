using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreCore
{
    class StoreContext : DbContext
    {
        public DbSet<User2> Users { get; set; }
        public DbSet<Product2> Products { get; set; }
        public DbSet<Order2> Orders { get; set; }
        public DbSet<OrderProduct2> OrderProducts { get; set; }
        public DbSet<Cart2> Carts { get; set; }
        public DbSet<CartProduct2> CartProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=ARTUROO-PC;Initial Catalog=Store2;Integrated Security=True;Pooling=False");
        }
    }
}
