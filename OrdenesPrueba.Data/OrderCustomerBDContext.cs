using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OrdenesPrueba.Data.Models;

#nullable disable

namespace OrdenesPrueba.Data
{
    public partial class OrderCustomerBDContext : DbContext
    {
        public OrderCustomerBDContext()
        {
        }

        public OrderCustomerBDContext(DbContextOptions<OrderCustomerBDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                /*
                    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                                    optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=OrderCustomerBD;Trusted_Connection=True;");
                */
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.HasIndex(e => e.CustomerName, "UQ__Customer__7A22C6EAC0040537")
                    .IsUnique();

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.DateCreate).HasColumnType("datetime");

                entity.Property(e => e.DateDelete).HasColumnType("datetime");

                entity.Property(e => e.LastModificated).HasColumnType("datetime");

                entity.Property(e => e.Phone).HasMaxLength(20);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.OrderNumber, "UQ__Orders__CAC5E74318979A56")
                    .IsUnique();

                entity.Property(e => e.DateCreate).HasColumnType("datetime");

                entity.Property(e => e.DateDelete).HasColumnType("datetime");

                entity.Property(e => e.LastModificated).HasColumnType("datetime");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.OrderNumber).HasMaxLength(10);

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders__Customer__4AB81AF0");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId })
                    .HasName("PK__OrderIte__08D097A3D71BCA69");

                entity.ToTable("OrderItem");

                entity.Property(e => e.DateCreate).HasColumnType("datetime");

                entity.Property(e => e.DateDelete).HasColumnType("datetime");

                entity.Property(e => e.LastModificated).HasColumnType("datetime");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(12, 2)");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderItem__Order__5441852A");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderItem__Produ__5535A963");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.HasIndex(e => e.SupplierId, "UQ__Product__4BE666B5EDA04FDF")
                    .IsUnique();

                entity.Property(e => e.DateCreate).HasColumnType("datetime");

                entity.Property(e => e.DateDelete).HasColumnType("datetime");

                entity.Property(e => e.LastModificated).HasColumnType("datetime");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(12, 2)");

                entity.HasOne(d => d.Supplier)
                    .WithOne(p => p.Product)
                    .HasForeignKey<Product>(d => d.SupplierId)
                    .HasConstraintName("FK__Product__Supplie__5165187F");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Supplier");

                entity.HasIndex(e => e.CopanyName, "UQ__Supplier__45B7EA9760FDC440")
                    .IsUnique();

                entity.Property(e => e.CopanyName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.DateCreate).HasColumnType("datetime");

                entity.Property(e => e.DateDelete).HasColumnType("datetime");

                entity.Property(e => e.LastModificated).HasColumnType("datetime");

                entity.Property(e => e.Phone).HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
