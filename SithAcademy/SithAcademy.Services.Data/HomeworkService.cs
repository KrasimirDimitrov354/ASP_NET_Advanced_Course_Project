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

    public async Task<bool> TrialHasHomeworkAsync(string trialId, string acolyteId)
    {
        return await dbContext.Homeworks
            .AnyAsync(h => h.TrialId.ToString() == trialId && 
                           h.AcolyteId.ToString() == acolyteId);
    }

    public async Task<bool> HomeworkBelongsToUserAsync(string homeworkId, string userId)
    {
        Homework? homework = await dbContext.Homeworks
            .FirstOrDefaultAsync(h => h.Id.ToString() == homeworkId && 
                                      h.AcolyteId.ToString() == userId);

        if (homework == null)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> HomeworkExistsByIdAsync(string homeworkdId)
    {
        return await dbContext.Homeworks.AnyAsync(h => h.Id.ToString() == homeworkdId);
    }

    public async Task<string> GetHomeworkIdByAcolyteIdAndTrialIdAsync(string acolyteId, string trialId)
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

    public async Task<string> GetUserIdByHomeworkIdAsync(string homeworkId)
    {
        Homework homework = await dbContext.Homeworks
            .FirstAsync(h => h.Id.ToString() == homeworkId);

        return homework.AcolyteId.ToString();
    }

    public async Task<SubmitHomeworkViewModel> GetHomeworkForEditAsync(string homeworkId)
    {
        SubmitHomeworkViewModel homeworkToEdit = await dbContext.Homeworks
            .Where(h => h.Id.ToString() == homeworkId)
            .Select(h => new SubmitHomeworkViewModel()
            {
                Content = h.Content
            })
            .FirstAsync();

        return homeworkToEdit;
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

    public async Task<string> GetTrialTitleByHomeworkIdAsync(string homeworkId)
    {
        Homework homework = await dbContext.Homeworks
            .Include(h => h.Trial)
            .FirstAsync(h => h.Id.ToString() == homeworkId);

        return homework.Trial.Title.ToString();
    }

    public async Task<DisplayHomeworkViewModel> DisplayHomeworkDetailsAsync(string homeworkId)
    {
        DisplayHomeworkViewModel homework = await dbContext.Homeworks
            .Include(h => h.Trial)
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

    public async Task EditHomeworkAsync(string homeworkId, SubmitHomeworkViewModel viewModel)
    {
        Homework homework = await dbContext.Homeworks
            .FirstAsync(h => h.Id.ToString() == homeworkId);

        homework.Content = viewModel.Content;

        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> HomeworkCanBeGradedAsync(string homeworkId)
    {
        Homework homework = await dbContext.Homeworks
            .Include(h => h.Trial)
            .FirstAsync(h => h.Id.ToString() == homeworkId);

        return homework.Score < homework.Trial.ScoreToPass;
    }

    public async Task<GradeHomeworkViewModel> GetHomeworkForGradingAsync(string homeworkId)
    {
        GradeHomeworkViewModel homeworkToGrade = await dbContext.Homeworks
            .Where(h => h.Id.ToString() == homeworkId)
            .Select(h => new GradeHomeworkViewModel()
            {
                Id = h.Id.ToString(),
                Score = h.Score,
                Feedback = h.ReviewerFeedback,
            })
            .FirstAsync();

        return homeworkToGrade;
    }

    public async Task<GradeHomeworkDetailsViewModel> GetHomeworkDetailsForGradingFormAsync(string homeworkId)
    {
        GradeHomeworkDetailsViewModel homeworkDetails = await dbContext.Homeworks
            .Include(h => h.Acolyte)
            .Where(h => h.Id.ToString() == homeworkId)
            .Select(h => new GradeHomeworkDetailsViewModel()
            {
                Content = h.Content,
                CreatedOn = h.CreatedOn.ToString("dddd, dd MMMM yyyy HH:mm:ss"),
                Submitter = h.Acolyte.UserName,
                CurrentScore = h.Score,
                LastReviewer = h.ReviewerName,
                LastFeedback = h.ReviewerFeedback
            })
            .FirstAsync();

        return homeworkDetails;
    }
}
