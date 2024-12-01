using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieProject.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class genreimg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Genres",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Genres");
        }
    }
}
