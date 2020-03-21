using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RentalCar.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auto",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarMake = table.Column<string>(nullable: false),
                    Model = table.Column<string>(nullable: false),
                    Issue = table.Column<DateTime>(nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    FuelConsuption = table.Column<double>(nullable: false),
                    EngineType = table.Column<string>(nullable: true),
                    EngineCapacity = table.Column<double>(nullable: false),
                    TransmissionType = table.Column<string>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    Aviablity = table.Column<bool>(nullable: false),
                    Mileage = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auto", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Passport = table.Column<string>(nullable: false),
                    DriveLisence = table.Column<string>(nullable: false),
                    BirthdayDate = table.Column<DateTime>(nullable: false),
                    RentalJoinDate = table.Column<DateTime>(nullable: false),
                    DriverPicturePath = table.Column<string>(nullable: true),
                    DistanceTraveled = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    AutoModelID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pictures_Auto_AutoModelID",
                        column: x => x.AutoModelID,
                        principalTable: "Auto",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AutoID = table.Column<int>(nullable: false),
                    DriverID = table.Column<int>(nullable: false),
                    OrderStartDate = table.Column<DateTime>(nullable: false),
                    OrderEndDate = table.Column<DateTime>(nullable: true),
                    OrderMilleage = table.Column<int>(nullable: true),
                    OrderDayCount = table.Column<DateTime>(nullable: true),
                    TotalPrice = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Orders_Auto_AutoID",
                        column: x => x.AutoID,
                        principalTable: "Auto",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Drivers_DriverID",
                        column: x => x.DriverID,
                        principalTable: "Drivers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AutoID",
                table: "Orders",
                column: "AutoID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DriverID",
                table: "Orders",
                column: "DriverID");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_AutoModelID",
                table: "Pictures",
                column: "AutoModelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Auto");
        }
    }
}
