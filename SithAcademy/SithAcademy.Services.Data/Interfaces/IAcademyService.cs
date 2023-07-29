namespace SithAcademy.Services.Data.Interfaces;

using SithAcademy.Web.ViewModels.Academy;

public interface IAcademyService
{
    Task<IEnumerable<AcademyOverviewViewModel>> GetAllAcademiesAsync();

    Task<AcademyDetailsViewModel> DisplayAcademyDetailsAsync(int academyId);

    Task<int> GetLocationIdByAcademyIdAsync(int academyId);

    Task<AcademyFormViewModel> GetAcademyForModificationAsync(int academyId);

    Task EditAcademyAsync(int academyId, AcademyFormViewModel viewModel);

    Task ChangeAcademyLockStatusAsync(int academyId);

    Task<bool> AcademyExistsAsync(int academyId);

    Task<bool> AcademyIsLockedAsync(int academyId);

    Task<bool> AcolyteExistsInAcademyAsync(int academyId, string acolyteId);

    Task AddAcolyteToAcademyAsync(int academyId, string acolyteId);

    Task RemoveAcolyteFromAcademyAsync(int academyId, string acolyteId);

    Task<IEnumerable<string>> GetAllAcademyNamesForQuerySelectAsync();
}
