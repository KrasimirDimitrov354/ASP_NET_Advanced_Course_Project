namespace SithAcademy.Web.Areas.Admin.Services.Interfaces;

using SithAcademy.Web.Areas.Admin.ViewModels.User;

public interface IUserService
{
    Task<IEnumerable<UserPreviewViewModel>> GetAllUsersAsync();

    Task<bool> UserExistsAsync(string userId);

    Task<OverseerDetailsViewModel> GetOverseerForModificationAsync(string overseerId);

    Task ModifyOverseerAsync(string userId, OverseerDetailsViewModel viewModel);

    Task DemoteOverseer(string overseerId);

    Task PromoteOverseer(string userId, OverseerFormViewModel viewModel);

    Task SetLocationToUser(string userId, int locationId);
}
