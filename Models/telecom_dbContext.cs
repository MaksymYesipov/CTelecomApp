using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CTelecomApp.Models
{
    public partial class telecom_dbContext : DbContext
    {
        public telecom_dbContext()
        {
        }

        public telecom_dbContext(DbContextOptions<telecom_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ContractStatus> ContractStatus { get; set; }
        public virtual DbSet<Managers> Managers { get; set; }
        public virtual DbSet<OrderStatus> OrderStatus { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<PackageTypes> PackageTypes { get; set; }
        public virtual DbSet<Services> Services { get; set; }
        public virtual DbSet<SimCards> SimCards { get; set; }
        public virtual DbSet<SimServices> SimServices { get; set; }
        public virtual DbSet<SimStatus> SimStatus { get; set; }
        public virtual DbSet<StarterPacks> StarterPacks { get; set; }
        public virtual DbSet<Tariffs> Tariffs { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=telecom_db;Username=postgres;Password=admin");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContractStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("contract_status_pkey");

                entity.ToTable("contract_status");

                entity.Property(e => e.StatusId)
                    .HasColumnName("status_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasColumnName("status_name")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Managers>(entity =>
            {
                entity.ToTable("managers");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("order_status");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.ClientName)
                    .HasColumnName("client_name")
                    .HasColumnType("character varying");

                entity.Property(e => e.ClientPhone)
                    .IsRequired()
                    .HasColumnName("client_phone")
                    .HasColumnType("character varying");

                entity.Property(e => e.PackId).HasColumnName("pack_id");

                entity.Property(e => e.StatusId)
                    .HasColumnName("status_id")
                    .HasDefaultValueSql("1");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("status_id");
            });

            modelBuilder.Entity<PackageTypes>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("package_types_pkey");

                entity.ToTable("package_types");

                entity.Property(e => e.TypeId)
                    .HasColumnName("type_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasColumnName("type_name")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Services>(entity =>
            {
                entity.HasKey(e => e.ServiceId)
                    .HasName("services_pkey");

                entity.ToTable("services");

                entity.Property(e => e.ServiceId)
                    .HasColumnName("service_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(1024);

                entity.Property(e => e.PackageType).HasColumnName("package_type");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ServiceName)
                    .IsRequired()
                    .HasColumnName("service_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Term).HasColumnName("term");

                entity.HasOne(d => d.PackageTypeNavigation)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.PackageType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("package");
            });

            modelBuilder.Entity<SimCards>(entity =>
            {
                entity.HasKey(e => e.SimId)
                    .HasName("sim_cards_pkey");

                entity.ToTable("sim_cards");

                entity.Property(e => e.SimId)
                    .HasColumnName("sim_id")
                    .HasMaxLength(20);

                entity.Property(e => e.Activated)
                    .HasColumnName("activated")
                    .HasColumnType("date");

                entity.Property(e => e.ActiveTill)
                    .HasColumnName("active_till")
                    .HasColumnType("date");

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id")
                    .HasMaxLength(30);

                entity.Property(e => e.MinutesAmount).HasColumnName("minutes_amount");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasColumnName("phone_number")
                    .HasMaxLength(15);

                entity.Property(e => e.SmsAmount).HasColumnName("sms_amount");

                entity.Property(e => e.StatusCode).HasColumnName("status_code");

                entity.Property(e => e.TariffId).HasColumnName("tariff_id");

                entity.Property(e => e.TrafficAmount).HasColumnName("traffic_amount");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.SimCards)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("customer");

                entity.HasOne(d => d.StatusCodeNavigation)
                    .WithMany(p => p.SimCards)
                    .HasForeignKey(d => d.StatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("status");

                entity.HasOne(d => d.Tariff)
                    .WithMany(p => p.SimCards)
                    .HasForeignKey(d => d.TariffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tariff");
            });

            modelBuilder.Entity<SimServices>(entity =>
            {
                entity.HasKey(e => e.ActivationId)
                    .HasName("activations_pkey");

                entity.ToTable("sim_services");

                entity.Property(e => e.ActivationId)
                    .HasColumnName("activation_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.ActivationDate)
                    .HasColumnName("activation_date")
                    .HasColumnType("date");

                entity.Property(e => e.ServiceId).HasColumnName("service_id");

                entity.Property(e => e.SimId)
                    .IsRequired()
                    .HasColumnName("sim_id")
                    .HasMaxLength(20);

                entity.Property(e => e.StatusCode)
                    .HasColumnName("status_code")
                    .HasDefaultValueSql("1");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.SimServices)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("service");

                entity.HasOne(d => d.Sim)
                    .WithMany(p => p.SimServices)
                    .HasForeignKey(d => d.SimId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sim");
            });

            modelBuilder.Entity<SimStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("sim_status_pkey");

                entity.ToTable("sim_status");

                entity.Property(e => e.StatusId)
                    .HasColumnName("status_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasColumnName("status_name")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<StarterPacks>(entity =>
            {
                entity.HasKey(e => e.PackId)
                    .HasName("starter_packs_pkey");

                entity.ToTable("starter_packs");

                entity.Property(e => e.PackId)
                    .HasColumnName("pack_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("date");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(1024);

                entity.Property(e => e.PackName)
                    .HasColumnName("pack_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.TariffId).HasColumnName("tariff_id");

                entity.HasOne(d => d.Tariff)
                    .WithMany(p => p.StarterPacks)
                    .HasForeignKey(d => d.TariffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tariff");
            });

            modelBuilder.Entity<Tariffs>(entity =>
            {
                entity.HasKey(e => e.TariffId)
                    .HasName("tariffs_pkey");

                entity.ToTable("tariffs");

                entity.Property(e => e.TariffId)
                    .HasColumnName("tariff_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(1024);

                entity.Property(e => e.MinutesPackage).HasColumnName("minutes_package");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.SmsPackage).HasColumnName("sms_package");

                entity.Property(e => e.TariffName)
                    .IsRequired()
                    .HasColumnName("tariff_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Term).HasColumnName("term");

                entity.Property(e => e.TrafficPackage).HasColumnName("traffic_package");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.IdCard)
                    .HasName("users_pkey");

                entity.ToTable("users");

                entity.Property(e => e.IdCard)
                    .HasColumnName("id_card")
                    .HasMaxLength(15);

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.Birthday)
                    .HasColumnName("birthday")
                    .HasColumnType("date");

                entity.Property(e => e.ContractStatus).HasColumnName("contract_status");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(30);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(300);

                entity.Property(e => e.RegDate)
                    .HasColumnName("reg_date")
                    .HasColumnType("date");

                entity.Property(e => e.Surname)
                    .HasColumnName("surname")
                    .HasMaxLength(30);

                entity.HasOne(d => d.ContractStatusNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.ContractStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("status");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
