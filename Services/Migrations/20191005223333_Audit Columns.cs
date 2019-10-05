using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Services.Migrations
{
    public partial class AuditColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookmarks",
                keyColumn: "Id",
                keyValue: new Guid("9a188693-3cd2-4163-b833-6e67b29eea64"));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Post",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Post",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Post",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Post",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedBy",
                table: "Post",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Post",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Post",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Employee",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Employee",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Employee",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Employee",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedBy",
                table: "Employee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Employee",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Employee",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Bookmarks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Bookmarks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Bookmarks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Bookmarks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedBy",
                table: "Bookmarks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Bookmarks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Bookmarks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Bookmarks");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Bookmarks");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Bookmarks");

            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "Bookmarks");

            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                table: "Bookmarks");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "Bookmarks");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Bookmarks");

            migrationBuilder.InsertData(
                table: "Bookmarks",
                columns: new[] { "Id", "CreatedOn", "Description", "ImageURL", "IsDeleted", "Title" },
                values: new object[] { new Guid("9a188693-3cd2-4163-b833-6e67b29eea64"), new DateTime(2019, 10, 3, 14, 36, 44, 691, DateTimeKind.Utc).AddTicks(5734), "OG description of the URL", "https://example.com/sample.png", false, "OG Title of the URL" });
        }
    }
}
