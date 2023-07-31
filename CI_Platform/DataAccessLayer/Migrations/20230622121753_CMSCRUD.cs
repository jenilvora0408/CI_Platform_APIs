using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class CMSCRUD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cms_pages",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    slug = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    created_on = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())"),
                    modified_on = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())"),
                    created_by = table.Column<long>(type: "bigint", nullable: false),
                    modified_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cms_pages", x => x.id);
                    table.ForeignKey(
                        name: "FK_cms_pages_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_cms_pages_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 6, 22, 17, 47, 53, 367, DateTimeKind.Unspecified).AddTicks(1654), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2023, 6, 22, 17, 47, 53, 367, DateTimeKind.Unspecified).AddTicks(1673), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_cms_pages_created_by",
                table: "cms_pages",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_cms_pages_modified_by",
                table: "cms_pages",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "IX_cms_pages_title",
                table: "cms_pages",
                column: "title",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cms_pages");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 6, 21, 15, 6, 8, 320, DateTimeKind.Unspecified).AddTicks(8677), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2023, 6, 21, 15, 6, 8, 320, DateTimeKind.Unspecified).AddTicks(8698), new TimeSpan(0, 5, 30, 0, 0)) });
        }
    }
}
