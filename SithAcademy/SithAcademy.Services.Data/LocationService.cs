namespace SithAcademy.Services.Data;

using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using SithAcademy.Data;
using SithAcademy.Data.Models;
using SithAcademy.Web.Views.Location;
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

    public async Task<bool> LocationIsLockedAsync(int locationId)
    {
        Location location = await dbContext.Locations
            .FirstAsync(l => l.Id == locationId);

        return location.IsLocked;
    }

    public async Task<IEnumerable<LocationDropdownViewModel>> GetAllLocationsForDropdownSelectAsync()
    {
        IEnumerable<LocationDropdownViewModel> allLocations = await dbContext.Locations
            .Select(l => new LocationDropdownViewModel()
            {
                Id = l.Id,
                Name = l.Name,
            })
            .ToArrayAsync();

        return allLocations;
    }

    public async Task<bool> LocationExistsAsync(int locationId)
    {
        return await dbContext.Locations.AnyAsync(l => l.Id == locationId);
    }

    public async Task<int> AddLocationAndReturnIdAsync(LocationFormViewModel viewModel)
    {
        Location location = new Location()
        {
            Name = viewModel.Name,
            Description = viewModel.Description,
            ImageUrl = viewModel.ImageUrl,
            IsLocked = viewModel.IsLocked
        };

        await dbContext.Locations.AddAsync(location);
        await dbContext.SaveChangesAsync();

        return location.Id;
    }
}
