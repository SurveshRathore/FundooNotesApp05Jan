﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RepoLayer.Context;

#nullable disable

namespace RepoLayer.Migrations
{
    [DbContext(typeof(FundooDBContext))]
    [Migration("20230107040625_ReviewTable")]
    partial class ReviewTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RepoLayer.Entity.CollabTable", b =>
                {
                    b.Property<long>("CollabId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("CollabId"), 1L, 1);

                    b.Property<string>("CollabEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CollabModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("NoteId")
                        .HasColumnType("bigint");

                    b.Property<long>("userId")
                        .HasColumnType("bigint");

                    b.HasKey("CollabId");

                    b.HasIndex("NoteId");

                    b.HasIndex("userId");

                    b.ToTable("CollabTables");
                });

            modelBuilder.Entity("RepoLayer.Entity.LabelTable", b =>
                {
                    b.Property<long>("LabelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("LabelId"), 1L, 1);

                    b.Property<string>("LabelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("NoteId")
                        .HasColumnType("bigint");

                    b.Property<long>("userId")
                        .HasColumnType("bigint");

                    b.HasKey("LabelId");

                    b.HasIndex("NoteId");

                    b.HasIndex("userId");

                    b.ToTable("LabelTable");
                });

            modelBuilder.Entity("RepoLayer.Entity.NoteTable", b =>
                {
                    b.Property<long>("NoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("NoteId"), 1L, 1);

                    b.Property<string>("NoteColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NoteCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("NoteDesciption")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoteImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("NoteIsArchive")
                        .HasColumnType("bit");

                    b.Property<bool>("NoteIsPin")
                        .HasColumnType("bit");

                    b.Property<bool>("NoteIsTrash")
                        .HasColumnType("bit");

                    b.Property<DateTime>("NoteModified")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NoteReminder")
                        .HasColumnType("datetime2");

                    b.Property<string>("NoteTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("userId")
                        .HasColumnType("bigint");

                    b.HasKey("NoteId");

                    b.HasIndex("userId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("RepoLayer.Entity.ReviewTable", b =>
                {
                    b.Property<long>("ReviewID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ReviewID"), 1L, 1);

                    b.Property<string>("ReviewComment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReviewRating")
                        .HasColumnType("int");

                    b.HasKey("ReviewID");

                    b.ToTable("ReviewTable");
                });

            modelBuilder.Entity("RepoLayer.Entity.UserTable", b =>
                {
                    b.Property<long>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("userId"), 1L, 1);

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userId");

                    b.ToTable("UserTable");
                });

            modelBuilder.Entity("RepoLayer.Entity.CollabTable", b =>
                {
                    b.HasOne("RepoLayer.Entity.NoteTable", "noteTable")
                        .WithMany()
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RepoLayer.Entity.UserTable", "userTable")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("noteTable");

                    b.Navigation("userTable");
                });

            modelBuilder.Entity("RepoLayer.Entity.LabelTable", b =>
                {
                    b.HasOne("RepoLayer.Entity.NoteTable", "noteTable")
                        .WithMany()
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RepoLayer.Entity.UserTable", "userTable")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("noteTable");

                    b.Navigation("userTable");
                });

            modelBuilder.Entity("RepoLayer.Entity.NoteTable", b =>
                {
                    b.HasOne("RepoLayer.Entity.UserTable", "userTable")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("userTable");
                });
#pragma warning restore 612, 618
        }
    }
}
