namespace SithAcademy.Services.Data.Interfaces;

using SithAcademy.Web.ViewModels.Location;

public interface ILocationService
{
    Task<IEnumerable<LocationOverviewViewModel>> GetAllLocationsAsync();

    Task<LocationDetailsViewModel> DisplayLocationDetailsAsync(int locationId);
}
