using Microsoft.EntityFrameworkCore.Migrations;

namespace sadalmelik_books.Migrations
{
    public partial class RemovedBookAuthorColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
