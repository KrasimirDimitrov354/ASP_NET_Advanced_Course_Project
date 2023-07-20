namespace SithAcademy.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using SithAcademy.Web.ViewModels.Academy;
using SithAcademy.Services.Data.Interfaces;
using SithAcademy.Web.Infrastructure.Extensions;

using static SithAcademy.Common.GeneralConstants;

public class AcademyController : Controller
{
    private readonly IAcademyService academyService;

    public AcademyController(IAcademyService academyService)
    {
        this.academyService = academyService;
    }

    [HttpGet]
    public async Task<IActionResult> Display()
    {
        IEnumerable<AcademyOverviewViewModel> academies = await academyService.GetAllAcademiesAsync();

        return View(academies);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                ViewData["UserId"] = User.GetId();
            }

            AcademyDetailsViewModel academyDetails = await academyService.DisplayAcademyDetailsAsync(id);
            return View(academyDetails);
        }
        catch (Exception)
        {
            TempData[WarningMessage] = "Do not try to access knowledge you are not prepared for.";
            return RedirectToAction("Display", "Academy");
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Apply()
    {

    }
}
