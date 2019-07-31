using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageCompression.Repository.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    imageID = table.Column<Guid>(nullable: false),
                    filename = table.Column<string>(nullable: true),
                    processingStart = table.Column<DateTime>(nullable: false),
                    processingEnd = table.Column<DateTime>(nullable: false),
                    sizeBeforeCompression = table.Column<float>(nullable: false),
                    sizeAfterCompression = table.Column<float>(nullable: false),
                    blobImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.imageID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}
