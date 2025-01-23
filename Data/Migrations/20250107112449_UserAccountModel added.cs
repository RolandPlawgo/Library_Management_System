using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserAccountModeladded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserAccountModelId",
                table: "Books",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserAccountModelId1",
                table: "Books",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserAccounts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccounts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_UserAccountModelId",
                table: "Books",
                column: "UserAccountModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_UserAccountModelId1",
                table: "Books",
                column: "UserAccountModelId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_UserAccounts_UserAccountModelId",
                table: "Books",
                column: "UserAccountModelId",
                principalTable: "UserAccounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_UserAccounts_UserAccountModelId1",
                table: "Books",
                column: "UserAccountModelId1",
                principalTable: "UserAccounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_UserAccounts_UserAccountModelId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_UserAccounts_UserAccountModelId1",
                table: "Books");

            migrationBuilder.DropTable(
                name: "UserAccounts");

            migrationBuilder.DropIndex(
                name: "IX_Books_UserAccountModelId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_UserAccountModelId1",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "UserAccountModelId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "UserAccountModelId1",
                table: "Books");
        }
    }
}
