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
            ImageUrl = "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/73e29da9-1d35-4f3b-976c-29aa9e2e11f0/dce4e87-9372d1cf-1921-4eba-bb39-4af28ffdb86f.jpg?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOjdlMGQxODg5ODIyNjQzNzNhNWYwZDQxNWVhMGQyNmUwIiwiaXNzIjoidXJuOmFwcDo3ZTBkMTg4OTgyMjY0MzczYTVmMGQ0MTVlYTBkMjZlMCIsIm9iaiI6W1t7InBhdGgiOiJcL2ZcLzczZTI5ZGE5LTFkMzUtNGYzYi05NzZjLTI5YWE5ZTJlMTFmMFwvZGNlNGU4Ny05MzcyZDFjZi0xOTIxLTRlYmEtYmIzOS00YWYyOGZmZGI4NmYuanBnIn1dXSwiYXVkIjpbInVybjpzZXJ2aWNlOmZpbGUuZG93bmxvYWQiXX0.vuR2MPGfi8YudSSvkO-OltPr8bd_zDICvhu9DAWRnDk",
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
            ImageUrl = "https://cdnb.artstation.com/p/assets/images/images/013/314/009/large/micah-brown-sith-temple-2.jpg?1539039990",
            LocationId = 1
        };
        academies.Add(academy);

        return academies.ToArray();
    }
}
