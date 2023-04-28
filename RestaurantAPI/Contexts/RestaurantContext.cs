using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Contexts
{
    partial class RestaurantContext : DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options)
          : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderMenu> OrderMenus { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=RestaurantDBConnectionString");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menus");

                entity.HasOne(d => d.Category).WithOne(p => p.Menu)
                .HasForeignKey<Menu>(d => d.CategoryID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Menus_Category");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders");

                entity.HasMany(d => d.OrderMenu).WithOne(p => p.Order)
                .HasForeignKey(d => d.OrderID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_OrderMenus");

                entity.HasOne(d => d.User).WithMany(p => p.Order)
                .HasForeignKey(d => d.UserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Orders");
            });

            modelBuilder.Entity<OrderMenu>(entity =>
            {
                entity.ToTable("OrderMenus");

                entity.HasOne(d => d.Menu).WithMany(p => p.OrderMenu)
                .HasForeignKey(d => d.MenuID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Menus_OrderMenus");

                entity.HasOne(d => d.Order).WithMany(p => p.OrderMenu)
                .HasForeignKey(d => d.OrderID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_OrderMenus");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }

}
