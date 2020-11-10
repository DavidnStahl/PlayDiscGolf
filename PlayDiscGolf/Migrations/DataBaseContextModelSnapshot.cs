﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlayDiscGolf.Models;

namespace PlayDiscGolf.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("PlayDiscGolf.Models.Course", b =>
                {
                    b.Property<int>("CourseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CourseID");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("PlayDiscGolf.Models.HoleCard", b =>
                {
                    b.Property<int>("HoleCardID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("HoleNumber")
                        .HasColumnType("int");

                    b.Property<int>("PlayerCardID")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.HasKey("HoleCardID");

                    b.HasIndex("PlayerCardID");

                    b.ToTable("HoleCards");
                });

            modelBuilder.Entity("PlayDiscGolf.Models.PlayerCard", b =>
                {
                    b.Property<int>("PlayerCardID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ScoreCardID")
                        .HasColumnType("int");

                    b.HasKey("PlayerCardID");

                    b.HasIndex("ScoreCardID");

                    b.ToTable("PlayerCards");
                });

            modelBuilder.Entity("PlayDiscGolf.Models.ScoreCard", b =>
                {
                    b.Property<int>("ScoreCardID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ScoreCardID");

                    b.HasIndex("CourseID");

                    b.ToTable("ScoreCards");
                });

            modelBuilder.Entity("PlayDiscGolf.Models.HoleCard", b =>
                {
                    b.HasOne("PlayDiscGolf.Models.PlayerCard", "PlayerCard")
                        .WithMany("HoleCards")
                        .HasForeignKey("PlayerCardID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlayerCard");
                });

            modelBuilder.Entity("PlayDiscGolf.Models.PlayerCard", b =>
                {
                    b.HasOne("PlayDiscGolf.Models.ScoreCard", "Scorecard")
                        .WithMany("PlayerCards")
                        .HasForeignKey("ScoreCardID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Scorecard");
                });

            modelBuilder.Entity("PlayDiscGolf.Models.ScoreCard", b =>
                {
                    b.HasOne("PlayDiscGolf.Models.Course", "Course")
                        .WithMany("ScoreCards")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("PlayDiscGolf.Models.Course", b =>
                {
                    b.Navigation("ScoreCards");
                });

            modelBuilder.Entity("PlayDiscGolf.Models.PlayerCard", b =>
                {
                    b.Navigation("HoleCards");
                });

            modelBuilder.Entity("PlayDiscGolf.Models.ScoreCard", b =>
                {
                    b.Navigation("PlayerCards");
                });
#pragma warning restore 612, 618
        }
    }
}
