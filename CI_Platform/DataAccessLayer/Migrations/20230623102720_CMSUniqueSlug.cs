using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class CMSUniqueSlug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 6, 23, 15, 57, 20, 86, DateTimeKind.Unspecified).AddTicks(3215), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2023, 6, 23, 15, 57, 20, 86, DateTimeKind.Unspecified).AddTicks(3232), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_cms_pages_slug",
                table: "cms_pages",
                column: "slug",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_cms_pages_slug",
                table: "cms_pages");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 6, 22, 17, 47, 53, 367, DateTimeKind.Unspecified).AddTicks(1654), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2023, 6, 22, 17, 47, 53, 367, DateTimeKind.Unspecified).AddTicks(1673), new TimeSpan(0, 5, 30, 0, 0)) });
        }
    }
}
