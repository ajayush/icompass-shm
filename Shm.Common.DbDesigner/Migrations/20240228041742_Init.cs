using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BridgeIntelligence.iCompass.Shm.Common.DbDesigner.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SHM_Bridges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BridgeAbbreviation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BridgeName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ItemCreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemLastModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateLastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHM_Bridges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SHM_Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFailedState = table.Column<bool>(type: "bit", nullable: false),
                    LastSuccessTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsWorker = table.Column<bool>(type: "bit", nullable: false),
                    PingUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    NotifiedForFailure = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHM_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SHM_TableStates",
                columns: table => new
                {
                    TableName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastProcessedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastRecNum = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHM_TableStates", x => x.TableName);
                });

            migrationBuilder.CreateTable(
                name: "SHM_Daqs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BridgeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    UpstreamDownstream = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Model = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    SpanOrPier = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    PingUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsFailedState = table.Column<bool>(type: "bit", nullable: false),
                    NotifiedForFailure = table.Column<bool>(type: "bit", nullable: false),
                    LastSuccessTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ItemCreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemLastModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateLastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHM_Daqs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SHM_Daqs_SHM_Bridges_BridgeId",
                        column: x => x.BridgeId,
                        principalTable: "SHM_Bridges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SHM_Sensors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DaqId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    AlternateName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Abbreviation = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    System = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Model = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    SpanOrPier = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    UpstreamDownstream = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Member = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Position = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ItemCreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemLastModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateLastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHM_Sensors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SHM_Sensors_SHM_Daqs_DaqId",
                        column: x => x.DaqId,
                        principalTable: "SHM_Daqs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SHM_SensorOutputs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SensorId = table.Column<int>(type: "int", nullable: false),
                    OutputType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    TableName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ColumnName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    SamplingFrequency = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    UpperBound = table.Column<float>(type: "real", nullable: true),
                    LowerBound = table.Column<float>(type: "real", nullable: true),
                    IsFailedState = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastSuccessTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ItemCreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemLastModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateLastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHM_SensorOutputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SHM_SensorOutputs_SHM_Sensors_SensorId",
                        column: x => x.SensorId,
                        principalTable: "SHM_Sensors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SHM_AlertRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SensorOutputId = table.Column<int>(type: "int", nullable: false),
                    ToEmailAddresses = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuleName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 4096, nullable: true),
                    ExpressionString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFailedState = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    AlertType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemCreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemLastModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateLastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHM_AlertRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SHM_AlertRules_SHM_SensorOutputs_SensorOutputId",
                        column: x => x.SensorOutputId,
                        principalTable: "SHM_SensorOutputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SHM_AlertRuleEmailNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlertRuleId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    To = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    DeliveryStatus = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHM_AlertRuleEmailNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SHM_AlertRuleEmailNotifications_SHM_AlertRules_AlertRuleId",
                        column: x => x.AlertRuleId,
                        principalTable: "SHM_AlertRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SHM_AlertRuleEmailNotifications_AlertRuleId",
                table: "SHM_AlertRuleEmailNotifications",
                column: "AlertRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_SHM_AlertRules_SensorOutputId",
                table: "SHM_AlertRules",
                column: "SensorOutputId");

            migrationBuilder.CreateIndex(
                name: "IX_SHM_Daqs_BridgeId",
                table: "SHM_Daqs",
                column: "BridgeId");

            migrationBuilder.CreateIndex(
                name: "IX_SHM_SensorOutputs_SensorId",
                table: "SHM_SensorOutputs",
                column: "SensorId");

            migrationBuilder.CreateIndex(
                name: "IX_SHM_Sensors_DaqId",
                table: "SHM_Sensors",
                column: "DaqId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SHM_AlertRuleEmailNotifications");

            migrationBuilder.DropTable(
                name: "SHM_Services");

            migrationBuilder.DropTable(
                name: "SHM_TableStates");

            migrationBuilder.DropTable(
                name: "SHM_AlertRules");

            migrationBuilder.DropTable(
                name: "SHM_SensorOutputs");

            migrationBuilder.DropTable(
                name: "SHM_Sensors");

            migrationBuilder.DropTable(
                name: "SHM_Daqs");

            migrationBuilder.DropTable(
                name: "SHM_Bridges");
        }
    }
}
