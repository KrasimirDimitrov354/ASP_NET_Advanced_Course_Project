﻿namespace SithAcademy.Web.Areas.Admin.Controllers;

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
}
