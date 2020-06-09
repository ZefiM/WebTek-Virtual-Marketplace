using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebTek.Models;
using WebTek.Models.ViewModels;

namespace WebTek.Controllers
{
    public class CartController : Controller
    {
        private ProductRepoInterface repository;
        private readonly UserManager<UserAccount> _userManager;
        private Cart cart;
        public CartController(ProductRepoInterface repo, Cart cartService, UserManager<UserAccount> userManager)
        {
            repository = repo;
            cart = cartService;
            _userManager = userManager;
        }
        public ViewResult Index(string returnUrl)
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
               // return View(TempData);
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
                //return View(TempData);
            }
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }
        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = repository.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (product != null && product.Quantity > 0)
            {
                cart.AddItem(product, 1);
                repository.RemoveQuantity(productId);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToActionResult RemoveFromCart(int productId,
        string returnUrl)
        {
            Product product = repository.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.RemoveLine(product);
                repository.AddQuantity(productId);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
 }
}
