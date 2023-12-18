using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Login.Web.Models;

public class UserController : Controller
{
    private readonly UserManager<AppUser> _userManager;

    public UserController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var users = _userManager.Users;
        return View(users);
    }
}
