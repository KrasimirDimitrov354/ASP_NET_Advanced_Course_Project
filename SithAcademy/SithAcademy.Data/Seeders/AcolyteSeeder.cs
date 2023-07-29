namespace SithAcademy.Data.Seeders;

using Microsoft.AspNetCore.Identity;

using SithAcademy.Data.Models;

internal class AcolyteSeeder
{
    internal AcademyUser[] GenerateAcolytes()
    {
        ICollection<AcademyUser> acolytes = new HashSet<AcademyUser>();
        AcademyUser acolyte;

        PasswordHasher<AcademyUser> hasher = new PasswordHasher<AcademyUser>();

        acolyte = new AcademyUser()
        {
            Id = Guid.Parse("04589b17-3b3a-4118-b34d-dbfc70cd31f0"),
            UserName = "DefaultAcolyte",
            NormalizedUserName = "DEFAULTACOLYTE",
            Email = "acolyte@acolyte.com",
            NormalizedEmail = "ACOLYTE@ACOLYTE.COM",
            EmailConfirmed = false,
            LockoutEnabled = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        string acolyteHash = hasher.HashPassword(acolyte, "123");
        acolyte.PasswordHash = acolyteHash;
        acolytes.Add(acolyte);

        acolyte = new AcademyUser()
        {
            Id = Guid.Parse("e1cd947b-04b7-4a29-a2c2-d5383dd294e4"),
            UserName = "DefaultOverseer",
            NormalizedUserName = "DEFAULTOVERSEER",
            Email = "overseer@overseer.com",
            NormalizedEmail = "OVERSEER@OVERSEER.COM",
            EmailConfirmed = false,
            LockoutEnabled = true,
            SecurityStamp = Guid.NewGuid().ToString(),
            LocationId = 2
        };
        string overseerHash = hasher.HashPassword(acolyte, "456");
        acolyte.PasswordHash = overseerHash;
        acolytes.Add(acolyte);

        return acolytes.ToArray();
    }
}
