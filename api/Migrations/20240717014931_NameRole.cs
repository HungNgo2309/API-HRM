using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class NameRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93bad82f-d1b8-4031-ba00-80c6711a18a7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d6a6fa23-7c05-44a1-9b2f-aed71d094331");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Positions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b0e8fe8e-7daa-4ada-b456-ad0924977654", null, "Admin", "ADMIN" },
                    { "d6d76755-5bd0-4a63-936f-7d256950dcd8", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b0e8fe8e-7daa-4ada-b456-ad0924977654");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d6d76755-5bd0-4a63-936f-7d256950dcd8");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Positions");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "93bad82f-d1b8-4031-ba00-80c6711a18a7", null, "Admin", "ADMIN" },
                    { "d6a6fa23-7c05-44a1-9b2f-aed71d094331", null, "User", "USER" }
                });
        }
    }
}
