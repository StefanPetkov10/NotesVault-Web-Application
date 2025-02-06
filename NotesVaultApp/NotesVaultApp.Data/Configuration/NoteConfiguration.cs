using Microsoft.EntityFrameworkCore;
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

            builder
                .HasOne(n => n.Category)
                .WithMany(c => c.Notes)
                .HasForeignKey(n => n.CategoryId);


            builder
                .HasData
                (
                    new Note
                    {
                        Id = 1,
                        Title = "First Note",
                        Content = "This is the content of the first note",
                        CreatedAt = DateTime.Today.AddDays(-1),
                        UpdatedAt = DateTime.Today.AddDays(1),
                        CategoryId = 1,
                    },
                    new Note
                    {
                        Id = 2,
                        Title = "Second Note",
                        Content = "This is the content of the second note",
                        CreatedAt = DateTime.Today,
                        CategoryId = 2,
                    },
                    new Note
                    {
                        Id = 3,
                        Title = "Third Note",
                        Content = "This is the content of the third note",
                        CreatedAt = DateTime.Today.AddDays(2),
                        CategoryId = 3,
                    }
                );
        }
    }
}
