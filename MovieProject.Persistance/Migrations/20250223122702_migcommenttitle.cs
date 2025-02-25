using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieProject.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class migcommenttitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommentTitle",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentTitle",
                table: "Comments");
        }
    }
}
