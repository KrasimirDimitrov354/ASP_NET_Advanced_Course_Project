﻿namespace SithAcademy.Services.Data.Interfaces;

using SithAcademy.Web.Views.Location;
using SithAcademy.Web.ViewModels.Location;

public interface ILocationService
{
    Task<IEnumerable<LocationOverviewViewModel>> GetAllLocationsAsync();

    Task<LocationDetailsViewModel> DisplayLocationDetailsAsync(int locationId);

    Task<bool> LocationIsLockedAsync(int locationId);

    Task<bool> LocationExistsAsync(int locationId);

    Task<IEnumerable<LocationDropdownViewModel>> GetAllLocationsForDropdownSelectAsync();

    Task<int> AddLocationAndReturnIdAsync(LocationFormViewModel viewModel);

    Task<LocationFormViewModel> GetLocationForModificationAsync(int locationId);

    Task EditLocationDetailsAsync(int locationId, LocationFormViewModel viewModel);

    Task ChangeLockStatusOfLocationAndAllLocationAcademiesAsync(int locationId);
}
