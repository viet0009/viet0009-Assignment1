using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lab4.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "communities",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Budget = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_communities", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Advertisements",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BlobKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommunityID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisements", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Advertisements_communities_CommunityID",
                        column: x => x.CommunityID,
                        principalTable: "communities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "memberships",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    CommunityID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_memberships", x => new { x.StudentID, x.CommunityID });
                    table.ForeignKey(
                        name: "FK_memberships_communities_CommunityID",
                        column: x => x.CommunityID,
                        principalTable: "communities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_memberships_students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "students",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_CommunityID",
                table: "Advertisements",
                column: "CommunityID");

            migrationBuilder.CreateIndex(
                name: "IX_memberships_CommunityID",
                table: "memberships",
                column: "CommunityID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advertisements");

            migrationBuilder.DropTable(
                name: "memberships");

            migrationBuilder.DropTable(
                name: "communities");

            migrationBuilder.DropTable(
                name: "students");
        }
    }
}
