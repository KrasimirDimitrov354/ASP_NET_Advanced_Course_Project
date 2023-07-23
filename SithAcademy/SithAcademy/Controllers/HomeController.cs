namespace SithAcademy.Controllers;

using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;

using SithAcademy.Web.ViewModels;
using SithAcademy.Web.ViewModels.Trial;
using SithAcademy.Services.Data.Interfaces;
using SithAcademy.Web.Infrastructure.Extensions;

using static SithAcademy.Common.GeneralConstants;

//TODO: Add custom error page to replace UnknownFailure with

public class HomeController : Controller
{
    private readonly ITrialService trialService;
    private readonly IOverseerService overseerService;

    public HomeController(ITrialService trialService, IOverseerService overseerService)
    {
        this.trialService = trialService;
        this.overseerService = overseerService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            try
            {
                string userId = User.GetId();
                bool userIsOverseer = await overseerService.UserIsOverseerAsync(userId);
                if (userIsOverseer)
                {
                    return View(new List<IncompleteTrialViewModel>());
                }
                else
                {
                    IEnumerable<IncompleteTrialViewModel> trials = await trialService.GetIncompleteTrialsOfAcolyteAsync(userId);
                    return View(trials);
                }               
            }
            catch (Exception)
            {
                return UnknownFailureMessage();
            }           
        }
        else 
        {
            return View(new List<IncompleteTrialViewModel>());
        }
    }

    [HttpGet]
    public IActionResult About()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private IActionResult UnknownFailureMessage()
    {
        TempData[InformationMessage] = "The Dark Side has prevented your academy application from going through. Meditate upon your failure or try again later.";
        return RedirectToAction("Index", "Home");
    }
}