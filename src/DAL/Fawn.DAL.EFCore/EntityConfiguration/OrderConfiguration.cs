using System;
using Fawn.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fawn.DAL.EFCore.EntityConfiguration
{
    public class OrderConfiguration : IEntityTypeConfiguration<OrderDTO>
    {
        public void Configure(EntityTypeBuilder<OrderDTO> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.IsPaid);

            builder.HasData(new[]
            {
                new OrderDTO
                {
                    Id = 1,
                    Status = OrderStatus.New,
                    IsPaid = false,
                    CreatedDateTime = DateTime.UtcNow,
                    PickupDateTime = DateTime.UtcNow.AddMinutes(20),
                    CustomerId = null
                },
                new OrderDTO
                {
                    Id = 2,
                    Status = OrderStatus.InProgress,
                    IsPaid = true,
                    CreatedDateTime = DateTime.UtcNow,
                    PickupDateTime = DateTime.UtcNow.AddMinutes(10),
                    CustomerId = null
                }
            });
        }
    }
}