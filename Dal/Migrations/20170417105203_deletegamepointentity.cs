using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Web.Migrations
{
    public partial class deletegamepointentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamePoints");

            migrationBuilder.AddColumn<int>(
                name: "Point",
                table: "Sessions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Point",
                table: "Sessions");

            migrationBuilder.CreateTable(
                name: "GamePoints",
                columns: table => new
                {
                    GamePointId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Points = table.Column<int>(nullable: false),
                    QuizUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePoints", x => x.GamePointId);
                    table.ForeignKey(
                        name: "FK_GamePoints_AspNetUsers_QuizUserId",
                        column: x => x.QuizUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamePoints_QuizUserId",
                table: "GamePoints",
                column: "QuizUserId");
        }
    }
}
