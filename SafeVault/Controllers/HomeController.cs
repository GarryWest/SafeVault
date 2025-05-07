using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SafeVault.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace SafeVault.Controllers;

public class HomeController : Controller
{


    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {

        return View();
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Admin()
    {
        return View("Admin");
    }

    [Authorize(Roles = "Admin, User")]
    public IActionResult User()
    {
        return View("User");
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


}
