using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesVaultApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedAt",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Notes",
                keyColumns: new[] { "CategoryId", "UserId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedAt",
                value: "04-02-2025");

            migrationBuilder.UpdateData(
                table: "Notes",
                keyColumns: new[] { "CategoryId", "UserId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedAt",
                value: "05-02-2025");

            migrationBuilder.UpdateData(
                table: "Notes",
                keyColumns: new[] { "CategoryId", "UserId" },
                keyValues: new object[] { 3, 1 },
                column: "CreatedAt",
                value: "06-02-2025");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "hjr5Qup2sbDpHJZQPphQlQ==:hRWH22x1dUOsEWfhJym3GbR/J70mfTlKfMvd9QkmvUE=");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "uIoxwBljgW/sv4kFGdxqnQ==:ZedA9qMNufYt8B2u7pthHF5UwWiVFuh4jGaK7hFuZRo=");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Notes");

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
    }
}
