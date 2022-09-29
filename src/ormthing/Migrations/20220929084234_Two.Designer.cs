﻿// <auto-generated />
using System;
using DBOpdracht;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ormthing.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220929084234_Two")]
    partial class Two
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DBOpdracht.Attractie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("reserveringId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("reserveringId");

                    b.ToTable("Attracties", (string)null);
                });

            modelBuilder.Entity("DBOpdracht.GastInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.HasKey("Id");

                    b.ToTable("GuestInfo");
                });

            modelBuilder.Entity("DBOpdracht.Gebruiker", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Email");

                    b.ToTable("Gebruikers", (string)null);
                });

            modelBuilder.Entity("DBOpdracht.Onderhoud", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Onderhoud_taken", (string)null);
                });

            modelBuilder.Entity("DBOpdracht.Reservering", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("GastId")
                        .HasColumnType("int");

                    b.Property<string>("gastEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("gastEmail");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("DBOpdracht.Gast", b =>
                {
                    b.HasBaseType("DBOpdracht.Gebruiker");

                    b.Property<string>("BegeleiderEmail")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Credits")
                        .HasColumnType("int");

                    b.Property<DateTime>("EersteBezoek")
                        .HasColumnType("datetime2");

                    b.Property<int?>("FavorieteAttractieId")
                        .HasColumnType("int");

                    b.Property<int>("GastinfoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("GeboorteDatum")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasIndex("BegeleiderEmail")
                        .IsUnique()
                        .HasFilter("[BegeleiderEmail] IS NOT NULL");

                    b.HasIndex("FavorieteAttractieId");

                    b.HasIndex("GastinfoId")
                        .IsUnique()
                        .HasFilter("[GastinfoId] IS NOT NULL");

                    b.ToTable("Gasten", (string)null);
                });

            modelBuilder.Entity("DBOpdracht.Medewerker", b =>
                {
                    b.HasBaseType("DBOpdracht.Gebruiker");

                    b.ToTable("Medewerkers", (string)null);
                });

            modelBuilder.Entity("DBOpdracht.Attractie", b =>
                {
                    b.HasOne("DBOpdracht.Reservering", "reservering")
                        .WithMany("ReservedAttractions")
                        .HasForeignKey("reserveringId");

                    b.Navigation("reservering");
                });

            modelBuilder.Entity("DBOpdracht.GastInfo", b =>
                {
                    b.OwnsOne("DBOpdracht.Coordinate", "coordinate", b1 =>
                        {
                            b1.Property<int>("GastInfoId")
                                .HasColumnType("int");

                            b1.HasKey("GastInfoId");

                            b1.ToTable("GuestInfo");

                            b1.WithOwner()
                                .HasForeignKey("GastInfoId");
                        });

                    b.Navigation("coordinate");
                });

            modelBuilder.Entity("DBOpdracht.Onderhoud", b =>
                {
                    b.HasOne("DBOpdracht.Attractie", "Target")
                        .WithMany("OnderhoudPunten")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Target");
                });

            modelBuilder.Entity("DBOpdracht.Reservering", b =>
                {
                    b.HasOne("DBOpdracht.Gast", "gast")
                        .WithMany("reservering")
                        .HasForeignKey("gastEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("DBOpdracht.DateTimeBereik", "VindtPlaatsTijdens", b1 =>
                        {
                            b1.Property<int>("ReserveringId")
                                .HasColumnType("int");

                            b1.HasKey("ReserveringId");

                            b1.ToTable("Reservations");

                            b1.WithOwner()
                                .HasForeignKey("ReserveringId");
                        });

                    b.Navigation("VindtPlaatsTijdens")
                        .IsRequired();

                    b.Navigation("gast");
                });

            modelBuilder.Entity("DBOpdracht.Gast", b =>
                {
                    b.HasOne("DBOpdracht.Gast", "Begeleider")
                        .WithOne("Begeleid")
                        .HasForeignKey("DBOpdracht.Gast", "BegeleiderEmail");

                    b.HasOne("DBOpdracht.Gebruiker", null)
                        .WithOne()
                        .HasForeignKey("DBOpdracht.Gast", "Email")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("DBOpdracht.Attractie", "FavorieteAttractie")
                        .WithMany()
                        .HasForeignKey("FavorieteAttractieId");

                    b.HasOne("DBOpdracht.GastInfo", "GastInformatie")
                        .WithOne("Gast")
                        .HasForeignKey("DBOpdracht.Gast", "GastinfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Begeleider");

                    b.Navigation("FavorieteAttractie");

                    b.Navigation("GastInformatie");
                });

            modelBuilder.Entity("DBOpdracht.Medewerker", b =>
                {
                    b.HasOne("DBOpdracht.Gebruiker", null)
                        .WithOne()
                        .HasForeignKey("DBOpdracht.Medewerker", "Email")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DBOpdracht.Attractie", b =>
                {
                    b.Navigation("OnderhoudPunten");
                });

            modelBuilder.Entity("DBOpdracht.GastInfo", b =>
                {
                    b.Navigation("Gast");
                });

            modelBuilder.Entity("DBOpdracht.Reservering", b =>
                {
                    b.Navigation("ReservedAttractions");
                });

            modelBuilder.Entity("DBOpdracht.Gast", b =>
                {
                    b.Navigation("Begeleid");

                    b.Navigation("reservering");
                });
#pragma warning restore 612, 618
        }
    }
}
