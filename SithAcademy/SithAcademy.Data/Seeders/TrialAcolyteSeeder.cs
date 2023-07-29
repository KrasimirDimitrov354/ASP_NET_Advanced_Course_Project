namespace SithAcademy.Data.Seeders;

using SithAcademy.Data.Models;

internal class TrialAcolyteSeeder
{
    internal TrialAcolyte[] GenerateTrialAcolytes()
    {
        ICollection<TrialAcolyte> trialsAcolytes = new HashSet<TrialAcolyte>();
        TrialAcolyte trialAcolyte;

        trialAcolyte = new TrialAcolyte()
        {
            TrialId = Guid.Parse("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9"),
            AcolyteId = Guid.Parse("e1cd947b-04b7-4a29-a2c2-d5383dd294e4"),
            IsCompleted = true,
        };
        trialsAcolytes.Add(trialAcolyte);

        trialAcolyte = new TrialAcolyte()
        {
            TrialId = Guid.Parse("aa37b907-5d8b-439c-a719-2a784c07744a"),
            AcolyteId = Guid.Parse("e1cd947b-04b7-4a29-a2c2-d5383dd294e4"),
            IsCompleted = true,
        };
        trialsAcolytes.Add(trialAcolyte);

        return trialsAcolytes.ToArray();
    }
}
