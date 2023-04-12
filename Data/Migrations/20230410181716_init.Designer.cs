﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230410181716_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Models.Personnel", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeleteDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("InsertDateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("UpdateDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Personnel", "prs");
                });

            modelBuilder.Entity("Models.Salary", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Allowance")
                        .HasPrecision(18)
                        .HasColumnType("decimal(18,0)");

                    b.Property<decimal>("BasicSalary")
                        .HasPrecision(18)
                        .HasColumnType("decimal(18,0)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeleteDateTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("FinalSalaryAmount")
                        .HasPrecision(18)
                        .HasColumnType("decimal(18,0)");

                    b.Property<Guid>("IdPersonnel")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("InsertDateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<decimal>("OverTimeAmount")
                        .HasPrecision(18)
                        .HasColumnType("decimal(18,0)");

                    b.Property<decimal>("Tax")
                        .HasPrecision(18)
                        .HasColumnType("decimal(18,0)");

                    b.Property<decimal>("Transportation")
                        .HasPrecision(18)
                        .HasColumnType("decimal(18,0)");

                    b.Property<DateTime>("UpdateDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("IdPersonnel");

                    b.ToTable("Salary", "acc");
                });

            modelBuilder.Entity("Models.Salary", b =>
                {
                    b.HasOne("Models.Personnel", "Personnel")
                        .WithMany("Salaries")
                        .HasForeignKey("IdPersonnel")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Personnel");
                });

            modelBuilder.Entity("Models.Personnel", b =>
                {
                    b.Navigation("Salaries");
                });
#pragma warning restore 612, 618
        }
    }
}
