using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WayVid.Migrations
{
    public partial class AddVisitLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VisitLogItem",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ExitedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    VisitorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstablishmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitLogItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VisitLogItem_Establishment_EstablishmentID",
                        column: x => x.EstablishmentID,
                        principalTable: "Establishment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisitLogItem_Visitor_VisitorID",
                        column: x => x.VisitorID,
                        principalTable: "Visitor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisitLogItem_EstablishmentID",
                table: "VisitLogItem",
                column: "EstablishmentID");

            migrationBuilder.CreateIndex(
                name: "IX_VisitLogItem_VisitorID",
                table: "VisitLogItem",
                column: "VisitorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitLogItem");
        }
    }
}
