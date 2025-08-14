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

    public virtual DbSet<Usercoupon> Usercoupons { get; set; }

    public virtual DbSet<Walletaccount> Walletaccounts { get; set; }

    public virtual DbSet<Walletrequest> Walletrequests { get; set; }

    public virtual DbSet<Wallettransaction> Wallettransactions { get; set; }

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
            entity.Property(e => e.AddressUseruuid).IsFixedLength();

            entity.HasOne(d => d.AddressUseruu).WithMany(p => p.Addresses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("address_user_user_uuid_fk");
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminUuid).HasName("PRIMARY");

            entity.Property(e => e.AdminUuid).IsFixedLength();
            entity.Property(e => e.AdminAccount).ValueGeneratedOnAdd();
            entity.Property(e => e.AdminKey).IsFixedLength();
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
            entity.HasKey(e => e.AuditUuid).HasName("PRIMARY");

            entity.Property(e => e.AuditUuid).IsFixedLength();
            entity.Property(e => e.AuditAuditoruuid).IsFixedLength();
            entity.Property(e => e.AuditGroupuuid).IsFixedLength();
            entity.Property(e => e.AuditObjectuuid).IsFixedLength();
            entity.Property(e => e.AuditStatus).HasDefaultValueSql("'pending'");
            entity.Property(e => e.AuditSubmitteruuid).IsFixedLength();

            entity.HasOne(d => d.AuditGroupuu).WithMany(p => p.Audits)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("audit_auditgroup_ag_uuid_fk");
        });

        modelBuilder.Entity<Auditgroup>(entity =>
        {
            entity.HasKey(e => e.AgUuid).HasName("PRIMARY");

            entity.Property(e => e.AgUuid).IsFixedLength();
            entity.Property(e => e.AgSubmitteruuid).IsFixedLength();
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartUuid).HasName("PRIMARY");

            entity.Property(e => e.CartUuid).IsFixedLength();
            entity.Property(e => e.CartProductuuid).IsFixedLength();
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

        modelBuilder.Entity<Delivery>(entity =>
        {
            entity.HasKey(e => e.DeliveryUuid).HasName("PRIMARY");

            entity.Property(e => e.DeliveryUuid).IsFixedLength();
            entity.Property(e => e.DeliveryNotificationuuid).IsFixedLength();
            entity.Property(e => e.DeliveryReceiveruuid).IsFixedLength();

            entity.HasOne(d => d.DeliveryNotificationuu).WithMany(p => p.Deliveries)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("delivery_notification_notification_uuid_fk");
        });

        modelBuilder.Entity<Localfile>(entity =>
        {
            entity.HasKey(e => e.LocalfileUuid).HasName("PRIMARY");

            entity.Property(e => e.LocalfileUuid).IsFixedLength();
            entity.Property(e => e.LocalfileObjectuuid).IsFixedLength();
            entity.Property(e => e.LocalfileUploaderuuid).IsFixedLength();
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

            entity.HasOne(d => d.MerchantAdminuu).WithMany(p => p.Merchants)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("merchant_admin_admin_uuid_fk");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationUuid).HasName("PRIMARY");

            entity.Property(e => e.NotificationUuid).IsFixedLength();
            entity.Property(e => e.NotificationSenderuuid).IsFixedLength();
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderUuid).HasName("PRIMARY");

            entity.Property(e => e.OrderUuid).IsFixedLength();
            entity.Property(e => e.OrderChannel).HasDefaultValueSql("'wechat'");
            entity.Property(e => e.OrderExpectedtime).HasDefaultValueSql("'顺序配送'");
            entity.Property(e => e.OrderPaymentuuid).IsFixedLength();
            entity.Property(e => e.OrderStatus).HasDefaultValueSql("'created'");
            entity.Property(e => e.OrderUcuuid).IsFixedLength();
            entity.Property(e => e.OrderUseruuid).IsFixedLength();

            entity.HasOne(d => d.OrderUcuu).WithMany(p => p.Orders).HasConstraintName("order_usercoupon_up_uuid_fk");

            entity.HasOne(d => d.OrderUseruu).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_user_user_uuid_fk");
        });

        modelBuilder.Entity<Orderitem>(entity =>
        {
            entity.HasKey(e => e.OrderitemUuid).HasName("PRIMARY");

            entity.Property(e => e.OrderitemUuid).IsFixedLength();
            entity.Property(e => e.OrderitemOrderuuid).IsFixedLength();
            entity.Property(e => e.OrderitemProductuuid).IsFixedLength();

            entity.HasOne(d => d.OrderitemOrderuu).WithMany(p => p.Orderitems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderitem_order_order_uuid_fk");

            entity.HasOne(d => d.OrderitemProductuu).WithMany(p => p.Orderitems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderitem_product_product_uuid_fk");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentUuid).HasName("PRIMARY");

            entity.Property(e => e.PaymentUuid).IsFixedLength();
            entity.Property(e => e.PaymentOrderuuid).IsFixedLength();
            entity.Property(e => e.PaymentStatus).HasDefaultValueSql("'pending'");

            entity.HasOne(d => d.PaymentOrderuu).WithMany(p => p.Payments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_order_order_uuid_fk");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PRIMARY");
        });

        modelBuilder.Entity<Privilege>(entity =>
        {
            entity.HasKey(e => e.PrivilegeId).HasName("PRIMARY");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductUuid).HasName("PRIMARY");

            entity.Property(e => e.ProductUuid).IsFixedLength();
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
            entity.Property(e => e.RefundStatus).HasDefaultValueSql("'create'");
            entity.Property(e => e.RefundUseruuid).IsFixedLength();

            entity.HasOne(d => d.RefundOrderuu).WithMany(p => p.Refunds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("refund_order_order_uuid_fk");

            entity.HasOne(d => d.RefundUseruu).WithMany(p => p.Refunds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("refund_user_user_uuid_fk");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");
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
            entity.HasKey(e => e.UserUuid).HasName("PRIMARY");

            entity.Property(e => e.UserUuid).IsFixedLength();
            entity.Property(e => e.UserCredit).HasDefaultValueSql("'50'");
            entity.Property(e => e.UserOpenid).IsFixedLength();
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

        modelBuilder.Entity<Usercoupon>(entity =>
        {
            entity.HasKey(e => e.UcUuid).HasName("PRIMARY");

            entity.Property(e => e.UcUuid).IsFixedLength();
            entity.Property(e => e.UcDiscountvalue).HasDefaultValueSql("'0.00'");
            entity.Property(e => e.UcStatus).HasDefaultValueSql("'unused'");
            entity.Property(e => e.UcUseruuid).IsFixedLength();

            entity.HasOne(d => d.UcCoupon).WithMany(p => p.Usercoupons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usercoupon_coupon_coupon_id_fk");

            entity.HasOne(d => d.UcUseruu).WithMany(p => p.Usercoupons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usercoupon_user_user_uuid_fk");
        });

        modelBuilder.Entity<Walletaccount>(entity =>
        {
            entity.HasKey(e => e.WaUuid).HasName("PRIMARY");

            entity.Property(e => e.WaUuid).IsFixedLength();
            entity.Property(e => e.WaMerchantuuid).IsFixedLength();

            entity.HasOne(d => d.WaMerchantuu).WithMany(p => p.Walletaccounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("wallet_account_merchant_merchant_uuid_fk");
        });

        modelBuilder.Entity<Walletrequest>(entity =>
        {
            entity.HasKey(e => e.WrUuid).HasName("PRIMARY");

            entity.Property(e => e.WrUuid).IsFixedLength();
            entity.Property(e => e.WrMerchantuuid).IsFixedLength();

            entity.HasOne(d => d.WrMerchantuu).WithMany(p => p.Walletrequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("walletrequest_merchant_merchant_uuid_fk");
        });

        modelBuilder.Entity<Wallettransaction>(entity =>
        {
            entity.HasKey(e => e.WtUuid).HasName("PRIMARY");

            entity.Property(e => e.WtUuid).IsFixedLength();
            entity.Property(e => e.WtMerchantuuid).IsFixedLength();
            entity.Property(e => e.WtObjectuuid).IsFixedLength();

            entity.HasOne(d => d.WtMerchantuu).WithMany(p => p.Wallettransactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("wallettransaction_merchant_merchant_uuid_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
