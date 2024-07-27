using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "06378d45-7eaf-4efa-ad4a-2549c94a3fea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "92766847-bace-4c6f-8747-5e321c7e2f93");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Departments");

            migrationBuilder.AddColumn<string>(
                name: "TelegramID",
                table: "Staffs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "93bad82f-d1b8-4031-ba00-80c6711a18a7", null, "Admin", "ADMIN" },
                    { "d6a6fa23-7c05-44a1-9b2f-aed71d094331", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93bad82f-d1b8-4031-ba00-80c6711a18a7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d6a6fa23-7c05-44a1-9b2f-aed71d094331");

            migrationBuilder.DropColumn(
                name: "TelegramID",
                table: "Staffs");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Departments",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "06378d45-7eaf-4efa-ad4a-2549c94a3fea", null, "Admin", "ADMIN" },
                    { "92766847-bace-4c6f-8747-5e321c7e2f93", null, "User", "USER" }
                });
        }
    }
}
