using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SithAcademy.Data.Migrations
{
    public partial class SeedDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LocationId", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("04589b17-3b3a-4118-b34d-dbfc70cd31f0"), 0, "c4330a2e-ab0d-4860-8f80-e8994626e0c6", "acolyte@acolyte.com", false, null, true, null, "ACOLYTE@ACOLYTE.COM", "DEFAULTACOLYTE", "AQAAAAEAACcQAAAAEPdQbZXgY7nlbaKfaUiE3cktHomiBRYpsAE4o5HFY2+I0TVZnEpZfjijhsMnEdajwA==", null, false, "13b8b855-53f7-48e5-b276-a5dc47ff3961", false, "DefaultAcolyte" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Description", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, "Dromund Kaas was originally a colony world of the Sith Empire, and at one point its capital. Its atmosphere is heavily charged with electricity to the point where lightning is a near-constant sight in the almost perpetually clouded sky - a result of ancient Sith experiments in arcane and forbidden uses of the dark side of the Force.", "https://www.worldanvil.com/media/cache/cover/uploads/images/7c2913da4c331e69f6dfc4fd2225fb0f.jpg", "Dromund Kaas" },
                    { 2, "Korriban was the original homeworld of the Sith species and a sacred planet for the Sith Order, housing the tombs of many ancient and powerful Dark Lords. Even to this day those tombs hold immense power and unfanthomable secrets, as well as untold horrors and the bleached bones of unlucky explorers.", "https://cdnb.artstation.com/p/assets/images/images/053/512/833/large/shiny-man-korriban-icon-01.jpg?1662395808", "Korriban" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Description", "ImageUrl", "IsLocked", "Name" },
                values: new object[] { 3, "Ziost was originally covered with vast thick forests and possessed a warm climate, however a sudden ice age experienced during the initial Sith colonization leveled the vast woodlands as well as most evidence of the pre-existing civilization. As a result, the planet was transformed into a bitterly cold tundra with an arid climate, its surface covered with rocky terrain, ice-encrusted mountains and titanic glaciers.", "https://cdnb.artstation.com/p/assets/images/images/020/733/493/4k/brian-hagan-ziost.jpg?1568947978", true, "Ziost" });

            migrationBuilder.InsertData(
                table: "Academies",
                columns: new[] { "Id", "Description", "ImageUrl", "LocationId", "Title" },
                values: new object[] { 1, "The facility known today as Dreshdae Academy was originally established by the disciples of Exar Kun during the Great Sith War. It has been abandoned and rebuilt several times throughout the millennia, each time emerging as a more prestigious school of Sith studies.", "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/73e29da9-1d35-4f3b-976c-29aa9e2e11f0/dce4e87-9372d1cf-1921-4eba-bb39-4af28ffdb86f.jpg?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOjdlMGQxODg5ODIyNjQzNzNhNWYwZDQxNWVhMGQyNmUwIiwiaXNzIjoidXJuOmFwcDo3ZTBkMTg4OTgyMjY0MzczYTVmMGQ0MTVlYTBkMjZlMCIsIm9iaiI6W1t7InBhdGgiOiJcL2ZcLzczZTI5ZGE5LTFkMzUtNGYzYi05NzZjLTI5YWE5ZTJlMTFmMFwvZGNlNGU4Ny05MzcyZDFjZi0xOTIxLTRlYmEtYmIzOS00YWYyOGZmZGI4NmYuanBnIn1dXSwiYXVkIjpbInVybjpzZXJ2aWNlOmZpbGUuZG93bmxvYWQiXX0.vuR2MPGfi8YudSSvkO-OltPr8bd_zDICvhu9DAWRnDk", 2, "Dreshdae Academy" });

            migrationBuilder.InsertData(
                table: "Academies",
                columns: new[] { "Id", "Description", "ImageUrl", "LocationId", "Title" },
                values: new object[] { 2, "The current-day Dark Temple complex is positioned close to the original structure of the same name, which has been fully destroyed during the last major war with the Galactic Republic. The wilderness surrounding the complex is home to a great deal of deadly predators, which provides a natural training ground for acolytes and overseers alike.", "https://cdnb.artstation.com/p/assets/images/images/013/314/009/large/micah-brown-sith-temple-2.jpg?1539039990", 1, "The Dark Temple" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LocationId", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("e1cd947b-04b7-4a29-a2c2-d5383dd294e4"), 0, "33b91e87-4ecd-440b-b747-515ba4bed579", "overseer@overseer.com", false, 2, true, null, "OVERSEER@OVERSEER.COM", "DEFAULTOVERSEER", "AQAAAAEAACcQAAAAEMyP9Mg608uVsI21oy4cuvQdZ3jt8gU9vjKf7LmLChJT0A+Sk1uPoUDdWMQxMlz12Q==", null, false, "fd5a6016-9504-40b9-a0a5-56d761d3daa0", false, "DefaultOverseer" });

            migrationBuilder.InsertData(
                table: "AcademiesAcolytes",
                columns: new[] { "AcademyId", "AcolyteId" },
                values: new object[] { 1, new Guid("e1cd947b-04b7-4a29-a2c2-d5383dd294e4") });

            migrationBuilder.InsertData(
                table: "Overseers",
                columns: new[] { "Id", "AcademyId", "Title", "UserId" },
                values: new object[] { new Guid("257d9119-875f-46b8-b205-a3b447cc6661"), 1, "Insert Very Cool Title Here", new Guid("e1cd947b-04b7-4a29-a2c2-d5383dd294e4") });

            migrationBuilder.InsertData(
                table: "Trials",
                columns: new[] { "Id", "AcademyId", "Description", "ScoreToPass", "Title" },
                values: new object[,]
                {
                    { new Guid("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9"), 1, "Dreshdae has a thriving population of underworld elements. Smugglers, bounty hunters, slavers, pirates. Mingle with them. Understand their passions. Succeed in this endeavour, and you will be able to control them.", 6.5m, "Trial of Passion" },
                    { new Guid("9595a701-973a-4d7c-819d-93efcfbf9fa8"), 2, "True power comes to the cunning. Remnants of a failed empire still eke out an existence amidst the endless jungles. Infiltrate one of warring clans and make them do your bidding. Do not underestimate the power of the superstitious mind.", 7.5m, "Trial of Power" },
                    { new Guid("aa37b907-5d8b-439c-a719-2a784c07744a"), 1, "Only the strongest of Sith earn the honour of resting in the Valley of the Dark Lords. Study their feats and histories. Explore their tombs to gain an understanding of what it takes to be Sith. Beware the Valley's guardians.", 7.0m, "Trial of Strength" },
                    { new Guid("b92c1895-a6ef-422d-b760-298a0785b612"), 2, "A Sith must accept nothing less than the complete destruction of their enemies. Venture out into the wilderness. Observe the primal savagery of the beasts while taking note of their weaknesses. Return with proof of your victory over them.", 8.0m, "Trial of Victory" }
                });

            migrationBuilder.InsertData(
                table: "Homeworks",
                columns: new[] { "Id", "AcolyteId", "Content", "CreatedOn", "ReviewerFeedback", "ReviewerName", "Score", "TrialId" },
                values: new object[,]
                {
                    { new Guid("08a54877-71fd-4582-87c3-b99ef700aa6f"), new Guid("e1cd947b-04b7-4a29-a2c2-d5383dd294e4"), "This is user DefaultOverseer's homework for the Trial of Passion.", new DateTime(2023, 7, 29, 18, 49, 11, 740, DateTimeKind.Utc).AddTicks(4062), "Very good and excellent homework!", "The Dark Side itself", 10.0m, new Guid("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9") },
                    { new Guid("9cc74c66-251a-44ab-b93b-a3baf9f627af"), new Guid("e1cd947b-04b7-4a29-a2c2-d5383dd294e4"), "This is user DefaultOverseer's homework for the Trial of Strength.", new DateTime(2023, 7, 29, 18, 49, 11, 740, DateTimeKind.Utc).AddTicks(4073), "Very good and excellent homework!", "The Dark Side itself", 10.0m, new Guid("aa37b907-5d8b-439c-a719-2a784c07744a") }
                });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "Id", "ImageUrl", "Name", "SourceUrl", "TrialId" },
                values: new object[,]
                {
                    { new Guid("1d15dbcc-67b8-4597-b32a-d9d54a91bb85"), "https://static.wikia.nocookie.net/aliens/images/b/b7/K'lor'slug.png", "K'lor'slug", "https://starwars.fandom.com/wiki/K'lor'slug/Legends", new Guid("aa37b907-5d8b-439c-a719-2a784c07744a") },
                    { new Guid("2c529f2b-d864-4dc7-b468-c44d630ec7c4"), "https://overmental.com/wp-content/uploads/2015/07/PrinceXizorart.png", "Black Sun", "https://starwars.fandom.com/wiki/Black_Sun/Legends", new Guid("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9") },
                    { new Guid("30bc967b-9c02-400b-b363-fc12f4929336"), "https://pm1.aminoapps.com/6435/a93762efd24f62a2395dd82b349729d35245d004_hq.jpg", "Yozusk", "https://starwars.fandom.com/wiki/Yozusk", new Guid("b92c1895-a6ef-422d-b760-298a0785b612") },
                    { new Guid("479a9611-5af8-4ebf-aa05-95d3d21397f6"), "https://pm1.aminoapps.com/5870/1727f7e20f6ef47d8605c148ff71bdabc8c9df3f_hq.jpg", "Tuk'ata", "https://starwars.fandom.com/wiki/Tuk'ata/Legends", new Guid("aa37b907-5d8b-439c-a719-2a784c07744a") },
                    { new Guid("559e40bd-13fa-47db-947e-0f087b3496a5"), "https://fictionhorizon.com/wp-content/uploads/2022/01/pykes.jpg", "Spice", "https://starwars.fandom.com/wiki/Spice/Legends", new Guid("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9") },
                    { new Guid("a6def1fb-93d8-43f2-bd5c-6d3bdf220694"), "https://pm1.aminoapps.com/6935/bb60fc764e739d6da08e25ae84d038c1885192ecr1-526-493v2_hq.jpg", "Shyrack", "https://starwars.fandom.com/wiki/Shyrack/Legends", new Guid("aa37b907-5d8b-439c-a719-2a784c07744a") },
                    { new Guid("b2b42c49-9fde-43cc-a409-5df9c1e7c774"), "https://www.gamesmanagers.com/images/posts/bc2ec369d3e6cf31786544799d3088c8-2.jpg", "Gundark", "https://starwars.fandom.com/wiki/Gundark/Legends", new Guid("b92c1895-a6ef-422d-b760-298a0785b612") },
                    { new Guid("b9da4d71-52bc-451e-951f-c46e04e8293c"), "https://ddx5i92cqts4o.cloudfront.net/images/1ejq0l57t_Fearful_Landscape_CotG.png", "History of the Valley of the Dark Lords", "https://starwars.fandom.com/wiki/Valley_of_the_Dark_Lords/Legends", new Guid("aa37b907-5d8b-439c-a719-2a784c07744a") },
                    { new Guid("de19a886-21a2-4550-ac26-34134ccf2268"), "https://pm1.aminoapps.com/6435/a30efbffef3abff7d2860524cb52b48aba89181d_hq.jpg", "Jurgoran", "https://starwars.fandom.com/wiki/Jurgoran", new Guid("b92c1895-a6ef-422d-b760-298a0785b612") },
                    { new Guid("e6d39382-06ef-47f6-887c-a6f4e7806047"), "https://www.worldanvil.com/media/cache/cover/uploads/images/e98ee0f248cd6fff599458a47aa7c1d4.jpg", "Bounty Hunters' Guild", "https://starwars.fandom.com/wiki/Bounty_Hunters'_Guild/Legends", new Guid("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9") },
                    { new Guid("e76679a2-14a4-4e91-8a06-c972da405f05"), "https://static.wikia.nocookie.net/starwars/images/b/b1/Darthmill.jpg/", "Study the origins of the clans you will encounter", "https://starwars.fandom.com/wiki/Prophets_of_the_Dark_Side", new Guid("9595a701-973a-4d7c-819d-93efcfbf9fa8") },
                    { new Guid("fc34dc68-b10e-4c14-a1d9-3ad96b73f431"), "https://media.moddb.com/images/mods/1/19/18461/hutt_fleet_by_wrait.jpg", "Hutt Cartel", "https://starwars.fandom.com/wiki/Hutt_Cartel", new Guid("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9") },
                    { new Guid("ff04a297-c227-4f02-8b0c-772f4213e6a9"), "https://oyster.ignimgs.com/mediawiki/apis.ign.com/star-wars-the-old-republic/1/15/Ss_vinecat01_800x450.jpg", "Vine cat", "https://starwars.fandom.com/wiki/Vine_cat", new Guid("b92c1895-a6ef-422d-b760-298a0785b612") }
                });

            migrationBuilder.InsertData(
                table: "TrialsAcolytes",
                columns: new[] { "AcolyteId", "TrialId", "IsCompleted" },
                values: new object[,]
                {
                    { new Guid("e1cd947b-04b7-4a29-a2c2-d5383dd294e4"), new Guid("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9"), true },
                    { new Guid("e1cd947b-04b7-4a29-a2c2-d5383dd294e4"), new Guid("aa37b907-5d8b-439c-a719-2a784c07744a"), true }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AcademiesAcolytes",
                keyColumns: new[] { "AcademyId", "AcolyteId" },
                keyValues: new object[] { 1, new Guid("e1cd947b-04b7-4a29-a2c2-d5383dd294e4") });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("04589b17-3b3a-4118-b34d-dbfc70cd31f0"));

            migrationBuilder.DeleteData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: new Guid("08a54877-71fd-4582-87c3-b99ef700aa6f"));

            migrationBuilder.DeleteData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: new Guid("9cc74c66-251a-44ab-b93b-a3baf9f627af"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Overseers",
                keyColumn: "Id",
                keyValue: new Guid("257d9119-875f-46b8-b205-a3b447cc6661"));

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: new Guid("1d15dbcc-67b8-4597-b32a-d9d54a91bb85"));

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: new Guid("2c529f2b-d864-4dc7-b468-c44d630ec7c4"));

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: new Guid("30bc967b-9c02-400b-b363-fc12f4929336"));

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: new Guid("479a9611-5af8-4ebf-aa05-95d3d21397f6"));

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: new Guid("559e40bd-13fa-47db-947e-0f087b3496a5"));

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: new Guid("a6def1fb-93d8-43f2-bd5c-6d3bdf220694"));

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: new Guid("b2b42c49-9fde-43cc-a409-5df9c1e7c774"));

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: new Guid("b9da4d71-52bc-451e-951f-c46e04e8293c"));

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: new Guid("de19a886-21a2-4550-ac26-34134ccf2268"));

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: new Guid("e6d39382-06ef-47f6-887c-a6f4e7806047"));

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: new Guid("e76679a2-14a4-4e91-8a06-c972da405f05"));

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: new Guid("fc34dc68-b10e-4c14-a1d9-3ad96b73f431"));

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: new Guid("ff04a297-c227-4f02-8b0c-772f4213e6a9"));

            migrationBuilder.DeleteData(
                table: "TrialsAcolytes",
                keyColumns: new[] { "AcolyteId", "TrialId" },
                keyValues: new object[] { new Guid("e1cd947b-04b7-4a29-a2c2-d5383dd294e4"), new Guid("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9") });

            migrationBuilder.DeleteData(
                table: "TrialsAcolytes",
                keyColumns: new[] { "AcolyteId", "TrialId" },
                keyValues: new object[] { new Guid("e1cd947b-04b7-4a29-a2c2-d5383dd294e4"), new Guid("aa37b907-5d8b-439c-a719-2a784c07744a") });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e1cd947b-04b7-4a29-a2c2-d5383dd294e4"));

            migrationBuilder.DeleteData(
                table: "Trials",
                keyColumn: "Id",
                keyValue: new Guid("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9"));

            migrationBuilder.DeleteData(
                table: "Trials",
                keyColumn: "Id",
                keyValue: new Guid("9595a701-973a-4d7c-819d-93efcfbf9fa8"));

            migrationBuilder.DeleteData(
                table: "Trials",
                keyColumn: "Id",
                keyValue: new Guid("aa37b907-5d8b-439c-a719-2a784c07744a"));

            migrationBuilder.DeleteData(
                table: "Trials",
                keyColumn: "Id",
                keyValue: new Guid("b92c1895-a6ef-422d-b760-298a0785b612"));

            migrationBuilder.DeleteData(
                table: "Academies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Academies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
