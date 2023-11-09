﻿// <auto-generated />
using DigitalGameStore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DigitalGameStore.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20231106123501_update")]
    partial class update
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("DB.Publisher", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Publisher");
                });

            modelBuilder.Entity("DigitalGameStore.DB.Game", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PublisherID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ReleaseDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Score")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("PublisherID");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("DigitalGameStore.DB.GameGenres", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("GameID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GenreID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("GameID");

                    b.HasIndex("GenreID");

                    b.ToTable("GameGenres");
                });

            modelBuilder.Entity("DigitalGameStore.DB.Genre", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("DigitalGameStore.DB.Interest", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("GameID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("GameID");

                    b.ToTable("Interest");
                });

            modelBuilder.Entity("DigitalGameStore.DB.Game", b =>
                {
                    b.HasOne("DB.Publisher", "Publisher")
                        .WithMany("Games")
                        .HasForeignKey("PublisherID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("DigitalGameStore.DB.GameGenres", b =>
                {
                    b.HasOne("DigitalGameStore.DB.Game", "Games")
                        .WithMany()
                        .HasForeignKey("GameID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DigitalGameStore.DB.Genre", "Genres")
                        .WithMany("GameGenres")
                        .HasForeignKey("GenreID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Games");

                    b.Navigation("Genres");
                });

            modelBuilder.Entity("DigitalGameStore.DB.Interest", b =>
                {
                    b.HasOne("DigitalGameStore.DB.Game", "Game")
                        .WithMany("Interests")
                        .HasForeignKey("GameID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("DB.Publisher", b =>
                {
                    b.Navigation("Games");
                });

            modelBuilder.Entity("DigitalGameStore.DB.Game", b =>
                {
                    b.Navigation("Interests");
                });

            modelBuilder.Entity("DigitalGameStore.DB.Genre", b =>
                {
                    b.Navigation("GameGenres");
                });
#pragma warning restore 612, 618
        }
    }
}
