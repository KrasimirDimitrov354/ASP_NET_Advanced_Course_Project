namespace SithAcademy.Services.Data;

using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using SithAcademy.Data;
using SithAcademy.Data.Models;
using SithAcademy.Web.ViewModels.Academy;
using SithAcademy.Web.ViewModels.Location;
using SithAcademy.Services.Data.Interfaces;

public class LocationService : ILocationService
{
    private readonly AcademyDbContext dbContext;

    public LocationService(AcademyDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<LocationOverviewViewModel>> GetAllLocationsAsync()
    {
        IEnumerable<LocationOverviewViewModel> locations = await dbContext.Locations
            .Select(l => new LocationOverviewViewModel()
            {
                Id = l.Id,
                Name = l.Name,
                ImageUrl = l.ImageUrl
            })
            .ToArrayAsync();

        return locations;
    }

    public async Task<LocationDetailsViewModel> DisplayLocationDetailsAsync(int locationId)
    {
        LocationDetailsViewModel location = await dbContext.Locations
            .Where(l => l.Id == locationId)
            .Select(l => new LocationDetailsViewModel()
            {
                Id = l.Id,
                Name = l.Name,
                ImageUrl = l.ImageUrl,
                Description = l.Description,
                IsLocked = l.IsLocked,
                Academies = l.Academies
                            .Select(a => new AcademySummaryViewModel()
                            {
                                Id = a.Id,
                                Title = a.Title
                            })
                            .ToArray()
            })
            .FirstAsync();

        return location;
    }
}
