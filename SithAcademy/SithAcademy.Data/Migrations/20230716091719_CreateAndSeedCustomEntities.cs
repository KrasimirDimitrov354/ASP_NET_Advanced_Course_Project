#nullable disable

namespace SithAcademy.Data.Migrations;

using Microsoft.EntityFrameworkCore.Migrations;

public partial class CreateAndSeedCustomEntities : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Locations",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false, comment: "ID of the location")
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false, comment: "Name of the location"),
                Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "Brief description of the location")
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
                Title = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false, comment: "Title of the academy"),
                Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "Brief description of the academy"),
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
                Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "Description of the trial"),
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
            name: "AcademiesStatistics",
            columns: table => new
            {
                AcolyteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TrialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                IsCompleted = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AcademiesStatistics", x => new { x.AcolyteId, x.TrialId });
                table.ForeignKey(
                    name: "FK_AcademiesStatistics_AspNetUsers_AcolyteId",
                    column: x => x.AcolyteId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_AcademiesStatistics_Trials_TrialId",
                    column: x => x.TrialId,
                    principalTable: "Trials",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Resources",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "ID of the resource"),
                Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false, comment: "Name of the resource"),
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

        migrationBuilder.InsertData(
            table: "Locations",
            columns: new[] { "Id", "Description", "Name" },
            values: new object[] { 1, "Dromund Kaas was originally a colony world of the Sith Empire, and at one point its capital.Its atmosphere is heavily charged with electricity to the point where lightning is a near-constant sight in the almost perpetually clouded sky - a result of ancient Sith experiments in arcane and forbidden uses of the dark side of the Force.", "Dromund Kaas" });

        migrationBuilder.InsertData(
            table: "Locations",
            columns: new[] { "Id", "Description", "Name" },
            values: new object[] { 2, "Korriban was the original homeworld of the Sith species and a sacred planet for the Sith Order, housing the tombs of many ancient and powerful Dark Lords. Even to this day those tombs hold immense power and unfanthomable secrets, as well as untold horrors and the bleached bones of unlucky explorers.", "Korriban" });

        migrationBuilder.InsertData(
            table: "Locations",
            columns: new[] { "Id", "Description", "Name" },
            values: new object[] { 3, "Ziost was originally covered with vast thick forests and possessed a warm climate, however a sudden ice age experienced during the initial Sith colonization leveled the vast woodlands as well as most evidence of the pre-existing civilization. As a result, the planet was transformed into a bitterly cold tundra with an arid climate, its surface covered with rocky terrain, ice-encrusted mountains and titanic glaciers.", "Ziost" });

        migrationBuilder.InsertData(
            table: "Academies",
            columns: new[] { "Id", "Description", "LocationId", "Title" },
            values: new object[] { 1, "The facility known today as Dreshdae Academy was originally established by the disciples of Exar Kun during the Great Sith War. It has been abandoned and rebuilt several times throughout the millennia, each time emerging as a more prestigious school of Sith studies.", 2, "Dreshdae Academy" });

        migrationBuilder.InsertData(
            table: "Academies",
            columns: new[] { "Id", "Description", "LocationId", "Title" },
            values: new object[] { 2, "The current-day Dark Temple complex is positioned close to the original structure of the same name, which has been fully destroyed during the last major war with the Galactic Republic. The wilderness surrounding the complex is home to a great deal of deadly predators, which provides a natural training ground for acolytes and overseers alike.", 1, "The Dark Temple" });

        migrationBuilder.InsertData(
            table: "Trials",
            columns: new[] { "Id", "AcademyId", "Description", "Title" },
            values: new object[,]
            {
                { new Guid("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9"), 1, "Dreshdae has a thriving population of underworld elements. Smugglers, bounty hunters, slavers, pirates. Mingle with them. Understand their passions. Succeed in this endeavour, and you will be able to control them.", "Trial of Passion" },
                { new Guid("9595a701-973a-4d7c-819d-93efcfbf9fa8"), 2, "True power comes to the cunning. Remnants of a failed empire still eke out an existence amidst the endless jungles. Infiltrate one of warring clans and make them do your bidding. Do not underestimate the power of the superstitious mind.", "Trial of Power" },
                { new Guid("aa37b907-5d8b-439c-a719-2a784c07744a"), 1, "Only the strongest of Sith earn the honour of resting in the Valley of the Dark Lords. Study their feats and histories.Explore their tombs to gain an understanding of what it takes to be Sith. Beware the Valley's guardians.", "Trial of Strength" },
                { new Guid("b92c1895-a6ef-422d-b760-298a0785b612"), 2, "A Sith must accept nothing less than the complete destruction of their enemies. Venture out into the wilderness. Observe the primal savagery of the beasts while taking note of their weaknesses. Return with proof of your victory over them.", "Trial of Victory" }
            });

        migrationBuilder.InsertData(
            table: "Resources",
            columns: new[] { "Id", "Link", "Name", "TrialId" },
            values: new object[,]
            {
                { new Guid("1d15dbcc-67b8-4597-b32a-d9d54a91bb85"), "starwars.fandom.com/wiki/K'lor'slug/Legends", "K'lor'slug", new Guid("aa37b907-5d8b-439c-a719-2a784c07744a") },
                { new Guid("2c529f2b-d864-4dc7-b468-c44d630ec7c4"), "starwars.fandom.com/wiki/Black_Sun/Legends", "Black Sun", new Guid("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9") },
                { new Guid("30bc967b-9c02-400b-b363-fc12f4929336"), "starwars.fandom.com/wiki/Yozusk", "Yozusk", new Guid("b92c1895-a6ef-422d-b760-298a0785b612") },
                { new Guid("479a9611-5af8-4ebf-aa05-95d3d21397f6"), "starwars.fandom.com/wiki/Tuk'ata/Legends", "Tuk'ata", new Guid("aa37b907-5d8b-439c-a719-2a784c07744a") },
                { new Guid("559e40bd-13fa-47db-947e-0f087b3496a5"), "starwars.fandom.com/wiki/Spice/Legends", "Spice", new Guid("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9") },
                { new Guid("a6def1fb-93d8-43f2-bd5c-6d3bdf220694"), "starwars.fandom.com/wiki/Shyrack/Legends", "Shyrack", new Guid("aa37b907-5d8b-439c-a719-2a784c07744a") },
                { new Guid("b2b42c49-9fde-43cc-a409-5df9c1e7c774"), "starwars.fandom.com/wiki/Gundark/Legends", "Gundark", new Guid("b92c1895-a6ef-422d-b760-298a0785b612") },
                { new Guid("b9da4d71-52bc-451e-951f-c46e04e8293c"), "starwars.fandom.com/wiki/Valley_of_the_Dark_Lords/Legends", "History of the Valley of the Dark Lords", new Guid("aa37b907-5d8b-439c-a719-2a784c07744a") },
                { new Guid("de19a886-21a2-4550-ac26-34134ccf2268"), "starwars.fandom.com/wiki/Jurgoran", "Jurgoran", new Guid("b92c1895-a6ef-422d-b760-298a0785b612") },
                { new Guid("e6d39382-06ef-47f6-887c-a6f4e7806047"), "starwars.fandom.com/wiki/Bounty_Hunters'_Guild/Legends", "Bounty Hunters' Guild", new Guid("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9") },
                { new Guid("e76679a2-14a4-4e91-8a06-c972da405f05"), "starwars.fandom.com/wiki/Prophets_of_the_Dark_Side", "Study the origins of the clans you will encounter", new Guid("9595a701-973a-4d7c-819d-93efcfbf9fa8") },
                { new Guid("fc34dc68-b10e-4c14-a1d9-3ad96b73f431"), "starwars.fandom.com/wiki/Hutt_Cartel", "Hutt Cartel", new Guid("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9") },
                { new Guid("ff04a297-c227-4f02-8b0c-772f4213e6a9"), "starwars.fandom.com/wiki/Vine_cat", "Vine cat", new Guid("b92c1895-a6ef-422d-b760-298a0785b612") }
            });

        migrationBuilder.CreateIndex(
            name: "IX_Academies_LocationId",
            table: "Academies",
            column: "LocationId");

        migrationBuilder.CreateIndex(
            name: "IX_AcademiesStatistics_TrialId",
            table: "AcademiesStatistics",
            column: "TrialId");

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
            name: "AcademiesStatistics");

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
