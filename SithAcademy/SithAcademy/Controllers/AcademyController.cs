namespace SithAcademy.Web.Controllers;

using Microsoft.AspNetCore.Mvc;

using SithAcademy.Services.Data.Interfaces;
using SithAcademy.Web.ViewModels.Academy;

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
}
