using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FinalyMvs.Migrations
{
    public partial class InitialContacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientsID = table.Column<int>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    Mail = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Contact_Client_ClientsID",
                        column: x => x.ClientsID,
                        principalTable: "Client",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contact_ClientsID",
                table: "Contact",
                column: "ClientsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contact");
        }
    }
}
