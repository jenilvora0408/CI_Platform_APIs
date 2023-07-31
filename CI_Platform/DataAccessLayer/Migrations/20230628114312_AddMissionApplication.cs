using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class AddMissionApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mission_applications",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    applied_on = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    volunteer_id = table.Column<long>(type: "bigint", nullable: false),
                    mission_id = table.Column<long>(type: "bigint", nullable: false),
                    created_on = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    modified_on = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: false),
                    modified_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mission_applications", x => x.id);
                    table.ForeignKey(
                        name: "FK_mission_applications_missions_mission_id",
                        column: x => x.mission_id,
                        principalTable: "missions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mission_applications_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_mission_applications_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_mission_applications_volunteers_volunteer_id",
                        column: x => x.volunteer_id,
                        principalTable: "volunteers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mission_applications_created_by",
                table: "mission_applications",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_mission_applications_mission_id_volunteer_id",
                table: "mission_applications",
                columns: new[] { "mission_id", "volunteer_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_mission_applications_modified_by",
                table: "mission_applications",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "IX_mission_applications_volunteer_id",
                table: "mission_applications",
                column: "volunteer_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mission_applications");
        }
    }
}
