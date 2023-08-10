namespace SithAcademy.Services.Data;

using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Ganss.Xss;

using SithAcademy.Data;
using SithAcademy.Data.Models;
using SithAcademy.Web.ViewModels.Resource;
using SithAcademy.Services.Data.Interfaces;

public class ResourceService : IResourceService
{
    private readonly AcademyDbContext dbContext;
    private readonly IHtmlSanitizer htmlSanitizer;

    public ResourceService(AcademyDbContext dbContext, IHtmlSanitizer htmlSanitizer)
    {
        this.dbContext = dbContext;
        this.htmlSanitizer = htmlSanitizer;
    }

    public async Task<bool> ResourceExistsAsync(string resourceId)
    {
        return await dbContext.Resources.AnyAsync(r => r.Id.ToString() == resourceId);
    }

    public async Task<string> GetTrialIdByResourceIdAsync(string resourceId)
    {
        Resource resource = await dbContext.Resources.FirstAsync(r => r.Id.ToString() == resourceId);

        return resource.TrialId.ToString();
    }

    public async Task<ResourceFormViewModel> GetResourceForModificationAsync(string resourceId)
    {
        ResourceFormViewModel resource = await dbContext.Resources
            .Where(r => r.Id.ToString() == resourceId)
            .Select(r => new ResourceFormViewModel()
            {
                Name = r.Name,
                ImageUrl = r.ImageUrl,
                SourceUrl = r.SourceUrl,
                TrialId = r.TrialId.ToString()
            })
            .FirstAsync();

        return resource;
    }

    public async Task AddResourceAsync(ResourceFormViewModel viewModel)
    {
        Resource resource = new Resource()
        {
            Name = htmlSanitizer.Sanitize(viewModel.Name),
            ImageUrl = htmlSanitizer.Sanitize(viewModel.ImageUrl),
            SourceUrl = htmlSanitizer.Sanitize(viewModel.SourceUrl),
            TrialId = Guid.Parse(viewModel.TrialId)
        };

        await dbContext.Resources.AddAsync(resource);
        await dbContext.SaveChangesAsync();
    }

    public async Task EditResourceAsync(string resourceId, ResourceFormViewModel viewModel)
    {
        Resource resource = await dbContext.Resources
            .FirstAsync(r => r.Id.ToString() == resourceId);

        resource.Name = htmlSanitizer.Sanitize(viewModel.Name);
        resource.ImageUrl = htmlSanitizer.Sanitize(viewModel.ImageUrl);
        resource.SourceUrl = htmlSanitizer.Sanitize(viewModel.SourceUrl);
        resource.TrialId = Guid.Parse(viewModel.TrialId);
        resource.IsDeleted = viewModel.IsDeleted;

        await dbContext.SaveChangesAsync();
    }
}
