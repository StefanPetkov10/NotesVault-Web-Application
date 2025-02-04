﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NotesVaultApp.Data;

#nullable disable

namespace NotesVaultApp.Data.Migrations
{
    [DbContext(typeof(NotesVaultDbContext))]
    partial class NotesVaultDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NotesVaultApp.Data.Models.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "stefan08@gmail.com",
                            PasswordHash = "HxmrEmbtYqARyJOVhBb5uA==:Gzcw+0pM2TdUeFr23KV4+aEnrAORKnRx6Ve1GLjPx8g=",
                            Username = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Email = "user1@gmail.com",
                            PasswordHash = "4fFuHL06s4/6FS6RK9M9ig==:eLVxipIV/608uj1+i/0NaNbiX/3WBttSjPNp5Kz/zaM=",
                            Username = "user1"
                        });
                });

            modelBuilder.Entity("NotesVaultApp.Data.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = 1
                        },
                        new
                        {
                            Id = 2,
                            Name = 2
                        },
                        new
                        {
                            Id = 3,
                            Name = 3
                        });
                });

            modelBuilder.Entity("NotesVaultApp.Data.Models.Note", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CategoryId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Notes");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            UserId = 1,
                            Content = "This is the content of the first note",
                            Id = 1,
                            Title = "First Note"
                        },
                        new
                        {
                            CategoryId = 2,
                            UserId = 1,
                            Content = "This is the content of the second note",
                            Id = 2,
                            Title = "Second Note"
                        },
                        new
                        {
                            CategoryId = 3,
                            UserId = 1,
                            Content = "This is the content of the third note",
                            Id = 3,
                            Title = "Third Note"
                        });
                });

            modelBuilder.Entity("NotesVaultApp.Data.Models.Note", b =>
                {
                    b.HasOne("NotesVaultApp.Data.Models.Category", "Category")
                        .WithMany("Notes")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotesVaultApp.Data.Models.ApplicationUser", "User")
                        .WithMany("Notes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NotesVaultApp.Data.Models.ApplicationUser", b =>
                {
                    b.Navigation("Notes");
                });

            modelBuilder.Entity("NotesVaultApp.Data.Models.Category", b =>
                {
                    b.Navigation("Notes");
                });
#pragma warning restore 612, 618
        }
    }
}
