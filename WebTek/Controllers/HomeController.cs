using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebTek.Models;

namespace WebTek.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<UserAccount> _userManager;

        public HomeController(UserManager<UserAccount> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var userid = _userManager.GetUserId(HttpContext.User);
            if (userid == null)
            {
                TempData["UserID"] = "Null";
                ViewBag.UsrID = "Null";
                ViewBag.UserID = "Null";
                ViewBag.User = "Null";
                ViewBag.UsrID = "Null";
                ViewBag.UID = "Null";
                return View(TempData);
                //return RedirectToAction("Login", "Account");
            }
            else
            {
                TempData["UserID"] = _userManager.GetUserName(HttpContext.User);
                ViewBag.UsrID = _userManager.GetUserName(HttpContext.User);
                ViewBag.UserID = _userManager.GetUserName(HttpContext.User);
                ViewBag.User = _userManager.GetUserName(HttpContext.User);
                ViewBag.UsrID = _userManager.GetUserName(HttpContext.User);
                ViewBag.UID = _userManager.GetUserName(HttpContext.User);
                return View(TempData);
            }
        }

        public IActionResult About()
        {
            var userid = _userManager.GetUserId(HttpContext.User);
            if (userid == null)
            {
                TempData["UserID"] = "Null";
                ViewBag.UsrID = "Null";
                ViewBag.UserID = "Null";
                ViewBag.User = "Null";
                ViewBag.UsrID = "Null";
                ViewBag.UID = "Null";
                return View(TempData);
                //return RedirectToAction("Login", "Account");
            }
            else
            {
                TempData["UserID"] = _userManager.GetUserName(HttpContext.User);
                ViewBag.UsrID = _userManager.GetUserName(HttpContext.User);
                ViewBag.UserID = _userManager.GetUserName(HttpContext.User);
                ViewBag.User = _userManager.GetUserName(HttpContext.User);
                ViewBag.UsrID = _userManager.GetUserName(HttpContext.User);
                ViewBag.UID = _userManager.GetUserName(HttpContext.User);
                return View(TempData);
            }
  
        }
    }
}