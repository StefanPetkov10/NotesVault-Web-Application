﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotesVaultApp.Data.Models;

namespace NotesVaultApp.Data.Configuration
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(n => n.Id);

            builder.Property(n => n.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(n => n.Content)
                .IsRequired();

            builder.HasKey(n => new { n.CategoryId, n.UserId });

            builder
                .HasOne(n => n.Category)
                .WithMany(c => c.Notes)
                .HasForeignKey(n => n.CategoryId);

            builder
                .HasOne(n => n.User)
                .WithMany(u => u.Notes)
                .HasForeignKey(n => n.UserId);

            builder
                .HasData
                (
                    new Note
                    {
                        Id = 1,
                        Title = "First Note",
                        Content = "This is the content of the first note",
                        CategoryId = 1,
                        UserId = 1
                    },
                    new Note
                    {
                        Id = 2,
                        Title = "Second Note",
                        Content = "This is the content of the second note",
                        CategoryId = 2,
                        UserId = 1
                    },
                    new Note
                    {
                        Id = 3,
                        Title = "Third Note",
                        Content = "This is the content of the third note",
                        CategoryId = 3,
                        UserId = 1
                    }
                );
        }
    }
}
