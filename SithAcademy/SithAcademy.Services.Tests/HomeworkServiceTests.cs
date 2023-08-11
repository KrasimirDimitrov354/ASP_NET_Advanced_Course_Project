namespace SithAcademy.Services.Tests;

using Microsoft.EntityFrameworkCore;

using Ganss.Xss;

using SithAcademy.Data;
using SithAcademy.Services.Data;
using SithAcademy.Services.Data.Interfaces;

using static SithAcademy.Services.Tests.TestData.HomeworkData;
using static SithAcademy.Services.Tests.TestData.UserData;

public class HomeworkServiceTests
{
    private DbContextOptions<AcademyDbContext> dbOptions;
    private AcademyDbContext dbContext;

    private IHomeworkService homeworkService;
    private IHtmlSanitizer htmlSanitizer;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        dbOptions = new DbContextOptionsBuilder<AcademyDbContext>()
            .UseInMemoryDatabase("SithAcademyInMemory" + Guid.NewGuid().ToString())
            .Options;

        dbContext = new AcademyDbContext(dbOptions);
        dbContext.Database.EnsureCreated();

        htmlSanitizer = new HtmlSanitizer();
        homeworkService = new HomeworkService(dbContext, htmlSanitizer);
    }

    [Test]
    public async Task HomeworkExistsByIdAsyncShouldReturnTrueWhenExists()
    {
        bool result = await homeworkService.HomeworkExistsByIdAsync(ExistingHomeworkId);
        Assert.IsTrue(result);
    }

    [Test]
    public async Task HomeworkExistsByIdAsyncShouldReturnFalseWhenDoesNotExist()
    {
        bool result = await homeworkService.HomeworkExistsByIdAsync(NonExistingHomeworkId);
        Assert.IsFalse(result);
    }

    [Test]
    public async Task HomeworkBelongsToUserAsyncShouldReturnTrueWhenHomeworkHasUserId()
    {
        bool result = await homeworkService.HomeworkBelongsToUserAsync(ExistingHomeworkId, ExistingUserId);
        Assert.IsTrue(result);
    }

    [Test]
    public async Task HomeworkBelongsToUserAsyncShouldReturnFalseWhenHomeworkDoesNotHaveUserId()
    {
        bool result = await homeworkService.HomeworkBelongsToUserAsync(ExistingHomeworkId, NonExistingUserId);
        Assert.IsFalse(result);
    }

    [Test]
    public async Task HomeworkBelongsToUserAsyncShouldReturnFalseWhenHomeworkDoesNotExist()
    {
        bool result = await homeworkService.HomeworkBelongsToUserAsync(NonExistingHomeworkId, ExistingUserId);
        Assert.IsFalse(result);
    }
}
