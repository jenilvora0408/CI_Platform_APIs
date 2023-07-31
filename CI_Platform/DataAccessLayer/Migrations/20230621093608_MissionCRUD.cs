using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class MissionCRUD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "missions",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    short_description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    start_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    end_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    registration_deadline = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    total_seat = table.Column<int>(type: "int", nullable: true),
                    mission_type = table.Column<int>(type: "int", nullable: false),
                    organization_name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    organization_details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    availability = table.Column<int>(type: "int", nullable: true),
                    city_id = table.Column<long>(type: "bigint", nullable: false),
                    mission_theme_id = table.Column<long>(type: "bigint", nullable: false),
                    goal_objective_title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    goal_objective = table.Column<long>(type: "bigint", nullable: true),
                    goal_objective_achieved = table.Column<long>(type: "bigint", nullable: true),
                    thumbnail_url = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    created_on = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    modified_on = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: false),
                    modified_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_missions", x => x.id);
                    table.ForeignKey(
                        name: "FK_missions_cities_city_id",
                        column: x => x.city_id,
                        principalTable: "cities",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_missions_mission_themes_mission_theme_id",
                        column: x => x.mission_theme_id,
                        principalTable: "mission_themes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_missions_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_missions_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "mission_media",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    name = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mission_id = table.Column<long>(type: "bigint", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mission_media", x => x.id);
                    table.ForeignKey(
                        name: "FK_mission_media_missions_mission_id",
                        column: x => x.mission_id,
                        principalTable: "missions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mission_skills",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mission_id = table.Column<long>(type: "bigint", nullable: false),
                    skill_id = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mission_skills", x => x.id);
                    table.ForeignKey(
                        name: "FK_mission_skills_missions_mission_id",
                        column: x => x.mission_id,
                        principalTable: "missions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mission_skills_skills_skill_id",
                        column: x => x.skill_id,
                        principalTable: "skills",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 6, 21, 15, 6, 8, 320, DateTimeKind.Unspecified).AddTicks(8677), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2023, 6, 21, 15, 6, 8, 320, DateTimeKind.Unspecified).AddTicks(8698), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_mission_media_mission_id",
                table: "mission_media",
                column: "mission_id");

            migrationBuilder.CreateIndex(
                name: "IX_mission_skills_mission_id",
                table: "mission_skills",
                column: "mission_id");

            migrationBuilder.CreateIndex(
                name: "IX_mission_skills_skill_id",
                table: "mission_skills",
                column: "skill_id");

            migrationBuilder.CreateIndex(
                name: "IX_missions_city_id",
                table: "missions",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_missions_created_by",
                table: "missions",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_missions_mission_theme_id",
                table: "missions",
                column: "mission_theme_id");

            migrationBuilder.CreateIndex(
                name: "IX_missions_modified_by",
                table: "missions",
                column: "modified_by");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mission_media");

            migrationBuilder.DropTable(
                name: "mission_skills");

            migrationBuilder.DropTable(
                name: "missions");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 6, 21, 14, 18, 54, 845, DateTimeKind.Unspecified).AddTicks(7995), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2023, 6, 21, 14, 18, 54, 845, DateTimeKind.Unspecified).AddTicks(8016), new TimeSpan(0, 5, 30, 0, 0)) });
        }
    }
}
