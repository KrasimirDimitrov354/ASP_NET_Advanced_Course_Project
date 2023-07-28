namespace SithAcademy.Data.Seeders;

using SithAcademy.Data.Models;

internal class TrialSeeder
{
    internal Trial[] GenerateTrials()
    {
        ICollection<Trial> trials = new HashSet<Trial>();
        Trial trial;

        trial = new Trial()
        {
            Id = Guid.Parse("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9"),
            Title = "Trial of Passion",
            Description = "Dreshdae has a thriving population of underworld elements. Smugglers, bounty hunters, slavers, pirates. " + 
            "Mingle with them. Understand their passions. Succeed in this endeavour, and you will be able to control them.",
            ScoreToPass = 6.5m,
            AcademyId = 1
        };
        trials.Add(trial);

        trial = new Trial()
        {
            Id = Guid.Parse("aa37b907-5d8b-439c-a719-2a784c07744a"),
            Title = "Trial of Strength",
            Description = "Only the strongest of Sith earn the honour of resting in the Valley of the Dark Lords. Study their feats and histories. " + 
            "Explore their tombs to gain an understanding of what it takes to be Sith. Beware the Valley's guardians.",
            ScoreToPass = 7.0m,
            AcademyId = 1
        };
        trials.Add(trial);

        trial = new Trial()
        {
            Id = Guid.Parse("9595a701-973a-4d7c-819d-93efcfbf9fa8"),
            Title = "Trial of Power",
            Description = "True power comes to the cunning. Remnants of a failed empire still eke out an existence amidst the endless jungles. " +
            "Infiltrate one of warring clans and make them do your bidding. Do not underestimate the power of the superstitious mind.",
            ScoreToPass = 7.5m,
            AcademyId = 2
        };
        trials.Add(trial);

        trial = new Trial()
        {
            Id = Guid.Parse("b92c1895-a6ef-422d-b760-298a0785b612"),
            Title = "Trial of Victory",
            Description = "A Sith must accept nothing less than the complete destruction of their enemies. Venture out into the wilderness. " +
            "Observe the primal savagery of the beasts while taking note of their weaknesses. Return with proof of your victory over them.",
            ScoreToPass = 8.0m,
            AcademyId = 2
        };
        trials.Add(trial);

        return trials.ToArray();
    }
}
