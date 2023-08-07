namespace SithAcademy.Data.Seeders;

using SithAcademy.Data.Models;

internal class AcademyAcolyteSeeder
{
    internal AcademyAcolyte[] GenerateAcademyAcolytes()
    {
        ICollection<AcademyAcolyte> academiesAcolytes = new HashSet<AcademyAcolyte>();
        AcademyAcolyte academyAcolyte;

        academyAcolyte = new AcademyAcolyte()
        {
            AcademyId = 1,
            AcolyteId = Guid.Parse("e1cd947b-04b7-4a29-a2c2-d5383dd294e4")
        };
        academiesAcolytes.Add(academyAcolyte);

        academyAcolyte = new AcademyAcolyte()
        {
            AcademyId = 2,
            AcolyteId = Guid.Parse("94ee6c77-02d6-44b4-8ef0-99d313d30bb8")
        };
        academiesAcolytes.Add(academyAcolyte);

        return academiesAcolytes.ToArray();
    }
}
