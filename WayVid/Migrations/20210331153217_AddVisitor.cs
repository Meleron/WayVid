using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WayVid.Migrations
{
    public partial class AddVisitor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VisitorID",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Visitor",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitor", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_VisitorID",
                table: "AspNetUsers",
                column: "VisitorID",
                unique: true,
                filter: "[VisitorID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Visitor_VisitorID",
                table: "AspNetUsers",
                column: "VisitorID",
                principalTable: "Visitor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Visitor_VisitorID",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Visitor");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_VisitorID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VisitorID",
                table: "AspNetUsers");
        }
    }
}
