using Kolokwium_apbd_s20414.Models;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium_apbd_s20414.Data
{
   
    public class KolokwiumContext : DbContext
    {
        // Komendy:
        // 1) Migracja początkowa: Add-Migration Init  
        // 2) Zastosowanie migracji do bazy: Update-Database 
        // Add-Migration AddPropertyConstraints 
        // Add-Migration Test 

        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }

        public KolokwiumContext(DbContextOptions options) : base(options)
        {

        }

        protected KolokwiumContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);

                modelBuilder.Entity<Client>().HasData(
                new Client { ID = 1, FirstName = "John", LastName = "Doe" },
                new Client { ID = 2, FirstName = "Jane", LastName = "Smith" }
                );
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.FulfilledAt);
                entity.HasOne(e => e.Client)
                    .WithMany(c => c.Orders)
                    .HasForeignKey(e => e.Client_ID)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Status)
                    .WithMany(s => s.Orders)
                    .HasForeignKey(e => e.Status_ID)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<Order>().HasData(
                new Order { ID = 1, CreatedAt = DateTime.Now, FulfilledAt = null, Client_ID = 1, Status_ID = 1 },
                new Order { ID = 2, CreatedAt = DateTime.Now, FulfilledAt = DateTime.Now, Client_ID = 2, Status_ID = 2 }
                );
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);

                modelBuilder.Entity<Status>().HasData(
                new Status { ID = 1, Name = "Created" },
                new Status { ID = 2, Name = "Created" }
                );
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);

                modelBuilder.Entity<Product>().HasData(
                new Product { ID = 1, Name = "Product 1", Price = 9.99 },
                new Product { ID = 2, Name = "Product 2", Price = 19.99 }
                );
            });

            modelBuilder.Entity<ProductOrder>(entity =>
            {
                entity.HasKey(e => new { e.Product_ID, e.Order_ID });
                entity.Property(e => e.Amount);
                entity.HasOne(e => e.Product)
                    .WithMany(p => p.ProductOrders)
                    .HasForeignKey(e => e.Product_ID)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Order)
                    .WithMany(o => o.ProductOrders)
                    .HasForeignKey(e => e.Order_ID)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<ProductOrder>().HasData(
                new ProductOrder { Product_ID = 1, Order_ID = 1, Amount = 2 },
                new ProductOrder { Product_ID = 2, Order_ID = 1, Amount = 1 },
                new ProductOrder { Product_ID = 1, Order_ID = 2, Amount = 3 }
                );
            });
        }
    }
}
