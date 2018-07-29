namespace Fawn.DAL.EFCore.Contexts
{
    using Fawn.DAL.EFCore.EntityConfiguration;
    using Fawn.DAL.Models;
    using Microsoft.EntityFrameworkCore;

    public class FawnAppContext : DbContext
    {
        public FawnAppContext(DbContextOptions<FawnAppContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new GoodsConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new OrderGoodsConfiguration());
            builder.ApplyConfiguration(new GoodsPriceConfiguration());
            builder.ApplyConfiguration(new GoodsImageConfiguration());
        }

        #region DbSets

        public DbSet<GoodsDTO> Goods { get; set; }

        public DbSet<OrderDTO> Orders { get; set; }

        #endregion
    }
}