namespace SithAcademy.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using SithAcademy.Services.Data.Interfaces;

[Authorize]
public class OverseerController : Controller
{
    private readonly IOverseerService overseerService;

    public OverseerController(IOverseerService overseerService)
    {
        this.overseerService = overseerService;
    }

    [HttpGet]
    public IActionResult Home()
    {
        return View();
    }
}
