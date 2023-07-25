namespace SithAcademy.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using SithAcademy.Web.ViewModels.Trial;
using SithAcademy.Services.Data.Interfaces;
using SithAcademy.Web.Infrastructure.Extensions;

using static SithAcademy.Common.GeneralConstants;

[Authorize]
public class TrialController : Controller
{
    private readonly ITrialService trialService;
    private readonly IOverseerService overseerService;

    public TrialController(ITrialService trialService, IOverseerService overseerService)
    {
        this.trialService = trialService;
        this.overseerService = overseerService;
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        string userId = User.GetId();
        bool userIsOverseer = await overseerService.UserIsOverseerAsync(userId);
        if (!userIsOverseer)
        {
            TempData[ErrorMessage] = "Acolytes cannot add a trial.";
            return RedirectToAction("Index", "Home");
        }

        TrialFormViewModel viewModel = new TrialFormViewModel();
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Add(TrialFormViewModel viewModel)
    {
        string userId = User.GetId();
        bool userIsOverseer = await overseerService.UserIsOverseerAsync(userId);
        if (!userIsOverseer)
        {
            TempData[ErrorMessage] = "Acolytes cannot add a trial.";
            return RedirectToAction("Index", "Home");
        }

        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        try
        {
            string overseerId = await overseerService.GetOverseerIdAsync(userId);
            int academyId = await overseerService.GetAcademyIdByOverseerIdAsync(overseerId);
            string trialId = await trialService.AddTrialAndReturnTrialIdAsync(academyId, viewModel);

            await trialService.AddTrialToAllAcolytesInAcademyAsync(trialId, academyId);

            TempData[SuccessMessage] = "Successfully added the trial to the academy.";
            return RedirectToAction("Details", "Trial", new { id = trialId });
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Unexpected error occured while adding a new trial! Please try again or contact the High Inquisitor.");
            return View(viewModel);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        string normalizedId = id.ToLower();

        bool trialExists = await trialService.TrialExistsAsync(normalizedId);
        if (!trialExists)
        {
            return InaccessibleMessage();
        }


        string userId = User.GetId();
        bool userCanAccessTrial = await trialService.UserCanAccessTrialAsync(normalizedId, userId);
        if (!userCanAccessTrial)
        {
            TempData[WarningMessage] = "You can only access the trials of academies you are part of.";

            int academyId = await trialService.GetAcademyIdByTrialIdAsync(normalizedId);
            return RedirectToAction("Details", "Academy", new { id = academyId });
        }

        try
        {
            TrialDetailsViewModel trial = await trialService.GetTrialDetailsAsync(normalizedId);
            return View(trial);
        }
        catch (Exception)
        {
            return UnknownFailureMessage();
        }
    }

    private IActionResult InaccessibleMessage()
    {
        TempData[ErrorMessage] = "Incorrect trial ID selected.";
        return RedirectToAction("Index", "Home");
    }

    private IActionResult UnknownFailureMessage()
    {
        TempData[InformationMessage] = "The Dark Side has prevented your academy application from going through. Meditate upon your failure or try again later.";
        return RedirectToAction("Index", "Home");
    }
}
