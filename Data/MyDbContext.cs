
using DriverApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DriverApi.Data;

public partial class MyDbContext : IdentityDbContext<Users, IdentityRole<string>,string, IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<DriverLocation> DriverLocations { get; set; }

    public virtual DbSet<DriverSubscription> DriverSubscriptions { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderTracking> OrderTrackings { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentTransaction> PaymentTransactions { get; set; }

    public virtual DbSet<SystemSetting> SystemSettings { get; set; }

    public override DbSet<Users> Users { get; set; }
    public virtual DbSet<RefreshToken> RefreshToken { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
     
        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.DriverId).HasName("PK__Drivers__3214EC071D23DA5D");

            entity.Property(e => e.AverageRating).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.IsAvailable).HasDefaultValue(true, "DF__Drivers__IsAvail__45F365D3");
            entity.Property(e => e.LicenseNumber).HasMaxLength(100);
        });

        modelBuilder.Entity<DriverLocation>(entity =>
        {
            entity.HasKey(e => e.Locid);

            entity.HasIndex(e => e.DriverId, "IX_DriverLocations_DriverId");

            entity.Property(e => e.Heading).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Latitude).HasColumnType("decimal(18, 15)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(18, 15)");
            entity.Property(e => e.Speed).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<DriverSubscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DriverSu__3214EC0732D3FB96");

            entity.Property(e => e.AutoRenew).HasDefaultValue(false, "DF__DriverSub__AutoR__787EE5A0");
            entity.Property(e => e.Subttxt).HasMaxLength(255);

            entity.HasOne(d => d.Driver).WithMany(p => p.DriverSubscriptions)
                .HasForeignKey(d => d.DriverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DriverSub__Drive__797309D9");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC0747D642DA");

            entity.Property(e => e.IsRead).HasDefaultValue(false, "DF__Notificat__IsRea__72C60C4A");
            entity.Property(e => e.NotificationTemplateId).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__UserI__73BA3083");
        });
        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Refresh__071D23DA5D");
       
        });
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC07F7C4C954");

            entity.HasIndex(e => e.OrderNumber, "UQ__Orders__CAC5E743D47A1353").IsUnique();

            entity.Property(e => e.DistanceKm).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.DropoffLatitude).HasColumnType("decimal(18, 15)");
            entity.Property(e => e.DropoffLongitude).HasColumnType("decimal(18, 15)");
            entity.Property(e => e.EstimatedPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FinalPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.OrderNumber).HasMaxLength(100);
            entity.Property(e => e.PickupLatitude).HasColumnType("decimal(18, 15)");
            entity.Property(e => e.PickupLongitude).HasColumnType("decimal(18, 15)");

            entity.HasOne(d => d.Dr).WithMany(p => p.Orders)
                .HasForeignKey(d => d.DrId)
                .HasConstraintName("FK__Orders__DrId__693CA210");

            entity.HasOne(d => d.Usr).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UsrId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__UsrId__68487DD7");
        });

        modelBuilder.Entity<OrderTracking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderTra__3214EC07D6126D2A");

            entity.ToTable("OrderTracking");

            entity.Property(e => e.Latitude).HasColumnType("decimal(18, 15)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(18, 15)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderTrackings)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderTrac__Order__6C190EBB");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payments__3214EC079F2CAD2C");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Currency).HasMaxLength(10);
            entity.Property(e => e.TransactionReference).HasMaxLength(255);

            entity.HasOne(d => d.DriverSubscription).WithMany(p => p.Payments)
                .HasForeignKey(d => d.DriverSubscriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__Driver__7C4F7684");
        });

        modelBuilder.Entity<PaymentTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PaymentT__3214EC0701C89B69");

            entity.Property(e => e.ExternalTransactionId).HasMaxLength(255);

            entity.HasOne(d => d.Payment).WithMany(p => p.PaymentTransactions)
                .HasForeignKey(d => d.PaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PaymentTr__Payme__7F2BE32F");
        });

        //modelBuilder.Entity<Role>(entity =>
        //{
        //    entity.HasKey(e => e.Rid);

        //    entity.HasIndex(e => e.Name, "UQ__Roles__737584F6239D1715").IsUnique();

        //    entity.Property(e => e.Description).HasMaxLength(255);
        //    entity.Property(e => e.Name).HasMaxLength(50);
        //});

        modelBuilder.Entity<SystemSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SystemSe__3214EC0704378048");

            entity.Property(e => e.Settxt).HasMaxLength(255);
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
              .HasColumnName("Id");
            entity.HasIndex(e => e.PhoneNumber, "UQ__Users__85FB4E38B5D2D46A").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105347FB5D232").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.LastLoginAt).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF__Users__IsActive__398D8EEE");
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        });

        //modelBuilder.Entity<UserRole>(entity =>
        //{
        //    entity.HasNoKey();
        //});
        modelBuilder.Entity<IdentityRole<string>>()
      .ToTable("AspNetRoles");

        modelBuilder.Entity<IdentityUserRole<string>>()
            .ToTable("AspNetUserRoles");

        modelBuilder.Entity<IdentityUserClaim<string>>()
            .ToTable("AspNetUserClaims");

        modelBuilder.Entity<IdentityUserLogin<string>>()
            .ToTable("AspNetUserLogins");

        modelBuilder.Entity<IdentityRoleClaim<string>>()
            .ToTable("AspNetRoleClaims");

        modelBuilder.Entity<IdentityUserToken<string>>()
            .ToTable("AspNetUserTokens");
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
