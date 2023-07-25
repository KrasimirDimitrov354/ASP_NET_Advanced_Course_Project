namespace SithAcademy.Services.Data.Interfaces;

using SithAcademy.Web.ViewModels.Trial;

public interface ITrialService
{
    Task AssignTrialsToAcolyteAsync(int academyId, string acolyteId);

    Task RemoveTrialsFromAcolyteAsync(string acolyteId);

    Task<IEnumerable<IncompleteTrialViewModel>> GetIncompleteTrialsOfAcolyteAsync(string acolyteId);

    Task<TrialDetailsViewModel> GetTrialDetailsAsync(string trialId);

    Task<bool> TrialExistsAsync(string trialId);

    Task<bool> UserCanAccessTrialAsync(string trialId, string userId);

    Task<int> GetAcademyIdByTrialIdAsync(string trialId);

    Task<string> AddTrialAndReturnTrialIdAsync(int academyId, TrialFormViewModel viewModel);

    Task AddTrialToAllAcolytesInAcademyAsync(string trialId, int academyId);
}
