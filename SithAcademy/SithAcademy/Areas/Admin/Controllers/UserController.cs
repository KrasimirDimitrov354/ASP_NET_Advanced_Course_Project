namespace SithAcademy.Web.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;

using SithAcademy.Services.Data.Interfaces;
using SithAcademy.Web.Areas.Admin.ViewModels.User;
using SithAcademy.Web.Areas.Admin.Services.Interfaces;

using static SithAcademy.Common.GeneralConstants;

public class UserController : BaseAdminController
{
    private readonly IUserService userService;
    private readonly IOverseerService overseerService;
    private readonly IAcademyService academyService;

    public UserController(IUserService userService, 
        IOverseerService overseerService, 
        IAcademyService academyService)
    {
        this.userService = userService;
        this.overseerService = overseerService;
        this.academyService = academyService;
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        IEnumerable<UserPreviewViewModel> allUsers = await userService.GetAllUsersAsync();

        foreach (UserPreviewViewModel user in allUsers)
        {
            bool userIsOverseer = await overseerService.UserIsOverseerAsync(user.Id);
            if (userIsOverseer)
            {
                user.IsOverseer = true;
            }
        }

        return View(allUsers);
    }

    [HttpGet]
    public async Task<IActionResult> Modify(string id)
    {
        bool userExists = await userService.UserExistsAsync(id);
        if (!userExists)
        {
            TempData[ErrorMessage] = "Incorrect user ID selected.";
            return RedirectToAction("All", "User", new { Area = AdminAreaName });
        }

        string overseerId = await overseerService.GetOverseerIdAsync(id);
        OverseerDetailsViewModel overseerDetails = await userService.GetOverseerForModificationAsync(overseerId);
        overseerDetails.Academies = await academyService.GetAllAcademiesForDropdownSelectAsync();

        return View(overseerDetails);
    }

    [HttpPost]
    public async Task<IActionResult> Modify(string id, OverseerDetailsViewModel viewModel)
    {
        bool userExists = await userService.UserExistsAsync(id);
        if (!userExists)
        {
            TempData[ErrorMessage] = "Incorrect user ID selected.";
            return RedirectToAction("All", "User", new { Area = AdminAreaName });
        }

        bool academyExists = await academyService.AcademyExistsAsync(viewModel.AcademyId);
        if (!academyExists)
        {
            ModelState.AddModelError(nameof(viewModel.AcademyId), "Selected academy does not exist!");
        }

        if (!ModelState.IsValid)
        {
            viewModel.Academies = await academyService.GetAllAcademiesForDropdownSelectAsync();
            return View(viewModel);
        }

        try
        {
            await userService.ModifyOverseerAsync(id, viewModel);

            TempData[SuccessMessage] = "You have successfully modified the chosen overseer's details.";
            return RedirectToAction("All", "User", new { Area = AdminAreaName });
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Unexpected error occured while trying to modify overseer's details. Please try again.");

            viewModel.Academies = await academyService.GetAllAcademiesForDropdownSelectAsync();
            return View(viewModel);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Demote(string id)
    {
        bool userExists = await userService.UserExistsAsync(id);
        if (!userExists)
        {
            TempData[ErrorMessage] = "Incorrect user ID selected.";
            return RedirectToAction("All", "User", new { Area = AdminAreaName });
        }

        string overseerId = await overseerService.GetOverseerIdAsync(id);
        OverseerDetailsViewModel overseerDetails = await userService.GetOverseerForModificationAsync(overseerId);

        return View(overseerDetails);
    }

    [HttpPost]
    public async Task<IActionResult> Demote(string id, OverseerDetailsViewModel viewModel)
    {
        bool userExists = await userService.UserExistsAsync(id);
        if (!userExists)
        {
            TempData[ErrorMessage] = "Incorrect user ID selected.";
            return RedirectToAction("All", "User", new { Area = AdminAreaName });
        }

        try
        {
            await userService.DemoteOverseer(id);

            TempData[SuccessMessage] = "Overseer has been demoted successfully.";
            return RedirectToAction("All", "User", new { Area = AdminAreaName });
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Something went wrong.";
            return RedirectToAction("Error", "Home", new { id = 500 });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Promote(string id)
    {
        bool userExists = await userService.UserExistsAsync(id);
        if (!userExists)
        {
            TempData[ErrorMessage] = "Incorrect user ID selected.";
            return RedirectToAction("All", "User", new { Area = AdminAreaName });
        }

        OverseerFormViewModel viewModel = new OverseerFormViewModel();
        viewModel.Academies = await academyService.GetAllAcademiesForDropdownSelectAsync();

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Promote(string id, OverseerFormViewModel viewModel)
    {
        bool userExists = await userService.UserExistsAsync(id);
        if (!userExists)
        {
            TempData[ErrorMessage] = "Incorrect user ID selected.";
            return RedirectToAction("All", "User", new { Area = AdminAreaName });
        }

        bool academyExists = await academyService.AcademyExistsAsync(viewModel.AcademyId);
        if (!academyExists)
        {
            ModelState.AddModelError(nameof(viewModel.AcademyId), "Selected academy does not exist!");
        }

        if (!ModelState.IsValid)
        {
            viewModel.Academies = await academyService.GetAllAcademiesForDropdownSelectAsync();
            return View(viewModel);
        }

        try
        {
            await userService.PromoteOverseer(id, viewModel);

            int locationId = await academyService.GetLocationIdByAcademyIdAsync(viewModel.AcademyId);
            await userService.SetLocationToUser(id, locationId);

            TempData[SuccessMessage] = "Acolyte has successfully been promoted to overseer.";
            return RedirectToAction("All", "User", new { Area = AdminAreaName });
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Unexpected error occured while trying to promote user to overseer. Please try again.");

            viewModel.Academies = await academyService.GetAllAcademiesForDropdownSelectAsync();
            return View(viewModel);
        }
    }
}
