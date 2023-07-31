namespace SithAcademy.Services.Data.Interfaces;

using SithAcademy.Web.ViewModels.Homework;

public interface IHomeworkService
{
    Task<bool> TrialHasHomework(string trialId, string acolyteId);

    Task<bool> HomeworkExistsByIdAsync(string homeworkdId);

    Task<string> GetHomeworkIdByAcolyteIdAndTrialId(string acolyteId, string trialId);

    Task<string> SubmitHomeworkAndReturnIdAsync(string acolyteId, SubmitHomeworkViewModel homeworkModel);

    Task<string> GetTrialIdByHomeworkIdAsync(string homeworkId);

    Task<DisplayHomeworkViewModel> DisplayHomeworkDetailsAsync(string homeworkId);
}
