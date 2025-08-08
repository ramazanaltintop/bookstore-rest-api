using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookstore.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class UserDetailsRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "first_name",
                schema: "public",
                table: "users");

            migrationBuilder.DropColumn(
                name: "last_name",
                schema: "public",
                table: "users");

            migrationBuilder.CreateTable(
                name: "user_details",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "varchar(128)", nullable: false),
                    last_name = table.Column<string>(type: "varchar(128)", nullable: false),
                    age = table.Column<byte>(type: "smallint", nullable: true),
                    phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_details", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_details_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_user_details_user_id",
                schema: "public",
                table: "user_details",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_details",
                schema: "public");

            migrationBuilder.AddColumn<string>(
                name: "first_name",
                schema: "public",
                table: "users",
                type: "varchar(128)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "last_name",
                schema: "public",
                table: "users",
                type: "varchar(128)",
                nullable: false,
                defaultValue: "");
        }
    }
}
