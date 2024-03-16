using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookReaderAPI.Migrations
{
    /// <inheritdoc />
    public partial class AuthorColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "BookDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "BookDetails");
        }
    }
}
