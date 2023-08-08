namespace SithAcademy.Web.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;

using SithAcademy.Web.ViewModels.Academy;
using SithAcademy.Services.Data.Interfaces;

public class AcademyController : BaseAdminController
{
    private readonly ILocationService locationService;
    private readonly IAcademyService academyService;

    public AcademyController(ILocationService locationService, IAcademyService academyService)
    {
        this.locationService = locationService;
        this.academyService = academyService;
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        AddAcademyViewModel viewModel = new AddAcademyViewModel();
        viewModel.Locations = await locationService.GetAllLocationsForDropdownSelectAsync();

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddAcademyViewModel viewModel)
    {
        bool locationExists = await locationService.LocationExistsAsync(viewModel.LocationId);
        if (!locationExists)
        {
            ModelState.AddModelError(nameof(viewModel.LocationId), "Selected location does not exist!");
        }

        if (!ModelState.IsValid)
        {
            viewModel.Locations = await locationService.GetAllLocationsForDropdownSelectAsync();
            return View(viewModel);
        }

        try
        {
            int academyId = await academyService.AddAcademyAndReturnIdAsync(viewModel);
            return RedirectToAction("Details", "Academy", new { Area = "", id =  academyId });
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Unexpected error occured while adding new academy, please try again.");

            viewModel.Locations = await locationService.GetAllLocationsForDropdownSelectAsync();
            return View(viewModel);
        }
    }
}
