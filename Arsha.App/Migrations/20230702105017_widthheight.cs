using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arsha.App.Migrations
{
    public partial class widthheight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ImageHeight",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ImageWidth",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageHeight",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImageWidth",
                table: "Products");
        }
    }
}
