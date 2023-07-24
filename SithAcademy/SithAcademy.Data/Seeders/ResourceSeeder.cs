namespace SithAcademy.Data.Seeders;

using SithAcademy.Data.Models;

internal class ResourceSeeder
{
    internal Resource[] GenerateResources()
    {
        ICollection<Resource> resources = new HashSet<Resource>();
        Resource resource;

        resource = new Resource()
        {
            Id = Guid.Parse("fc34dc68-b10e-4c14-a1d9-3ad96b73f431"),
            Name = "Hutt Cartel",
            SourceUrl = "https://starwars.fandom.com/wiki/Hutt_Cartel",
            ImageUrl = "https://media.moddb.com/images/mods/1/19/18461/hutt_fleet_by_wrait.jpg",
            TrialId = Guid.Parse("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9")
        };
        resources.Add(resource);

        resource = new Resource()
        {
            Id = Guid.Parse("2c529f2b-d864-4dc7-b468-c44d630ec7c4"),
            Name = "Black Sun",
            SourceUrl = "https://starwars.fandom.com/wiki/Black_Sun/Legends",
            ImageUrl = "https://overmental.com/wp-content/uploads/2015/07/PrinceXizorart.png",
            TrialId = Guid.Parse("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9")
        };
        resources.Add(resource);

        resource = new Resource()
        {
            Id = Guid.Parse("e6d39382-06ef-47f6-887c-a6f4e7806047"),
            Name = "Bounty Hunters' Guild",
            SourceUrl = "https://starwars.fandom.com/wiki/Bounty_Hunters'_Guild/Legends",
            ImageUrl = "https://www.worldanvil.com/media/cache/cover/uploads/images/e98ee0f248cd6fff599458a47aa7c1d4.jpg",
            TrialId = Guid.Parse("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9")
        };
        resources.Add(resource);

        resource = new Resource()
        {
            Id = Guid.Parse("559e40bd-13fa-47db-947e-0f087b3496a5"),
            Name = "Spice",
            SourceUrl = "https://starwars.fandom.com/wiki/Spice/Legends",
            ImageUrl = "https://fictionhorizon.com/wp-content/uploads/2022/01/pykes.jpg",
            TrialId = Guid.Parse("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9")
        };
        resources.Add(resource);

        resource = new Resource()
        {
            Id = Guid.Parse("b9da4d71-52bc-451e-951f-c46e04e8293c"),
            Name = "History of the Valley of the Dark Lords",
            SourceUrl = "https://starwars.fandom.com/wiki/Valley_of_the_Dark_Lords/Legends",
            ImageUrl = "https://ddx5i92cqts4o.cloudfront.net/images/1ejq0l57t_Fearful_Landscape_CotG.png",
            TrialId = Guid.Parse("aa37b907-5d8b-439c-a719-2a784c07744a")
        };
        resources.Add(resource);

        resource = new Resource()
        {
            Id = Guid.Parse("1d15dbcc-67b8-4597-b32a-d9d54a91bb85"),
            Name = "K'lor'slug",
            SourceUrl = "https://starwars.fandom.com/wiki/K'lor'slug/Legends",
            ImageUrl = "https://static.wikia.nocookie.net/aliens/images/b/b7/K'lor'slug.png",
            TrialId = Guid.Parse("aa37b907-5d8b-439c-a719-2a784c07744a")
        };
        resources.Add(resource);

        resource = new Resource()
        {
            Id = Guid.Parse("a6def1fb-93d8-43f2-bd5c-6d3bdf220694"),
            Name = "Shyrack",
            SourceUrl = "https://starwars.fandom.com/wiki/Shyrack/Legends",
            ImageUrl = "https://pm1.aminoapps.com/6935/bb60fc764e739d6da08e25ae84d038c1885192ecr1-526-493v2_hq.jpg",
            TrialId = Guid.Parse("aa37b907-5d8b-439c-a719-2a784c07744a")
        };
        resources.Add(resource);

        resource = new Resource()
        {
            Id = Guid.Parse("479a9611-5af8-4ebf-aa05-95d3d21397f6"),
            Name = "Tuk'ata",
            SourceUrl = "https://starwars.fandom.com/wiki/Tuk'ata/Legends",
            ImageUrl = "https://pm1.aminoapps.com/5870/1727f7e20f6ef47d8605c148ff71bdabc8c9df3f_hq.jpg",
            TrialId = Guid.Parse("aa37b907-5d8b-439c-a719-2a784c07744a")
        };
        resources.Add(resource);

        resource = new Resource()
        {
            Id = Guid.Parse("e76679a2-14a4-4e91-8a06-c972da405f05"),
            Name = "Study the origins of the clans you will encounter",
            SourceUrl = "https://starwars.fandom.com/wiki/Prophets_of_the_Dark_Side",
            ImageUrl = "https://static.wikia.nocookie.net/starwars/images/b/b1/Darthmill.jpg/",
            TrialId = Guid.Parse("9595a701-973a-4d7c-819d-93efcfbf9fa8")
        };
        resources.Add(resource);

        resource = new Resource()
        {
            Id = Guid.Parse("de19a886-21a2-4550-ac26-34134ccf2268"),
            Name = "Jurgoran",
            SourceUrl = "https://starwars.fandom.com/wiki/Jurgoran",
            ImageUrl = "https://pm1.aminoapps.com/6435/a30efbffef3abff7d2860524cb52b48aba89181d_hq.jpg",
            TrialId = Guid.Parse("b92c1895-a6ef-422d-b760-298a0785b612")
        };
        resources.Add(resource);

        resource = new Resource()
        {
            Id = Guid.Parse("b2b42c49-9fde-43cc-a409-5df9c1e7c774"),
            Name = "Gundark",
            SourceUrl = "https://starwars.fandom.com/wiki/Gundark/Legends",
            ImageUrl = "https://www.gamesmanagers.com/images/posts/bc2ec369d3e6cf31786544799d3088c8-2.jpg",
            TrialId = Guid.Parse("b92c1895-a6ef-422d-b760-298a0785b612")
        };
        resources.Add(resource);

        resource = new Resource()
        {
            Id = Guid.Parse("ff04a297-c227-4f02-8b0c-772f4213e6a9"),
            Name = "Vine cat",
            SourceUrl = "https://starwars.fandom.com/wiki/Vine_cat",
            ImageUrl = "https://oyster.ignimgs.com/mediawiki/apis.ign.com/star-wars-the-old-republic/1/15/Ss_vinecat01_800x450.jpg",
            TrialId = Guid.Parse("b92c1895-a6ef-422d-b760-298a0785b612")
        };
        resources.Add(resource);

        resource = new Resource()
        {
            Id = Guid.Parse("30bc967b-9c02-400b-b363-fc12f4929336"),
            Name = "Yozusk",
            SourceUrl = "https://starwars.fandom.com/wiki/Yozusk",
            ImageUrl = "https://pm1.aminoapps.com/6435/a93762efd24f62a2395dd82b349729d35245d004_hq.jpg",
            TrialId = Guid.Parse("b92c1895-a6ef-422d-b760-298a0785b612")
        };
        resources.Add(resource);

        return resources.ToArray();
    }
}
