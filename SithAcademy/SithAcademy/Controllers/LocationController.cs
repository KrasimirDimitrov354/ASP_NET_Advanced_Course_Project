namespace SithAcademy.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using SithAcademy.Web.ViewModels.Location;
using SithAcademy.Services.Data.Interfaces;

using static SithAcademy.Common.GeneralConstants;

//TODO: Add CRUD operations (can be performed only by admin)

[Authorize]
public class LocationController : Controller
{
    private readonly ILocationService locationService;

    public LocationController(ILocationService locationService)
    {
        this.locationService = locationService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Display()
    {
        IEnumerable<LocationOverviewViewModel> locations = await locationService.GetAllLocationsAsync();

        return View(locations);
    }

    [HttpGet]
    [AllowAnonymous]
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
