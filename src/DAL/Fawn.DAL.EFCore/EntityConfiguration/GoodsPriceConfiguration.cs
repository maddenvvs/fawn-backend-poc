

namespace Fawn.DAL.EFCore.EntityConfiguration
{
    using Fawn.DAL.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class GoodsPriceConfiguration : IEntityTypeConfiguration<GoodsPriceDTO>
    {
        public void Configure(EntityTypeBuilder<GoodsPriceDTO> builder)
        {
            builder.ToTable("GoodsPrice");

            builder.Property(p => p.ItemPrice)
                .HasColumnType("money")
                .IsRequired();

            builder.HasData(new[] {
                new GoodsPriceDTO
                {
                    Id = 1,
                    ItemPrice = 10.5m
                },
                new GoodsPriceDTO
                {
                    Id = 2,
                    ItemPrice = 4m
                },
                new GoodsPriceDTO
                {
                    Id = 3,
                    ItemPrice = 5m
                }
            });
        }
    }
}