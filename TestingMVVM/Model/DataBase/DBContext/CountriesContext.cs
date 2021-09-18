using System;
using System.Data.SqlClient;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using TestingMVVM.Model.DataBase.Entities;
#nullable disable

namespace TestingMVVM.Model.DataBase.DBContext
{
    public partial class CountriesContext : DbContext
    {
        public CountriesContext()
        {
        }

        public CountriesContext(DbContextOptions<CountriesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Region> Regions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(CodeFirstUpdateConnectionString);
            }
        }

        #region Источник БД

        private static string _SourceDB;

        public static string SourceDB
        {
            get => _SourceDB;
            set => _SourceDB = value;
        }

        #endregion

        #region Название БД

        private static string _CatalogDB;

        public static string CatalogDB
        {
            get => _CatalogDB;
            set => _CatalogDB = value;
        }

        #endregion

        private static string _codeFirstUpdateConnectionString;

        private static string CodeFirstUpdateConnectionString
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_codeFirstUpdateConnectionString))
                {
                    string dir = Environment.CurrentDirectory;

                    var config = new ConfigurationBuilder()
                        .AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.json"))
                        .Build();

                    var codeFirstConnectionString = config.GetSection("ConnectionStrings")["CountriesContext"];

                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(codeFirstConnectionString);

                    builder.DataSource = SourceDB;
                    builder.InitialCatalog = CatalogDB;
                    //builder.IntegratedSecurity = true;
                    //builder.ConnectTimeout = 30;
                    //builder.Encrypt = false;
                    //builder.TrustServerCertificate = false;
                    //builder.ApplicationIntent = ApplicationIntent.ReadWrite;
                    //builder.MultiSubnetFailover = false;

                    _codeFirstUpdateConnectionString = builder.ConnectionString;
                }
                return _codeFirstUpdateConnectionString;
            }
        }

        //#region получение строки
        //public static string CreateConnectionString(string DataSource, string Catalog)
        //{
        //    var config = new ConfigurationBuilder()
        //                .AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.json"))
        //                .Build();

        //    var codeFirstConnectionString = config.GetSection("ConnectionStrings")["CountriesContext"];

        //    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(codeFirstConnectionString);

        //    builder.DataSource = DataSource;
        //    builder.InitialCatalog = Catalog;

        //    return builder.ConnectionString;
        //}
        //#endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CityName).HasColumnName("city_name");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Area)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("area");

                entity.Property(e => e.Capital).HasColumnName("capital");

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("country_code");

                entity.Property(e => e.CountryName)
                    .HasMaxLength(100)
                    .HasColumnName("country_name");

                entity.Property(e => e.CountryPopulation).HasColumnName("country_population");

                entity.Property(e => e.Region).HasColumnName("region");

                entity.HasOne(d => d.CapitalNavigation)
                    .WithMany(p => p.Countries)
                    .HasForeignKey(d => d.Capital)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Countries__capit__286302EC");

                entity.HasOne(d => d.RegionNavigation)
                    .WithMany(p => p.Countries)
                    .HasForeignKey(d => d.Region)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Countries__regio__29572725");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RegionName)
                    .HasMaxLength(20)
                    .HasColumnName("region_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
