using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebTek.Models.ViewModels;
using WebTek.Models;
using System.Web;
using System.Linq;

namespace WebTek.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<UserAccount> userManager;
        private SignInManager<UserAccount> signInManager;
        private ProductRepoInterface repository;
        UserAccount usrr;
        public AccountController(UserManager<UserAccount> userMgr,
        SignInManager<UserAccount> signInMgr, ProductRepoInterface repo)
        {
            userManager = userMgr;
            signInManager = signInMgr;
            repository = repo;
        }

        [AllowAnonymous]
        public ViewResult Login(string returnUrl)
        {
            var userid = userManager.GetUserId(HttpContext.User);
            if (userid != null)
            {
                ModelState.AddModelError("", "You are already Logged in");
                TempData["UserID"] = userManager.GetUserName(HttpContext.User);
                ViewBag.UsrID = userManager.GetUserName(HttpContext.User);
                ViewBag.UserID = userManager.GetUserName(HttpContext.User);
                ViewBag.User = userManager.GetUserName(HttpContext.User);
                ViewBag.UsrID = userManager.GetUserName(HttpContext.User);
                ViewBag.UID = userManager.GetUserName(HttpContext.User);
                return View();
            }
            
            return View(new LoginModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var userid = userManager.GetUserId(HttpContext.User);
                usrr = await userManager.FindByIdAsync(userid);
                UserAccount user = await userManager.FindByEmailAsync(loginModel.Email);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    if ((await signInManager.PasswordSignInAsync(user,
                     loginModel.Password, false, false)).Succeeded)
                    {
							//This sets the current logged in users name in Viewbags so it can be
							//displayed in the pages
                            TempData["UserID"] = userManager.GetUserName(HttpContext.User);
                            ViewBag.UsrID = userManager.GetUserName(HttpContext.User);
                            ViewBag.UserID = userManager.GetUserName(HttpContext.User);
                            ViewBag.User = userManager.GetUserName(HttpContext.User);
                            ViewBag.UsrID = userManager.GetUserName(HttpContext.User);
                            ViewBag.UID = userManager.GetUserName(HttpContext.User);
                            //return View(TempData);
                            return Redirect(loginModel?.ReturnUrl ?? "/Home/Index");
                    }
                }
            }
			//User input not valid
            ModelState.AddModelError("", "Invalid name or password");
			//This sets the current logged in users name in Viewbags so it can be
			//displayed in the pages
            TempData["UserID"] = "Null";
            ViewBag.UsrID = "Null";
            ViewBag.UserID = "Null";
            ViewBag.User = "Null";
            ViewBag.UsrID = "Null";
            ViewBag.UID = "Null";
            //return View(TempData);
            return View(loginModel);
        }
        [AllowAnonymous]
        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();
            TempData["UserID"] = "Null";
            ViewBag.UsrID = "Null";
            ViewBag.UserID = "Null";
            ViewBag.User = "Null";
            ViewBag.UsrID = "Null";
            ViewBag.UID = "Null";
            return Redirect("/Home/Index");
            //return Redirect(returnUrl);
        }

        // GET: /<controller>/
        //[AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.UsrID = userManager.GetUserName(HttpContext.User);
            ViewBag.UserID = userManager.GetUserName(HttpContext.User);
            ViewBag.User = userManager.GetUserName(HttpContext.User);
            ViewBag.UsrID = userManager.GetUserName(HttpContext.User);
            ViewBag.UID = userManager.GetUserName(HttpContext.User);
            return View(userManager.Users);

        }
        [AllowAnonymous]
        public ViewResult Create()
        {
            return View();
        }
        //POST: Account/Create
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserAccount user = new UserAccount
                {
                    UserName = model.UserName,
                    Email = model.Email
                };
                IdentityResult result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //await signInManager.SignInAsync(user, isPersistent: false);
                    return Redirect("/Account/Login");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }
        private void AddErrors(IdentityResult result)
        {
        foreach (IdentityError error in result.Errors)
            {
                ModelState.TryAddModelError("", error.Description);
            }
        }
        public async Task<IActionResult> Delete(string id)
        {
            string userName = userManager.GetUserName(HttpContext.User);
            string userID = userManager.GetUserId(HttpContext.User);
            UserAccount userDel = await userManager.FindByIdAsync(userID);
            if (userDel != null)
            {
                await signInManager.SignOutAsync();
                repository.DeleteSellerProduct(userName);
                IdentityResult result = await userManager.DeleteAsync(userDel);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return RedirectToAction("Index", "Cart");
        }
        public async Task<IActionResult> Edit(string id)
        {
            UserAccount user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
