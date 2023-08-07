namespace SithAcademy.Data.Seeders;

using SithAcademy.Data.Models;

internal class HomeworkSeeder
{
    internal Homework[] GenerateHomeworks()
    {
        ICollection<Homework> homeworks = new HashSet<Homework>();
        Homework homework;

        homework = new Homework()
        {
            Content = "This is user DreshdaeOverseer's homework for the Trial of Passion.",
            Score = 10.0m,
            ReviewerName = "The Dark Side itself",
            ReviewerFeedback = "Very good and excellent homework!",
            TrialId = Guid.Parse("1ad699ea-450b-48fe-8b3a-59e4f4ed61a9"),
            AcolyteId = Guid.Parse("e1cd947b-04b7-4a29-a2c2-d5383dd294e4")
        };
        homeworks.Add(homework);

        homework = new Homework()
        {
            Content = "This is user DreshdaeOverseer's homework for the Trial of Strength.",
            Score = 10.0m,
            ReviewerName = "The Dark Side itself",
            ReviewerFeedback = "Very good and excellent homework!",
            TrialId = Guid.Parse("aa37b907-5d8b-439c-a719-2a784c07744a"),
            AcolyteId = Guid.Parse("e1cd947b-04b7-4a29-a2c2-d5383dd294e4")
        };
        homeworks.Add(homework);

        homework = new Homework()
        {
            Content = "This is user DarkTempleOverseer's homework for the Trial of Power.",
            Score = 10.0m,
            ReviewerName = "The Dark Side itself",
            ReviewerFeedback = "Very good and excellent homework!",
            TrialId = Guid.Parse("9595a701-973a-4d7c-819d-93efcfbf9fa8"),
            AcolyteId = Guid.Parse("94ee6c77-02d6-44b4-8ef0-99d313d30bb8")
        };
        homeworks.Add(homework);

        homework = new Homework()
        {
            Content = "This is user DarkTempleOverseer's homework for the Trial of Victory.",
            Score = 10.0m,
            ReviewerName = "The Dark Side itself",
            ReviewerFeedback = "Very good and excellent homework!",
            TrialId = Guid.Parse("b92c1895-a6ef-422d-b760-298a0785b612"),
            AcolyteId = Guid.Parse("94ee6c77-02d6-44b4-8ef0-99d313d30bb8")
        };
        homeworks.Add(homework);

        return homeworks.ToArray();
    }
}
