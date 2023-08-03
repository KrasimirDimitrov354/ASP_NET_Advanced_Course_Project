namespace SithAcademy.Services.Data.Interfaces;

using SithAcademy.Web.ViewModels.Homework;

public interface IHomeworkService
{
    Task<bool> TrialHasHomeworkAsync(string trialId, string acolyteId);

    Task<bool> HomeworkExistsByIdAsync(string homeworkdId);

    Task<string> GetHomeworkIdByAcolyteIdAndTrialIdAsync(string acolyteId, string trialId);

    Task<SubmitHomeworkViewModel> GetHomeworkForEditAsync(string homeworkId);

    Task<string> SubmitHomeworkAndReturnIdAsync(string acolyteId, SubmitHomeworkViewModel homeworkModel);

    Task<string> GetTrialIdByHomeworkIdAsync(string homeworkId);

    Task<DisplayHomeworkViewModel> DisplayHomeworkDetailsAsync(string homeworkId);

    Task EditHomeworkAsync(string homeworkId, SubmitHomeworkViewModel viewModel);
}
