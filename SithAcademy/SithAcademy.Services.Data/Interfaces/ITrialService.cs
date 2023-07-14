namespace SithAcademy.Services.Data.Interfaces;

using SithAcademy.Web.ViewModels.Trial;

public interface ITrialService
{
    Task<IEnumerable<TrialOverviewViewModel>> GetInProgressTrialsOfUserAsync(string acolyteId);
}
