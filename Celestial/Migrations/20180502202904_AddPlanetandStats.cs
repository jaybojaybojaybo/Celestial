using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Celestial.Migrations
{
    public partial class AddPlanetandStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Planets",
                columns: table => new
                {
                    PlanetId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Name = table.Column<string>(nullable: true),
                    StatId = table.Column<int>(nullable: false),
                    economy = table.Column<string>(nullable: true),
                    geography = table.Column<string>(nullable: true),
                    government = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planets", x => x.PlanetId);
                });

            migrationBuilder.CreateTable(
                name: "Stats",
                columns: table => new
                {
                    StatId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    PlanetId = table.Column<int>(nullable: false),
                    capital = table.Column<int>(nullable: false),
                    crystal = table.Column<int>(nullable: false),
                    pop = table.Column<int>(nullable: false),
                    stability = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stats", x => x.StatId);
                    table.ForeignKey(
                        name: "FK_Stats_Planets_PlanetId",
                        column: x => x.PlanetId,
                        principalTable: "Planets",
                        principalColumn: "PlanetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stats_PlanetId",
                table: "Stats",
                column: "PlanetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stats");

            migrationBuilder.DropTable(
                name: "Planets");
        }
    }
}
