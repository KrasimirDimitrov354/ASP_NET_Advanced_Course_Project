namespace SithAcademy.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using SithAcademy.Services.Data.Interfaces;
using SithAcademy.Web.ViewModels.Homework;
using SithAcademy.Web.Infrastructure.Extensions;

using static SithAcademy.Common.GeneralConstants;

[Authorize]
public class HomeworkController : Controller
{
    private readonly IHomeworkService homeworkService;
    private readonly IOverseerService overseerService;
    private readonly ITrialService trialService;

    public HomeworkController(IHomeworkService homeworkService, 
        IOverseerService overseerService, 
        ITrialService trialService)
    {
        this.homeworkService = homeworkService;
        this.overseerService = overseerService;
        this.trialService = trialService;

    }

    [HttpGet]
    public async Task<IActionResult> Submit(string id)
    {
        string userId = User.GetId()!;
        bool userIsOverseer = await overseerService.UserIsOverseerAsync(userId);
        if (userIsOverseer)
        {
            TempData[ErrorMessage] = "Overseers cannot create new homeworks. You should have already done it!";
            return RedirectToAction("Index", "Home");
        }

        bool trialExists = await trialService.TrialExistsAsync(id);
        if (!trialExists)
        {
            TempData[ErrorMessage] = "Incorrect trial ID selected!";
            return RedirectToAction("Index", "Home");
        }

        bool acolyteCanAccessTrial = await trialService.UserCanAccessTrialAsync(id, userId);
        if(!acolyteCanAccessTrial)
        {
            TempData[WarningMessage] = "You do not have access to this trial";
            return RedirectToAction("Index", "Home");
        }

        bool trialHasHomework = await homeworkService.TrialHasHomework(id, userId);
        if (trialHasHomework)
        {
            TempData[WarningMessage] = "You have already submitted a homework for this trial.";

            string homeworkId = await homeworkService.GetHomeworkIdByAcolyteIdAndTrialId(userId, id);
            return RedirectToAction("Details", "Homework", new { id = homeworkId });
        }

        SubmitHomeworkViewModel viewModel = new SubmitHomeworkViewModel();
        viewModel.TrialInfo = await trialService.GetTrialInfoForHomeworkAsync(id);
        return View(viewModel);
    }
}
