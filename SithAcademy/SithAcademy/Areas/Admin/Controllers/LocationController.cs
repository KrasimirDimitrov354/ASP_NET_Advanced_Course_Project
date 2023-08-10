namespace SithAcademy.Web.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;

using SithAcademy.Web.ViewModels.Location;
using SithAcademy.Services.Data.Interfaces;

using static SithAcademy.Common.GeneralConstants;

public class LocationController : BaseAdminController
{
    private readonly ILocationService locationService;

    public LocationController(ILocationService locationService)
    {
        this.locationService = locationService;
    }

    [HttpGet]
    public IActionResult Add()
    {
        LocationFormViewModel viewModel = new LocationFormViewModel();
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Add(LocationFormViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        try
        {
            int locationId = await locationService.AddLocationAndReturnIdAsync(viewModel);

            TempData[SuccessMessage] = "Location has been added successfully.";
            return RedirectToAction("Details", "Location", new { Area = "", id = locationId });
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Unexpected error occured while trying to add new location, please try again.");
            return View(viewModel);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        bool locationExists = await locationService.LocationExistsAsync(id);
        if (!locationExists)
        {
            TempData[ErrorMessage] = "Incorrect location ID selected.";
            return RedirectToAction("Display", "Location", new { Area = "" });
        }

        try
        {
            LocationFormViewModel locationToEdit = await locationService.GetLocationForModificationAsync(id);
            return View(locationToEdit);
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home", new { id = 500 });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, LocationFormViewModel locationToEdit)
    {
        bool locationExists = await locationService.LocationExistsAsync(id);
        if (!locationExists)
        {
            TempData[ErrorMessage] = "Incorrect location ID selected.";
            return RedirectToAction("Display", "Location", new { Area = "" });
        }

        if (!ModelState.IsValid)
        {
            return View(locationToEdit);
        }

        try
        {
            await locationService.EditLocationDetailsAsync(id, locationToEdit);

            TempData[SuccessMessage] = "Location details have been successfully edited.";
            return RedirectToAction("Details", "Location", new { Area = "", id = id });
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Unexpected error occured while editing the details of the selected location. Please try again.");
            return View(locationToEdit);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Lock(int id)
    {
        bool locationExists = await locationService.LocationExistsAsync(id);
        if (!locationExists)
        {
            TempData[ErrorMessage] = "Incorrect location ID selected.";
            return RedirectToAction("Display", "Location", new { Area = "" });
        }

        try
        {
            LocationFormViewModel locationToLock = await locationService.GetLocationForModificationAsync(id);
            return View(locationToLock);
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home", new { id = 500 });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Lock(int id, LocationFormViewModel locationToLock)
    {
        bool locationExists = await locationService.LocationExistsAsync(id);
        if (!locationExists)
        {
            TempData[ErrorMessage] = "Incorrect location ID selected.";
            return RedirectToAction("Display", "Location", new { Area = "" });
        }

        try
        {
            await locationService.ChangeLockStatusOfLocationAndAllLocationAcademiesAsync(id);

            TempData[SuccessMessage] = "Lock status of the selected location and all of it's academies has been changed.";
            return RedirectToAction("Details", "Location", new { Area = "", id = id });
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Unexpected error occured while locking location. Please try again.");
            return View(locationToLock);
        }
    }
}
