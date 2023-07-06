#nullable disable

namespace SithAcademy.Data.Migrations;

using Microsoft.EntityFrameworkCore.Migrations;

public partial class CreateSithAcademyEntities : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Locations",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false, comment: "ID of the location")
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, comment: "Name of the location"),
                Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Brief description of the location")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Locations", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Academies",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false, comment: "ID of the academy")
                    .Annotation("SqlServer:Identity", "1, 1"),
                Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Title of the academy"),
                Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Brief description of the academy"),
                LocationId = table.Column<int>(type: "int", nullable: false, comment: "ID of the academy's location")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Academies", x => x.Id);
                table.ForeignKey(
                    name: "FK_Academies_Locations_LocationId",
                    column: x => x.LocationId,
                    principalTable: "Locations",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "Overseers",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "ID of the overseer"),
                HoloFrequency = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, comment: "Phone number of the overseer"),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "ID of the existing user that is also an overseer"),
                AcademyId = table.Column<int>(type: "int", nullable: false, comment: "ID of the academy in which the overseer is assigned to")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Overseers", x => x.Id);
                table.ForeignKey(
                    name: "FK_Overseers_Academies_AcademyId",
                    column: x => x.AcademyId,
                    principalTable: "Academies",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Overseers_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Trials",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "ID of the trial"),
                Title = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false, comment: "Title of the trial"),
                Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Description of the trial"),
                AcademyId = table.Column<int>(type: "int", nullable: false, comment: "ID of the academy which hosts the trial")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Trials", x => x.Id);
                table.ForeignKey(
                    name: "FK_Trials_Academies_AcademyId",
                    column: x => x.AcademyId,
                    principalTable: "Academies",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "Resources",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "ID of the resource"),
                Name = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, comment: "Name of the resource"),
                Link = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false, comment: "Link leading to the resource's location on the Internet"),
                TrialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Resources", x => x.Id);
                table.ForeignKey(
                    name: "FK_Resources_Trials_TrialId",
                    column: x => x.TrialId,
                    principalTable: "Trials",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Academies_LocationId",
            table: "Academies",
            column: "LocationId");

        migrationBuilder.CreateIndex(
            name: "IX_Overseers_AcademyId",
            table: "Overseers",
            column: "AcademyId");

        migrationBuilder.CreateIndex(
            name: "IX_Overseers_UserId",
            table: "Overseers",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_Resources_TrialId",
            table: "Resources",
            column: "TrialId");

        migrationBuilder.CreateIndex(
            name: "IX_Trials_AcademyId",
            table: "Trials",
            column: "AcademyId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Overseers");

        migrationBuilder.DropTable(
            name: "Resources");

        migrationBuilder.DropTable(
            name: "Trials");

        migrationBuilder.DropTable(
            name: "Academies");

        migrationBuilder.DropTable(
            name: "Locations");
    }
}
