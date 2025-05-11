using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class relationsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Files");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId1",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId1",
                table: "Folders",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FolderId1",
                table: "Files",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Files",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyId1",
                table: "Users",
                column: "CompanyId1");

            migrationBuilder.CreateIndex(
                name: "IX_Folders_CompanyId",
                table: "Folders",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Folders_CompanyId1",
                table: "Folders",
                column: "CompanyId1");

            migrationBuilder.CreateIndex(
                name: "IX_Files_FolderId",
                table: "Files",
                column: "FolderId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_FolderId1",
                table: "Files",
                column: "FolderId1");

            migrationBuilder.CreateIndex(
                name: "IX_Files_UserId",
                table: "Files",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_UserId1",
                table: "Files",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Folders_FolderId",
                table: "Files",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Folders_FolderId1",
                table: "Files",
                column: "FolderId1",
                principalTable: "Folders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Users_UserId",
                table: "Files",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Users_UserId1",
                table: "Files",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Companies_CompanyId",
                table: "Folders",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Companies_CompanyId1",
                table: "Folders",
                column: "CompanyId1",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Companies_CompanyId1",
                table: "Users",
                column: "CompanyId1",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Folders_FolderId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Folders_FolderId1",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Users_UserId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Users_UserId1",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Companies_CompanyId",
                table: "Folders");

            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Companies_CompanyId1",
                table: "Folders");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Companies_CompanyId1",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CompanyId1",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Folders_CompanyId",
                table: "Folders");

            migrationBuilder.DropIndex(
                name: "IX_Folders_CompanyId1",
                table: "Folders");

            migrationBuilder.DropIndex(
                name: "IX_Files_FolderId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_FolderId1",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_UserId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_UserId1",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "CompanyId1",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CompanyId1",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "FolderId1",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Files");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Files",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
