namespace SithAcademy.Services.Data.Interfaces;

using SithAcademy.Web.ViewModels.Resource;

public interface IResourceService
{
    Task<bool> ResourceExistsAsync(string resourceId);

    Task<string> GetTrialIdByResourceIdAsync(string resourceId);

    Task<ResourceFormViewModel> GetResourceForModificationAsync(string resourceId);

    Task AddResourceAsync(ResourceFormViewModel viewModel);

    Task EditResourceAsync(string resourceId, ResourceFormViewModel viewModel);
}
