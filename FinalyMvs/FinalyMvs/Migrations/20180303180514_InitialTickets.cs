using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FinalyMvs.Migrations
{
    public partial class InitialTickets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientsID = table.Column<int>(nullable: false),
                    Problem = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    UsersID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Client_ClientsID",
                        column: x => x.ClientsID,
                        principalTable: "Client",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Users_UsersID",
                        column: x => x.UsersID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ClientsID",
                table: "Tickets",
                column: "ClientsID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UsersID",
                table: "Tickets",
                column: "UsersID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");
        }
    }
}
