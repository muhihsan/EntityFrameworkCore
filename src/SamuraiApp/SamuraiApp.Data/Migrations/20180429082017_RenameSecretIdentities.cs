using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SamuraiApp.Data.Migrations
{
    public partial class RenameSecretIdentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SecretIdentity_Samurais_SamuraiId",
                table: "SecretIdentity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SecretIdentity",
                table: "SecretIdentity");

            migrationBuilder.RenameTable(
                name: "SecretIdentity",
                newName: "SecretIdentities");

            migrationBuilder.RenameIndex(
                name: "IX_SecretIdentity_SamuraiId",
                table: "SecretIdentities",
                newName: "IX_SecretIdentities_SamuraiId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SecretIdentities",
                table: "SecretIdentities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SecretIdentities_Samurais_SamuraiId",
                table: "SecretIdentities",
                column: "SamuraiId",
                principalTable: "Samurais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SecretIdentities_Samurais_SamuraiId",
                table: "SecretIdentities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SecretIdentities",
                table: "SecretIdentities");

            migrationBuilder.RenameTable(
                name: "SecretIdentities",
                newName: "SecretIdentity");

            migrationBuilder.RenameIndex(
                name: "IX_SecretIdentities_SamuraiId",
                table: "SecretIdentity",
                newName: "IX_SecretIdentity_SamuraiId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SecretIdentity",
                table: "SecretIdentity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SecretIdentity_Samurais_SamuraiId",
                table: "SecretIdentity",
                column: "SamuraiId",
                principalTable: "Samurais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
