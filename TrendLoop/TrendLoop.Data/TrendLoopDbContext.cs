using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TrendLoop.Data.Models;

namespace TrendLoop.Data
{
    public class TrendLoopDbContext : IdentityDbContext
    {
        public TrendLoopDbContext(DbContextOptions<TrendLoopDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Brand> Brands { get; set; } = null!;
        public virtual DbSet<Material> Materials { get; set; } = null!;
        public virtual DbSet<AttributeType> AttributeTypes { get; set; } = null!;
        public virtual DbSet<AttributeValue> AttributeValue { get; set; } = null!;
        
        public virtual DbSet<CategoryAttributeType> CategoriesAttributeTypes { get; set; } = null!;
        public virtual DbSet<ProductAttributeValue> ProductsAttributeValues { get; set; } = null!;
        public virtual DbSet<ProductBuyer> ProductsBuyers { get; set; } = null!;
        public virtual DbSet<ProductSeller> ProductsSellers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
