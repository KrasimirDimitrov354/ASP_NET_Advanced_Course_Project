namespace SithAcademy.Web.Controllers;

using Microsoft.AspNetCore.Mvc;

using SithAcademy.Services.Data.Interfaces;
using SithAcademy.Web.ViewModels.Location;

using static SithAcademy.Common.GeneralConstants;

public class LocationController : Controller
{
    private readonly ILocationService locationService;

    public LocationController(ILocationService locationService)
    {
        this.locationService = locationService;
    }

    [HttpGet]
    public async Task<IActionResult> Display()
    {
        IEnumerable<LocationOverviewViewModel> locations = await locationService.GetAllLocationsAsync();

        return View(locations);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            LocationDetailsViewModel location = await locationService.DisplayLocationDetailsAsync(id);
            return View(location);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "The location you're seeking does not exist in the archives.";
            return RedirectToAction("Display", "Location");
        }
    }
}
