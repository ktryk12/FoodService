using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer ;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using static Azure.Core.HttpHeader;
using System.Reflection.Emit;

namespace FoodService.Modellayer
{


    public class ServiceContext : DbContext, IServiceContext
    {
        private readonly ILoggerFactory _loggerFactory;
        public ServiceContext(DbContextOptions<ServiceContext> options, ILoggerFactory loggerFactory)
    : base(options)
        {
            _loggerFactory = loggerFactory;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLoggerFactory(_loggerFactory);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalesItemComposition>(entity =>
            {
                // Definerer en sammensat nøgle bestående af ParentItemId og ChildItemId
                entity.HasKey(sic => new { sic.ParentItemId, sic.ChildItemId });

                // Konfigurerer en-til-mange relation fra SalesItem til SalesItemComposition som ParentItem
                entity.HasOne(sic => sic.ParentItem)
                      .WithMany(si => si.ParentCompositions) // Brug navigationsegenskab i SalesItem
                      .HasForeignKey(sic => sic.ParentItemId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Konfigurerer en-til-mange relation fra SalesItem til SalesItemComposition som ChildItem
                entity.HasOne(sic => sic.ChildItem)
                      .WithMany(si => si.ChildCompositions) // Brug navigationsegenskab i SalesItem
                      .HasForeignKey(sic => sic.ChildItemId)
                      .OnDelete(DeleteBehavior.Restrict);
            });



            modelBuilder.Entity<CustomerGroup>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC2718DCCB36");
                entity.ToTable("CustomerGroup");
                entity.Property(e => e.Id).HasColumnName("ID")
                  .ValueGeneratedOnAdd();
                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Discount__3214EC272E754D2D");
                entity.ToTable("Discount");
                entity.Property(e => e.Id).HasColumnName("ID")
                  .ValueGeneratedOnAdd();
                entity.Property(e => e.CustomerGroupId).HasColumnName("CustomerGroupID");
                entity.Property(e => e.SalesItemGroupId).HasColumnName("ProductGroupID");
                entity.Property(e => e.Rate).HasColumnType("decimal(5, 2)");
                // Oprettelse af mange-til-mange relation mellem CustomerGruop og Discount
                entity.HasOne(d => d.CustomerGroup)
            .WithMany(p => p.Discounts)
            .HasForeignKey(d => d.CustomerGroupId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK__Discount__Custom__49C3F6B7");
                // Oprettelse af mange-til-mange relation mellem SalesItemGroup og Discount
                entity.HasOne(d => d.SalesItemGroup)
             .WithMany(p => p.Discounts)
             .HasForeignKey(d => d.SalesItemGroupId)
             .OnDelete(DeleteBehavior.Restrict)
             .HasConstraintName("FK__Discount__SalesIte__48CFD27E");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Ingredie__3214EC2778E4B991");
                entity.ToTable("Ingredient");
                entity.Property(e => e.Id).HasColumnName("ID")
                  .ValueGeneratedOnAdd();
                entity.Property(e => e.IngredientPrice).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.ImageUrl)
                 .HasMaxLength(500);
                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<IngredientOrderline>(entity =>
            {
                entity.HasKey(e => new { e.IngredientId, e.OrderlineId }).HasName("PK__Ingredie__8127A0A74100BD35");
                entity.ToTable("IngredientOrderline");
                entity.Property(e => e.IngredientId).HasColumnName("IngredientID");
                entity.Property(e => e.OrderlineId).HasColumnName("OrderlineID");
                // Oprettelse af mange-til-mange relation mellem Ingredient og IngredientOrderline
                entity.HasOne(d => d.Ingredient)
            .WithMany(p => p.IngredientOrderlines)
            .HasForeignKey(d => d.IngredientId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK__Ingredien__Ingre__4316F928");
                // Oprettelse af mange-til-mange relation mellem Orderline og IngredientOrderline
                entity.HasOne(d => d.Orderline)
                    .WithMany(p => p.IngredientOrderlines)
                    .HasForeignKey(d => d.OrderlineId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__Ingredien__Order__440B1D61");
            });

            modelBuilder.Entity<IngredientSalesItem>(entity =>
            {
                entity.HasKey(e => new { e.SalesItemId, e.IngredientId }).HasName("PK__Ingredie__0FE62DCA5B3496C2");

                entity.ToTable("IngredientSalesItem");

                entity.Property(e => e.SalesItemId).HasColumnName("SalesItemID");
                entity.Property(e => e.IngredientId).HasColumnName("IngredientID");
                entity.Property(e => e.Min).IsRequired();
                entity.Property(e => e.Max).IsRequired();
                entity.Property(e => e.Count)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
                // Oprettelse af mange-til-mange relation mellem Ingrediens og IngredientSalesItem
                entity.HasOne(d => d.Ingredient)
        .WithMany(p => p.IngredientSalesItems)
        .HasForeignKey(d => d.IngredientId)
        .OnDelete(DeleteBehavior.Restrict)
        .HasConstraintName("FK__Ingredien__Ingre__300424B4");
                // Oprettelse af mange-til-mange relation mellem SalesItem og IngredientSaleItem
                entity.HasOne(d => d.SalesItem)
                    .WithMany(p => p.IngredientSalesItems)
                    .HasForeignKey(d => d.SalesItemId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__Ingredien__SalesIte__2F10007B");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__OrderData__3214EC27...");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");
                entity.Property(e => e.Datetime).HasColumnType("datetime");
                entity.Property(e => e.ShopId).HasColumnName("ShopID");
                entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

                // Opdatering af relationen mellem Order og Shop
                entity.HasOne(d => d.Shop)
        .WithMany(p => p.Orders)
        .HasForeignKey(d => d.ShopId)
        .OnDelete(DeleteBehavior.Restrict)
        .HasConstraintName("FK__OrderData__ShopID__32E0915F");
            });





            modelBuilder.Entity<Orderline>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Orderlin__3214EC273B1DF0EA");

                entity.ToTable("Orderline");

                entity.Property(e => e.Id).HasColumnName("ID").ValueGeneratedOnAdd();
                entity.Property(e => e.OrderlinePrice).HasColumnType("decimal(10, 2)");

                // Gør OrderDataId til en obligatorisk felt
                entity.Property(e => e.OrderId).HasColumnName("OrderId").IsRequired();
                // Oprettelse af mange-til-mange relation mellem Order og Orderline 
                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Orderlines)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__Orderline__Order__36B12243");
            });


            modelBuilder.Entity<SalesItem>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__SalesItem__3214EC2714F20DB5");

                entity.ToTable("SalesItem");
                entity.HasIndex(e => e.ProductNumber, "UQ__SalesItem__49A3C8398A3F3285").IsUnique();
                entity.Property(e => e.Id).HasColumnName("ID")
                  .ValueGeneratedOnAdd();
                entity.Property(e => e.BasePrice).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.Category)
                    .HasMaxLength(30)
                    .IsUnicode(false);
                entity.Property(e => e.Name).HasMaxLength(30).IsRequired();
                entity.Property(e => e.ImageUrl).HasMaxLength(500);
                entity.Property(e => e.IsActive).HasColumnType("bit");
                entity.Property(e => e.IsComposite).HasColumnType("bit");
                entity.Property(e => e.SalesItemGroupId).HasColumnName("SalesItemGroupID");
                entity.Property(e => e.ProductNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                // Oprettelse af mange-til-mange relation mellem SalesItem og SalesItemGroup
                entity.HasOne(d => d.SalesItemGroup)
    .WithMany(p => p.SalesItems)
    .HasForeignKey(d => d.SalesItemGroupId)
    .OnDelete(DeleteBehavior.Restrict)
    .HasConstraintName("FK__SalesItem__SalesItem__286302EC");
            });


            modelBuilder.Entity<SalesItemGroup>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__SalesItemGroup__3214EC27745E8750");

                entity.ToTable("SalesItemGroup"); // Ret tabellens navn her

                entity.Property(e => e.Id).HasColumnName("ID")
                .ValueGeneratedOnAdd();
                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Shop__3214EC2718F16219");

                entity.ToTable("Shop");

                entity.Property(e => e.Id).HasColumnName("ID")
                .ValueGeneratedOnAdd();
                entity.Property(e => e.Location)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                // Oprettelse af mange-til-mange relation mellem Shop og SalesItem
                entity.HasMany(d => d.SalesItems)
                .WithMany(p => p.Shops)
                .UsingEntity<Dictionary<string, object>>(
                    "ShopSalesItem", // Navnet på join-tabellen
                    r => r.HasOne<SalesItem>().WithMany()
                        .HasForeignKey("SalesItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("FK__ShopSalesItem__SalesItemId"),
                    l => l.HasOne<Shop>().WithMany()
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("FK__ShopSalesItem__ShopId"),
                    j =>
                    {
                        j.HasKey("SalesItemId", "ShopId"); // Angiver sammensatte nøgler for join-tabellen
                    });
            });
        }











        public virtual DbSet<SalesItem> SalesItems { get; set; }
        public virtual DbSet<CustomerGroup> CustomerGroups { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<IngredientOrderline> IngredientOrderlines { get; set; }
        public virtual DbSet<IngredientSalesItem> IngredientSalesItems { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Orderline> Orderlines { get; set; }
        public virtual DbSet<SalesItemComposition> SalesItemCompositions { get; set; }
        public virtual DbSet<SalesItemGroup> SalesItemGroups { get; set; }

        public virtual DbSet<Shop> Shops { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            base.Add(entity);
        }

        public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return base.Entry(entity);
        }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        public new void Dispose()
        {
            base.Dispose();
        }
        
    }

}
    


     
