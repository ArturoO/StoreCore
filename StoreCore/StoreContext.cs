using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreCore
{
    class StoreContext : DbContext
    {
        public DbSet<User2> Users { get; set; }        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=ARTUROO-PC;Initial Catalog=Store2;Integrated Security=True;Pooling=False");
        }
    }
}
