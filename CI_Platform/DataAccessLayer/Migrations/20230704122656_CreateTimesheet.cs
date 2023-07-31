using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class CreateTimesheet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "timesheets",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    volunteer_id = table.Column<long>(type: "bigint", nullable: false),
                    mission_id = table.Column<long>(type: "bigint", nullable: false),
                    time = table.Column<TimeSpan>(type: "time", nullable: true),
                    goal_achieved = table.Column<int>(type: "int", nullable: true),
                    date_volunteered = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false, defaultValue: 4),
                    created_on = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())"),
                    modified_on = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())"),
                    created_by = table.Column<long>(type: "bigint", nullable: false),
                    modified_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_timesheets", x => x.id);
                    table.ForeignKey(
                        name: "FK_timesheets_missions_mission_id",
                        column: x => x.mission_id,
                        principalTable: "missions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_timesheets_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_timesheets_users_modified_by",
                        column: x => x.modified_by,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_timesheets_volunteers_volunteer_id",
                        column: x => x.volunteer_id,
                        principalTable: "volunteers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_timesheets_created_by",
                table: "timesheets",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_timesheets_mission_id",
                table: "timesheets",
                column: "mission_id");

            migrationBuilder.CreateIndex(
                name: "IX_timesheets_modified_by",
                table: "timesheets",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "IX_timesheets_volunteer_id",
                table: "timesheets",
                column: "volunteer_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "timesheets");
        }
    }
}
