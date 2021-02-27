using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class TestCRMContext : DbContext
    {
        public TestCRMContext()
        {
        }

        public TestCRMContext(DbContextOptions<TestCRMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountStatus> AccountStatuses { get; set; }
        public virtual DbSet<BranchOffice> BranchOffices { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Entitlement> Entitlements { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserStatus> UserStatuses { get; set; }
        public virtual DbSet<UserDetails> UserDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source =WAVETACTIC; Initial Catalog=TestCRM; Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AccountStatus>(entity =>
            {
                entity.ToTable("AccountStatus");

                entity.HasIndex(e => e.AccountStatusDesc, "uq_account_status")
                    .IsUnique();

                entity.Property(e => e.AccountStatusId).ValueGeneratedOnAdd();

                entity.Property(e => e.AccountStatusDesc)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<BranchOffice>(entity =>
            {
                entity.ToTable("BranchOffice");

                entity.HasIndex(e => e.BranchOfficeLocation, "uq_branch_office_location")
                    .IsUnique();

                entity.Property(e => e.BranchOfficeLocation)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");

                entity.HasIndex(e => e.ClientId, "uq_client_id")
                    .IsUnique();

                entity.Property(e => e.AssetValue).HasColumnType("money");

                entity.Property(e => e.ClientId)
                    .HasMaxLength(11)
                    .HasComputedColumnSql("('C'+CONVERT([nvarchar](10),[Id]))", false);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.DriversLicenseIdNum).HasMaxLength(20);

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.HomePhoneNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.MiddleName).HasMaxLength(200);

                entity.Property(e => e.OfficePhoneNumber)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.HasOne(d => d.AccountStatus)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.AccountStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_client_account_status");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_client_trust_officer");
            });

            modelBuilder.Entity<Entitlement>(entity =>
            {
                entity.ToTable("Entitlement");

                entity.HasIndex(e => e.EntitlementDesc, "uq_entitlement")
                    .IsUnique();

                entity.Property(e => e.EntitlementId).ValueGeneratedOnAdd();

                entity.Property(e => e.EntitlementDesc)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("Login");

                entity.HasIndex(e => e.Username, "uq_username")
                    .IsUnique();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_login_role_id");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.HasIndex(e => e.RoleDesc, "uq_role")
                    .IsUnique();

                entity.Property(e => e.RoleId).ValueGeneratedOnAdd();

                entity.Property(e => e.RoleDesc)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => new { e.FirstName, e.LastName, e.PhoneNumber }, "uq_user_and_number")
                    .IsUnique();

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.LoginId)
                    .HasConstraintName("fk_user_login_id");
            });

            modelBuilder.Entity<UserStatus>(entity =>
            {
                entity.ToTable("UserStatus");

                entity.HasIndex(e => e.UserStatusDesc, "uq_user_status")
                    .IsUnique();

                entity.Property(e => e.UserStatusId).ValueGeneratedOnAdd();

                entity.Property(e => e.UserStatusDesc)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserDetails>(entity =>
            {
                entity.HasKey(ud => new { ud.FirstName, ud.LastName, ud.PhoneNumber });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
