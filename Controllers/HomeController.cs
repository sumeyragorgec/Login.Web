using Login.Web.Models;
using Login.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Login.Web.NewFolder2;
using Login.Web.Models.NewFolder;
namespace Login.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _UserManager;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _UserManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SignUp()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model, string? returnUrl = null)
        { // return url için ?

            returnUrl = returnUrl ?? Url.Action("Index", "Home");

            var hasUser = await _UserManager.FindByEmailAsync(model.Email);


            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Email veya þifre yanlýþ");
                return View();
            }

            var signInResult = await _signInManager.PasswordSignInAsync(hasUser, model.Password, model.RememberMe, true);


            if (signInResult.Succeeded)
            {

                return Redirect(returnUrl);
            }


            if (signInResult.IsLockedOut)
            {

                ModelState.AddModelErrorList(new List<string>() { "3 dakika boyunca giriþ yapamazsýnýz." });
                return View();
            }

            ModelState.AddModelErrorList(new List<string>() { $"Email veya þifre yanlýþ", $"Baþarýsýz giriþ sayýsý = {await _UserManager.GetAccessFailedCountAsync(hasUser)}" });

            return View();

        }

        public IActionResult SignIn()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel request)
        {


            if (!ModelState.IsValid)
            {
                return View();
            }


            var identityResult = await _UserManager.CreateAsync(new() { UserName = request.UserName, Email = request.Email }, request.Password);

            if (identityResult.Succeeded)
            {
                ViewBag.SuccessMessage = "Üyelik kayýt iþlemi baþarýyla gerçekleþmiþtir.";

                return View();
            }

            ModelState.AddModelErrorList(identityResult.Errors.Select(x=>x.Description).ToList());  
            return View();

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
