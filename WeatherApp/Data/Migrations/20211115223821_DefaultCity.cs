using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherApp.Data.Migrations
{
    public partial class DefaultCity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DefaultCity",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            /*
            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "DescriptionTemperature",
                columns: table => new
                {
                    DescriptionTemperatureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionTemperatureDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DescriptionTemperature", x => x.DescriptionTemperatureId);
                });

            migrationBuilder.CreateTable(
                name: "Temperature",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false),
                    DateTemperature = table.Column<DateTime>(type: "Date", nullable: false),
                    MinTemperature = table.Column<int>(type: "int", nullable: false),
                    MaxTemperature = table.Column<int>(type: "int", nullable: false),
                    DescriptionTemperature = table.Column<int>(type: "int", nullable: false),
                    WindTemperature = table.Column<int>(type: "int", nullable: false),
                    PrecipitationTemperature = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temperature", x => new { x.CityId, x.DateTemperature });
                });

            migrationBuilder.CreateTable(
                name: "UserImage",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserImage", x => x.Email);
                });
            */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "DescriptionTemperature");

            migrationBuilder.DropTable(
                name: "Temperature");

            migrationBuilder.DropTable(
                name: "UserImage");
            */

            migrationBuilder.DropColumn(
                name: "DefaultCity",
                table: "AspNetUsers");
        }
    }
}
