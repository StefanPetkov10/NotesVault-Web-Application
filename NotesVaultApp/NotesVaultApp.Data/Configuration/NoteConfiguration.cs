using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotesVaultApp.Data.Models;

namespace NotesVaultApp.Data.Configuration
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        private readonly SeedData _seedData;

        public NoteConfiguration(SeedData seedData)
        {
            _seedData = seedData;
        }

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
                .HasData(_seedData.Note1,
                        _seedData.Note2);

        }
    }
}
