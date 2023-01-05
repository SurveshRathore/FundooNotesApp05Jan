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
    [Migration("20221229021400_Note")]
    partial class Note
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

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

                    b.ToTable("NoteTable");
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

            modelBuilder.Entity("RepoLayer.Entity.NoteTable", b =>
                {
                    b.HasOne("RepoLayer.Entity.UserTable", "UserTable")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserTable");
                });
#pragma warning restore 612, 618
        }
    }
}
