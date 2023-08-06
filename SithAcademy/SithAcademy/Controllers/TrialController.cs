namespace SithAcademy.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using SithAcademy.Web.ViewModels.Trial;
using SithAcademy.Web.ViewModels.Query;
using SithAcademy.Web.Infrastructure.Extensions;
using SithAcademy.Services.Data.Interfaces;
using SithAcademy.Services.Data.Models.Trial;

using static SithAcademy.Common.GeneralConstants;

[Authorize]
public class TrialController : Controller
{
    private readonly ITrialService trialService;
    private readonly IOverseerService overseerService;
    private readonly IAcademyService academyService;

    public TrialController(ITrialService trialService, 
        IOverseerService overseerService, 
        IAcademyService academyService)
    {
        this.trialService = trialService;
        this.overseerService = overseerService;
        this.academyService = academyService;
    }

    [HttpGet]
    public async Task<IActionResult> All([FromQuery]AllTrialsQueryModel queryModel)
    {
        //Only admin can access this, implement check later
        AllTrialsFilteredAndPagedServiceModel serviceModel = await trialService.GetAllTrialsAsync(queryModel);

        queryModel.Trials = serviceModel.Trials;
        queryModel.TotalRecords = serviceModel.TotalTrialsCount;
        queryModel.Academies = await academyService.GetAllAcademyNamesForQuerySelectAsync();

        return View(queryModel);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        string userId = User.GetId()!;
        bool userIsOverseer = await overseerService.UserIsOverseerAsync(userId);
        if (!userIsOverseer)
        {
            TempData[ErrorMessage] = "Acolytes cannot add trials!";
            return RedirectToAction("Index", "Home");
        }

        TrialFormViewModel viewModel = new TrialFormViewModel();
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Add(TrialFormViewModel viewModel)
    {
        string userId = User.GetId()!;
        bool userIsOverseer = await overseerService.UserIsOverseerAsync(userId);
        if (!userIsOverseer)
        {
            TempData[ErrorMessage] = "Acolytes cannot add trials!";
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
            TempData[ErrorMessage] = "Incorrect trial ID selected.";
            return RedirectToAction("Index", "Home");
        }

        string userId = User.GetId()!;
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
            return RedirectToAction("Error", "Home", new { id = 500 });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        string normalizedId = id.ToLower();
        bool trialExists = await trialService.TrialExistsAsync(normalizedId);
        if (!trialExists)
        {
            TempData[ErrorMessage] = "Incorrect trial ID selected.";
            return RedirectToAction("Index", "Home");
        }

        string userId = User.GetId()!;
        bool userIsOverseer = await overseerService.UserIsOverseerAsync(userId);
        if (!userIsOverseer)
        {
            TempData[ErrorMessage] = "Acolytes cannot edit trials!";
            return RedirectToAction("Index", "Home");
        }

        int academyId = await trialService.GetAcademyIdByTrialIdAsync(normalizedId);
        string overseerId = await overseerService.GetOverseerIdAsync(userId);
        bool overseerCanModify = await overseerService.OverseerCanModifyAsync(academyId, overseerId);
        if (!overseerCanModify)
        {
            TempData[ErrorMessage] = "Overseers can modify trials only in academies they are assigned to.";
            return RedirectToAction("Details", "Academy", new { id = academyId });
        }

        try
        {
            TrialFormViewModel viewModel = await trialService.GetTrialForModificationAsync(normalizedId);
            return View(viewModel);
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home", new { id = 500 });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string id, TrialFormViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        string normalizedId = id.ToLower();
        bool trialExists = await trialService.TrialExistsAsync(normalizedId);
        if (!trialExists)
        {
            TempData[ErrorMessage] = "Incorrect trial ID selected.";
            return RedirectToAction("Index", "Home");
        }

        string userId = User.GetId()!;
        bool userIsOverseer = await overseerService.UserIsOverseerAsync(userId);
        if (!userIsOverseer)
        {
            TempData[ErrorMessage] = "Acolytes cannot edit trials!";
            return RedirectToAction("Index", "Home");
        }

        int academyId = await trialService.GetAcademyIdByTrialIdAsync(normalizedId);
        string overseerId = await overseerService.GetOverseerIdAsync(userId);
        bool overseerCanModify = await overseerService.OverseerCanModifyAsync(academyId, overseerId);
        if (!overseerCanModify)
        {
            TempData[ErrorMessage] = "Overseers can modify trials only in academies they are assigned to.";
            return RedirectToAction("Details", "Academy", new { id = academyId });
        }

        try
        {
            await trialService.EditTrialAsync(normalizedId, viewModel);
            TempData[SuccessMessage] = "Trial details have been successfully edited.";

            return RedirectToAction("Details", "Trial", new { id = id });
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home", new { id = 500 });
        }
    }
}
