using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Api.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "soru_turu",
                table: "Questions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "soru_turu",
                table: "Questions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
