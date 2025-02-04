using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesVaultApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditPass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "HxmrEmbtYqARyJOVhBb5uA==:Gzcw+0pM2TdUeFr23KV4+aEnrAORKnRx6Ve1GLjPx8g=");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "4fFuHL06s4/6FS6RK9M9ig==:eLVxipIV/608uj1+i/0NaNbiX/3WBttSjPNp5Kz/zaM=");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEC+MDsyxYvdzhZcKyZPIRhy97VFw+67R+nw6vY3rhBN09KAsZxscAHutFPNH6Bdu/A==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEAKycxXonGJFq5PZ01pUmd7G0UJduUuNjE3Csk9XOfCEjiRJRhJZxSJSX4N0lLYnWg==");
        }
    }
}
