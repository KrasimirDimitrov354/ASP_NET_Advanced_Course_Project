namespace SithAcademy.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using SithAcademy.Services.Data.Interfaces;
using SithAcademy.Web.Infrastructure.Extensions;

using static SithAcademy.Common.GeneralConstants;

[Authorize]
public class OverseerController : Controller
{
    private readonly IOverseerService overseerService;

    public OverseerController(IOverseerService overseerService)
    {
        this.overseerService = overseerService;
    }

    [HttpGet]
    public async Task<IActionResult> Home()
    {
        string userId = User.GetId()!;
        bool userIsOverseer = await overseerService.UserIsOverseerAsync(userId);
        if (!userIsOverseer)
        {
            TempData[WarningMessage] = "You do not have access to this section of the website.";
            return RedirectToAction("Index", "Home");
        }

        return View();
    }
}
