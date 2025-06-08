using System;
using System.Collections.Generic;
using API.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace API.Database;

public partial class OnlineshopContext : DbContext
{
    public OnlineshopContext()
    {
    }

    public OnlineshopContext(DbContextOptions<OnlineshopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Localfile> Localfiles { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Merchant> Merchants { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Refund> Refunds { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Usercoupon> Usercoupons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=onlineshop;user id=root;password=123456", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.41-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressUuid).HasName("PRIMARY");

            entity.Property(e => e.AddressUuid).IsFixedLength();
            entity.Property(e => e.AddressIsdefault).HasDefaultValueSql("'1'");
            entity.Property(e => e.AddressUseruuid).IsFixedLength();

            entity.HasOne(d => d.AddressUseruu).WithMany(p => p.Addresses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("address_user_user_uuid_fk");
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminUuid).HasName("PRIMARY");

            entity.Property(e => e.AdminUuid).IsFixedLength();
            entity.Property(e => e.AdminKey).IsFixedLength();
            entity.Property(e => e.AdminLevel).HasDefaultValueSql("'DEFAULT'");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartUuid).HasName("PRIMARY");

            entity.Property(e => e.CartUuid).IsFixedLength();
            entity.Property(e => e.CartQuantity).HasDefaultValueSql("'1'");
            entity.Property(e => e.CartUseruuid).IsFixedLength();

            entity.HasOne(d => d.CartUseruu).WithMany(p => p.Carts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cart_user_user_uuid_fk");
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.HasKey(e => e.CouponId).HasName("PRIMARY");

            entity.Property(e => e.CouponStatus).HasDefaultValueSql("'NA'");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.ImageUuid).HasName("PRIMARY");

            entity.Property(e => e.ImageUuid).IsFixedLength();
            entity.Property(e => e.ImageFileuuid).IsFixedLength();
            entity.Property(e => e.ImageProductuuid).IsFixedLength();

            entity.HasOne(d => d.ImageFileuu).WithMany(p => p.Images)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("image_file_file_uuid_fk");

            entity.HasOne(d => d.ImageProductuu).WithMany(p => p.Images)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("image_product_product_uuid_fk");
        });

        modelBuilder.Entity<Localfile>(entity =>
        {
            entity.HasKey(e => e.LocalfileUuid).HasName("PRIMARY");

            entity.Property(e => e.LocalfileUuid).IsFixedLength();
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.LogUuid).HasName("PRIMARY");

            entity.Property(e => e.LogUuid).IsFixedLength();
            entity.Property(e => e.LogObjectuuid).IsFixedLength();
        });

        modelBuilder.Entity<Merchant>(entity =>
        {
            entity.HasKey(e => e.MerchantUuid).HasName("PRIMARY");

            entity.Property(e => e.MerchantUuid).IsFixedLength();
            entity.Property(e => e.MerchantAdminuuid).IsFixedLength();
            entity.Property(e => e.MerchantIsclosed).HasDefaultValueSql("'1'");

            entity.HasOne(d => d.MerchantAdminuu).WithMany(p => p.Merchants).HasConstraintName("merchant_admin_admin_uuid_fk");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationUuid).HasName("PRIMARY");

            entity.Property(e => e.NotificationUuid).IsFixedLength();
            entity.Property(e => e.NotificationUseruuid).IsFixedLength();

            entity.HasOne(d => d.NotificationUseruu).WithMany(p => p.Notifications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("notification_user_user_uuid_fk");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderUuid).HasName("PRIMARY");

            entity.Property(e => e.OrderUuid).IsFixedLength();
            entity.Property(e => e.OrderChannel).HasDefaultValueSql("'wechat'");
            entity.Property(e => e.OrderExpectedtime).HasDefaultValueSql("'顺序配送'");
            entity.Property(e => e.OrderRider).HasDefaultValueSql("'店家骑手'");
            entity.Property(e => e.OrderRiderservice).HasDefaultValueSql("'店家配送'");
            entity.Property(e => e.OrderStatus).HasDefaultValueSql("'new'");
            entity.Property(e => e.OrderUcuuid).IsFixedLength();
            entity.Property(e => e.OrderUseruuid).IsFixedLength();

            entity.HasOne(d => d.OrderUcuu).WithMany(p => p.Orders).HasConstraintName("order_usercoupon_up_uuid_fk");

            entity.HasOne(d => d.OrderUseruu).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_user_user_uuid_fk");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductUuid).HasName("PRIMARY");

            entity.Property(e => e.ProductUuid).IsFixedLength();
            entity.Property(e => e.ProductIslisted).HasDefaultValueSql("'1'");
            entity.Property(e => e.ProductMerchantuuid).IsFixedLength();

            entity.HasOne(d => d.ProductMerchantuu).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_merchant_merchant_uuid_fk");
        });

        modelBuilder.Entity<Refund>(entity =>
        {
            entity.HasKey(e => e.RefundUuid).HasName("PRIMARY");

            entity.Property(e => e.RefundUuid).IsFixedLength();
            entity.Property(e => e.RefundOrderuuid).IsFixedLength();
            entity.Property(e => e.RefundStatus).HasDefaultValueSql("'new'");
            entity.Property(e => e.RefundUseruuid).IsFixedLength();

            entity.HasOne(d => d.RefundOrderuu).WithMany(p => p.Refunds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("refund_order_order_uuid_fk");

            entity.HasOne(d => d.RefundUseruu).WithMany(p => p.Refunds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("refund_user_user_uuid_fk");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserUuid).HasName("PRIMARY");

            entity.Property(e => e.UserUuid).IsFixedLength();
            entity.Property(e => e.UserCredit).HasDefaultValueSql("'50'");
            entity.Property(e => e.UserOpenid).IsFixedLength();
            entity.Property(e => e.UserStatus).HasDefaultValueSql("'new'");
        });

        modelBuilder.Entity<Usercoupon>(entity =>
        {
            entity.HasKey(e => e.UpUuid).HasName("PRIMARY");

            entity.Property(e => e.UpUuid).IsFixedLength();
            entity.Property(e => e.UpDiscountvalue).HasDefaultValueSql("'0.00'");
            entity.Property(e => e.UpStatus).HasDefaultValueSql("'unused'");
            entity.Property(e => e.UpUseruuid).IsFixedLength();

            entity.HasOne(d => d.UpUseruu).WithMany(p => p.Usercoupons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usercoupon_user_user_uuid_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
