namespace SithAcademy.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using SithAcademy.Web.ViewModels.Query;
using SithAcademy.Web.ViewModels.Homework;
using SithAcademy.Web.Infrastructure.Extensions;
using SithAcademy.Services.Data.Interfaces;
using SithAcademy.Services.Data.Models.Homework;

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
        if (User.IsAdmin())
        {
            TempData[ErrorMessage] = "The High Inquisitor does not waste his time with submitting homeworks!";
            return RedirectToAction("Index", "Home");
        }

        string userId = User.GetId()!;
        bool userIsOverseer = await overseerService.UserIsOverseerAsync(userId);
        if (userIsOverseer)
        {
            TempData[ErrorMessage] = "Overseers cannot submit new homeworks. You should have already done it!";
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
            TempData[WarningMessage] = "You do not have access to this trial.";
            return RedirectToAction("Index", "Home");
        }

        bool trialIsLocked = await trialService.TrialIsLockedAsync(id);
        if (trialIsLocked)
        {
            TempData[WarningMessage] = "Trial has been locked. Await further developments.";
            return RedirectToAction("Index", "Home");
        }

        bool trialHasHomework = await homeworkService.TrialHasHomeworkAsync(id, userId);
        if (trialHasHomework)
        {
            TempData[WarningMessage] = "You have already submitted a homework for this trial.";

            string homeworkId = await homeworkService.GetHomeworkIdByAcolyteIdAndTrialIdAsync(userId, id);
            return RedirectToAction("Details", "Homework", new { id = homeworkId });
        }

        SubmitHomeworkViewModel viewModel = new SubmitHomeworkViewModel();
        viewModel.TrialInfo = await trialService.GetTrialInfoForHomeworkAsync(id);
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Submit(string id, SubmitHomeworkViewModel homeworkModel)
    {
        if (User.IsAdmin())
        {
            TempData[ErrorMessage] = "The High Inquisitor does not waste his time with submitting homeworks!";
            return RedirectToAction("Index", "Home");
        }

        string userId = User.GetId()!;
        bool userIsOverseer = await overseerService.UserIsOverseerAsync(userId);
        if (userIsOverseer)
        {
            TempData[ErrorMessage] = "Overseers cannot submit new homeworks. You should have already done it!";
            return RedirectToAction("Index", "Home");
        }

        bool trialExists = await trialService.TrialExistsAsync(id);
        if (!trialExists)
        {
            ModelState.AddModelError(nameof(id), "Selected trial does not exist!");
        }

        if (!ModelState.IsValid)
        {
            homeworkModel.TrialInfo = await trialService.GetTrialInfoForHomeworkAsync(id);
            return View(homeworkModel);
        }

        bool acolyteCanAccessTrial = await trialService.UserCanAccessTrialAsync(id, userId);
        if (!acolyteCanAccessTrial)
        {
            TempData[WarningMessage] = "You do not have access to this trial.";
            return RedirectToAction("Index", "Home");
        }

        bool trialIsLocked = await trialService.TrialIsLockedAsync(id);
        if (trialIsLocked)
        {
            TempData[WarningMessage] = "Trial has been locked. Await further developments.";
            return RedirectToAction("Index", "Home");
        }

        bool trialHasHomework = await homeworkService.TrialHasHomeworkAsync(id, userId);
        if (trialHasHomework)
        {
            TempData[WarningMessage] = "You have already submitted a homework for this trial.";

            string homeworkId = await homeworkService.GetHomeworkIdByAcolyteIdAndTrialIdAsync(userId, id);
            return RedirectToAction("Details", "Homework", new { id = homeworkId });
        }

        try
        {
            homeworkModel.TrialInfo = await trialService.GetTrialInfoForHomeworkAsync(id);
            string homeworkId = await homeworkService.SubmitHomeworkAndReturnIdAsync(userId, homeworkModel);

            TempData[SuccessMessage] = $"You have successfully submitted your homework for the {homeworkModel.TrialInfo.Title}";

            return RedirectToAction("Details", "Homework", new { id = homeworkId });
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Unexpected error occured while adding your homework, please try again.");

            homeworkModel.TrialInfo = await trialService.GetTrialInfoForHomeworkAsync(id);
            return View(homeworkModel);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        if (User.IsAdmin())
        {
            TempData[ErrorMessage] = "The High Inquisitor does not waste his time with submitting homeworks!";
            return RedirectToAction("Index", "Home");
        }

        bool homeworkExists = await homeworkService.HomeworkExistsByIdAsync(id);
        if (!homeworkExists)
        {
            TempData[ErrorMessage] = "No homework with such ID found.";
            return RedirectToAction("Index", "Home");
        }

        string userId = User.GetId()!;
        string trialId = await homeworkService.GetTrialIdByHomeworkIdAsync(id);

        if (!User.IsAdmin())
        {
            bool homeworkBelongsToUser = await homeworkService.HomeworkBelongsToUserAsync(id, userId);
            if (!homeworkBelongsToUser)
            {
                bool userIsOverseer = await overseerService.UserIsOverseerAsync(userId);
                if (userIsOverseer)
                {
                    string overseerId = await overseerService.GetOverseerIdAsync(userId);
                    int academyId = await trialService.GetAcademyIdByTrialIdAsync(trialId);

                    bool overseerCanModify = await overseerService.OverseerCanModifyAsync(academyId, overseerId);
                    if (!overseerCanModify)
                    {
                        TempData[WarningMessage] = "You cannot view details of homeworks not submitted to your academy";
                        return RedirectToAction("Details", "Academy", new { id = academyId });
                    }
                }
                else
                {
                    TempData[WarningMessage] = "You are trying to access homework you haven't submitted.";
                    return RedirectToAction("Details", "Trial", new { id = trialId });
                }  
            }
        }

        try
        {
            DisplayHomeworkViewModel homeworkModel = await homeworkService.DisplayHomeworkDetailsAsync(id);
            homeworkModel.TrialInfo = await trialService.GetTrialInfoForHomeworkAsync(trialId);

            return View(homeworkModel);
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home", new { id = 500 });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        bool homeworkExists = await homeworkService.HomeworkExistsByIdAsync(id);
        if (!homeworkExists)
        {
            TempData[ErrorMessage] = "No homework with such ID found.";
            return RedirectToAction("Index", "Home");
        }

        string userId = User.GetId()!;
        string trialId = await homeworkService.GetTrialIdByHomeworkIdAsync(id);
        bool homeworkBelongsToUser = await homeworkService.HomeworkBelongsToUserAsync(id, userId);
        if (!homeworkBelongsToUser)
        {
            TempData[WarningMessage] = "You are trying to access homework you haven't submitted.";
            return RedirectToAction("Details", "Trial", new { id = trialId });
        }

        bool trialIsCompleted = await trialService.UserHasCompletedTrialAsync(trialId, userId);
        if (trialIsCompleted)
        {
            TempData[WarningMessage] = "You cannot edit a homework for a trial which you have already completed.";
            return RedirectToAction("Details", "Homework", new { id });
        }

        try
        {
            SubmitHomeworkViewModel homeworkToEdit = await homeworkService.GetHomeworkForEditAsync(id);
            homeworkToEdit.TrialInfo = await trialService.GetTrialInfoForHomeworkAsync(trialId);
            return View(homeworkToEdit);
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home", new { id = 500 });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string id, SubmitHomeworkViewModel homeworkToEdit)
    {
        bool homeworkExists = await homeworkService.HomeworkExistsByIdAsync(id);
        if (!homeworkExists)
        {
            TempData[ErrorMessage] = "No homework with such ID found.";
            return RedirectToAction("Index", "Home");
        }

        string userId = User.GetId()!;
        string trialId = await homeworkService.GetTrialIdByHomeworkIdAsync(id);
        bool homeworkBelongsToUser = await homeworkService.HomeworkBelongsToUserAsync(id, userId);
        if (!homeworkBelongsToUser)
        {
            TempData[WarningMessage] = "You are trying to access homework you haven't submitted.";
            return RedirectToAction("Details", "Trial", new { id = trialId });
        }

        bool trialIsCompleted = await trialService.UserHasCompletedTrialAsync(trialId, userId);
        if (trialIsCompleted)
        {
            TempData[WarningMessage] = "You cannot edit a homework for a trial which you have already completed.";
            return RedirectToAction("Details", "Homework", new { id });
        }

        if (!ModelState.IsValid)
        {
            homeworkToEdit.TrialInfo = await trialService.GetTrialInfoForHomeworkAsync(id);
            return View(homeworkToEdit);
        }

        try
        {
            await homeworkService.EditHomeworkAsync(id, homeworkToEdit);
            TempData[SuccessMessage] = "Homework has been edited successfully."; 
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Unexpected error occured while trying to edit your homework details, please try again later.";
        }

        return RedirectToAction("Details", "Homework", new { id });
    }

    [HttpGet]
    public async Task<IActionResult> Grade(string id)
    {
        bool homeworkExists = await homeworkService.HomeworkExistsByIdAsync(id);
        if (!homeworkExists)
        {
            TempData[ErrorMessage] = "No homework with such ID found.";
            return RedirectToAction("Index", "Home");
        }

        if (!User.IsAdmin())
        {
            string userId = User.GetId()!;
            bool userIsOverseer = await overseerService.UserIsOverseerAsync(userId);
            if (!userIsOverseer)
            {
                TempData[ErrorMessage] = "Acolytes cannot grade homeworks.";
                return RedirectToAction("Index", "Home");
            }

            string trialId = await homeworkService.GetTrialIdByHomeworkIdAsync(id);
            int academyId = await trialService.GetAcademyIdByTrialIdAsync(trialId);
            string overseerId = await overseerService.GetOverseerIdAsync(userId);
            bool overseerCanModify = await overseerService.OverseerCanModifyAsync(academyId, overseerId);
            if (!overseerCanModify)
            {
                TempData[ErrorMessage] = "You don't have access to this homework.";
                return RedirectToAction("Index", "Home");
            }

            bool homeworkBelongsToUser = await homeworkService.HomeworkBelongsToUserAsync(id, userId);
            if (homeworkBelongsToUser)
            {
                TempData[ErrorMessage] = "You cannot grade your own homework.";
                return RedirectToAction("Index", "Home");
            }
        }

        bool homeworkCanBeGraded = await homeworkService.HomeworkCanBeGradedAsync(id);
        if (!homeworkCanBeGraded)
        {
            TempData[WarningMessage] = "This homework has already been graded as sufficient for trial completion.";
            return RedirectToAction("Index", "Home");
        }

        try
        {
            GradeHomeworkViewModel homeworkToGrade = await homeworkService.GetHomeworkForGradingAsync(id);
            return View(homeworkToGrade);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Unexpected error occured while trying to get homework details, please try again later.";
            return RedirectToAction("Details", "Homework", new { id });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Grade(string id, GradeHomeworkViewModel viewModel)
    {
        bool homeworkExists = await homeworkService.HomeworkExistsByIdAsync(id);
        if (!homeworkExists)
        {
            TempData[ErrorMessage] = "No homework with such ID found.";
            return RedirectToAction("Index", "Home");
        }

        string userId = User.GetId()!;
        bool userIsOverseer = await overseerService.UserIsOverseerAsync(userId);
        if (!userIsOverseer && !User.IsAdmin())
        {
            TempData[ErrorMessage] = "Acolytes cannot grade homeworks.";
            return RedirectToAction("Index", "Home");
        }

        string trialId = await homeworkService.GetTrialIdByHomeworkIdAsync(id);
        int academyId = await trialService.GetAcademyIdByTrialIdAsync(trialId);
        string overseerId = await overseerService.GetOverseerIdAsync(userId);
        bool overseerCanModify = await overseerService.OverseerCanModifyAsync(academyId, overseerId);
        if (!overseerCanModify && !User.IsAdmin())
        {
            TempData[ErrorMessage] = "You don't have access to this homework.";
            return RedirectToAction("Index", "Home");
        }

        bool homeworkBelongsToUser = await homeworkService.HomeworkBelongsToUserAsync(id, userId);
        if (homeworkBelongsToUser)
        {
            TempData[ErrorMessage] = "You cannot grade your own homework.";
            return RedirectToAction("Index", "Home");
        }

        bool homeworkCanBeGraded = await homeworkService.HomeworkCanBeGradedAsync(id);
        if (!homeworkCanBeGraded)
        {
            TempData[WarningMessage] = "This homework has already been graded as sufficient for trial completion.";
            return RedirectToAction("Index", "Home");
        }

        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        try
        {
            if (User.IsAdmin())
            {
                await overseerService.GradeHomeworkAsync(viewModel);
            }
            else
            {
                await overseerService.GradeHomeworkAsync(viewModel, overseerId);
            }

            string acolyteId = await homeworkService.GetUserIdByHomeworkIdAsync(id);
            await trialService.CompleteTrialAsync(trialId, acolyteId, viewModel.Score);

            TempData[SuccessMessage] = "You have succesfully reviewed and graded the chosen homework.";
            return RedirectToAction("Index", "Home");
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Unexpected error occured while grading homework, please try again.";
            return View(viewModel);
        }
    }

    [HttpGet]
    public async Task<IActionResult> All([FromQuery]AllHomeworksQueryModel queryModel)
    {
        string userId = User.GetId()!;
        bool userIsOverseer = await overseerService.UserIsOverseerAsync(userId);
        if (!userIsOverseer && !User.IsAdmin())
        {
            TempData[ErrorMessage] = "You do not have access to this page.";
            return RedirectToAction("Index", "Home");
        }

        AllHomeworksFilteredAndPagedServiceModel serviceModel;

        if (User.IsAdmin())
        {
            serviceModel = await homeworkService.GetAllHomeworksAsync(queryModel);
            queryModel.Trials = await trialService.GetAllTrialTitlesForQuerySelectAsync();
        }
        else
        {
            string overseerId = await overseerService.GetOverseerIdAsync(userId);
            serviceModel = await homeworkService.GetAllHomeworksAsync(queryModel, overseerId, userId);

            int academyId = await overseerService.GetAcademyIdByOverseerIdAsync(overseerId);
            queryModel.Trials = await trialService.GetAllTrialTitlesForQuerySelectAsync(academyId);
        }

        queryModel.Homeworks = serviceModel.Homeworks;
        queryModel.TotalRecords = serviceModel.HomeworksCount;

        return View(queryModel);
    }
}
