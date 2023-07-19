using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedditClone.Server.Migrations
{
    public partial class uniqueHash3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HashId",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "HashId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_HashId",
                table: "Posts",
                column: "HashId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_HashId",
                table: "Comments",
                column: "HashId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posts_HashId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Comments_HashId",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "HashId",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "HashId",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
