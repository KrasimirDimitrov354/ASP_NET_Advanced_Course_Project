namespace SithAcademy.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using SithAcademy.Web.ViewModels.Academy;
using SithAcademy.Services.Data.Interfaces;
using SithAcademy.Web.Infrastructure.Extensions;

using static SithAcademy.Common.GeneralConstants;

//TODO: Create Academy (Get and Post methods) - only admin can do that

[Authorize]
public class AcademyController : Controller
{
    private readonly IAcademyService academyService;
    private readonly IOverseerService overseerService;
    private readonly IAcolyteService acolyteService;
    private readonly ITrialService trialService;

    public AcademyController(IAcademyService academyService, 
        IOverseerService overseerService,
        IAcolyteService acolyteService,
        ITrialService trialService)
    {
        this.academyService = academyService;
        this.overseerService = overseerService;
        this.acolyteService = acolyteService;
        this.trialService = trialService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Display()
    {
        IEnumerable<AcademyOverviewViewModel> academies = await academyService.GetAllAcademiesAsync();

        return View(academies);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        bool academyExists = await academyService.AcademyExistsAsync(id);
        if (!academyExists)
        {
            return InaccessibleMessage();
        }

        try
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                ViewData["UserId"] = User.GetId()!.ToUpper();
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
    public async Task<IActionResult> Join(int id)
    {
        bool academyExists = await academyService.AcademyExistsAsync(id);
        if (!academyExists)
        {
            return InaccessibleMessage();
        }

        bool academyIsLocked = await academyService.AcademyIsLockedAsync(id);
        if (academyIsLocked)
        {
            TempData[WarningMessage] = "This academy has been placed on lockdown. Await further developments.";
            return RedirectToAction("Display", "Academy", new { id });
        }

        string userId = User.GetId()!;
        bool userIsOverseer = await overseerService.UserIsOverseerAsync(userId);
        if (userIsOverseer)
        {
            TempData[ErrorMessage] = "Only the High Inquisitor can re-assign an overseer to a different academy.";
            return RedirectToAction("Display", "Academy");
        }

        int locationId = await academyService.GetLocationIdByAcademyIdAsync(id);
        bool acolyteIsInLocation = await acolyteService.AcolyteIsInLocationAsync(locationId, userId);
        if (!acolyteIsInLocation)
        {
            TempData[WarningMessage] = "You cannot travel to the selected location before you leave the academies in the current location.";
            return RedirectToAction("Details", "Location", new { id = await acolyteService.GetAcolyteCurrentLocationAsync(userId) });
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
            await trialService.AssignTrialsToAcolyteAsync(id, userId);

            TempData[SuccessMessage] = "You have successfully joined the selected academy.";

            return RedirectToAction("Details", "Academy", new { id });
        }
        catch (Exception)
        {
            return UnknownFailureMessage();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Leave(int id)
    {
        bool academyExists = await academyService.AcademyExistsAsync(id);
        if (!academyExists)
        {
            return InaccessibleMessage();
        }

        bool academyIsLocked = await academyService.AcademyIsLockedAsync(id);
        if (academyIsLocked)
        {
            TempData[WarningMessage] = "This academy has been placed on lockdown. Await further developments.";
            return RedirectToAction("Display", "Academy", new { id });
        }

        string userId = User.GetId()!;
        bool userIsOverseer = await overseerService.UserIsOverseerAsync(userId);
        if (userIsOverseer)
        {
            TempData[ErrorMessage] = "Only the High Inquisitor can re-assign an overseer to a different academy.";
            return RedirectToAction("Display", "Academy");
        }

        bool acolyteExistsInAcademy = await academyService.AcolyteExistsInAcademyAsync(id, userId);
        if (!acolyteExistsInAcademy)
        {
            TempData[ErrorMessage] = "You have never joined joined this academy.";
            return RedirectToAction("Details", "Academy", new { id });
        }

        try
        {
            await academyService.RemoveAcolyteFromAcademyAsync(id, userId);
            await trialService.RemoveTrialsFromAcolyteAsync(userId);

            int locationId = await academyService.GetLocationIdByAcademyIdAsync(id);
            await acolyteService.RemoveAcolyteFromLocationAsync(locationId, userId);

            TempData[SuccessMessage] = "You have successfully left the selected academy.";
            return RedirectToAction("Details", "Academy", new { id });
        }
        catch (Exception)
        {
            return UnknownFailureMessage();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        bool academyExists = await academyService.AcademyExistsAsync(id);
        if (!academyExists)
        {
            return InaccessibleMessage();
        }

        string userId = User.GetId()!;
        bool userIsOverseer = await overseerService.UserIsOverseerAsync(userId);
        if (!userIsOverseer)
        {
            TempData[ErrorMessage] = "Acolytes cannot change an academy's details.";
            return RedirectToAction("Display", "Academy");
        }

        string overseerId = await overseerService.GetOverseerIdAsync(userId);
        bool overseerCanModify = await overseerService.OverseerCanModifyAsync(id, overseerId);
        if (!overseerCanModify)
        {
            TempData[ErrorMessage] = "Overseers can only modify academies they are assigned to.";

            int academyId = await overseerService.GetAcademyIdByOverseerIdAsync(overseerId);
            return RedirectToAction("Details", "Academy", new { id = academyId });
        }

        try
        {
            AcademyFormViewModel academyToEdit = await academyService.GetAcademyForModificationAsync(id);
            return View(academyToEdit);
        }
        catch (Exception)
        {
            return UnknownFailureMessage();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, AcademyFormViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        bool academyExists = await academyService.AcademyExistsAsync(id);
        if (!academyExists)
        {
            return InaccessibleMessage();
        }

        string userId = User.GetId()!;
        bool userIsOverseer = await overseerService.UserIsOverseerAsync(userId);
        if (!userIsOverseer)
        {
            TempData[ErrorMessage] = "Acolytes cannot change an academy's details.";
            return RedirectToAction("Display", "Academy");
        }

        string overseerId = await overseerService.GetOverseerIdAsync(userId);
        bool overseerCanModify = await overseerService.OverseerCanModifyAsync(id, overseerId);
        if (!overseerCanModify)
        {
            TempData[ErrorMessage] = "Overseers can only modify academies they are assigned to.";

            int academyId = await overseerService.GetAcademyIdByOverseerIdAsync(overseerId);
            return RedirectToAction("Details", "Academy", new { id = academyId });
        }

        try
        {
            await academyService.EditAcademyAsync(id, viewModel);
            TempData[SuccessMessage] = "Academy details have been successfully edited.";
            return RedirectToAction("Details", "Academy", new { id });
        }
        catch (Exception)
        {
            return UnknownFailureMessage();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Lock(int id)
    {
        bool academyExists = await academyService.AcademyExistsAsync(id);
        if (!academyExists)
        {
            return InaccessibleMessage();
        }

        string userId = User.GetId()!;
        bool userIsOverseer = await overseerService.UserIsOverseerAsync(userId);
        if (!userIsOverseer)
        {
            TempData[ErrorMessage] = "Acolytes cannot change an academy's details.";
            return RedirectToAction("Display", "Academy");
        }

        string overseerId = await overseerService.GetOverseerIdAsync(userId);
        bool overseerCanModify = await overseerService.OverseerCanModifyAsync(id, overseerId);
        if (!overseerCanModify)
        {
            TempData[ErrorMessage] = "Overseers can only modify academies they are assigned to.";

            int academyId = await overseerService.GetAcademyIdByOverseerIdAsync(overseerId);
            return RedirectToAction("Details", "Academy", new { id = academyId });
        }

        try
        {
            AcademyFormViewModel academyToLock = await academyService.GetAcademyForModificationAsync(id);
            return View(academyToLock);
        }
        catch (Exception)
        {
            return UnknownFailureMessage();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Lock(int id, AcademyFormViewModel academyToLock)
    {
        bool academyExists = await academyService.AcademyExistsAsync(id);
        if (!academyExists)
        {
            return InaccessibleMessage();
        }

        string userId = User.GetId()!;
        bool userIsOverseer = await overseerService.UserIsOverseerAsync(userId);
        if (!userIsOverseer)
        {
            TempData[ErrorMessage] = "Acolytes cannot change an academy's details.";
            return RedirectToAction("Display", "Academy");
        }

        string overseerId = await overseerService.GetOverseerIdAsync(userId);
        bool overseerCanModify = await overseerService.OverseerCanModifyAsync(id, overseerId);
        if (!overseerCanModify)
        {
            TempData[ErrorMessage] = "Overseers can only modify academies they are assigned to.";

            int academyId = await overseerService.GetAcademyIdByOverseerIdAsync(overseerId);
            return RedirectToAction("Details", "Academy", new { id = academyId });
        }

        try
        {
            await academyService.ChangeAcademyLockStatusAsync(id);
            TempData[WarningMessage] = "Academy lock status has been changed.";
            return RedirectToAction("Details", "Academy", new { id });
        }
        catch (Exception)
        {
            return UnknownFailureMessage();
        }
    }

    private IActionResult InaccessibleMessage()
    {
        TempData[ErrorMessage] = "Incorrect academy ID selected.";
        return RedirectToAction("Display", "Academy");
    }

    private IActionResult UnknownFailureMessage()
    {
        TempData[InformationMessage] = "The Dark Side has prevented your academy application from going through. Meditate upon your failure or try again later.";
        return RedirectToAction("Index", "Home");
    }
}
