using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LittleSocialNetwork.DataAccess.Migrations
{
    public partial class AddSingleChat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(  
                name: "SingleChatMessage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FromId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<int>(type: "int", nullable: false),
                    LastEdited = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PostTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleChatMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SingleChatMessage_Profiles_FromId",
                        column: x => x.FromId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SingleChatMessage_Profiles_ToId",
                        column: x => x.ToId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SingleChatMessage_FromId",
                table: "SingleChatMessage",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleChatMessage_ToId",
                table: "SingleChatMessage",
                column: "ToId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SingleChatMessage");
        }
    }
}
