using Microsoft.EntityFrameworkCore;
using NotesVaultApp.Data;
using NotesVaultApp.Data.Configuration;
using NotesVaultApp.Data.Models;
using NotesVaultApp.DTOs.Note_DTOs.Mappings;
using NotesVaultApp.Service.Data.Interfaces;
using NotesVaultApp.Web.Infrastucture.Extensions;

namespace NotesVaultApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("SQLConnection");

            builder.Services.AddDbContext<NotesVaultDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<SeedData>();
            //builder.Services.AddScoped<IRepository<Note>, BaseRepository<Note>>();
            builder.Services.RegisterRepositories(typeof(Note).Assembly);
            builder.Services.RegisterUserDefinedServices(typeof(INoteService).Assembly);
            builder.Services.AddAutoMapper(typeof(NoteProfile).Assembly);

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAuthorization();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                    });
            });

            var app = builder.Build();

            app.UseCors("AllowAllOrigins");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }

}