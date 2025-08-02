using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bookstore.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "books",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "varchar(256)", nullable: false),
                    price = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_books", x => x.id);
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "books",
                columns: new[] { "id", "price", "title" },
                values: new object[,]
                {
                    { new Guid("0b9b6ce9-9eb0-497b-90c7-2e5be345f139"), 675m, "Clean Architecture" },
                    { new Guid("19d8fb37-087f-45a3-b261-683261249c3f"), 450m, "Vertical Slice Architecture" },
                    { new Guid("9e9a1fa2-9c28-4404-bcba-f81c6f70264b"), 590m, "Onion Architecture" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "books",
                schema: "public");
        }
    }
}
