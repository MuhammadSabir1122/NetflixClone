using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetflixClone.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMyList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyLists_Users_UserId1",
                table: "MyLists");

            migrationBuilder.DropIndex(
                name: "IX_MyLists_UserId1",
                table: "MyLists");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "MyLists");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "MyLists",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_MyLists_UserId",
                table: "MyLists",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyLists_Users_UserId",
                table: "MyLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyLists_Users_UserId",
                table: "MyLists");

            migrationBuilder.DropIndex(
                name: "IX_MyLists_UserId",
                table: "MyLists");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "MyLists",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "MyLists",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyLists_UserId1",
                table: "MyLists",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MyLists_Users_UserId1",
                table: "MyLists",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
