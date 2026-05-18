
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

  
    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentTransaction> PaymentTransactions { get; set; }

    public virtual DbSet<SystemSetting> SystemSettings { get; set; }

    public override DbSet<Users> Users { get; set; }
    public virtual DbSet<RefreshToken> RefreshToken { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
     
        
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
