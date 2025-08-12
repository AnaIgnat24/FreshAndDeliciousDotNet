using System;
using System.Collections.Generic;
using FreshAndDelicious.Core.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace FreshAndDelicious.Core.Data;

public partial class FreshAndDeliciousDbContext : DbContext
{
    public FreshAndDeliciousDbContext()
    {
    }

    public FreshAndDeliciousDbContext(DbContextOptions<FreshAndDeliciousDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categories> Categories { get; set; }

    public virtual DbSet<Customers> Customers { get; set; }

    public virtual DbSet<Discounts> Discounts { get; set; }

    public virtual DbSet<OrderDetails> OrderDetails { get; set; }

    public virtual DbSet<OrderDetailsDiscounts> OrderDetailsDiscounts { get; set; }

    public virtual DbSet<OrderDiscounts> OrderDiscounts { get; set; }

    public virtual DbSet<OrderStatus> OrderStatus { get; set; }

    public virtual DbSet<Orders> Orders { get; set; }

    public virtual DbSet<ProductReviews> ProductReviews { get; set; }

    public virtual DbSet<Products> Products { get; set; }

    public virtual DbSet<Shippers> Shippers { get; set; }

    public virtual DbSet<Suppliers> Suppliers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Categories>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CategoryDescription)
                .HasMaxLength(255)
                .HasColumnName("Category_description");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("Category_name");
        });

        modelBuilder.Entity<Customers>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.ContactName)
                .HasMaxLength(255)
                .HasColumnName("Contact_name");
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("First_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("Last_name");
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.PostalCode)
                .HasMaxLength(50)
                .HasColumnName("Postal_code");
        });

        modelBuilder.Entity<Discounts>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AppliesTo)
                .HasColumnType("enum('product','category','order','all')")
                .HasColumnName("Applies_to");
            entity.Property(e => e.BuyQuantity).HasColumnName("Buy_quantity");
            entity.Property(e => e.CouponCode)
                .HasMaxLength(255)
                .HasColumnName("Coupon_code");
            entity.Property(e => e.DiscountName)
                .HasMaxLength(255)
                .HasColumnName("Discount_name");
            entity.Property(e => e.DiscountType)
                .HasColumnType("enum('percent','fix','bogo','free_shipping','coupon')")
                .HasColumnName("Discount_type");
            entity.Property(e => e.DiscountValue)
                .HasPrecision(10, 2)
                .HasColumnName("Discount_value");
            entity.Property(e => e.EndDate).HasColumnName("End_date");
            entity.Property(e => e.FreeQuantity).HasColumnName("Free_quantity");
            entity.Property(e => e.IsActive).HasColumnName("Is_active");
            entity.Property(e => e.StartDate).HasColumnName("Start_date");
        });

        modelBuilder.Entity<OrderDetails>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Order_details");

            entity.HasIndex(e => e.OrderId, "fk_order_details_order");

            entity.HasIndex(e => e.ProductId, "fk_order_details_product");

            entity.Property(e => e.OrderId).HasColumnName("Order_id");
            entity.Property(e => e.OrderValue).HasColumnName("Order_value");
            entity.Property(e => e.ProductId).HasColumnName("Product_id");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_details_order");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_details_product");
        });

        modelBuilder.Entity<OrderDetailsDiscounts>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Order_details_discounts");

            entity.HasIndex(e => e.OrderDetailId, "fk_order_details_discounts_detail");

            entity.HasIndex(e => e.DiscountId, "fk_order_details_discounts_discount");

            entity.Property(e => e.AppliedValue)
                .HasPrecision(10, 2)
                .HasColumnName("Applied_value");
            entity.Property(e => e.DiscountId).HasColumnName("Discount_id");
            entity.Property(e => e.OrderDetailId).HasColumnName("Order_detail_id");

            entity.HasOne(d => d.Discount).WithMany(p => p.OrderDetailsDiscounts)
                .HasForeignKey(d => d.DiscountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_details_discounts_discount");

            entity.HasOne(d => d.OrderDetail).WithMany(p => p.OrderDetailsDiscounts)
                .HasForeignKey(d => d.OrderDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_details_discounts_detail");
        });

        modelBuilder.Entity<OrderDiscounts>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Order_discounts");

            entity.HasIndex(e => e.DiscountId, "fk_order_discounts_discount");

            entity.HasIndex(e => e.OrderId, "fk_order_discounts_order");

            entity.Property(e => e.AppliedValue)
                .HasPrecision(10, 2)
                .HasColumnName("Applied_value");
            entity.Property(e => e.DiscountId).HasColumnName("Discount_id");
            entity.Property(e => e.OrderId).HasColumnName("Order_id");

            entity.HasOne(d => d.Discount).WithMany(p => p.OrderDiscounts)
                .HasForeignKey(d => d.DiscountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_discounts_discount");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDiscounts)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_discounts_order");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Order_status");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.StatusType)
                .HasColumnType("enum('pending','processing','shipped','delivered','cancelled','returned','failed')")
                .HasColumnName("Status_type");
        });

        modelBuilder.Entity<Orders>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.ShipperId, "fk_orders_shipper");

            entity.HasIndex(e => e.StatusId, "fk_orders_status");

            entity.Property(e => e.CustomerId).HasColumnName("Customer_id");
            entity.Property(e => e.OrderDate).HasColumnName("Order_date");
            entity.Property(e => e.ShippedDate).HasColumnName("Shipped_date");
            entity.Property(e => e.ShipperId).HasColumnName("Shipper_id");
            entity.Property(e => e.StatusId).HasColumnName("Status_id");

            entity.HasOne(d => d.Shipper).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ShipperId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orders_shipper");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orders_status");
        });

        modelBuilder.Entity<ProductReviews>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Product_reviews");

            entity.HasIndex(e => e.CustomerId, "fk_product_reviews_customer");

            entity.HasIndex(e => e.ProductId, "fk_product_reviews_product");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("Created_at");
            entity.Property(e => e.CustomerId).HasColumnName("Customer_id");
            entity.Property(e => e.ProductId).HasColumnName("Product_id");
            entity.Property(e => e.Review).HasColumnType("text");
            entity.Property(e => e.ReviewRate)
                .HasComment("Review rate 1-5")
                .HasColumnName("Review_rate");

            entity.HasOne(d => d.Customer).WithMany(p => p.ProductReviews)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product_reviews_customer");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductReviews)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product_reviews_product");
        });

        modelBuilder.Entity<Products>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.CategoryId, "Category_id");

            entity.HasIndex(e => e.DiscountId, "Discount_id");

            entity.HasIndex(e => e.SupplierId, "Supplier_id");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CategoryId).HasColumnName("Category_id");
            entity.Property(e => e.DiscountId).HasColumnName("Discount_id");
            entity.Property(e => e.ImagesNames)
                .HasMaxLength(255)
                .HasColumnName("Images_names");
            entity.Property(e => e.ProductDescription)
                .HasColumnType("text")
                .HasColumnName("Product_description");
            entity.Property(e => e.ProductHealthBenefits)
                .HasColumnType("text")
                .HasColumnName("Product_health_benefits");
            entity.Property(e => e.ProductIngredients)
                .HasColumnType("text")
                .HasColumnName("Product_ingredients");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .HasColumnName("Product_name");
            entity.Property(e => e.ProductNutritionInformation)
                .HasColumnType("text")
                .HasColumnName("Product_nutrition_information");
            entity.Property(e => e.QuantityUnit)
                .HasMaxLength(255)
                .HasColumnName("Quantity_unit");
            entity.Property(e => e.SupplierId).HasColumnName("Supplier_id");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_ibfk_2");

            entity.HasOne(d => d.Discount).WithMany(p => p.Products)
                .HasForeignKey(d => d.DiscountId)
                .HasConstraintName("products_ibfk_1");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_ibfk_3");
        });


        modelBuilder.Entity<Shippers>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.ContactName)
                .HasMaxLength(255)
                .HasColumnName("Contact_name");
            entity.Property(e => e.Country).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(255);
            entity.Property(e => e.PostalCode)
                .HasMaxLength(255)
                .HasColumnName("Postal_code");
            entity.Property(e => e.ShippedValue)
                .HasMaxLength(255)
                .HasColumnName("Shipped_value");
            entity.Property(e => e.ShipperName)
                .HasMaxLength(255)
                .HasColumnName("Shipper_name");
        });

        modelBuilder.Entity<Suppliers>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.ContactName)
                .HasMaxLength(255)
                .HasColumnName("Contact_name");
            entity.Property(e => e.Country).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(255);
            entity.Property(e => e.PostalCode)
                .HasMaxLength(255)
                .HasColumnName("Postal_code");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(255)
                .HasColumnName("Supplier_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
