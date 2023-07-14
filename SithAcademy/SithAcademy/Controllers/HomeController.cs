namespace SithAcademy.Controllers;

using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;

using SithAcademy.Web.ViewModels;
using SithAcademy.Web.ViewModels.Trial;
using SithAcademy.Services.Data.Interfaces;
using SithAcademy.Web.Infrastructure.Extensions;

public class HomeController : Controller
{
    private readonly ITrialService trialService;

    public HomeController(ITrialService trialService)
    {
        this.trialService = trialService;
    }

    public async Task<IActionResult> Index()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            IEnumerable<TrialOverviewViewModel> trials = await trialService.GetInProgressTrialsOfUserAsync(User.GetId());
            return View(trials);
        }
        else 
        {
            return View(new List<TrialOverviewViewModel>());
        }
    }

    public IActionResult About()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}