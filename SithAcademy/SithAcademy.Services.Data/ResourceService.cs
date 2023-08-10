namespace SithAcademy.Services.Data;

using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using SithAcademy.Data;
using SithAcademy.Data.Models;
using SithAcademy.Web.ViewModels.Resource;
using SithAcademy.Services.Data.Interfaces;
using SithAcademy.Services.Infrastructure;

public class ResourceService : IResourceService
{
    private readonly AcademyDbContext dbContext;
    private readonly Sanitizer sanitizer;

    public ResourceService(AcademyDbContext dbContext)
    {
        this.dbContext = dbContext;
        sanitizer = new Sanitizer();
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
            Name = sanitizer.Sanitize(viewModel.Name),
            ImageUrl = sanitizer.Sanitize(viewModel.ImageUrl),
            SourceUrl = sanitizer.Sanitize(viewModel.SourceUrl),
            TrialId = Guid.Parse(viewModel.TrialId)
        };

        await dbContext.Resources.AddAsync(resource);
        await dbContext.SaveChangesAsync();
    }

    public async Task EditResourceAsync(string resourceId, ResourceFormViewModel viewModel)
    {
        Resource resource = await dbContext.Resources
            .FirstAsync(r => r.Id.ToString() == resourceId);

        resource.Name = sanitizer.Sanitize(viewModel.Name);
        resource.ImageUrl = sanitizer.Sanitize(viewModel.ImageUrl);
        resource.SourceUrl = sanitizer.Sanitize(viewModel.SourceUrl);
        resource.TrialId = Guid.Parse(viewModel.TrialId);
        resource.IsDeleted = viewModel.IsDeleted;

        await dbContext.SaveChangesAsync();
    }
}
