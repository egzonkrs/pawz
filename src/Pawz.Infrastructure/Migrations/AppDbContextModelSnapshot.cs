﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pawz.Infrastructure.Data;

#nullable disable

namespace Pawz.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("Pawz.Domain.Entities.Adoption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("AdoptionDate")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("AdoptionFee")
                        .HasColumnType("TEXT");

                    b.Property<int>("AdoptionRequestId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AdoptionRequestId")
                        .IsUnique();

                    b.ToTable("Adoptions");
                });

            modelBuilder.Entity("Pawz.Domain.Entities.AdoptionRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("PetId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ResponseDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PetId");

                    b.ToTable("AdoptionRequests");
                });

            modelBuilder.Entity("Pawz.Domain.Entities.Breed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("SpeciesId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SpeciesId");

                    b.ToTable("Breeds");
                });

            modelBuilder.Entity("Pawz.Domain.Entities.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .HasColumnType("TEXT");

                    b.Property<string>("PostalCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Pawz.Domain.Entities.Pet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("About")
                        .HasColumnType("TEXT");

                    b.Property<int>("AgeMonths")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AgeYears")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BreedId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("LocationId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<int>("SpeciesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BreedId");

                    b.HasIndex("LocationId");

                    b.HasIndex("SpeciesId");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("Pawz.Domain.Entities.PetImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsPrimary")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PetId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UploadedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PetId");

                    b.ToTable("PetImages");
                });

            modelBuilder.Entity("Pawz.Domain.Entities.Species", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Species");
                });

            modelBuilder.Entity("Pawz.Domain.Entities.Adoption", b =>
                {
                    b.HasOne("Pawz.Domain.Entities.AdoptionRequest", "AdoptionRequest")
                        .WithOne("Adoption")
                        .HasForeignKey("Pawz.Domain.Entities.Adoption", "AdoptionRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AdoptionRequest");
                });

            modelBuilder.Entity("Pawz.Domain.Entities.AdoptionRequest", b =>
                {
                    b.HasOne("Pawz.Domain.Entities.Pet", "Pet")
                        .WithMany("AdoptionRequests")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pet");
                });

            modelBuilder.Entity("Pawz.Domain.Entities.Breed", b =>
                {
                    b.HasOne("Pawz.Domain.Entities.Species", "Species")
                        .WithMany("Breeds")
                        .HasForeignKey("SpeciesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Species");
                });

            modelBuilder.Entity("Pawz.Domain.Entities.Pet", b =>
                {
                    b.HasOne("Pawz.Domain.Entities.Breed", "Breed")
                        .WithMany("Pets")
                        .HasForeignKey("BreedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pawz.Domain.Entities.Location", "Location")
                        .WithMany("Pets")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pawz.Domain.Entities.Species", "Species")
                        .WithMany("Pets")
                        .HasForeignKey("SpeciesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Breed");

                    b.Navigation("Location");

                    b.Navigation("Species");
                });

            modelBuilder.Entity("Pawz.Domain.Entities.PetImage", b =>
                {
                    b.HasOne("Pawz.Domain.Entities.Pet", "Pet")
                        .WithMany("PetImages")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pet");
                });

            modelBuilder.Entity("Pawz.Domain.Entities.AdoptionRequest", b =>
                {
                    b.Navigation("Adoption");
                });

            modelBuilder.Entity("Pawz.Domain.Entities.Breed", b =>
                {
                    b.Navigation("Pets");
                });

            modelBuilder.Entity("Pawz.Domain.Entities.Location", b =>
                {
                    b.Navigation("Pets");
                });

            modelBuilder.Entity("Pawz.Domain.Entities.Pet", b =>
                {
                    b.Navigation("AdoptionRequests");

                    b.Navigation("PetImages");
                });

            modelBuilder.Entity("Pawz.Domain.Entities.Species", b =>
                {
                    b.Navigation("Breeds");

                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}
