using Microsoft.AspNetCore.Mvc;
using SafeVault.Models;
using SafeVault.Helpers;

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
        model.Username = InputSanitizer.Sanitize(model.Username);
        model.Password = InputSanitizer.Sanitize(model.Password);
        var user = _context.Users
            .FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

        if (user != null)
        {
            return RedirectToAction("Success", new { username = user.Username, email = user.Email });
        }

        ModelState.AddModelError(string.Empty, "Invalid username or password.");
        return View();
    }


    // Success page after login
    public IActionResult Success(string username, string email)
    {
        var user = new User { Username = username, Email = email };
        return View(user);
    }


    [HttpPost]
    public IActionResult Update(User model)
    {
        // Sanitize input to prevent XSS and SQL injection
        model.Username = InputSanitizer.Sanitize(model.Username);
        model.Email = InputSanitizer.Sanitize(model.Email);

        if (ModelState.IsValid)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == model.Username);

            if (user != null)
            {
                //do not change the username user.Username = model.Username;
                user.Email = model.Email;
                _context.SaveChanges();
                return RedirectToAction("Success"); // Reload success page with updated data
            }
        }

        return View("Success", model);
    }


}
