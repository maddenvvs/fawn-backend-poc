namespace Fawn.DAL.EFCore.EntityConfiguration
{
    using Fawn.DAL.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class GoodsConfiguration : IEntityTypeConfiguration<GoodsDTO>
    {
        public void Configure(EntityTypeBuilder<GoodsDTO> builder)
        {
            builder.ToTable("Goods");

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(g => g.Description)
                .HasMaxLength(4000);

            builder.HasOne(g => g.Price)
                .WithOne()
                .HasForeignKey<GoodsPriceDTO>(p => p.Id)
                .IsRequired();

            builder.HasData(new[] {
                new GoodsDTO {
                    Id = 1,
                    Name = "Latte",
                    Description = "Tasty latte"
                },
                new GoodsDTO {
                    Id = 2,
                    Name = "Milk",
                    Description = "Tasty milk for you!"
                },
                new GoodsDTO {
                    Id = 3,
                    Name = "Cappucino",
                    Description = null
                }
            });
        }
    }
}