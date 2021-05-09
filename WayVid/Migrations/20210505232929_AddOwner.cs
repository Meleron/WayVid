using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WayVid.Migrations
{
    public partial class AddOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OwnerID",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Owner",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owner", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_OwnerID",
                table: "AspNetUsers",
                column: "OwnerID",
                unique: true,
                filter: "[OwnerID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Owner_OwnerID",
                table: "AspNetUsers",
                column: "OwnerID",
                principalTable: "Owner",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Owner_OwnerID",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Owner");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_OwnerID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "AspNetUsers");
        }
    }
}
