using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS.Migrations
{
    public partial class AddAttachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttachmentVersions",
                columns: table => new
                {
                    AttachmentVersionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AttachmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachmentVersions", x => x.AttachmentVersionId);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    AttachmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentVersionAttachmentVersionId = table.Column<int>(type: "int", nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.AttachmentId);
                    table.ForeignKey(
                        name: "FK_Attachments_AttachmentVersions_CurrentVersionAttachmentVersionId",
                        column: x => x.CurrentVersionAttachmentVersionId,
                        principalTable: "AttachmentVersions",
                        principalColumn: "AttachmentVersionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attachments_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_CurrentVersionAttachmentVersionId",
                table: "Attachments",
                column: "CurrentVersionAttachmentVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_SubjectId",
                table: "Attachments",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentVersions_AttachmentId",
                table: "AttachmentVersions",
                column: "AttachmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttachmentVersions_Attachments_AttachmentId",
                table: "AttachmentVersions",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "AttachmentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_AttachmentVersions_CurrentVersionAttachmentVersionId",
                table: "Attachments");

            migrationBuilder.DropTable(
                name: "AttachmentVersions");

            migrationBuilder.DropTable(
                name: "Attachments");
        }
    }
}
