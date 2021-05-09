using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WayVid.Migrations
{
    public partial class AddOwnerEstablishmentEntityCols : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "OwnerEstablishment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedOn",
                table: "OwnerEstablishment",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "OwnerEstablishment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedOn",
                table: "OwnerEstablishment",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "OwnerEstablishment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "OwnerEstablishment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedOn",
                table: "OwnerEstablishment",
                type: "datetimeoffset",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "OwnerEstablishment");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "OwnerEstablishment");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "OwnerEstablishment");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "OwnerEstablishment");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "OwnerEstablishment");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "OwnerEstablishment");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "OwnerEstablishment");
        }
    }
}
