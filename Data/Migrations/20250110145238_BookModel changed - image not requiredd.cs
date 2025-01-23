using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class BookModelchangedimagenotrequiredd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "Books",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Availability", "Description", "Image", "Publisher", "Title", "UserBorrowingId", "UserReservingId" },
                values: new object[,]
                {
                    { 1, "Morgan Housel", true, "Doing well with money isn’t necessarily about what you know. It’s about how you behave. And behavior is hard to teach, even to really smart people.\r\nMoney—investing, personal finance, and business decisions—is typically taught as a math-based field, where data and formulas tell us exactly what to do. But in the real world people don’t make financial decisions on a spreadsheet. They make them at the dinner table, or in a meeting room, where personal history, your own unique view of the world, ego, pride, marketing, and odd incentives are scrambled together.\r\nIn The Psychology of Money, award-winning author Morgan Housel shares 19 short stories exploring the strange ways people think about money and teaches you how to make better sense of one of life’s most important topics.", null, "Harriman House", "Psychology of Money", null, null },
                    { 2, "Daniel Kahneman", true, "The New York Times Bestseller, acclaimed by author such as Freakonomics co- author Steven D. Levitt, Black Swan author Nassim Nicholas Taleb and Nudge co- author Richard Thaler, Thinking Fast and Slow offers a whole new look at the way our minds work, and how we make decisions.  Why is there more chance we'll believe something if it's in a bold type face? Why are judges more likely to deny parole before lunch? Why do we assume a good-looking person will be more competent?  The answer lies in the two ways we make choices: fast, intuitive thinking, and slow, rational thinking. This book reveals how our minds are tripped up by error and prejudice (even when we think we are being logical), and gives you practical techniques for slower, smarter thinking. It will enable to you make better decisions at work, at home, and in everything you do.", null, "Penguin Books 2012", "Thinking Fast and Slow", null, null },
                    { 3, "Daniel Kahneman, Oliver Sibony, Cass R. Sunstein", true, "THE INTERNATIONAL BESTSELLER. A monumental, gripping book ... Outstanding SUNDAY TIMES", null, "Harpercollins Publishers", "Noise: A Flaw in Human Judgement", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "Books",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);
        }
    }
}
