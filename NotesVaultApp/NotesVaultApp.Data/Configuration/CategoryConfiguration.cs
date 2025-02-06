using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotesVaultApp.Data.Models;

namespace NotesVaultApp.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        private readonly SeedData _seedData;

        public CategoryConfiguration(SeedData seedData)
        {
            _seedData = seedData;
        }

        public void Configure(EntityTypeBuilder<Category> builder)
        {

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .HasMany(c => c.Notes)
                .WithOne(n => n.Category)
                .HasForeignKey(n => n.CategoryId);

            builder
                .HasData(_seedData.Category1,
                        _seedData.Category2);
        }
    }
}
