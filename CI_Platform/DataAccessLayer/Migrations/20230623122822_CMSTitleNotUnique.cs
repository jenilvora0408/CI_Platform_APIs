using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class CMSTitleNotUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_cms_pages_title",
                table: "cms_pages");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 6, 23, 17, 58, 21, 589, DateTimeKind.Unspecified).AddTicks(8314), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2023, 6, 23, 17, 58, 21, 589, DateTimeKind.Unspecified).AddTicks(8332), new TimeSpan(0, 5, 30, 0, 0)) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 6, 23, 15, 57, 20, 86, DateTimeKind.Unspecified).AddTicks(3215), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2023, 6, 23, 15, 57, 20, 86, DateTimeKind.Unspecified).AddTicks(3232), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_cms_pages_title",
                table: "cms_pages",
                column: "title",
                unique: true);
        }
    }
}
