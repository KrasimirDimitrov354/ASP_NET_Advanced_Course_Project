namespace SithAcademy.Web.Areas.Admin.Services;

using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using SithAcademy.Data;
using SithAcademy.Data.Models;
using SithAcademy.Web.Areas.Admin.ViewModels.User;
using SithAcademy.Web.Areas.Admin.Services.Interfaces;

using static SithAcademy.Common.GeneralConstants;

public class UserService : IUserService
{
    private readonly AcademyDbContext dbContext;

    public UserService(AcademyDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<UserPreviewViewModel>> GetAllUsersAsync()
    {
        IEnumerable<UserPreviewViewModel> allUsers = await dbContext.Users
            .Where(u => u.UserName != AdminRoleName)
            .Select(u => new UserPreviewViewModel()
            {
                Id = u.Id.ToString(),
                Username = u.UserName,
                IsOverseer = false
            })
            .ToArrayAsync();

        return allUsers;
    }

    public async Task<bool> UserExistsAsync(string userId)
    {
        return await dbContext.Users.AnyAsync(u => u.Id.ToString() == userId);
    }

    public async Task<OverseerDetailsViewModel> GetOverseerForModificationAsync(string overseerId)
    {
        OverseerDetailsViewModel overseerDetails = await dbContext.Overseers
            .Include(o => o.Academy)
            .Where(o => o.Id.ToString() == overseerId)
            .Select(o => new OverseerDetailsViewModel()
            {
                Title = o.Title,
                CurrentAcademyId = o.AcademyId,
                CurrentAcademyTitle = o.Academy.Title
            })
            .FirstAsync();

        return overseerDetails;
    }

    public async Task ModifyOverseerAsync(string userId, OverseerDetailsViewModel viewModel)
    {
        Overseer overseer = await dbContext.Overseers
            .FirstAsync(o => o.UserId.ToString() == userId);

        overseer.Title = viewModel.Title;
        overseer.AcademyId = viewModel.AcademyId;

        await dbContext.SaveChangesAsync();
    }

    public async Task DemoteOverseer(string userId)
    {
        Overseer overseer = await dbContext.Overseers
            .FirstAsync(o => o.UserId.ToString() == userId);

        dbContext.Overseers.Remove(overseer);

        await dbContext.SaveChangesAsync();
    }

    public async Task PromoteOverseer(string userId, OverseerFormViewModel viewModel)
    {
        Overseer overseer = new Overseer()
        {
            Title = viewModel.Title,
            AcademyId = viewModel.AcademyId,
            UserId = Guid.Parse(userId),
        };

        await dbContext.Overseers.AddAsync(overseer);
        await dbContext.SaveChangesAsync();
    }

    public async Task SetLocationToUser(string userId, int locationId)
    {
        AcademyUser user = await dbContext.Users.FirstAsync(u => u.Id.ToString() == userId);

        user.LocationId = locationId;
        await dbContext.SaveChangesAsync();
    }
}
