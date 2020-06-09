using Microsoft.AspNetCore.Mvc;
using WebTek.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebTek.Controllers
{
    public class OrderController : Controller
    {
        private OrderRepoInterface repo;
        private ProductRepoInterface productrepo;
        private Cart cart;
        private UserManager<UserAccount> userManager;

        public OrderController(OrderRepoInterface repoService, Cart cartService, UserManager<UserAccount> _userManager, ProductRepoInterface _productrepo)
        {
            repo = repoService;
            cart = cartService;
            userManager = _userManager;
            productrepo = _productrepo;
        }
        [Authorize]
        public ViewResult List()
        {
            TempData["UserID"] = userManager.GetUserName(HttpContext.User);
            ViewBag.UsrID = userManager.GetUserName(HttpContext.User);
            ViewBag.UserID = userManager.GetUserName(HttpContext.User);
            ViewBag.User = userManager.GetUserName(HttpContext.User);
            ViewBag.UsrID = userManager.GetUserName(HttpContext.User);
            ViewBag.UID = userManager.GetUserName(HttpContext.User);
            return View(repo.Orders.Where(o => !o.Shipped && o.UserName == userManager.GetUserName(HttpContext.User)));
        }

        [HttpPost]
        [Authorize]
        public IActionResult MarkShipped(int orderID)
        {
            TempData["UserID"] = userManager.GetUserName(HttpContext.User);
            ViewBag.UsrID = userManager.GetUserName(HttpContext.User);
            ViewBag.UserID = userManager.GetUserName(HttpContext.User);
            ViewBag.User = userManager.GetUserName(HttpContext.User);
            ViewBag.UsrID = userManager.GetUserName(HttpContext.User);
            ViewBag.UID = userManager.GetUserName(HttpContext.User);
            Order order = repo.Orders
            .FirstOrDefault(o => o.OrderID == orderID);
            if (order != null)
            {
                order.Shipped = true;
                repo.SaveOrder(order);
            }
            return RedirectToAction(nameof(List));
        }

        public ViewResult Checkout()
        {
            TempData["UserID"] = userManager.GetUserName(HttpContext.User);
            ViewBag.UsrID = userManager.GetUserName(HttpContext.User);
            ViewBag.UserID = userManager.GetUserName(HttpContext.User);
            ViewBag.User = userManager.GetUserName(HttpContext.User);
            ViewBag.UsrID = userManager.GetUserName(HttpContext.User);
            ViewBag.UID = userManager.GetUserName(HttpContext.User);
            return View(new Order());
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                order.UserName = userManager.GetUserName(HttpContext.User);
                order.Lines = cart.Lines.ToArray();
                repo.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }
        public ViewResult Completed()
        {
            TempData["UserID"] = userManager.GetUserName(HttpContext.User);
            ViewBag.UsrID = userManager.GetUserName(HttpContext.User);
            ViewBag.UserID = userManager.GetUserName(HttpContext.User);
            ViewBag.User = userManager.GetUserName(HttpContext.User);
            ViewBag.UsrID = userManager.GetUserName(HttpContext.User);
            ViewBag.UID = userManager.GetUserName(HttpContext.User);
            cart.Clear();
            return View();
        }
    }
}