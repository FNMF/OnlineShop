using System;
using System.Collections.Generic;
using API.Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace API.Infrastructure.Database;

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

    public virtual DbSet<AdminRole> AdminRoles { get; set; }

    public virtual DbSet<Audit> Audits { get; set; }

    public virtual DbSet<Auditgroup> Auditgroups { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Cartitem> Cartitems { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<Delivery> Deliveries { get; set; }

    public virtual DbSet<Localfile> Localfiles { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Merchant> Merchants { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderitem> Orderitems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Privilege> Privileges { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Refund> Refunds { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPrivilege> UserPrivileges { get; set; }

    public virtual DbSet<UserCoupon> Usercoupons { get; set; }

    public virtual DbSet<WalletAccount> Walletaccounts { get; set; }

    public virtual DbSet<WalletRequest> Walletrequests { get; set; }

    public virtual DbSet<WalletTransaction> Wallettransactions { get; set; }

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
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.Property(e => e.Uuid).IsFixedLength();
            entity.Property(e => e.UserUuid).IsFixedLength();

            entity.HasOne(d => d.AddressUseruu).WithMany(p => p.Addresses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("address_user_user_uuid_fk");
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.Property(e => e.Uuid).IsFixedLength();
            entity.Property(e => e.Account).ValueGeneratedOnAdd();
            entity.Property(e => e.Key).IsFixedLength();
        });

        modelBuilder.Entity<AdminRole>(entity =>
        {
            entity.HasKey(e => e.ArId).HasName("PRIMARY");

            entity.Property(e => e.ArAdminuuid).IsFixedLength();

            entity.HasOne(d => d.ArAdminuu).WithMany(p => p.AdminRoles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("admin_role_admin_admin_uuid_fk");

            entity.HasOne(d => d.ArRole).WithMany(p => p.AdminRoles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("admin_role_role_role_id_fk");
        });

        modelBuilder.Entity<Audit>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.Property(e => e.Uuid).IsFixedLength();
            entity.Property(e => e.AuditorUuid).IsFixedLength();
            entity.Property(e => e.GroupUuid).IsFixedLength();
            entity.Property(e => e.ObjectUuid).IsFixedLength();
            entity.Property(e => e.AuditStatus).HasDefaultValueSql("'pending'");
            entity.Property(e => e.SubmitterUuid).IsFixedLength();

            entity.HasOne(d => d.AuditGroupuu).WithMany(p => p.Audits)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("audit_auditgroup_ag_uuid_fk");
        });

        modelBuilder.Entity<Auditgroup>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.Property(e => e.Uuid).IsFixedLength();
            entity.Property(e => e.SubmitterUuid).IsFixedLength();
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.Property(e => e.Uuid).IsFixedLength();
            entity.Property(e => e.MerchantUuid).IsFixedLength();
            entity.Property(e => e.UserUuid).IsFixedLength();

            entity.HasOne(d => d.CartUseruu).WithMany(p => p.Carts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cart_user_user_uuid_fk");
        });

        modelBuilder.Entity<Cartitem>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.Property(e => e.Uuid).IsFixedLength();
            entity.Property(e => e.CartUuid).IsFixedLength();
            entity.Property(e => e.ProductUuid).IsFixedLength();

            entity.HasOne(d => d.CartitemCartuu).WithMany(p => p.Cartitems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cartitem_cart_cart_uuid_fk");
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CouponStatus).HasDefaultValueSql("'NA'");
        });

        modelBuilder.Entity<Delivery>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.Property(e => e.Uuid).IsFixedLength();
            entity.Property(e => e.NotificationUuid).IsFixedLength();
            entity.Property(e => e.ReceiverUuid).IsFixedLength();

            entity.HasOne(d => d.DeliveryNotificationuu).WithMany(p => p.Deliveries)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("delivery_notification_notification_uuid_fk");
        });

        modelBuilder.Entity<Localfile>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.Property(e => e.Uuid).IsFixedLength();
            entity.Property(e => e.ObjectUuid).IsFixedLength();
            entity.Property(e => e.UploaderUuid).IsFixedLength();
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.Property(e => e.Uuid).IsFixedLength();
            entity.Property(e => e.ObjectUuid).IsFixedLength();
        });

        modelBuilder.Entity<Merchant>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.Property(e => e.Uuid).IsFixedLength();
            entity.Property(e => e.AdminUuid).IsFixedLength();
            entity.Property(e => e.IsClosed).HasDefaultValueSql("'1'");

            entity.HasOne(d => d.MerchantAdminuu).WithMany(p => p.Merchants)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("merchant_admin_admin_uuid_fk");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.Property(e => e.Uuid).IsFixedLength();
            entity.Property(e => e.ObjectUuid).IsFixedLength();
            entity.Property(e => e.SenderUuid).IsFixedLength();
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.Property(e => e.Uuid).IsFixedLength();
            entity.Property(e => e.Channel).HasDefaultValueSql("'wechat'");
            entity.Property(e => e.ExpectedTime).HasDefaultValueSql("'顺序配送'");
            entity.Property(e => e.PaymentUuid).IsFixedLength();
            entity.Property(e => e.OrderStatus).HasDefaultValueSql("'created'");
            entity.Property(e => e.UserCouponUuid).IsFixedLength();
            entity.Property(e => e.UserUuid).IsFixedLength();

            entity.HasOne(d => d.OrderUcuu).WithMany(p => p.Orders).HasConstraintName("order_usercoupon_up_uuid_fk");

            entity.HasOne(d => d.OrderUseruu).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_user_user_uuid_fk");
        });

        modelBuilder.Entity<Orderitem>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.Property(e => e.Uuid).IsFixedLength();
            entity.Property(e => e.OrderUuid).IsFixedLength();
            entity.Property(e => e.ProductUuid).IsFixedLength();

            entity.HasOne(d => d.OrderitemOrderuu).WithMany(p => p.Orderitems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderitem_order_order_uuid_fk");

            entity.HasOne(d => d.OrderitemProductuu).WithMany(p => p.Orderitems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderitem_product_product_uuid_fk");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.Property(e => e.Uuid).IsFixedLength();
            entity.Property(e => e.OrderUuid).IsFixedLength();
            entity.Property(e => e.PaymentStatus).HasDefaultValueSql("'pending'");

            entity.HasOne(d => d.PaymentOrderuu).WithMany(p => p.Payments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_order_order_uuid_fk");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<Privilege>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.Property(e => e.Uuid).IsFixedLength();
            entity.Property(e => e.MerchantUuid).IsFixedLength();

            entity.HasOne(d => d.ProductMerchantuu).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_merchant_merchant_uuid_fk");
        });

        modelBuilder.Entity<Refund>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.Property(e => e.Uuid).IsFixedLength();
            entity.Property(e => e.OrderUuid).IsFixedLength();
            entity.Property(e => e.RefundStatus).HasDefaultValueSql("'create'");
            entity.Property(e => e.UserUuid).IsFixedLength();

            entity.HasOne(d => d.RefundOrderuu).WithMany(p => p.Refunds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("refund_order_order_uuid_fk");

            entity.HasOne(d => d.RefundUseruu).WithMany(p => p.Refunds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("refund_user_user_uuid_fk");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => e.RpId).HasName("PRIMARY");

            entity.HasOne(d => d.RpPermission).WithMany(p => p.RolePermissions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_permission_permission_permission_id_fk");

            entity.HasOne(d => d.RpRole).WithMany(p => p.RolePermissions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_permission_role_role_id_fk");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.Property(e => e.Uuid).IsFixedLength();
            entity.Property(e => e.Credit).HasDefaultValueSql("'50'");
            entity.Property(e => e.OpenId).IsFixedLength();
        });

        modelBuilder.Entity<UserPrivilege>(entity =>
        {
            entity.HasKey(e => e.UpUuid).HasName("PRIMARY");

            entity.Property(e => e.UpUuid).IsFixedLength();
            entity.Property(e => e.UpUseruuid).IsFixedLength();

            entity.HasOne(d => d.UpPrivilege).WithMany(p => p.UserPrivileges)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_privilege_privilege_privilege_id_fk");

            entity.HasOne(d => d.UpUseruu).WithMany(p => p.UserPrivileges)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_privilege_user_user_uuid_fk");
        });

        modelBuilder.Entity<UserCoupon>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.Property(e => e.Uuid).IsFixedLength();
            entity.Property(e => e.DiscountValue).HasDefaultValueSql("'0.00'");
            entity.Property(e => e.UserCouponStatus).HasDefaultValueSql("'unused'");
            entity.Property(e => e.UserUuid).IsFixedLength();

            entity.HasOne(d => d.UcCoupon).WithMany(p => p.Usercoupons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usercoupon_coupon_coupon_id_fk");

            entity.HasOne(d => d.UcUseruu).WithMany(p => p.Usercoupons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usercoupon_user_user_uuid_fk");
        });

        modelBuilder.Entity<WalletAccount>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.Property(e => e.Uuid).IsFixedLength();
            entity.Property(e => e.MerchantUuid).IsFixedLength();

            entity.HasOne(d => d.WaMerchantuu).WithMany(p => p.Walletaccounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("wallet_account_merchant_merchant_uuid_fk");
        });

        modelBuilder.Entity<WalletRequest>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.Property(e => e.Uuid).IsFixedLength();
            entity.Property(e => e.MerchantUuid).IsFixedLength();

            entity.HasOne(d => d.WrMerchantuu).WithMany(p => p.Walletrequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("walletrequest_merchant_merchant_uuid_fk");
        });

        modelBuilder.Entity<WalletTransaction>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.Property(e => e.Uuid).IsFixedLength();
            entity.Property(e => e.MerchantUuid).IsFixedLength();
            entity.Property(e => e.ObjectUuid).IsFixedLength();

            entity.HasOne(d => d.WtMerchantuu).WithMany(p => p.Wallettransactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("wallettransaction_merchant_merchant_uuid_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
