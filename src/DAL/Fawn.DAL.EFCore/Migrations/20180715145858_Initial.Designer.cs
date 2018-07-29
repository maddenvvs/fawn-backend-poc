﻿// <auto-generated />
using System;
using Fawn.DAL.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Fawn.DAL.EFCore.Migrations
{
    [DbContext(typeof(FawnAppContext))]
    [Migration("20180715145858_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Fawn.DAL.Models.GoodsDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(4000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Goods");

                    b.HasData(
                        new { Id = 1, Description = "Tasty latte", Name = "Latte" },
                        new { Id = 2, Description = "Tasty milk for you!", Name = "Milk" },
                        new { Id = 3, Name = "Cappucino" }
                    );
                });

            modelBuilder.Entity("Fawn.DAL.Models.GoodsImageDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GoodsId");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasIndex("GoodsId")
                        .IsUnique();

                    b.ToTable("GoodsImages");
                });

            modelBuilder.Entity("Fawn.DAL.Models.GoodsPriceDTO", b =>
                {
                    b.Property<int>("Id");

                    b.Property<decimal>("ItemPrice")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.ToTable("GoodsPrice");

                    b.HasData(
                        new { Id = 1, ItemPrice = 10.5m },
                        new { Id = 2, ItemPrice = 4m },
                        new { Id = 3, ItemPrice = 5m }
                    );
                });

            modelBuilder.Entity("Fawn.DAL.Models.OrderDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<int?>("CustomerId");

                    b.Property<bool>("IsPaid");

                    b.Property<DateTime>("PickupDateTime");

                    b.Property<byte>("Status");

                    b.HasKey("Id");

                    b.ToTable("Orders");

                    b.HasData(
                        new { Id = 1, CreatedDateTime = new DateTime(2018, 7, 15, 14, 58, 58, 2, DateTimeKind.Utc), IsPaid = false, PickupDateTime = new DateTime(2018, 7, 15, 15, 18, 58, 2, DateTimeKind.Utc), Status = (byte)0 },
                        new { Id = 2, CreatedDateTime = new DateTime(2018, 7, 15, 14, 58, 58, 2, DateTimeKind.Utc), IsPaid = true, PickupDateTime = new DateTime(2018, 7, 15, 15, 8, 58, 2, DateTimeKind.Utc), Status = (byte)1 }
                    );
                });

            modelBuilder.Entity("Fawn.DAL.Models.OrderGoodsDTO", b =>
                {
                    b.Property<int>("OrderId");

                    b.Property<int>("GoodsId");

                    b.Property<int>("Amount");

                    b.Property<decimal>("ItemPrice")
                        .HasColumnType("money");

                    b.HasKey("OrderId", "GoodsId");

                    b.HasIndex("GoodsId");

                    b.ToTable("OrderGoods");

                    b.HasData(
                        new { OrderId = 1, GoodsId = 1, Amount = 2, ItemPrice = 5m },
                        new { OrderId = 1, GoodsId = 2, Amount = 1, ItemPrice = 4.5m },
                        new { OrderId = 2, GoodsId = 3, Amount = 3, ItemPrice = 7m }
                    );
                });

            modelBuilder.Entity("Fawn.DAL.Models.GoodsImageDTO", b =>
                {
                    b.HasOne("Fawn.DAL.Models.GoodsDTO", "GoodsItem")
                        .WithOne("Image")
                        .HasForeignKey("Fawn.DAL.Models.GoodsImageDTO", "GoodsId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fawn.DAL.Models.GoodsPriceDTO", b =>
                {
                    b.HasOne("Fawn.DAL.Models.GoodsDTO")
                        .WithOne("Price")
                        .HasForeignKey("Fawn.DAL.Models.GoodsPriceDTO", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fawn.DAL.Models.OrderGoodsDTO", b =>
                {
                    b.HasOne("Fawn.DAL.Models.GoodsDTO", "GoodsItem")
                        .WithMany()
                        .HasForeignKey("GoodsId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Fawn.DAL.Models.OrderDTO", "Order")
                        .WithMany("OrderGoods")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
