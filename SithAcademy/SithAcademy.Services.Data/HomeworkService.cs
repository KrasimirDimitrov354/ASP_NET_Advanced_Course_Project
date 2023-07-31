namespace SithAcademy.Services.Data;

using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using SithAcademy.Data;
using SithAcademy.Data.Models;
using SithAcademy.Web.ViewModels.Homework;
using SithAcademy.Services.Data.Interfaces;

public class HomeworkService : IHomeworkService
{
    private readonly AcademyDbContext dbContext;

    public HomeworkService(AcademyDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<bool> TrialHasHomework(string trialId, string acolyteId)
    {
        return await dbContext.Homeworks
            .AnyAsync(h => h.TrialId.ToString() == trialId && 
                           h.AcolyteId.ToString() == acolyteId);
    }

    public async Task<bool> HomeworkExistsByIdAsync(string homeworkdId)
    {
        return await dbContext.Homeworks.AnyAsync(h => h.Id.ToString() == homeworkdId);
    }

    public async Task<string> GetHomeworkIdByAcolyteIdAndTrialId(string acolyteId, string trialId)
    {
        Homework? homework = await dbContext.Homeworks
            .FirstOrDefaultAsync(h => h.AcolyteId.ToString() == acolyteId &&
                             h.TrialId.ToString() == trialId);

        if (homework != null)
        {
            return homework.Id.ToString();
        }

        return string.Empty;
    }

    public async Task<string> SubmitHomeworkAndReturnIdAsync(string acolyteId, SubmitHomeworkViewModel homeworkModel)
    {
        Homework homework = new Homework()
        {
            Content = homeworkModel.Content,
            TrialId = Guid.Parse(homeworkModel.TrialInfo!.Id),
            AcolyteId = Guid.Parse(acolyteId)
        };

        await dbContext.Homeworks.AddAsync(homework);
        await dbContext.SaveChangesAsync();

        return homework.Id.ToString();
    }

    public async Task<string> GetTrialIdByHomeworkIdAsync(string homeworkId)
    {
        Homework homework = await dbContext.Homeworks
            .FirstAsync(h => h.Id.ToString() == homeworkId);

        return homework.TrialId.ToString();
    }

    public async Task<DisplayHomeworkViewModel> DisplayHomeworkDetailsAsync(string homeworkId)
    {
        DisplayHomeworkViewModel homework = await dbContext.Homeworks
            .Where(h => h.Id.ToString() == homeworkId)
            .Select(h => new DisplayHomeworkViewModel()
            {
                Id = h.Id.ToString(),
                Content = h.Content,
                Score = h.Score,
                CreatedOn = h.CreatedOn.ToString("dddd, dd MMMM yyyy HH:mm:ss"),
                Reviewer = h.ReviewerName,
                Feedback = h.ReviewerFeedback
            })
            .FirstAsync();

        return homework;
    }
}
