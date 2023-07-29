namespace SithAcademy.Data.Seeders;

using SithAcademy.Data.Models;

internal class OverseerSeeder
{ 
    internal Overseer[] GenerateOverseers()
    {
        ICollection<Overseer> overseers = new HashSet<Overseer>();
        Overseer overseer;

        overseer = new Overseer()
        {
            Id = Guid.Parse("257d9119-875f-46b8-b205-a3b447cc6661"),
            Title = "Insert Very Cool Title Here",
            UserId = Guid.Parse("e1cd947b-04b7-4a29-a2c2-d5383dd294e4"),
            AcademyId = 1
        };
        overseers.Add(overseer);

        return overseers.ToArray();
    }
}
