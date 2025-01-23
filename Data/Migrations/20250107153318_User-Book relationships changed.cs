using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserBookrelationshipschanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_UserAccounts_UserAccountModelId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_UserAccounts_UserAccountModelId1",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "UserAccountModelId1",
                table: "Books",
                newName: "UserReservingId");

            migrationBuilder.RenameColumn(
                name: "UserAccountModelId",
                table: "Books",
                newName: "UserBorrowingId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_UserAccountModelId1",
                table: "Books",
                newName: "IX_Books_UserReservingId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_UserAccountModelId",
                table: "Books",
                newName: "IX_Books_UserBorrowingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_UserAccounts_UserBorrowingId",
                table: "Books",
                column: "UserBorrowingId",
                principalTable: "UserAccounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_UserAccounts_UserReservingId",
                table: "Books",
                column: "UserReservingId",
                principalTable: "UserAccounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_UserAccounts_UserBorrowingId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_UserAccounts_UserReservingId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "UserReservingId",
                table: "Books",
                newName: "UserAccountModelId1");

            migrationBuilder.RenameColumn(
                name: "UserBorrowingId",
                table: "Books",
                newName: "UserAccountModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_UserReservingId",
                table: "Books",
                newName: "IX_Books_UserAccountModelId1");

            migrationBuilder.RenameIndex(
                name: "IX_Books_UserBorrowingId",
                table: "Books",
                newName: "IX_Books_UserAccountModelId");

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
    }
}
