using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppAss.Migrations
{
    public partial class AddConcurrencyTokenToMenuItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyToken",
                table: "Sides",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyToken",
                table: "Drinks",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyToken",
                table: "Desserts",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyToken",
                table: "Burgers",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyToken",
                table: "Sides");

            migrationBuilder.DropColumn(
                name: "ConcurrencyToken",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "ConcurrencyToken",
                table: "Desserts");

            migrationBuilder.DropColumn(
                name: "ConcurrencyToken",
                table: "Burgers");
        }
    }
}
