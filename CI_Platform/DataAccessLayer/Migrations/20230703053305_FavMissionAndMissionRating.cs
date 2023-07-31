using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class FavMissionAndMissionRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "avatar",
                table: "users",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValueSql: "'/assets/avatar/profile.png'",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldDefaultValueSql: "'/wwwroot/assets/avatar/profile.png'");

            migrationBuilder.CreateTable(
                name: "favourite_missions",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mission_id = table.Column<long>(type: "bigint", nullable: false),
                    is_favourite = table.Column<bool>(type: "bit", nullable: false),
                    volunteer_id = table.Column<long>(type: "bigint", nullable: false),
                    created_on = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())"),
                    modified_on = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_favourite_missions", x => x.id);
                    table.ForeignKey(
                        name: "FK_favourite_missions_missions_mission_id",
                        column: x => x.mission_id,
                        principalTable: "missions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_favourite_missions_volunteers_volunteer_id",
                        column: x => x.volunteer_id,
                        principalTable: "volunteers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mission_ratings",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mission_id = table.Column<long>(type: "bigint", nullable: false),
                    rating = table.Column<int>(type: "int", nullable: false),
                    volunteer_id = table.Column<long>(type: "bigint", nullable: false),
                    created_on = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())"),
                    modified_on = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mission_ratings", x => x.id);
                    table.ForeignKey(
                        name: "FK_mission_ratings_missions_mission_id",
                        column: x => x.mission_id,
                        principalTable: "missions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mission_ratings_volunteers_volunteer_id",
                        column: x => x.volunteer_id,
                        principalTable: "volunteers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "banners",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 7, 3, 11, 3, 4, 616, DateTimeKind.Unspecified).AddTicks(332), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2023, 7, 3, 11, 3, 4, 616, DateTimeKind.Unspecified).AddTicks(350), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 7, 3, 11, 3, 4, 616, DateTimeKind.Unspecified).AddTicks(102), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2023, 7, 3, 11, 3, 4, 616, DateTimeKind.Unspecified).AddTicks(121), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_favourite_missions_mission_id_volunteer_id",
                table: "favourite_missions",
                columns: new[] { "mission_id", "volunteer_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_favourite_missions_volunteer_id",
                table: "favourite_missions",
                column: "volunteer_id");

            migrationBuilder.CreateIndex(
                name: "IX_mission_ratings_mission_id_volunteer_id",
                table: "mission_ratings",
                columns: new[] { "mission_id", "volunteer_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_mission_ratings_volunteer_id",
                table: "mission_ratings",
                column: "volunteer_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "favourite_missions");

            migrationBuilder.DropTable(
                name: "mission_ratings");

            migrationBuilder.AlterColumn<string>(
                name: "avatar",
                table: "users",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValueSql: "'/wwwroot/assets/avatar/profile.png'",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldDefaultValueSql: "'/assets/avatar/profile.png'");

            migrationBuilder.UpdateData(
                table: "banners",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 6, 26, 13, 2, 54, 32, DateTimeKind.Unspecified).AddTicks(4154), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2023, 6, 26, 13, 2, 54, 32, DateTimeKind.Unspecified).AddTicks(4155), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 6, 26, 13, 2, 54, 32, DateTimeKind.Unspecified).AddTicks(3991), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2023, 6, 26, 13, 2, 54, 32, DateTimeKind.Unspecified).AddTicks(4009), new TimeSpan(0, 5, 30, 0, 0)) });
        }
    }
}
