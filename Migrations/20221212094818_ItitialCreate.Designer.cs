﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RunGroupClubWebApp.Data;

#nullable disable

namespace RunGroupClubWebApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221212094818_ItitialCreate")]
    partial class ItitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RunGroupClubWebApp.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("RunGroupClubWebApp.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AdressId")
                        .HasColumnType("int");

                    b.Property<int>("MileAge")
                        .HasColumnType("int");

                    b.Property<int>("Pace")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdressId");

                    b.ToTable("AppUser");
                });

            modelBuilder.Entity("RunGroupClubWebApp.Models.Club", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AdressId")
                        .HasColumnType("int");

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ClubCategory")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AdressId");

                    b.HasIndex("AppUserId");

                    b.ToTable("Clubs");
                });

            modelBuilder.Entity("RunGroupClubWebApp.Models.Race", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AdressId")
                        .HasColumnType("int");

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RaceCategory")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AdressId");

                    b.HasIndex("AppUserId");

                    b.ToTable("Races");
                });

            modelBuilder.Entity("RunGroupClubWebApp.Models.AppUser", b =>
                {
                    b.HasOne("RunGroupClubWebApp.Models.Address", "Adress")
                        .WithMany()
                        .HasForeignKey("AdressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Adress");
                });

            modelBuilder.Entity("RunGroupClubWebApp.Models.Club", b =>
                {
                    b.HasOne("RunGroupClubWebApp.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AdressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RunGroupClubWebApp.Models.AppUser", "AppUser")
                        .WithMany("clubs")
                        .HasForeignKey("AppUserId");

                    b.Navigation("Address");

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("RunGroupClubWebApp.Models.Race", b =>
                {
                    b.HasOne("RunGroupClubWebApp.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AdressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RunGroupClubWebApp.Models.AppUser", "AppUser")
                        .WithMany("races")
                        .HasForeignKey("AppUserId");

                    b.Navigation("Address");

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("RunGroupClubWebApp.Models.AppUser", b =>
                {
                    b.Navigation("clubs");

                    b.Navigation("races");
                });
#pragma warning restore 612, 618
        }
    }
}
