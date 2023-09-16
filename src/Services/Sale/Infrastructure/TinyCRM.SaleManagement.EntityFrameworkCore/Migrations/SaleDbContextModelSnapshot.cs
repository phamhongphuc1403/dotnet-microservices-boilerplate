﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TinyCRM.SaleManagement.EntityFrameworkCore;

#nullable disable

namespace TinyCRM.SaleManagement.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(SaleDbContext))]
    partial class SaleDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TinyCRM.SaleManagement.Domain.Entities.Deal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid>("LeadId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(1);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LeadId")
                        .IsUnique();

                    b.ToTable("Deals");
                });

            modelBuilder.Entity("TinyCRM.SaleManagement.Domain.Entities.DealLine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DealId")
                        .HasColumnType("uuid");

                    b.Property<double>("PricePerUnit")
                        .HasColumnType("double precision");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DealId");

                    b.HasIndex("ProductId");

                    b.ToTable("DealLines", (string)null);
                });

            modelBuilder.Entity("TinyCRM.SaleManagement.Domain.Entities.Lead", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("DisqualificationDescription")
                        .HasColumnType("text");

                    b.Property<int?>("DisqualificationReason")
                        .HasColumnType("integer");

                    b.Property<double?>("EstimatedRevenue")
                        .HasColumnType("double precision");

                    b.Property<int?>("Source")
                        .HasColumnType("integer");

                    b.Property<int?>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(1);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Leads");
                });

            modelBuilder.Entity("TinyCRM.SaleManagement.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Products");
                });

            modelBuilder.Entity("TinyCRM.SaleManagement.Domain.Entities.Deal", b =>
                {
                    b.HasOne("TinyCRM.SaleManagement.Domain.Entities.Lead", "Lead")
                        .WithOne("Deal")
                        .HasForeignKey("TinyCRM.SaleManagement.Domain.Entities.Deal", "LeadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lead");
                });

            modelBuilder.Entity("TinyCRM.SaleManagement.Domain.Entities.DealLine", b =>
                {
                    b.HasOne("TinyCRM.SaleManagement.Domain.Entities.Deal", "Deal")
                        .WithMany("DealLines")
                        .HasForeignKey("DealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TinyCRM.SaleManagement.Domain.Entities.Product", "Product")
                        .WithMany("DealLines")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deal");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("TinyCRM.SaleManagement.Domain.Entities.Deal", b =>
                {
                    b.Navigation("DealLines");
                });

            modelBuilder.Entity("TinyCRM.SaleManagement.Domain.Entities.Lead", b =>
                {
                    b.Navigation("Deal");
                });

            modelBuilder.Entity("TinyCRM.SaleManagement.Domain.Entities.Product", b =>
                {
                    b.Navigation("DealLines");
                });
#pragma warning restore 612, 618
        }
    }
}