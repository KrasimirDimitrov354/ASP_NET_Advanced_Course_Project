namespace SithAcademy.Data.Seeders;

using SithAcademy.Data.Models;

internal class AcademySeeder
{
    internal Academy[] GenerateAcademies()
    {
        ICollection<Academy> academies = new HashSet<Academy>();
        Academy academy;

        academy = new Academy()
        {
            Id = 1,
            Title = "Dreshdae Academy",
            Description = "The facility known today as Dreshdae Academy was originally established by the disciples of Exar Kun during the Great Sith War. " +
            "It has been abandoned and rebuilt several times throughout the millennia, each time emerging as a more prestigious school of Sith studies.",
            LocationId = 2
        };
        academies.Add(academy);

        academy = new Academy()
        {
            Id = 2,
            Title = "The Dark Temple",
            Description = "The current-day Dark Temple complex is positioned close to the original structure of the same name, which has been fully destroyed during the " +
            "last major war with the Galactic Republic. The wilderness surrounding the complex is home to a great deal of deadly predators, which provides a natural " +
            "training ground for acolytes and overseers alike.",
            LocationId = 1
        };
        academies.Add(academy);

        return academies.ToArray();
    }
}
