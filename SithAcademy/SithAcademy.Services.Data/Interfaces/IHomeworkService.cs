namespace SithAcademy.Services.Data.Interfaces;

using SithAcademy.Web.ViewModels.Query;
using SithAcademy.Web.ViewModels.Homework;
using SithAcademy.Services.Data.Models.Homework;

public interface IHomeworkService
{
    Task<bool> TrialHasHomeworkAsync(string trialId, string acolyteId);

    Task<bool> HomeworkBelongsToUserAsync(string homeworkId, string userId);

    Task<bool> HomeworkExistsByIdAsync(string homeworkdId);

    Task<string> GetHomeworkIdByAcolyteIdAndTrialIdAsync(string acolyteId, string trialId);

    Task<string> GetUserIdByHomeworkIdAsync(string homeworkId);

    Task<SubmitHomeworkViewModel> GetHomeworkForEditAsync(string homeworkId);

    Task<string> SubmitHomeworkAndReturnIdAsync(string acolyteId, SubmitHomeworkViewModel homeworkModel);

    Task<string> GetTrialIdByHomeworkIdAsync(string homeworkId);

    Task<string> GetTrialTitleByHomeworkIdAsync(string homeworkId);

    Task<DisplayHomeworkViewModel> DisplayHomeworkDetailsAsync(string homeworkId);

    Task EditHomeworkAsync(string homeworkId, SubmitHomeworkViewModel viewModel);

    Task<bool> HomeworkCanBeGradedAsync(string homeworkId);

    Task<GradeHomeworkViewModel> GetHomeworkForGradingAsync(string homeworkId);

    Task<GradeHomeworkDetailsViewModel> GetHomeworkDetailsForGradingFormAsync(string homeworkId);

    Task<AllHomeworksFilteredAndPagedServiceModel> GetAllHomeworksAsync(AllHomeworksQueryModel queryModel, string overseerId = "");
}
