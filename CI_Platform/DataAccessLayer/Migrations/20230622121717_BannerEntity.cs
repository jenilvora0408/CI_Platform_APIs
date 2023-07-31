using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class BannerEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "banners",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    image = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    sort_order = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    created_on = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())"),
                    modified_on = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())"),
                    created_by = table.Column<long>(type: "bigint", nullable: false),
                    modified_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_banners", x => x.id);
                    table.ForeignKey(
                        name: "FK_banners_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_banners_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "banners",
                columns: new[] { "id", "created_by", "created_on", "description", "image", "modified_by", "modified_on", "sort_order", "status" },
                values: new object[] { 1, 1L, new DateTimeOffset(new DateTime(2023, 6, 22, 17, 47, 17, 495, DateTimeKind.Unspecified).AddTicks(3528), new TimeSpan(0, 5, 30, 0, 0)), "At [Organization Name], our mission is to promote a sustainable and greener future by actively participating in tree planting initiatives. We firmly believe that trees play a vital role in maintaining a healthy environment, combating climate change, and improving the overall well-being of our communities. Through our dedicated efforts, we aim to foster a culture of tree planting, nurture ecosystems, and inspire individuals to become stewards of the environment.\r\nOur mission begins with raising awareness about the importance of trees and their positive impact on the planet. We strive to educate individuals, schools, businesses, and local communities about the numerous benefits of trees, such as improving air quality, conserving water, providing habitat for wildlife, and reducing the carbon footprint. By highlighting the intrinsic value of trees, we aim to inspire a sense of responsibility and motivate people to actively participate in our tree planting initiatives.", "/assets/banner/CSR-initiative-stands-for-Coffee--and-Farmer-Equity.png", 1L, new DateTimeOffset(new DateTime(2023, 6, 22, 17, 47, 17, 495, DateTimeKind.Unspecified).AddTicks(3529), new TimeSpan(0, 5, 30, 0, 0)), 1, 1 });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 6, 22, 17, 47, 17, 495, DateTimeKind.Unspecified).AddTicks(3294), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2023, 6, 22, 17, 47, 17, 495, DateTimeKind.Unspecified).AddTicks(3317), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_banners_created_by",
                table: "banners",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_banners_modified_by",
                table: "banners",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "IX_banners_sort_order",
                table: "banners",
                column: "sort_order",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "banners");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 6, 21, 15, 6, 8, 320, DateTimeKind.Unspecified).AddTicks(8677), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2023, 6, 21, 15, 6, 8, 320, DateTimeKind.Unspecified).AddTicks(8698), new TimeSpan(0, 5, 30, 0, 0)) });
        }
    }
}
