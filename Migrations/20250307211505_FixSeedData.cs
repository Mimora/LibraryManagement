using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class FixSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "Name" },
                values: new object[,]
                {
                    { 1, "Gene Kim" },
                    { 2, "Andy Weir" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Name" },
                values: new object[,]
                {
                    { 1, "Harry Potter" },
                    { 2, "Hermione Granger" }
                });

            migrationBuilder.InsertData(
                table: "LibraryBranches",
                columns: new[] { "LibraryBranchId", "BranchName" },
                values: new object[,]
                {
                    { 1, "VPL Downtown" },
                    { 2, "VPL Marpole" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "AuthorId", "Genre", "LibraryBranchId", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Novel", 1, "The Unicorn Project" },
                    { 2, 2, "Science Fiction", 2, "The Martian" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_LibraryBranchId",
                table: "Books",
                column: "LibraryBranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_LibraryBranches_LibraryBranchId",
                table: "Books",
                column: "LibraryBranchId",
                principalTable: "LibraryBranches",
                principalColumn: "LibraryBranchId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_LibraryBranches_LibraryBranchId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_LibraryBranchId",
                table: "Books");

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LibraryBranches",
                keyColumn: "LibraryBranchId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LibraryBranches",
                keyColumn: "LibraryBranchId",
                keyValue: 2);
        }
    }
}
