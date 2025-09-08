using Microsoft.EntityFrameworkCore;
using Final_Project.Models;
using System;

namespace Final_Project.AppDbContext
{
    public class PetDbContext : DbContext
    {
        public PetDbContext(DbContextOptions<PetDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Store> Stores { get; set; } = null!;
        public DbSet<Pet> Pets { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;
        public DbSet<Cart> Carts { get; set; } = null!;
        public DbSet<CartItem> CartItems { get; set; } = null!;
        public DbSet<Return> Returns { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ---------- Seed data (static values) ----------
            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = 1,
                Name = "Admin User",
                Email = "admin@pets.com",
                PasswordHash = "$2a$12$z7g3rB7mYFv6kU1fF0N4EuVqV7M4hN1G6v7J8kPqK9yXyH5gWQY8e", // pre-hashed
                Role = "Admin",
                Password = string.Empty
            });

            modelBuilder.Entity<Store>().HasData(new Store
            {
                StoreId = 1,
                StoreName = "Pet Paradise",
                Location = "Main Street",
                OwnerId = 1
            });

            modelBuilder.Entity<Pet>().HasData(new Pet
            {
                PetId = 1,
                Name = "Buddy",
                Type = "Dog",
                Breed = "Golden Retriever",
                Gender = "Male",
                Price = 15000m,
                StoreId = 1
            });

            modelBuilder.Entity<Order>().HasData(new Order
            {
                OrderId = 1,
                UserId = 1,
                StoreId = 1,
                OrderDate = new DateTime(2025, 9, 9, 0, 0, 0, DateTimeKind.Utc),
                Status = "Pending",
                TotalAmount = 15000m
            });

            modelBuilder.Entity<OrderItem>().HasData(new OrderItem
            {
                OrderItemId = 1,
                OrderId = 1,
                PetId = 1,
                Quantity = 1,
                Price = 15000m
            });

            // ---------- Precision for decimals ----------
            modelBuilder.Entity<Order>().Property(o => o.TotalAmount).HasPrecision(18, 2);
            modelBuilder.Entity<OrderItem>().Property(oi => oi.Price).HasPrecision(18, 2);
            modelBuilder.Entity<Pet>().Property(p => p.Price).HasPrecision(18, 2);

            // ---------- Relationships ----------
            modelBuilder.Entity<Store>()
                .HasMany(s => s.Pets)
                .WithOne(p => p.Store)
                .HasForeignKey(p => p.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Store>()
                .HasMany(s => s.Orders)
                .WithOne(o => o.Store)
                .HasForeignKey(o => o.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Pet)
                .WithMany() // Pet doesn't have OrderItems collection in this design
                .HasForeignKey(oi => oi.PetId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Returns)
                .WithOne(r => r.Order)
                .HasForeignKey(r => r.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasMany(oi => oi.Returns)
                .WithOne(r => r.OrderItem)
                .HasForeignKey(r => r.OrderItemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Pet)
                .WithMany()
                .HasForeignKey(ci => ci.PetId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
