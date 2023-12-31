﻿namespace SithAcademy.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using SithAcademy.Web.ViewModels.Academy;
using SithAcademy.Web.Infrastructure.Extensions;
using SithAcademy.Services.Data.Interfaces;

using static SithAcademy.Common.GeneralConstants;

[Authorize]
public class AcademyController : Controller
{
    private readonly IAcademyService academyService;
    private readonly ILocationService locationService;
    private readonly IOverseerService overseerService;
    private readonly IAcolyteService acolyteService;
    private readonly ITrialService trialService;

    public AcademyController(IAcademyService academyService, 
        ILocationService locationService,
        IOverseerService overseerService,
        IAcolyteService acolyteService,
        ITrialService trialService)
    {
        this.academyService = academyService;
        this.locationService = locationService;
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
            TempData[ErrorMessage] = "Incorrect academy ID selected.";
            return RedirectToAction("Display", "Academy");
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
            return RedirectToAction("Error", "Home", new { id = 500 });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Join(int id)
    {
        bool academyExists = await academyService.AcademyExistsAsync(id);
        if (!academyExists)
        {
            TempData[ErrorMessage] = "Incorrect academy ID selected.";
            return RedirectToAction("Display", "Academy");
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
        bool locationIsLocked = await locationService.LocationIsLockedAsync(locationId);
        if (locationIsLocked)
        {
            TempData[WarningMessage] = "The location is currently under lockdown. You cannot join any of its academies.";
            return RedirectToAction("Details", "Location", new { id = locationId });
        }

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
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Republic interference has caused something to go wrong. Try again.";
        }

        return RedirectToAction("Details", "Academy", new { id });
    }

    [HttpPost]
    public async Task<IActionResult> Leave(int id)
    {
        bool academyExists = await academyService.AcademyExistsAsync(id);
        if (!academyExists)
        {
            TempData[ErrorMessage] = "Incorrect academy ID selected.";
            return RedirectToAction("Display", "Academy");
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
            
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Republic interference has caused something to go wrong. Try again.";
        }

        return RedirectToAction("Details", "Academy", new { id });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        bool academyExists = await academyService.AcademyExistsAsync(id);
        if (!academyExists)
        {
            TempData[ErrorMessage] = "Incorrect academy ID selected.";
            return RedirectToAction("Display", "Academy");
        }

        if (!User.IsAdmin())
        {
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
        }

        try
        {
            AcademyFormViewModel academyToEdit = await academyService.GetAcademyForModificationAsync(id);
            return View(academyToEdit);
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home", new { id = 500 });
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
            TempData[ErrorMessage] = "Incorrect academy ID selected.";
            return RedirectToAction("Display", "Academy");
        }

        if (!User.IsAdmin())
        {
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
        }

        try
        {
            await academyService.EditAcademyAsync(id, viewModel);
            TempData[SuccessMessage] = "Academy details have been successfully edited.";

            return RedirectToAction("Details", "Academy", new { id });
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home", new { id = 500 });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Lock(int id)
    {
        bool academyExists = await academyService.AcademyExistsAsync(id);
        if (!academyExists)
        {
            TempData[ErrorMessage] = "Incorrect academy ID selected.";
            return RedirectToAction("Display", "Academy");
        }

        if (!User.IsAdmin())
        {
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
        }

        int locationId = await academyService.GetLocationIdByAcademyIdAsync(id);
        bool locationIsLocked = await locationService.LocationIsLockedAsync(locationId);
        if (locationIsLocked)
        {
            TempData[ErrorMessage] = "You cannot modify an academy's lock status while the location is locked.";
            if (User.IsAdmin())
            {
                return RedirectToAction("Details", "Location", new { id = locationId });
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        try
        {
            AcademyFormViewModel academyToLock = await academyService.GetAcademyForModificationAsync(id);
            return View(academyToLock);
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home", new { id = 500 });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Lock(int id, AcademyFormViewModel viewModel)
    {
        bool academyExists = await academyService.AcademyExistsAsync(id);
        if (!academyExists)
        {
            TempData[ErrorMessage] = "Incorrect academy ID selected.";
            return RedirectToAction("Display", "Academy");
        }

        if (!User.IsAdmin())
        {
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
        }

        int locationId = await academyService.GetLocationIdByAcademyIdAsync(id);
        bool locationIsLocked = await locationService.LocationIsLockedAsync(locationId);
        if (locationIsLocked)
        {
            TempData[ErrorMessage] = "You cannot modify an academy's lock status while the location is locked.";
            if (User.IsAdmin())
            {
                return RedirectToAction("Details", "Location", new { id = locationId });
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        try
        {
            await academyService.ChangeAcademyLockStatusAsync(id);
            TempData[WarningMessage] = "Academy lock status has been changed.";

            return RedirectToAction("Details", "Academy", new { id });
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home", new { id = 500 });
        }
    }
}
