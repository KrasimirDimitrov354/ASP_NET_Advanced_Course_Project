using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SithAcademy.Data.Migrations
{
    public partial class CreateAndSeedDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "ID of the location")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false, comment: "Name of the location"),
                    ImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false, comment: "URL of the image that will be used to visualize the location"),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Boolean showing whether or not the location is accessible for new acolytes"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "Brief description of the location")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Academies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "ID of the academy")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false, comment: "Title of the academy"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "Brief description of the academy"),
                    ImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false, comment: "URL of the image that will be used to visualize the academy"),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Boolean showing whether or not the academy is accessible for new acolytes"),
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
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: true, comment: "ID of the location on which the acolyte is currently located"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "ID of the trial"),
                    Title = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false, comment: "Title of the trial"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "Description of the trial"),
                    ScoreToPass = table.Column<decimal>(type: "decimal(4,2)", nullable: false, comment: "The score needed by an acolyte to complete the trial"),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Boolean showing whether or not the trial can be participated in"),
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
                name: "AcademiesAcolytes",
                columns: table => new
                {
                    AcademyId = table.Column<int>(type: "int", nullable: false, comment: "ID of the academy in which the acolyte is assigned to"),
                    AcolyteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "ID of the acolyte")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademiesAcolytes", x => new { x.AcademyId, x.AcolyteId });
                    table.ForeignKey(
                        name: "FK_AcademiesAcolytes_Academies_AcademyId",
                        column: x => x.AcademyId,
                        principalTable: "Academies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcademiesAcolytes_AspNetUsers_AcolyteId",
                        column: x => x.AcolyteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Overseers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "ID of the overseer"),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Title of the overseer"),
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
                name: "Homeworks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "ID of the homework"),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "Content of the homework"),
                    Score = table.Column<decimal>(type: "decimal(4,2)", nullable: false, comment: "Score of the homework"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date when the homework was created"),
                    ReviewerName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, comment: "Name of the user that has reviewed the homework - overseer or admin"),
                    ReviewerFeedback = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "Feedback left by the user that has reviewed the homework - overseer or admin"),
                    TrialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "ID of the trial for which the homework is"),
                    AcolyteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "ID of the acolyte to which the homework belongs")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homeworks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Homeworks_AspNetUsers_AcolyteId",
                        column: x => x.AcolyteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Homeworks_Trials_TrialId",
                        column: x => x.TrialId,
                        principalTable: "Trials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "ID of the resource"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false, comment: "Name of the resource"),
                    SourceUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false, comment: "URL leading to the resource's location"),
                    ImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false, comment: "URL of the image that will be used to preview the resource"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Boolean showing whether or not the resource should be displayed"),
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

            migrationBuilder.CreateTable(
                name: "TrialsAcolytes",
                columns: table => new
                {
                    TrialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "ID of the trial which the acolyte must complete"),
                    AcolyteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "ID of the acolyte"),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Boolean showing whether or not the acolyte has an approved homework for the trial")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrialsAcolytes", x => new { x.TrialId, x.AcolyteId });
                    table.ForeignKey(
                        name: "FK_TrialsAcolytes_AspNetUsers_AcolyteId",
                        column: x => x.AcolyteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrialsAcolytes_Trials_TrialId",
                        column: x => x.TrialId,
                        principalTable: "Trials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LocationId", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("04589b17-3b3a-4118-b34d-dbfc70cd31f0"), 0, "cca0224a-d754-409a-9688-58b4698cad59", "acolyte@acolyte.com", false, null, true, null, "ACOLYTE@ACOLYTE.COM", "DEFAULTACOLYTE", "AQAAAAEAACcQAAAAEEhUV1c6DcCbezDCymxI5CTat+Cy+li70M7PQI3O5ohvSN1FOuNMnMyitzI70c2Guw==", null, false, "29f98652-c84f-481c-9993-057f275e1d8f", false, "DefaultAcolyte" },
                    { new Guid("a7fba81f-237e-4c59-8fe5-7a5e2c40e403"), 0, "fd52e766-4de8-43f1-b9ec-6d23f6b8eda6", "admin@sithacademy.com", false, null, true, null, "ADMIN@SITHACADEMY.COM", "ADMINISTRATOR", "AQAAAAEAACcQAAAAEEZvfGVOPXtdKnOurdvR6OraC0uUBiRAYdJ654zvR7e9Ut4AxQ3Xa5jWGTu6r9/m6w==", null, false, "d2cce644-0b69-48b9-931d-365e7e530932", false, "Administrator" }
                });

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
                values: new object[,]
                {
                    { 1, "The facility known today as Dreshdae Academy was originally established by the disciples of Exar Kun during the Great Sith War. It has been abandoned and rebuilt several times throughout the millennia, each time emerging as a more prestigious school of Sith studies.", "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/73e29da9-1d35-4f3b-976c-29aa9e2e11f0/dce4e87-9372d1cf-1921-4eba-bb39-4af28ffdb86f.jpg?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOjdlMGQxODg5ODIyNjQzNzNhNWYwZDQxNWVhMGQyNmUwIiwiaXNzIjoidXJuOmFwcDo3ZTBkMTg4OTgyMjY0MzczYTVmMGQ0MTVlYTBkMjZlMCIsIm9iaiI6W1t7InBhdGgiOiJcL2ZcLzczZTI5ZGE5LTFkMzUtNGYzYi05NzZjLTI5YWE5ZTJlMTFmMFwvZGNlNGU4Ny05MzcyZDFjZi0xOTIxLTRlYmEtYmIzOS00YWYyOGZmZGI4NmYuanBnIn1dXSwiYXVkIjpbInVybjpzZXJ2aWNlOmZpbGUuZG93bmxvYWQiXX0.vuR2MPGfi8YudSSvkO-OltPr8bd_zDICvhu9DAWRnDk", 2, "Dreshdae Academy" },
                    { 2, "The current-day Dark Temple complex is positioned close to the original structure of the same name, which has been fully destroyed during the last major war with the Galactic Republic. The wilderness surrounding the complex is home to a great deal of deadly predators, which provides a natural training ground for acolytes and overseers alike.", "https://cdnb.artstation.com/p/assets/images/images/013/314/009/large/micah-brown-sith-temple-2.jpg?1539039990", 1, "The Dark Temple" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LocationId", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("94ee6c77-02d6-44b4-8ef0-99d313d30bb8"), 0, "2ee622a4-fdf6-474f-aebd-d89ab3fb2369", "overseer@overseer.com", false, 1, true, null, "OVERSEER@OVERSEER.COM", "DARKTEMPLEOVERSEER", "AQAAAAEAACcQAAAAEAfdutnO68extW9t7Omq9JsIa8BFv8eoXXcMu2B+DaAvz0l4yjwMSd1dPfRsetN+QQ==", null, false, "13cfb343-779b-4031-82c5-8d0fbda7851a", false, "DarkTempleOverseer" },
                    { new Guid("e1cd947b-04b7-4a29-a2c2-d5383dd294e4"), 0, "b6d75686-755e-4e1a-a4ba-82d473259295", "overseer@overseer.com", false, 2, true, null, "OVERSEER@OVERSEER.COM", "DRESHDAEOVERSEER", "AQAAAAEAACcQAAAAEFmCJzWd7Rl2XbuP3So2S8QSafn42yTkgD0N5S7j12elfUgVpvz3gtxfwSaNZJ3h4A==", null, false, "47a3ae3c-b670-4eb8-bd57-f4b332cbb951", false, "DreshdaeOverseer" }
                });

            migrationBuilder.InsertData(
                table: "AcademiesAcolytes",
                columns: new[] { "AcademyId", "AcolyteId" },
                values: new object[,]
                {
                    { 1, new Guid("e1cd947b-04b7-4a29-a2c2-d5383dd294e4") },
                    { 2, new Guid("94ee6c77-02d6-44b4-8ef0-99d313d30bb8") }
                });

            migrationBuilder.InsertData(
                table: "Overseers",
                columns: new[] { "Id", "AcademyId", "Title", "UserId" },
                values: new object[,]
                {
                    { new Guid("257d9119-875f-46b8-b205-a3b447cc6661"), 1, "Lord Korriban", new Guid("e1cd947b-04b7-4a29-a2c2-d5383dd294e4") },
                    { new Guid("d3b5555f-4b88-47fe-b55a-8e1d92562cac"), 2, "Lord Kaas", new Guid("94ee6c77-02d6-44b4-8ef0-99d313d30bb8") }
                });

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
                    { new Guid("487248b1-3d9d-4165-b005-eeb7d3fa56a0"), new Guid("e1cd947b-04b7-4a29-a2c2-d5383dd294e4"), "This is user DreshdaeOverseer's homework for the Trial of Passion.", new DateTime(2023, 8, 7, 17, 0, 35, 982, DateTimeKind.Utc).AddTicks(1749), "Very good and excellent homework!", "The Dark Side itself", 10.0m, new Guid("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9") },
                    { new Guid("6fe2e8be-cf3a-467d-b760-a2dd7e426dc4"), new Guid("94ee6c77-02d6-44b4-8ef0-99d313d30bb8"), "This is user DarkTempleOverseer's homework for the Trial of Power.", new DateTime(2023, 8, 7, 17, 0, 35, 982, DateTimeKind.Utc).AddTicks(1770), "Very good and excellent homework!", "The Dark Side itself", 10.0m, new Guid("9595a701-973a-4d7c-819d-93efcfbf9fa8") },
                    { new Guid("701c939f-774a-46ce-91fb-c747d98ed4b3"), new Guid("e1cd947b-04b7-4a29-a2c2-d5383dd294e4"), "This is user DreshdaeOverseer's homework for the Trial of Strength.", new DateTime(2023, 8, 7, 17, 0, 35, 982, DateTimeKind.Utc).AddTicks(1761), "Very good and excellent homework!", "The Dark Side itself", 10.0m, new Guid("aa37b907-5d8b-439c-a719-2a784c07744a") },
                    { new Guid("aa92f0ed-de04-4bf1-97b4-aa048c05fd61"), new Guid("94ee6c77-02d6-44b4-8ef0-99d313d30bb8"), "This is user DarkTempleOverseer's homework for the Trial of Victory.", new DateTime(2023, 8, 7, 17, 0, 35, 982, DateTimeKind.Utc).AddTicks(1774), "Very good and excellent homework!", "The Dark Side itself", 10.0m, new Guid("b92c1895-a6ef-422d-b760-298a0785b612") }
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
                    { new Guid("94ee6c77-02d6-44b4-8ef0-99d313d30bb8"), new Guid("9595a701-973a-4d7c-819d-93efcfbf9fa8"), true },
                    { new Guid("e1cd947b-04b7-4a29-a2c2-d5383dd294e4"), new Guid("aa37b907-5d8b-439c-a719-2a784c07744a"), true },
                    { new Guid("94ee6c77-02d6-44b4-8ef0-99d313d30bb8"), new Guid("b92c1895-a6ef-422d-b760-298a0785b612"), true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Academies_LocationId",
                table: "Academies",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademiesAcolytes_AcolyteId",
                table: "AcademiesAcolytes",
                column: "AcolyteId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LocationId",
                table: "AspNetUsers",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Homeworks_AcolyteId",
                table: "Homeworks",
                column: "AcolyteId");

            migrationBuilder.CreateIndex(
                name: "IX_Homeworks_TrialId",
                table: "Homeworks",
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

            migrationBuilder.CreateIndex(
                name: "IX_TrialsAcolytes_AcolyteId",
                table: "TrialsAcolytes",
                column: "AcolyteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademiesAcolytes");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Homeworks");

            migrationBuilder.DropTable(
                name: "Overseers");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "TrialsAcolytes");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Trials");

            migrationBuilder.DropTable(
                name: "Academies");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
