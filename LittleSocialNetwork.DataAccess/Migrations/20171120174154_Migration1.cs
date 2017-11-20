using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LittleSocialNetwork.DataAccess.Migrations
{
    public partial class Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SingleChatMessage_UserProfiles_FromId",
                table: "SingleChatMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleChatMessage_UserProfiles_ToId",
                table: "SingleChatMessage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SingleChatMessage",
                table: "SingleChatMessage");

            migrationBuilder.RenameTable(
                name: "SingleChatMessage",
                newName: "SingleChatMessages");

            migrationBuilder.RenameIndex(
                name: "IX_SingleChatMessage_ToId",
                table: "SingleChatMessages",
                newName: "IX_SingleChatMessages_ToId");

            migrationBuilder.RenameIndex(
                name: "IX_SingleChatMessage_FromId",
                table: "SingleChatMessages",
                newName: "IX_SingleChatMessages_FromId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SingleChatMessages",
                table: "SingleChatMessages",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Friendships",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FromId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ToId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Friendships_UserProfiles_FromId",
                        column: x => x.FromId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friendships_UserProfiles_ToId",
                        column: x => x.ToId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_FromId",
                table: "Friendships",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_ToId",
                table: "Friendships",
                column: "ToId");

            migrationBuilder.AddForeignKey(
                name: "FK_SingleChatMessages_UserProfiles_FromId",
                table: "SingleChatMessages",
                column: "FromId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SingleChatMessages_UserProfiles_ToId",
                table: "SingleChatMessages",
                column: "ToId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SingleChatMessages_UserProfiles_FromId",
                table: "SingleChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleChatMessages_UserProfiles_ToId",
                table: "SingleChatMessages");

            migrationBuilder.DropTable(
                name: "Friendships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SingleChatMessages",
                table: "SingleChatMessages");

            migrationBuilder.RenameTable(
                name: "SingleChatMessages",
                newName: "SingleChatMessage");

            migrationBuilder.RenameIndex(
                name: "IX_SingleChatMessages_ToId",
                table: "SingleChatMessage",
                newName: "IX_SingleChatMessage_ToId");

            migrationBuilder.RenameIndex(
                name: "IX_SingleChatMessages_FromId",
                table: "SingleChatMessage",
                newName: "IX_SingleChatMessage_FromId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SingleChatMessage",
                table: "SingleChatMessage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SingleChatMessage_UserProfiles_FromId",
                table: "SingleChatMessage",
                column: "FromId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SingleChatMessage_UserProfiles_ToId",
                table: "SingleChatMessage",
                column: "ToId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
