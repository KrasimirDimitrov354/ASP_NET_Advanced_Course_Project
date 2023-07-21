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
    private readonly IOverseerService overseerService;

    public AcademyController(IAcademyService academyService, IOverseerService overseerService)
    {
        this.academyService = academyService;
        this.overseerService = overseerService;
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
            return InaccessibleMessage();
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Apply(int id)
    {
        bool academyExistAndIsNotLocked = await academyService.AcademyExistsAndIsNotLocked(id);
        if (!academyExistAndIsNotLocked)
        {
            return InaccessibleMessage();
        }

        string userId = User.GetId();
        bool userIsOverseer = await overseerService.UserIsOverseerAsync(userId);
        if (userIsOverseer)
        {
            TempData[ErrorMessage] = "Overseers cannot join academies as acolytes.";
            return RedirectToAction("Display", "Academy");

        }

        bool acolyteExistsInAcademy = await academyService.AcolyteExistsInAcademyAsync(id, userId);
        if (acolyteExistsInAcademy)
        {
            TempData[ErrorMessage] = "You have already joined this academy.";
            return RedirectToAction("Details", "Academy", new { id });
        }

        try
        {
            await academyService.AddAcolyteToAcademyAsync(id, userId);
            TempData[SuccessMessage] = "You have successfully joined your selected academy.";
            return RedirectToAction("Details", "Academy", new { id });
        }
        catch (Exception)
        {
            return UnknownFailureMessage();
        }
    }

    private IActionResult InaccessibleMessage()
    {
        TempData[WarningMessage] = "Do not try to access knowledge you are not prepared for.";
        return RedirectToAction("Display", "Academy");
    }

    private IActionResult UnknownFailureMessage()
    {
        TempData[InformationMessage] = "The Dark Side has prevented your academy application from going through. Meditate upon your failure or try again later.";
        return RedirectToAction("Index", "Home");
    }
}
