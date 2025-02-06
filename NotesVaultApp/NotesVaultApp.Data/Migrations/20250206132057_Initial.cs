using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NotesVaultApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<int>(type: "int", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("6412b356-57b0-4952-bc46-34920e67c73a"), 2 },
                    { new Guid("8dbc8abe-dac4-4203-ac82-6b70eaa95ab7"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedAt", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("6c1017d7-631b-471a-b6b6-21a83ca07ff9"), new Guid("8dbc8abe-dac4-4203-ac82-6b70eaa95ab7"), "This is the content of the first note", new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Local), "First Note", new DateTime(2025, 2, 7, 0, 0, 0, 0, DateTimeKind.Local) },
                    { new Guid("7639f555-2f90-44b0-9a2b-408c397f410c"), new Guid("6412b356-57b0-4952-bc46-34920e67c73a"), "This is the content of the second note", new DateTime(2025, 2, 6, 0, 0, 0, 0, DateTimeKind.Local), "Second Note", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_CategoryId",
                table: "Notes",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
