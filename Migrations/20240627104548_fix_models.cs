using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelWebApi.Migrations
{
    /// <inheritdoc />
    public partial class fix_models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMainAuthor",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Authors");

            migrationBuilder.AddColumn<int>(
                name: "MainAuthorId",
                table: "Books",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainAuthorId",
                table: "Books");

            migrationBuilder.AddColumn<bool>(
                name: "IsMainAuthor",
                table: "Authors",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Authors",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
