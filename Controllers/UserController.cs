using Microsoft.AspNetCore.Mvc;
using SafeVault.Models;

public class UserController : Controller
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
        _context = context;
    }

    // Action to load the login form
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // Action to handle login submissions
    [HttpPost]
    public IActionResult Login(User model)
    {
        if (ModelState.IsValid)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

            if (user != null)
            {
                return RedirectToAction("Success");
            }

            ModelState.AddModelError(string.Empty, "Invalid username or password.");
        }

        return View();
    }

    // Success page after login
    public IActionResult Success()
    {
        return View();
    }
}
