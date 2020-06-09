using Microsoft.AspNetCore.Mvc;
using WebTek.Models;
using WebTek.Models.ViewModels;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace WebTek.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private ProductRepoInterface repo;
        private readonly UserManager<UserAccount> _userManager;
        private Product SellerID = new Product();
        public AdminController(ProductRepoInterface repository, UserManager<UserAccount> userManager)
        {
            repo = repository;
            _userManager = userManager;
        }

        public ViewResult Index()
        {
            return View(repo.Products
            .Where(p => p.Seller == _userManager.GetUserName(HttpContext.User)));
            
        }

    }
}