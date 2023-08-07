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
            UserName = "DreshdaeOverseer",
            NormalizedUserName = "DRESHDAEOVERSEER",
            Email = "overseer@overseer.com",
            NormalizedEmail = "OVERSEER@OVERSEER.COM",
            EmailConfirmed = false,
            LockoutEnabled = true,
            SecurityStamp = Guid.NewGuid().ToString(),
            LocationId = 2
        };
        string dreshdaeHash = hasher.HashPassword(acolyte, "456");
        acolyte.PasswordHash = dreshdaeHash;
        acolytes.Add(acolyte);

        acolyte = new AcademyUser()
        {
            Id = Guid.Parse("94ee6c77-02d6-44b4-8ef0-99d313d30bb8"),
            UserName = "DarkTempleOverseer",
            NormalizedUserName = "DARKTEMPLEOVERSEER",
            Email = "overseer@overseer.com",
            NormalizedEmail = "OVERSEER@OVERSEER.COM",
            EmailConfirmed = false,
            LockoutEnabled = true,
            SecurityStamp = Guid.NewGuid().ToString(),
            LocationId = 1
        };
        string darkTempleHash = hasher.HashPassword(acolyte, "789");
        acolyte.PasswordHash = darkTempleHash;
        acolytes.Add(acolyte);

        acolyte = new AcademyUser()
        {
            Id = Guid.Parse("a7fba81f-237e-4c59-8fe5-7a5e2c40e403"),
            UserName = "Administrator",
            NormalizedUserName = "ADMINISTRATOR",
            Email = "admin@sithacademy.com",
            NormalizedEmail = "ADMIN@SITHACADEMY.COM",
            EmailConfirmed = false,
            LockoutEnabled = true,
            SecurityStamp = Guid.NewGuid().ToString(),
        };
        string adminHash = hasher.HashPassword(acolyte, "admin");
        acolyte.PasswordHash = adminHash;
        acolytes.Add(acolyte);

        return acolytes.ToArray();
    }
}
