using Microsoft.EntityFrameworkCore;
using NotesVaultApp.Data.Configuration;
using NotesVaultApp.Data.Models;

namespace NotesVaultApp.Data
{
    public class NotesVaultDbContext : DbContext
    {
        private readonly SeedData _seedData;
        public NotesVaultDbContext(DbContextOptions<NotesVaultDbContext> options, SeedData seedData)
            : base(options)
        {
            _seedData = seedData;
        }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CategoryConfiguration(_seedData));
            modelBuilder.ApplyConfiguration(new NoteConfiguration(_seedData));
        }
    }
}
