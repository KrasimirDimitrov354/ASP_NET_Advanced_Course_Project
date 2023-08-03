namespace SithAcademy.Services.Data.Interfaces;

using SithAcademy.Web.ViewModels.Trial;
using SithAcademy.Services.Data.Models.Trial;

public interface ITrialService
{
    Task AssignTrialsToAcolyteAsync(int academyId, string acolyteId);

    Task RemoveTrialsFromAcolyteAsync(string acolyteId);

    Task<IEnumerable<IncompleteTrialViewModel>> GetIncompleteTrialsOfAcolyteAsync(string acolyteId);

    Task<TrialDetailsViewModel> GetTrialDetailsAsync(string trialId);

    Task<bool> TrialExistsAsync(string trialId);

    Task<bool> TrialIsLockedAsync(string trialId);

    Task<bool> UserCanAccessTrialAsync(string trialId, string userId);

    Task<bool> UserHasCompletedTrialAsync(string trialId, string userId);

    Task<int> GetAcademyIdByTrialIdAsync(string trialId);

    Task<string> AddTrialAndReturnTrialIdAsync(int academyId, TrialFormViewModel viewModel);

    Task AddTrialToAllAcolytesInAcademyAsync(string trialId, int academyId);

    Task<TrialFormViewModel> GetTrialForModificationAsync(string trialId);

    Task EditTrialAsync(string trialId, TrialFormViewModel viewModel);

    Task CompleteTrialAsync(string trialId, string userId, decimal trialScore);

    Task<IEnumerable<TrialOverviewViewModel>> GetAllTrialsForSelectByAcademyIdAsync(int academyId);

    Task<AllTrialsFilteredAndPagedServiceModel> GetAllTrialsAsync(AllTrialsQueryModel queryModel);

    Task<Trial_InfoViewModel> GetTrialInfoForHomeworkAsync(string trialId);
}
