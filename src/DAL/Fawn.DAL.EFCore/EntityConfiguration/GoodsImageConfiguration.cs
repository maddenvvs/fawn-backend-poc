namespace Fawn.DAL.EFCore.EntityConfiguration
{
    using Fawn.DAL.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class GoodsImageConfiguration : IEntityTypeConfiguration<GoodsImageDTO>
    {
        public void Configure(EntityTypeBuilder<GoodsImageDTO> builder)
        {
            builder.ToTable("GoodsImages");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Image)
                .IsRequired()
                .HasColumnType("bytea");

            builder.HasOne(i => i.GoodsItem)
                .WithOne(g => g.Image)
                .HasForeignKey<GoodsImageDTO>(i => i.GoodsId)
                .IsRequired();
        }
    }
}