namespace SithAcademy.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using SithAcademy.Web.ViewModels.Academy;
using SithAcademy.Services.Data.Interfaces;
using SithAcademy.Web.Infrastructure.Extensions;

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
            return RedirectToAction("Display", "Academy");
        }
    }

    [Authorize]
    public async Task Apply()
    {

    }
}
