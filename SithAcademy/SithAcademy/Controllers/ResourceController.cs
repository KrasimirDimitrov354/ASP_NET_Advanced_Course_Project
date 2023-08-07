namespace SithAcademy.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using SithAcademy.Web.ViewModels.Resource;
using SithAcademy.Services.Data.Interfaces;
using SithAcademy.Web.Infrastructure.Extensions;

using static SithAcademy.Common.GeneralConstants;

[Authorize]
public class ResourceController : Controller
{
    private readonly IResourceService resourceService;
    private readonly ITrialService trialService;
    private readonly IOverseerService overseerService;

    public ResourceController(IResourceService resourceService, 
        ITrialService trialService, 
        IOverseerService overseerService)
    {
        this.resourceService = resourceService;
        this.trialService = trialService;
        this.overseerService = overseerService;
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        string userId = User.GetId()!;
        bool userIsOverseer = await overseerService.UserIsOverseerAsync(userId);
        if (!userIsOverseer && !User.IsAdmin()) 
        {
            TempData[ErrorMessage] = "Acolytes cannot add resources!";
            return RedirectToAction("Index", "Home");
        }

        try
        {
            ResourceFormViewModel viewModel = new ResourceFormViewModel();

            if (User.IsAdmin())
            {
                viewModel.Trials = await trialService.GetAllTrialsForDropdownSelectByAcademyIdAsync();
            }
            else
            {
                string overseerId = await overseerService.GetOverseerIdAsync(userId);
                int academyId = await overseerService.GetAcademyIdByOverseerIdAsync(overseerId);
                viewModel.Trials = await trialService.GetAllTrialsForDropdownSelectByAcademyIdAsync(academyId);
            }

            return View(viewModel);
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home", new { id = 500 });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add(ResourceFormViewModel viewModel)
    {
        string userId = User.GetId()!;
        bool userIsOverseer = await overseerService.UserIsOverseerAsync(userId);
        if (!userIsOverseer && !User.IsAdmin())
        {
            TempData[ErrorMessage] = "Acolytes cannot add resources!";
            return RedirectToAction("Index", "Home");
        }

        bool trialExists = await trialService.TrialExistsAsync(viewModel.TrialId);
        if (!trialExists)
        {
            ModelState.AddModelError(nameof(viewModel.TrialId), "Selected trial does not exist!");
        }

        string overseerId = await overseerService.GetOverseerIdAsync(userId);
        int academyId = await overseerService.GetAcademyIdByOverseerIdAsync(overseerId);

        if (!ModelState.IsValid)
        {
            viewModel.Trials = await trialService.GetAllTrialsForDropdownSelectByAcademyIdAsync(academyId);
            return View(viewModel);
        }

        try
        {
            await resourceService.AddResourceAsync(viewModel);

            TempData[SuccessMessage] = "Successfully added the resource to the selected trial";
            return RedirectToAction("Details", "Trial", new { id = viewModel.TrialId });
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Unexpected error occured while trying to add the resource. Please try again later.");

            viewModel.Trials = await trialService.GetAllTrialsForDropdownSelectByAcademyIdAsync(academyId);
            return View(viewModel);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        string normalizedId = id.ToLower();
        string trialId = await resourceService.GetTrialIdByResourceIdAsync(normalizedId);
        bool resourceExists = await resourceService.ResourceExistsAsync(normalizedId);
        if (!resourceExists)
        {
            TempData[ErrorMessage] = "Incorrect resource ID selected.";
            return RedirectToAction("Details", "Trial", new { id = trialId });
        }

        int academyId = 0;

        if (!User.IsAdmin())
        {
            string userId = User.GetId()!;
            bool userIsOverseer = await overseerService.UserIsOverseerAsync(userId);
            if (!userIsOverseer)
            {
                TempData[ErrorMessage] = "Acolytes cannot edit resources!";
                return RedirectToAction("Index", "Home");
            }

            academyId = await trialService.GetAcademyIdByTrialIdAsync(trialId);
            string overseerId = await overseerService.GetOverseerIdAsync(userId);
            bool overseerCanModify = await overseerService.OverseerCanModifyAsync(academyId, overseerId);
            if (!overseerCanModify)
            {
                TempData[ErrorMessage] = "Overseers can modify resources only in academies they are assigned to.";
                return RedirectToAction("Details", "Academy", new { id = academyId });
            }
        }

        try
        {
            ResourceFormViewModel resourceToEdit = await resourceService.GetResourceForModificationAsync(normalizedId);
            resourceToEdit.Trials = await trialService.GetAllTrialsForDropdownSelectByAcademyIdAsync(academyId);
            return View(resourceToEdit);
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home", new { id = 500 });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string id, ResourceFormViewModel viewModel)
    {
        string normalizedId = id.ToLower();
        string trialId = await resourceService.GetTrialIdByResourceIdAsync(normalizedId);
        bool resourceExists = await resourceService.ResourceExistsAsync(normalizedId);
        if (!resourceExists)
        {
            TempData[ErrorMessage] = "Incorrect resource ID selected.";
            return RedirectToAction("Details", "Trial", new { id = trialId });
        }

        bool trialExists = await trialService.TrialExistsAsync(viewModel.TrialId);
        if (!trialExists)
        {
            ModelState.AddModelError(nameof(viewModel.TrialId), "Selected trial does not exist!");
        }

        int academyId = 0;
        
        if (!User.IsAdmin())
        {
            string userId = User.GetId()!;
            bool userIsOverseer = await overseerService.UserIsOverseerAsync(userId);
            if (!userIsOverseer)
            {
                TempData[ErrorMessage] = "Acolytes cannot edit resources!";
                return RedirectToAction("Index", "Home");
            }

            string overseerId = await overseerService.GetOverseerIdAsync(userId);
            academyId = await trialService.GetAcademyIdByTrialIdAsync(trialId);
            bool overseerCanModify = await overseerService.OverseerCanModifyAsync(academyId, overseerId);
            if (!overseerCanModify)
            {
                TempData[ErrorMessage] = "Overseers can modify resources only in academies they are assigned to.";
                return RedirectToAction("Details", "Academy", new { id = academyId });
            }
        }

        if (!ModelState.IsValid)
        {
            viewModel.Trials = await trialService.GetAllTrialsForDropdownSelectByAcademyIdAsync(academyId);
            return View(viewModel);
        }

        try
        {
            await resourceService.EditResourceAsync(normalizedId, viewModel);
            TempData[SuccessMessage] = "Resource details have been successfully edited.";

            return RedirectToAction("Details", "Trial", new { id = trialId });
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Unexpected error occured while trying to edit the resource. Please try again.");

            viewModel.Trials = await trialService.GetAllTrialsForDropdownSelectByAcademyIdAsync(academyId);
            return View(viewModel);
        }
    }
}
