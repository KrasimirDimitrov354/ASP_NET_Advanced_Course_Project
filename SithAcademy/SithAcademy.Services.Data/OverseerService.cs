namespace SithAcademy.Services.Data;

using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using SithAcademy.Data;
using SithAcademy.Data.Models;
using SithAcademy.Services.Data.Interfaces;
using SithAcademy.Web.ViewModels.Homework;

public class OverseerService : IOverseerService
{
    private readonly AcademyDbContext dbContext;

    public OverseerService(AcademyDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<bool> UserIsOverseerAsync(string userId)
    {
        return await dbContext.Overseers.AnyAsync(o => o.UserId.ToString() == userId);
    }

    public async Task<string> GetOverseerIdAsync(string userId)
    {
        Overseer overseer = await dbContext.Overseers
            .FirstAsync(o => o.UserId.ToString() == userId);

        return overseer.Id.ToString();
    }

    public async Task<int> GetAcademyIdByOverseerIdAsync(string overseerId)
    {
        Overseer overseer = await dbContext.Overseers
            .FirstAsync(o => o.Id.ToString() == overseerId);

        return overseer.AcademyId;
    }

    public async Task<bool> OverseerCanModifyAsync(int academyId, string overseerId)
    {
        Overseer? overseer = await dbContext.Overseers
            .Where(o => o.AcademyId == academyId)
            .FirstOrDefaultAsync(o => o.Id.ToString() == overseerId);

        if (overseer == null)
        {
            return false;
        }

        return true;
    }

    public async Task GradeHomeworkAsync(string overseerId, GradeHomeworkViewModel viewModel)
    {
        Overseer overseer = await dbContext.Overseers.FirstAsync(o => o.Id.ToString() == overseerId);

        Homework homework = await dbContext.Homeworks.FirstAsync(h => h.Id.ToString() == viewModel.Id);

        homework.ReviewerName = overseer.Title;
        homework.ReviewerFeedback = viewModel.Feedback;
        homework.Score = viewModel.Score;

        await dbContext.SaveChangesAsync();
    }
}
