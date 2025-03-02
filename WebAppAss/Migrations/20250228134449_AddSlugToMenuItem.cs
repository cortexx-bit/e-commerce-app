using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppAss.Migrations
{
    public partial class AddSlugToMenuItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_BasketItems_Baskets_BasketID",
            //    table: "BasketItems");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Sides",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Drinks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Desserts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompositeID",
                table: "CheckoutItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Burgers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Sides");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Desserts");

            migrationBuilder.DropColumn(
                name: "CompositeID",
                table: "CheckoutItems");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Burgers");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_BasketItems_Baskets_BasketID",
            //    table: "BasketItems",
            //    column: "BasketID",
            //    principalTable: "Baskets",
            //    principalColumn: "BasketID",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
