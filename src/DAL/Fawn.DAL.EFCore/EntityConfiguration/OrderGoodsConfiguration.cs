using Fawn.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fawn.DAL.EFCore.EntityConfiguration
{
    public class OrderGoodsConfiguration : IEntityTypeConfiguration<OrderGoodsDTO>
    {
        public void Configure(EntityTypeBuilder<OrderGoodsDTO> builder)
        {
            builder.ToTable("OrderGoods");

            builder.HasKey(og => new { og.OrderId, og.GoodsId });

            builder.Property(og => og.ItemPrice)
                .HasColumnType("money")
                .IsRequired();

            builder.Property(og => og.Amount)
                .IsRequired();

            builder.HasOne(og => og.Order)
                .WithMany(o => o.OrderGoods)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(og => og.OrderId);

            builder.HasOne(og => og.GoodsItem)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(og => og.GoodsId);

            builder.HasData(new[]
            {
                new OrderGoodsDTO
                {
                    OrderId = 1,
                    GoodsId = 1,
                    Amount = 2,
                    ItemPrice = 5m
                },
                new OrderGoodsDTO
                {
                    OrderId = 1,
                    GoodsId = 2,
                    Amount = 1,
                    ItemPrice = 4.5m
                },
                new OrderGoodsDTO
                {
                    OrderId = 2,
                    GoodsId = 3,
                    Amount = 3,
                    ItemPrice = 7m
                }
            });
        }
    }
}