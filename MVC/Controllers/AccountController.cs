using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MVC.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IDistributedCache _distributedCache;
        public AccountController( UserManager<User> userManager, SignInManager<User> signInManager,IDistributedCache distributedCache)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _distributedCache = distributedCache;
        }
        public async Task<IActionResult> ResetPassword(string id)
        {
            id= _userManager.GetUserId(User);
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var isVerified = await _userManager.CheckPasswordAsync(user, model.CurrentPassword);
                if (isVerified)
                {
                    var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword,
                        model.NewPassword);
                    if (result.Succeeded)
                    {
                        await _userManager.UpdateSecurityStampAsync(user);
                        await _signInManager.SignOutAsync();
                        await _signInManager.PasswordSignInAsync(user, model.NewPassword, true, false);
                        TempData.Add("SuccessMessage", $"Change Password is Success.");
                        return RedirectToAction("Index", "Personal");
                    }
                }
                else
                {
                    TempData.Add("ErrorMessage", $"Please Check Password.");
                    return View(model);
                }
            }
            else
            {
                TempData.Add("ErrorMessage", $"Please Check Password.");
                return View(model);
            }

            return View();
        }
        public async Task<IActionResult> Manage(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var user2 = _userManager.GetUserId(User);

            if (user != null)
            {
                return View(new UserDetailsModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,                   
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,

                });
            }
            return Redirect("~/Account/Manage/" + user2);
        }

        [HttpPost]
        public async Task<IActionResult> Manage(UserDetailsModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);

                var userList = _userManager.Users.ToList();
                var resultEmail = userList.Where(e => e.Email == model.Email && e.Id!=model.UserId).FirstOrDefault();
                var resultUserName = userList.Where(e => e.UserName == model.UserName && e.Id!=model.UserId).FirstOrDefault();
                if (user != null && resultEmail == null && resultUserName==null)
                {                 
                    user.UserName = model.UserName;
                    user.Email = model.Email;                    

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        TempData.Add("SuccessMessage", $"Success.");
                        return Redirect("/Personal/Index");
                    }
                }
                ModelState.AddModelError("", "Email or UserName is not available.");
                return View(model);
            }
            ModelState.AddModelError("", "Error");
            return View(model);

        }


        [AllowAnonymous]
        public ActionResult Login()
        {           
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password,
                        true, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Personal");
                    }
                    else
                    {
                        ModelState.AddModelError("", "E-Mail or Password false.");
                        return View("Login");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "E-Mail or Password false.");
                    return View("Login");
                }
            }
            else
            {
                ModelState.AddModelError("", "E-Mail or Password false.");
                return View("Login");
            }
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var userList = _userManager.Users.ToList() ;
            var result1 = userList.Where(e=>e.Email == model.Email).FirstOrDefault();
            if (result1 != null)
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var user = new User()
                {
                    UserName = model.UserName,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // generate token
                    return RedirectToAction("Login", "Account");
                }

              
            }
            ModelState.AddModelError("", "Bilinmeyen hata oldu lütfen tekrar deneyiniz.");
            return View(model);

        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Personal");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
