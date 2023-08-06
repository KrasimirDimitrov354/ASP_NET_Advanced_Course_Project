namespace SithAcademy.Services.Data;

using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using SithAcademy.Data;
using SithAcademy.Data.Models;
using SithAcademy.Web.ViewModels.Query;
using SithAcademy.Web.ViewModels.Homework;
using SithAcademy.Web.ViewModels.Query.Enums;
using SithAcademy.Services.Data.Interfaces;
using SithAcademy.Services.Data.Models.Homework;

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

    public async Task<AllHomeworksFilteredAndPagedServiceModel> GetAllHomeworksAsync(AllHomeworksQueryModel queryModel, string overseerId = "")
    {
        IQueryable<Homework> homeworksQuery;

        if (!string.IsNullOrWhiteSpace(overseerId))
        {
            homeworksQuery = dbContext.Homeworks
                .Where(h => h.Trial.Academy.Overseers.Any(o => o.Id.ToString() == overseerId))
                .AsQueryable();
        }
        else
        {
            homeworksQuery = dbContext.Homeworks.AsQueryable();
        }

        if (!string.IsNullOrWhiteSpace(queryModel.Trial))
        {
            homeworksQuery = homeworksQuery.Where(h => h.Trial.Title == queryModel.Trial);
        }

        if (!string.IsNullOrWhiteSpace(queryModel.SearchTerm))
        {
            string wildcard = $"%{queryModel.SearchTerm.ToLower()}%";

            homeworksQuery = homeworksQuery.Where(h => EF.Functions.Like(h.Acolyte.UserName, wildcard) ||
                                                       EF.Functions.Like(h.ReviewerName, wildcard));
        }

        homeworksQuery = queryModel.HomeworkSorting switch
        {
            HomeworkSorting.Newest => homeworksQuery.OrderByDescending(h => h.CreatedOn),
            HomeworkSorting.Oldest => homeworksQuery.OrderBy(h => h.CreatedOn),
            HomeworkSorting.HighestScoring => homeworksQuery.OrderByDescending(h => h.Score),
            HomeworkSorting.LowestScoring => homeworksQuery.OrderBy(h => h.Score),
            HomeworkSorting.Passed => homeworksQuery.OrderByDescending(h => h.Score >= h.Trial.ScoreToPass),
            HomeworkSorting.NotPassed => homeworksQuery.OrderByDescending(h => h.Score < h.Trial.ScoreToPass),
            _ => homeworksQuery
                    .OrderBy(h => h.ReviewerName == null)
                    .ThenByDescending(h => h.CreatedOn),
        };

        IEnumerable<HomeworkSortingViewModel> allHomeworks = await homeworksQuery
            .Skip((queryModel.CurrentPage - 1) * queryModel.RecordsPerPage)
            .Take(queryModel.RecordsPerPage)
            .Select(h => new HomeworkSortingViewModel()
            {
                Id = h.Id.ToString(),
                Submitter = h.Acolyte.UserName,
                SubmittedOn = h.CreatedOn.ToString("dddd, dd MMMM yyyy HH:mm:ss"),
                TrialTitle = h.Trial.Title,
                HomeworkScore = h.Score,
                TrialScore = h.Trial.ScoreToPass
            })
            .ToArrayAsync();

        return new AllHomeworksFilteredAndPagedServiceModel()
        {
            HomeworksCount = allHomeworks.Count(),
            Homeworks = allHomeworks,
        };
    }
}
