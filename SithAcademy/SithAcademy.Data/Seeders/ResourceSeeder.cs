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
            Url = "starwars.fandom.com/wiki/Hutt_Cartel",
            TrialId = Guid.Parse("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9")
        };
        resources.Add(resource);

        resource = new Resource()
        {
            Id = Guid.Parse("2c529f2b-d864-4dc7-b468-c44d630ec7c4"),
            Name = "Black Sun",
            Url = "starwars.fandom.com/wiki/Black_Sun/Legends",
            TrialId = Guid.Parse("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9")
        };
        resources.Add(resource);

        resource = new Resource()
        {
            Id = Guid.Parse("e6d39382-06ef-47f6-887c-a6f4e7806047"),
            Name = "Bounty Hunters' Guild",
            Url = "starwars.fandom.com/wiki/Bounty_Hunters'_Guild/Legends",
            TrialId = Guid.Parse("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9")
        };
        resources.Add(resource);

        resource = new Resource()
        {
            Id = Guid.Parse("559e40bd-13fa-47db-947e-0f087b3496a5"),
            Name = "Spice",
            Url = "starwars.fandom.com/wiki/Spice/Legends",
            TrialId = Guid.Parse("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9")
        };
        resources.Add(resource);

        resource = new Resource()
        {
            Id = Guid.Parse("b9da4d71-52bc-451e-951f-c46e04e8293c"),
            Name = "History of the Valley of the Dark Lords",
            Url = "starwars.fandom.com/wiki/Valley_of_the_Dark_Lords/Legends",
            TrialId = Guid.Parse("aa37b907-5d8b-439c-a719-2a784c07744a")
        };
        resources.Add(resource);

        resource = new Resource()
        {
            Id = Guid.Parse("1d15dbcc-67b8-4597-b32a-d9d54a91bb85"),
            Name = "K'lor'slug",
            Url = "starwars.fandom.com/wiki/K'lor'slug/Legends",
            TrialId = Guid.Parse("aa37b907-5d8b-439c-a719-2a784c07744a")
        };
        resources.Add(resource);

        resource = new Resource()
        {
            Id = Guid.Parse("a6def1fb-93d8-43f2-bd5c-6d3bdf220694"),
            Name = "Shyrack",
            Url = "starwars.fandom.com/wiki/Shyrack/Legends",
            TrialId = Guid.Parse("aa37b907-5d8b-439c-a719-2a784c07744a")
        };
        resources.Add(resource);

        resource = new Resource()
        {
            Id = Guid.Parse("479a9611-5af8-4ebf-aa05-95d3d21397f6"),
            Name = "Tuk'ata",
            Url = "starwars.fandom.com/wiki/Tuk'ata/Legends",
            TrialId = Guid.Parse("aa37b907-5d8b-439c-a719-2a784c07744a")
        };
        resources.Add(resource);

        resource = new Resource()
        {
            Id = Guid.Parse("e76679a2-14a4-4e91-8a06-c972da405f05"),
            Name = "Study the origins of the clans you will encounter",
            Url = "starwars.fandom.com/wiki/Prophets_of_the_Dark_Side",
            TrialId = Guid.Parse("9595a701-973a-4d7c-819d-93efcfbf9fa8")
        };
        resources.Add(resource);

        resource = new Resource()
        {
            Id = Guid.Parse("de19a886-21a2-4550-ac26-34134ccf2268"),
            Name = "Jurgoran",
            Url = "starwars.fandom.com/wiki/Jurgoran",
            TrialId = Guid.Parse("b92c1895-a6ef-422d-b760-298a0785b612")
        };
        resources.Add(resource);

        resource = new Resource()
        {
            Id = Guid.Parse("b2b42c49-9fde-43cc-a409-5df9c1e7c774"),
            Name = "Gundark",
            Url = "starwars.fandom.com/wiki/Gundark/Legends",
            TrialId = Guid.Parse("b92c1895-a6ef-422d-b760-298a0785b612")
        };
        resources.Add(resource);

        resource = new Resource()
        {
            Id = Guid.Parse("ff04a297-c227-4f02-8b0c-772f4213e6a9"),
            Name = "Vine cat",
            Url = "starwars.fandom.com/wiki/Vine_cat",
            TrialId = Guid.Parse("b92c1895-a6ef-422d-b760-298a0785b612")
        };
        resources.Add(resource);

        resource = new Resource()
        {
            Id = Guid.Parse("30bc967b-9c02-400b-b363-fc12f4929336"),
            Name = "Yozusk",
            Url = "starwars.fandom.com/wiki/Yozusk",
            TrialId = Guid.Parse("b92c1895-a6ef-422d-b760-298a0785b612")
        };
        resources.Add(resource);

        return resources.ToArray();
    }
}
