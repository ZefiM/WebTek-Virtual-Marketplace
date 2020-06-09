using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebTek.Models;
using WebTek.Models.ViewModels;

namespace WebTek.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private ProductRepoInterface repository;
        public int PageSize = 4;
        private readonly UserManager<UserAccount> _userManager;
        Product SellerID = new Product();

        public ProductController(ProductRepoInterface repo, UserManager<UserAccount> userManager)
        {
            repository = repo;
            _userManager = userManager;
        }
        public ViewResult Index()
        {
            SellerID.Seller = _userManager.GetUserName(HttpContext.User);
            TempData["UserID"] = _userManager.GetUserName(HttpContext.User);
            ViewBag.UsrID = _userManager.GetUserName(HttpContext.User);
            ViewBag.UserID = _userManager.GetUserName(HttpContext.User);
            ViewBag.User = _userManager.GetUserName(HttpContext.User);
            ViewBag.UsrID = _userManager.GetUserName(HttpContext.User);
            ViewBag.UID = _userManager.GetUserName(HttpContext.User);
            return View(repository.Products
            .Where(p => p.Seller == SellerID.Seller));

        }
        [AllowAnonymous]
        public ViewResult List(string category, int page = 1)
        {
            SellerID.Quantity = 0;
            TempData["UserID"] = _userManager.GetUserName(HttpContext.User);
            ViewBag.UsrID = _userManager.GetUserName(HttpContext.User);
            ViewBag.UserID = _userManager.GetUserName(HttpContext.User);
            ViewBag.User = _userManager.GetUserName(HttpContext.User);
            ViewBag.UsrID = _userManager.GetUserName(HttpContext.User);
            ViewBag.UID = _userManager.GetUserName(HttpContext.User);
            return View(new ProductsListViewModel
            {
                Products = repository.Products
                       .Where(p => category == null || p.Category == category)
                       //.FirstOrDefault(p => p.Quantity > 0)
                       .OrderBy(p => p.ProductID)
                       .Skip((page - 1) * PageSize)
                       .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                               repository.Products.Count() :
                               repository.Products.Where(e => e.Category == category).Count()
                },
                CurrentCategory = category
            });
        }

        public ViewResult Add()
        {
            TempData["UserID"] = _userManager.GetUserName(HttpContext.User);
            ViewBag.UsrID = _userManager.GetUserName(HttpContext.User);
            ViewBag.UserID = _userManager.GetUserName(HttpContext.User);
            ViewBag.User = _userManager.GetUserName(HttpContext.User);
            ViewBag.UsrID = _userManager.GetUserName(HttpContext.User);
            ViewBag.UID = _userManager.GetUserName(HttpContext.User);
            return View(new Product());
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    product.Seller = _userManager.GetUserName(HttpContext.User);
                    repository.SaveProduct(product);
                    return RedirectToAction("Index", "Home");
                    //return RedirectToAction(nameof());
                    //return View();
                }
                else
                {
                    return View();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public ViewResult Remove()
        {
            TempData["UserID"] = _userManager.GetUserName(HttpContext.User);
            ViewBag.UsrID = _userManager.GetUserName(HttpContext.User);
            ViewBag.UserID = _userManager.GetUserName(HttpContext.User);
            ViewBag.User = _userManager.GetUserName(HttpContext.User);
            ViewBag.UsrID = _userManager.GetUserName(HttpContext.User);
            ViewBag.UID = _userManager.GetUserName(HttpContext.User);
            return View(new Product());
        }

        [HttpPost]
        public IActionResult Remove(int ProductID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repository.DeleteProduct(ProductID);
                    //return RedirectToAction(nameof(Page1));
                    return View();
                }
                else
                {
                    return View();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ViewResult Edit(int productId)
        {
            TempData["UserID"] = _userManager.GetUserName(HttpContext.User);
            ViewBag.UsrID = _userManager.GetUserName(HttpContext.User);
            ViewBag.UserID = _userManager.GetUserName(HttpContext.User);
            ViewBag.User = _userManager.GetUserName(HttpContext.User);
            ViewBag.UsrID = _userManager.GetUserName(HttpContext.User);
            ViewBag.UID = _userManager.GetUserName(HttpContext.User);
            return View(repository.Products
            .Where(p => p.Seller == SellerID.Seller)
            .FirstOrDefault(p => p.ProductID == productId));
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(product);
            }
        }
       // public ViewResult Create() => View("Edit", new Product());

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            Product deletedProduct = repository.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.Name} was deleted";
            }
            return RedirectToAction("Index");
        }
    }
}
