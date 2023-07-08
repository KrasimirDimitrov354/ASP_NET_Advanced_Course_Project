namespace SithAcademy.Data.Seeders;

using SithAcademy.Data.Models;

internal class LocationSeeder
{
    internal Location[] GenerateLocations()
    {
        ICollection<Location> locations = new HashSet<Location>();
        Location location;

        location = new Location()
        {
            Id = 1,
            Name = "Dromund Kaas",
            Description = "Dromund Kaas was originally a colony world of the Sith Empire, and at one point its capital." +
            "Its atmosphere is heavily charged with electricity to the point where lightning is a near-constant sight in the almost perpetually clouded sky - " + 
            "a result of ancient Sith experiments in arcane and forbidden uses of the dark side of the Force."
        };
        locations.Add(location);

        location = new Location()
        {
            Id = 2,
            Name = "Korriban",
            Description = "Korriban was the original homeworld of the Sith species and a sacred planet for the Sith Order, " +
            "housing the tombs of many ancient and powerful Dark Lords. Even to this day those tombs hold immense power and unfanthomable secrets, " +
            "as well as untold horrors and the bleached bones of unlucky explorers."
        };
        locations.Add(location);

        location = new Location()
        {
            Id = 3,
            Name = "Ziost",
            Description = "Ziost was originally covered with vast thick forests and possessed a warm climate, " + 
            "however a sudden ice age experienced during the initial Sith colonization leveled the vast woodlands as well as most evidence of the pre-existing civilization. " +
            "As a result, the planet was transformed into a bitterly cold tundra with an arid climate, its surface covered with rocky terrain, ice-encrusted mountains and titanic glaciers."
        };
        locations.Add(location);

        return locations.ToArray();
    }
}
