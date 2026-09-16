using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace API_Prctice_6.Models
{
    public class OrderDBContext : DbContext
    {
        public OrderDBContext() : base("OrderDBContext") { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<MenuItem> MenuItems { get; set; }
    }
}