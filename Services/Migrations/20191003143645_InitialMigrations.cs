using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Services.Migrations
{
    public partial class InitialMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookmarks",
                keyColumn: "Id",
                keyValue: new Guid("dd10a082-780a-4e53-a8de-143996e623ea"));

            migrationBuilder.InsertData(
                table: "Bookmarks",
                columns: new[] { "Id", "CreatedOn", "Description", "ImageURL", "IsDeleted", "Title" },
                values: new object[] { new Guid("9a188693-3cd2-4163-b833-6e67b29eea64"), new DateTime(2019, 10, 3, 14, 36, 44, 691, DateTimeKind.Utc).AddTicks(5734), "OG description of the URL", "https://example.com/sample.png", false, "OG Title of the URL" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookmarks",
                keyColumn: "Id",
                keyValue: new Guid("9a188693-3cd2-4163-b833-6e67b29eea64"));

            migrationBuilder.InsertData(
                table: "Bookmarks",
                columns: new[] { "Id", "CreatedOn", "Description", "ImageURL", "IsDeleted", "Title" },
                values: new object[] { new Guid("dd10a082-780a-4e53-a8de-143996e623ea"), new DateTime(2019, 10, 3, 14, 27, 44, 411, DateTimeKind.Utc).AddTicks(7827), "OG description of the URL", "https://example.com/sample.png", false, "OG Title of the URL" });
        }
    }
}
