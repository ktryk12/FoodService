using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FoodService.Modellayer
{
    public interface IServiceContext
    {
        DbSet<SalesItem> SalesItems { get; set; }
        DbSet<CustomerGroup> CustomerGroups { get; set; }
        DbSet<Discount> Discounts { get; set; }
        DbSet<Ingredient> Ingredients { get; set; }
        DbSet<IngredientOrderline> IngredientOrderlines { get; set; }
        DbSet<IngredientSalesItem> IngredientSalesItems { get; set; }
        DbSet<Order> Order { get; set; }
        DbSet<Orderline> Orderlines { get; set; }
        DbSet<SalesItemComposition> SalesItemCompositions { get; set; }
        DbSet<SalesItemGroup> SalesItemGroups { get; set; }
        DbSet<Shop> Shops { get; set; }
        DbSet<AdminUser> AdminUsers { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        void Add<TEntity>(TEntity entity) where TEntity : class;
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        void Dispose();
        // Eventuelle andre nødvendige metoder...
    }
}
