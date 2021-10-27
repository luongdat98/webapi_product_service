using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI2.Models
{
    public class EcommerceContext : DbContext
    {
        public  DbSet<Category> Categories { get; set; }
        public  DbSet<Product> Products { get; set; }
        public  DbSet<ProductDetail> ProductDetails { get; set; }
        public  DbSet<Supplier> Suppliers { get; set; }
        public  DbSet<TypeUser> TypeUsers { get; set; }
        public  DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public EcommerceContext()
        {

        }
        public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-VRBQT6T\\TEAMDAT;Initial Catalog=Ecommerce ;Integrated Security=True;ConnectRetryCount=0");
            }
        }
    }
}
