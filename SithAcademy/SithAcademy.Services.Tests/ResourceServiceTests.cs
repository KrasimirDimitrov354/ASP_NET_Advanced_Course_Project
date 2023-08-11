namespace SithAcademy.Services.Tests;

using System.Text.Json;

using Microsoft.EntityFrameworkCore;

using Ganss.Xss;

using SithAcademy.Data;
using SithAcademy.Services.Data;
using SithAcademy.Services.Data.Interfaces;
using SithAcademy.Web.ViewModels.Resource;

using static SithAcademy.Services.Tests.TestData.ResourceData;

public class ResourceServiceTests
{
    private DbContextOptions<AcademyDbContext> dbOptions;
    private AcademyDbContext dbContext;

    private IResourceService resourceService;
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
        resourceService = new ResourceService(dbContext, htmlSanitizer);
    }

    [Test]
    public async Task ResourceExistsAsyncShouldReturnTrueWhenExists()
    {
        bool result = await resourceService.ResourceExistsAsync(ExistingResourceId);
        Assert.IsTrue(result);
    }

    [Test]
    public async Task ResourceExistsAsyncShouldReturnFalseWhenDoesNotExist()
    {
        bool result = await resourceService.ResourceExistsAsync(NonExistingResourceId);
        Assert.IsFalse(result);
    }

    [Test]
    public async Task GetTrialIdByResourceIdAsyncShouldReturnTrialIdWhenExists()
    {
        string trialId = await resourceService.GetTrialIdByResourceIdAsync(ExistingResourceId);
        Assert.That(ExistingResourceTrialId, Is.EqualTo(trialId));
    }

    [Test]
    public async Task GetTrialIdByResourceIdAsyncShouldThrowWhenTrialIdDoesNotExist()
    {
        Assert.ThrowsAsync<InvalidOperationException>(async () => await resourceService.GetTrialIdByResourceIdAsync(NonExistingResourceId));
    }

    [Test]
    public async Task GetResourceForModificationAsyncShouldGetCorrectResourceWhenExists()
    {
        ResourceFormViewModel expectedResource = GenerateResource();
        ResourceFormViewModel returnedResource = await resourceService.GetResourceForModificationAsync(ExistingResourceId);

        string expected = JsonSerializer.Serialize(expectedResource);
        string returned = JsonSerializer.Serialize(returnedResource);

        Assert.That(returned, Is.EqualTo(expected));
    }

    [Test]
    public async Task GetResourceForModificationAsyncShouldThrowWhenResourceDoesNotExist()
    {
        Assert.ThrowsAsync<InvalidOperationException>(async () => await resourceService.GetResourceForModificationAsync(NonExistingResourceId));
    }
}