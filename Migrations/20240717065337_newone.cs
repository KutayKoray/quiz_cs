using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Api.Migrations
{
    /// <inheritdoc />
    public partial class newone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    soru_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    alan_bilgisi = table.Column<string>(type: "TEXT", nullable: false),
                    soru_turu = table.Column<string>(type: "TEXT", nullable: false),
                    soru_dersi = table.Column<string>(type: "TEXT", nullable: false),
                    dogru_cevap = table.Column<string>(type: "TEXT", nullable: false),
                    ImageFileName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.soru_id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Surname = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    is_student = table.Column<bool>(type: "INTEGER", nullable: false),
                    is_teacher = table.Column<bool>(type: "INTEGER", nullable: false),
                    Total_questions = table.Column<int>(type: "INTEGER", nullable: false),
                    Correct_answers = table.Column<int>(type: "INTEGER", nullable: false),
                    Wrong_answers = table.Column<int>(type: "INTEGER", nullable: false),
                    Wrong_questions_Ids = table.Column<string>(type: "TEXT", nullable: false),
                    Score = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
