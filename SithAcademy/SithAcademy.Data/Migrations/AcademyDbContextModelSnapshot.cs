﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SithAcademy.Data;

#nullable disable

namespace SithAcademy.Data.Migrations
{
    [DbContext(typeof(AcademyDbContext))]
    partial class AcademyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("SithAcademy.Data.Models.Academy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("ID of the academy");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasComment("Brief description of the academy");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)")
                        .HasComment("URL of the image that will be used to visualize the academy");

                    b.Property<bool>("IsLocked")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasComment("Boolean showing whether or not the academy is accessible for new acolytes");

                    b.Property<int>("LocationId")
                        .HasColumnType("int")
                        .HasComment("ID of the academy's location");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)")
                        .HasComment("Title of the academy");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Academies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "The facility known today as Dreshdae Academy was originally established by the disciples of Exar Kun during the Great Sith War. It has been abandoned and rebuilt several times throughout the millennia, each time emerging as a more prestigious school of Sith studies.",
                            ImageUrl = "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/73e29da9-1d35-4f3b-976c-29aa9e2e11f0/dce4e87-9372d1cf-1921-4eba-bb39-4af28ffdb86f.jpg?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOjdlMGQxODg5ODIyNjQzNzNhNWYwZDQxNWVhMGQyNmUwIiwiaXNzIjoidXJuOmFwcDo3ZTBkMTg4OTgyMjY0MzczYTVmMGQ0MTVlYTBkMjZlMCIsIm9iaiI6W1t7InBhdGgiOiJcL2ZcLzczZTI5ZGE5LTFkMzUtNGYzYi05NzZjLTI5YWE5ZTJlMTFmMFwvZGNlNGU4Ny05MzcyZDFjZi0xOTIxLTRlYmEtYmIzOS00YWYyOGZmZGI4NmYuanBnIn1dXSwiYXVkIjpbInVybjpzZXJ2aWNlOmZpbGUuZG93bmxvYWQiXX0.vuR2MPGfi8YudSSvkO-OltPr8bd_zDICvhu9DAWRnDk",
                            IsLocked = false,
                            LocationId = 2,
                            Title = "Dreshdae Academy"
                        },
                        new
                        {
                            Id = 2,
                            Description = "The current-day Dark Temple complex is positioned close to the original structure of the same name, which has been fully destroyed during the last major war with the Galactic Republic. The wilderness surrounding the complex is home to a great deal of deadly predators, which provides a natural training ground for acolytes and overseers alike.",
                            ImageUrl = "https://cdnb.artstation.com/p/assets/images/images/013/314/009/large/micah-brown-sith-temple-2.jpg?1539039990",
                            IsLocked = false,
                            LocationId = 1,
                            Title = "The Dark Temple"
                        });
                });

            modelBuilder.Entity("SithAcademy.Data.Models.AcademyAcolyte", b =>
                {
                    b.Property<int>("AcademyId")
                        .HasColumnType("int")
                        .HasComment("ID of the academy in which the acolyte is assigned to");

                    b.Property<Guid>("AcolyteId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("ID of the acolyte");

                    b.HasKey("AcademyId", "AcolyteId");

                    b.HasIndex("AcolyteId");

                    b.ToTable("AcademiesAcolytes");
                });

            modelBuilder.Entity("SithAcademy.Data.Models.AcademyUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int?>("LocationId")
                        .HasColumnType("int")
                        .HasComment("ID of the location on which the acolyte is currently located");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("SithAcademy.Data.Models.Homework", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("ID of the homework");

                    b.Property<Guid>("AcolyteId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("ID of the acolyte to which the homework belongs");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasComment("Content of the homework");

                    b.Property<bool>("IsApproved")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasComment("Boolean showing whether or not the homework has been approved");

                    b.Property<Guid>("TrialId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("ID of the trial for which the homework is");

                    b.HasKey("Id");

                    b.HasIndex("AcolyteId");

                    b.HasIndex("TrialId");

                    b.ToTable("Homeworks");
                });

            modelBuilder.Entity("SithAcademy.Data.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("ID of the location");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasComment("Brief description of the location");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)")
                        .HasComment("URL of the image that will be used to visualize the location");

                    b.Property<bool>("IsLocked")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasComment("Boolean showing whether or not the location is accessible for new acolytes");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)")
                        .HasComment("Name of the location");

                    b.HasKey("Id");

                    b.ToTable("Locations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Dromund Kaas was originally a colony world of the Sith Empire, and at one point its capital.Its atmosphere is heavily charged with electricity to the point where lightning is a near-constant sight in the almost perpetually clouded sky - a result of ancient Sith experiments in arcane and forbidden uses of the dark side of the Force.",
                            ImageUrl = "https://www.worldanvil.com/media/cache/cover/uploads/images/7c2913da4c331e69f6dfc4fd2225fb0f.jpg",
                            IsLocked = false,
                            Name = "Dromund Kaas"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Korriban was the original homeworld of the Sith species and a sacred planet for the Sith Order, housing the tombs of many ancient and powerful Dark Lords. Even to this day those tombs hold immense power and unfanthomable secrets, as well as untold horrors and the bleached bones of unlucky explorers.",
                            ImageUrl = "https://cdnb.artstation.com/p/assets/images/images/053/512/833/large/shiny-man-korriban-icon-01.jpg?1662395808",
                            IsLocked = false,
                            Name = "Korriban"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Ziost was originally covered with vast thick forests and possessed a warm climate, however a sudden ice age experienced during the initial Sith colonization leveled the vast woodlands as well as most evidence of the pre-existing civilization. As a result, the planet was transformed into a bitterly cold tundra with an arid climate, its surface covered with rocky terrain, ice-encrusted mountains and titanic glaciers.",
                            ImageUrl = "https://cdnb.artstation.com/p/assets/images/images/020/733/493/4k/brian-hagan-ziost.jpg?1568947978",
                            IsLocked = true,
                            Name = "Ziost"
                        });
                });

            modelBuilder.Entity("SithAcademy.Data.Models.Overseer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("ID of the overseer");

                    b.Property<int>("AcademyId")
                        .HasColumnType("int")
                        .HasComment("ID of the academy in which the overseer is assigned to");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasComment("Title of the overseer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("ID of the existing user that is also an overseer");

                    b.HasKey("Id");

                    b.HasIndex("AcademyId");

                    b.HasIndex("UserId");

                    b.ToTable("Overseers");
                });

            modelBuilder.Entity("SithAcademy.Data.Models.Resource", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("ID of the resource");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasComment("Boolean showing whether or not the resource should be displayed");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)")
                        .HasComment("Name of the resource");

                    b.Property<Guid>("TrialId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)")
                        .HasComment("URL for the resource's location");

                    b.HasKey("Id");

                    b.HasIndex("TrialId");

                    b.ToTable("Resources");

                    b.HasData(
                        new
                        {
                            Id = new Guid("fc34dc68-b10e-4c14-a1d9-3ad96b73f431"),
                            IsDeleted = false,
                            Name = "Hutt Cartel",
                            TrialId = new Guid("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9"),
                            Url = "starwars.fandom.com/wiki/Hutt_Cartel"
                        },
                        new
                        {
                            Id = new Guid("2c529f2b-d864-4dc7-b468-c44d630ec7c4"),
                            IsDeleted = false,
                            Name = "Black Sun",
                            TrialId = new Guid("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9"),
                            Url = "starwars.fandom.com/wiki/Black_Sun/Legends"
                        },
                        new
                        {
                            Id = new Guid("e6d39382-06ef-47f6-887c-a6f4e7806047"),
                            IsDeleted = false,
                            Name = "Bounty Hunters' Guild",
                            TrialId = new Guid("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9"),
                            Url = "starwars.fandom.com/wiki/Bounty_Hunters'_Guild/Legends"
                        },
                        new
                        {
                            Id = new Guid("559e40bd-13fa-47db-947e-0f087b3496a5"),
                            IsDeleted = false,
                            Name = "Spice",
                            TrialId = new Guid("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9"),
                            Url = "starwars.fandom.com/wiki/Spice/Legends"
                        },
                        new
                        {
                            Id = new Guid("b9da4d71-52bc-451e-951f-c46e04e8293c"),
                            IsDeleted = false,
                            Name = "History of the Valley of the Dark Lords",
                            TrialId = new Guid("aa37b907-5d8b-439c-a719-2a784c07744a"),
                            Url = "starwars.fandom.com/wiki/Valley_of_the_Dark_Lords/Legends"
                        },
                        new
                        {
                            Id = new Guid("1d15dbcc-67b8-4597-b32a-d9d54a91bb85"),
                            IsDeleted = false,
                            Name = "K'lor'slug",
                            TrialId = new Guid("aa37b907-5d8b-439c-a719-2a784c07744a"),
                            Url = "starwars.fandom.com/wiki/K'lor'slug/Legends"
                        },
                        new
                        {
                            Id = new Guid("a6def1fb-93d8-43f2-bd5c-6d3bdf220694"),
                            IsDeleted = false,
                            Name = "Shyrack",
                            TrialId = new Guid("aa37b907-5d8b-439c-a719-2a784c07744a"),
                            Url = "starwars.fandom.com/wiki/Shyrack/Legends"
                        },
                        new
                        {
                            Id = new Guid("479a9611-5af8-4ebf-aa05-95d3d21397f6"),
                            IsDeleted = false,
                            Name = "Tuk'ata",
                            TrialId = new Guid("aa37b907-5d8b-439c-a719-2a784c07744a"),
                            Url = "starwars.fandom.com/wiki/Tuk'ata/Legends"
                        },
                        new
                        {
                            Id = new Guid("e76679a2-14a4-4e91-8a06-c972da405f05"),
                            IsDeleted = false,
                            Name = "Study the origins of the clans you will encounter",
                            TrialId = new Guid("9595a701-973a-4d7c-819d-93efcfbf9fa8"),
                            Url = "starwars.fandom.com/wiki/Prophets_of_the_Dark_Side"
                        },
                        new
                        {
                            Id = new Guid("de19a886-21a2-4550-ac26-34134ccf2268"),
                            IsDeleted = false,
                            Name = "Jurgoran",
                            TrialId = new Guid("b92c1895-a6ef-422d-b760-298a0785b612"),
                            Url = "starwars.fandom.com/wiki/Jurgoran"
                        },
                        new
                        {
                            Id = new Guid("b2b42c49-9fde-43cc-a409-5df9c1e7c774"),
                            IsDeleted = false,
                            Name = "Gundark",
                            TrialId = new Guid("b92c1895-a6ef-422d-b760-298a0785b612"),
                            Url = "starwars.fandom.com/wiki/Gundark/Legends"
                        },
                        new
                        {
                            Id = new Guid("ff04a297-c227-4f02-8b0c-772f4213e6a9"),
                            IsDeleted = false,
                            Name = "Vine cat",
                            TrialId = new Guid("b92c1895-a6ef-422d-b760-298a0785b612"),
                            Url = "starwars.fandom.com/wiki/Vine_cat"
                        },
                        new
                        {
                            Id = new Guid("30bc967b-9c02-400b-b363-fc12f4929336"),
                            IsDeleted = false,
                            Name = "Yozusk",
                            TrialId = new Guid("b92c1895-a6ef-422d-b760-298a0785b612"),
                            Url = "starwars.fandom.com/wiki/Yozusk"
                        });
                });

            modelBuilder.Entity("SithAcademy.Data.Models.Trial", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("ID of the trial");

                    b.Property<int>("AcademyId")
                        .HasColumnType("int")
                        .HasComment("ID of the academy which hosts the trial");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasComment("Description of the trial");

                    b.Property<bool>("IsLocked")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasComment("Boolean showing whether or not the trial can be participated in");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)")
                        .HasComment("Title of the trial");

                    b.HasKey("Id");

                    b.HasIndex("AcademyId");

                    b.ToTable("Trials");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9"),
                            AcademyId = 1,
                            Description = "Dreshdae has a thriving population of underworld elements. Smugglers, bounty hunters, slavers, pirates. Mingle with them. Understand their passions. Succeed in this endeavour, and you will be able to control them.",
                            IsLocked = false,
                            Title = "Trial of Passion"
                        },
                        new
                        {
                            Id = new Guid("aa37b907-5d8b-439c-a719-2a784c07744a"),
                            AcademyId = 1,
                            Description = "Only the strongest of Sith earn the honour of resting in the Valley of the Dark Lords. Study their feats and histories.Explore their tombs to gain an understanding of what it takes to be Sith. Beware the Valley's guardians.",
                            IsLocked = false,
                            Title = "Trial of Strength"
                        },
                        new
                        {
                            Id = new Guid("9595a701-973a-4d7c-819d-93efcfbf9fa8"),
                            AcademyId = 2,
                            Description = "True power comes to the cunning. Remnants of a failed empire still eke out an existence amidst the endless jungles. Infiltrate one of warring clans and make them do your bidding. Do not underestimate the power of the superstitious mind.",
                            IsLocked = false,
                            Title = "Trial of Power"
                        },
                        new
                        {
                            Id = new Guid("b92c1895-a6ef-422d-b760-298a0785b612"),
                            AcademyId = 2,
                            Description = "A Sith must accept nothing less than the complete destruction of their enemies. Venture out into the wilderness. Observe the primal savagery of the beasts while taking note of their weaknesses. Return with proof of your victory over them.",
                            IsLocked = false,
                            Title = "Trial of Victory"
                        });
                });

            modelBuilder.Entity("SithAcademy.Data.Models.TrialAcolyte", b =>
                {
                    b.Property<Guid>("TrialId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("ID of the trial which the acolyte must complete");

                    b.Property<Guid>("AcolyteId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("ID of the acolyte");

                    b.Property<bool>("IsCompleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasComment("Boolean showing whether or not the acolyte has an approved homework for the trial");

                    b.HasKey("TrialId", "AcolyteId");

                    b.HasIndex("AcolyteId");

                    b.ToTable("TrialsAcolytes");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("SithAcademy.Data.Models.AcademyUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("SithAcademy.Data.Models.AcademyUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SithAcademy.Data.Models.AcademyUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("SithAcademy.Data.Models.AcademyUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SithAcademy.Data.Models.Academy", b =>
                {
                    b.HasOne("SithAcademy.Data.Models.Location", "Location")
                        .WithMany("Academies")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("SithAcademy.Data.Models.AcademyAcolyte", b =>
                {
                    b.HasOne("SithAcademy.Data.Models.Academy", "Academy")
                        .WithMany("Acolytes")
                        .HasForeignKey("AcademyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SithAcademy.Data.Models.AcademyUser", "Acolyte")
                        .WithMany("JoinedAcademies")
                        .HasForeignKey("AcolyteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Academy");

                    b.Navigation("Acolyte");
                });

            modelBuilder.Entity("SithAcademy.Data.Models.AcademyUser", b =>
                {
                    b.HasOne("SithAcademy.Data.Models.Location", "Location")
                        .WithMany("Acolytes")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Location");
                });

            modelBuilder.Entity("SithAcademy.Data.Models.Homework", b =>
                {
                    b.HasOne("SithAcademy.Data.Models.AcademyUser", "Acolyte")
                        .WithMany("PublishedHomeworks")
                        .HasForeignKey("AcolyteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SithAcademy.Data.Models.Trial", "Trial")
                        .WithMany("PublishedHomeworks")
                        .HasForeignKey("TrialId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Acolyte");

                    b.Navigation("Trial");
                });

            modelBuilder.Entity("SithAcademy.Data.Models.Overseer", b =>
                {
                    b.HasOne("SithAcademy.Data.Models.Academy", "Academy")
                        .WithMany("Overseers")
                        .HasForeignKey("AcademyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SithAcademy.Data.Models.AcademyUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Academy");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SithAcademy.Data.Models.Resource", b =>
                {
                    b.HasOne("SithAcademy.Data.Models.Trial", "Trial")
                        .WithMany("Resources")
                        .HasForeignKey("TrialId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Trial");
                });

            modelBuilder.Entity("SithAcademy.Data.Models.Trial", b =>
                {
                    b.HasOne("SithAcademy.Data.Models.Academy", "Academy")
                        .WithMany("Trials")
                        .HasForeignKey("AcademyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Academy");
                });

            modelBuilder.Entity("SithAcademy.Data.Models.TrialAcolyte", b =>
                {
                    b.HasOne("SithAcademy.Data.Models.AcademyUser", "Acolyte")
                        .WithMany("AssignedTrials")
                        .HasForeignKey("AcolyteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SithAcademy.Data.Models.Trial", "Trial")
                        .WithMany("AssignedAcolytes")
                        .HasForeignKey("TrialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Acolyte");

                    b.Navigation("Trial");
                });

            modelBuilder.Entity("SithAcademy.Data.Models.Academy", b =>
                {
                    b.Navigation("Acolytes");

                    b.Navigation("Overseers");

                    b.Navigation("Trials");
                });

            modelBuilder.Entity("SithAcademy.Data.Models.AcademyUser", b =>
                {
                    b.Navigation("AssignedTrials");

                    b.Navigation("JoinedAcademies");

                    b.Navigation("PublishedHomeworks");
                });

            modelBuilder.Entity("SithAcademy.Data.Models.Location", b =>
                {
                    b.Navigation("Academies");

                    b.Navigation("Acolytes");
                });

            modelBuilder.Entity("SithAcademy.Data.Models.Trial", b =>
                {
                    b.Navigation("AssignedAcolytes");

                    b.Navigation("PublishedHomeworks");

                    b.Navigation("Resources");
                });
#pragma warning restore 612, 618
        }
    }
}
