using Microsoft.EntityFrameworkCore;
using NotesVaultApp.Data.Models;
using NotesVaultApp.Service.Data.Interfaces;

namespace NotesVaultApp.Data
{
    public class NotesVaultDbContext : DbContext
    {
        private readonly IAuthService _authService; // Add this line

        public NotesVaultDbContext(DbContextOptions<NotesVaultDbContext> options, IAuthService authService) // Modify constructor
            : base(options)
        {
            _authService = authService; // Add this line
        }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configuration.ApplicationUserConfiguration(_authService)); // Modify this line
            modelBuilder.ApplyConfiguration(new Configuration.CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new Configuration.NoteConfiguration());
        }
    }
}
