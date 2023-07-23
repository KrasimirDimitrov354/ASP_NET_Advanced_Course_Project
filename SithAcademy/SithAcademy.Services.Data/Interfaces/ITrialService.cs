namespace SithAcademy.Services.Data.Interfaces;

using SithAcademy.Web.ViewModels.Trial;

public interface ITrialService
{
    Task AssignTrialsToAcolyteAsync(int academyId, string acolyteId);

    Task RemoveTrialsFromAcolyteAsync(string acolyteId);

    Task<IEnumerable<IncompleteTrialViewModel>> GetIncompleteTrialsOfAcolyteAsync(string acolyteId);
}
