using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotesVaultApp.Data.Models;
using NotesVaultApp.Service.Data.Interfaces;

namespace NotesVaultApp.Data.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        private readonly IAuthService _authService;

        public ApplicationUserConfiguration(IAuthService authService)
        {
            _authService = authService;
        }

        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            var hasher = new PasswordHasher<ApplicationUser>();

            builder.
                HasData(
                new ApplicationUser
                {
                    //add the data here
                    Id = 1,
                    Username = "admin",
                    Email = "stefan08@gmail.com",
                    PasswordHash = _authService.HashPassword("admin")
                },
                new ApplicationUser
                {
                    //add the data here
                    Id = 2,
                    Username = "user1",
                    Email = "user1@gmail.com",
                    PasswordHash = _authService.HashPassword("user1")
                }
                );
        }
    }
}
