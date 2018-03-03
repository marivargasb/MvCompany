using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FinalyMvs.Migrations
{
    public partial class InitialMeetings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meeting",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientsID = table.Column<int>(nullable: false),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    UsersID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meeting", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Meeting_Client_ClientsID",
                        column: x => x.ClientsID,
                        principalTable: "Client",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Meeting_Users_UsersID",
                        column: x => x.UsersID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meeting_ClientsID",
                table: "Meeting",
                column: "ClientsID");

            migrationBuilder.CreateIndex(
                name: "IX_Meeting_UsersID",
                table: "Meeting",
                column: "UsersID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meeting");
        }
    }
}
