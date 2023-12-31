﻿namespace SithAcademy.Controllers;

using Microsoft.AspNetCore.Mvc;

using SithAcademy.Web.ViewModels.Trial;
using SithAcademy.Web.Infrastructure.Extensions;
using SithAcademy.Services.Data.Interfaces;

using static SithAcademy.Common.GeneralConstants;

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
                string userId = User.GetId()!;
                bool userIsOverseer = await overseerService.UserIsOverseerAsync(userId);
                if (userIsOverseer)
                {
                    return RedirectToAction("Home", "Overseer");
                }
                else if (User.IsAdmin())
                {
                    return RedirectToAction("Index", "Home", new { Area = AdminAreaName });
                }
                else
                {
                    IEnumerable<IncompleteTrialViewModel> trials = await trialService.GetIncompleteTrialsOfAcolyteAsync(userId);
                    return View(trials);
                }               
            }
            catch (Exception)
            {
                return Error(500);
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
    public IActionResult Error(int statusCode)
    {
        if (statusCode == 400 || statusCode == 404)
        {
            return View("Error404");
        }

        if (statusCode == 401)
        {
            return View("Error401");
        }

        if (statusCode == 405)
        {
            return View("Error405");
        }

        return View();
    }
}