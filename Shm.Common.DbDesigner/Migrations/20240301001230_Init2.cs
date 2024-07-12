using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BridgeIntelligence.iCompass.Shm.Common.DbDesigner.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastAlertRuleFailureId",
                table: "SHM_AlertRules",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SHM_AlertRuleFailures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlertRuleId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsNotificationSentForFailure = table.Column<bool>(type: "bit", nullable: false),
                    IsNotificationSentForResolved = table.Column<bool>(type: "bit", nullable: false),
                    ToEmailAddresses = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHM_AlertRuleFailures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SHM_AlertRuleFailures_SHM_AlertRules_AlertRuleId",
                        column: x => x.AlertRuleId,
                        principalTable: "SHM_AlertRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SHM_SensorOutputSummary",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BridgeId = table.Column<int>(type: "int", nullable: false),
                    SensorOutputId = table.Column<int>(type: "int", nullable: false),
                    Minimum = table.Column<float>(type: "real", nullable: true),
                    Maximum = table.Column<float>(type: "real", nullable: true),
                    Average = table.Column<float>(type: "real", nullable: true),
                    Median = table.Column<float>(type: "real", nullable: true),
                    StandardDev = table.Column<float>(type: "real", nullable: true),
                    FirstQuartile = table.Column<float>(type: "real", nullable: true),
                    ThirdQuartile = table.Column<float>(type: "real", nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHM_SensorOutputSummary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SHM_SensorOutputSummary_SHM_Bridges_BridgeId",
                        column: x => x.BridgeId,
                        principalTable: "SHM_Bridges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SHM_SensorOutputSummary_SHM_SensorOutputs_SensorOutputId",
                        column: x => x.SensorOutputId,
                        principalTable: "SHM_SensorOutputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SHM_AlertRules_LastAlertRuleFailureId",
                table: "SHM_AlertRules",
                column: "LastAlertRuleFailureId",
                unique: true,
                filter: "[LastAlertRuleFailureId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SHM_AlertRuleFailures_AlertRuleId",
                table: "SHM_AlertRuleFailures",
                column: "AlertRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_SHM_SensorOutputSummary_BridgeId",
                table: "SHM_SensorOutputSummary",
                column: "BridgeId");

            migrationBuilder.CreateIndex(
                name: "IX_SHM_SensorOutputSummary_SensorOutputId",
                table: "SHM_SensorOutputSummary",
                column: "SensorOutputId");

            migrationBuilder.AddForeignKey(
                name: "FK_SHM_AlertRules_SHM_AlertRuleFailures_LastAlertRuleFailureId",
                table: "SHM_AlertRules",
                column: "LastAlertRuleFailureId",
                principalTable: "SHM_AlertRuleFailures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SHM_AlertRules_SHM_AlertRuleFailures_LastAlertRuleFailureId",
                table: "SHM_AlertRules");

            migrationBuilder.DropTable(
                name: "SHM_AlertRuleFailures");

            migrationBuilder.DropTable(
                name: "SHM_SensorOutputSummary");

            migrationBuilder.DropIndex(
                name: "IX_SHM_AlertRules_LastAlertRuleFailureId",
                table: "SHM_AlertRules");

            migrationBuilder.DropColumn(
                name: "LastAlertRuleFailureId",
                table: "SHM_AlertRules");
        }
    }
}
