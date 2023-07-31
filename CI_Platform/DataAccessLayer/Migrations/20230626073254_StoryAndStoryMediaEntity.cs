using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class StoryAndStoryMediaEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {   
            migrationBuilder.CreateTable(
                name: "stories",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    short_description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false, defaultValue: 3),
                    views = table.Column<long>(type: "bigint", nullable: false),
                    published_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    volunteer_id = table.Column<long>(type: "bigint", nullable: false),
                    mission_id = table.Column<long>(type: "bigint", nullable: false),
                    created_on = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())"),
                    modified_on = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())"),
                    created_by = table.Column<long>(type: "bigint", nullable: false),
                    modified_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stories", x => x.id);
                    table.ForeignKey(
                        name: "FK_stories_missions_mission_id",
                        column: x => x.mission_id,
                        principalTable: "missions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_stories_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_stories_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_stories_volunteers_volunteer_id",
                        column: x => x.volunteer_id,
                        principalTable: "volunteers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "story_media",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    story_id = table.Column<long>(type: "bigint", nullable: false),
                    path = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_story_media", x => x.id);
                    table.ForeignKey(
                        name: "FK_story_media_stories_story_id",
                        column: x => x.story_id,
                        principalTable: "stories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

           
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 6, 26, 13, 2, 54, 32, DateTimeKind.Unspecified).AddTicks(3991), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2023, 6, 26, 13, 2, 54, 32, DateTimeKind.Unspecified).AddTicks(4009), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_stories_created_by",
                table: "stories",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_stories_mission_id",
                table: "stories",
                column: "mission_id");

            migrationBuilder.CreateIndex(
                name: "IX_stories_modified_by",
                table: "stories",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "IX_stories_volunteer_id",
                table: "stories",
                column: "volunteer_id");

            migrationBuilder.CreateIndex(
                name: "IX_story_media_story_id",
                table: "story_media",
                column: "story_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "story_media");

            migrationBuilder.DropTable(
                name: "stories");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 6, 23, 17, 58, 21, 589, DateTimeKind.Unspecified).AddTicks(8314), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2023, 6, 23, 17, 58, 21, 589, DateTimeKind.Unspecified).AddTicks(8332), new TimeSpan(0, 5, 30, 0, 0)) });
        }
    }
}
