using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AndrewSMoroz.Data.Migrations
{
    public partial class CreateExploreSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "explore");

            migrationBuilder.CreateTable(
                name: "Map",
                schema: "explore",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Map", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                schema: "explore",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "varchar(1000)", nullable: true),
                    IsInitialLocation = table.Column<bool>(nullable: false, defaultValueSql: "0"),
                    MapID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Location_MapID",
                        column: x => x.MapID,
                        principalSchema: "explore",
                        principalTable: "Map",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MapSessionSave",
                schema: "explore",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MapID = table.Column<int>(nullable: false),
                    SaveData = table.Column<string>(type: "varchar(max)", nullable: true),
                    SaveDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "SYSDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapSessionSave", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MapSessionSave_MapID",
                        column: x => x.MapID,
                        principalSchema: "explore",
                        principalTable: "Map",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                schema: "explore",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "varchar(500)", nullable: true),
                    Determiner = table.Column<string>(type: "varchar(20)", nullable: true),
                    LocationID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Plural = table.Column<bool>(nullable: false, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Item_LocationID",
                        column: x => x.LocationID,
                        principalSchema: "explore",
                        principalTable: "Location",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocationConnection",
                schema: "explore",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Direction = table.Column<string>(type: "varchar(10)", nullable: false),
                    FromLocationID = table.Column<int>(nullable: false),
                    ToLocationID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationConnection", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LocationConnection_FromLocationID",
                        column: x => x.FromLocationID,
                        principalSchema: "explore",
                        principalTable: "Location",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LocationConnection_ToLocationID",
                        column: x => x.ToLocationID,
                        principalSchema: "explore",
                        principalTable: "Location",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_LocationID",
                schema: "explore",
                table: "Item",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Location_MapID",
                schema: "explore",
                table: "Location",
                column: "MapID");

            migrationBuilder.CreateIndex(
                name: "IX_LocationConnection_FromLocationID",
                schema: "explore",
                table: "LocationConnection",
                column: "FromLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_LocationConnection_ToLocationID",
                schema: "explore",
                table: "LocationConnection",
                column: "ToLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_MapSessionSave_MapID",
                schema: "explore",
                table: "MapSessionSave",
                column: "MapID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item",
                schema: "explore");

            migrationBuilder.DropTable(
                name: "LocationConnection",
                schema: "explore");

            migrationBuilder.DropTable(
                name: "MapSessionSave",
                schema: "explore");

            migrationBuilder.DropTable(
                name: "Location",
                schema: "explore");

            migrationBuilder.DropTable(
                name: "Map",
                schema: "explore");
        }
    }
}
