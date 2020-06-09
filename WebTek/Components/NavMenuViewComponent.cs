using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebTek.Models;

namespace WebTek.Components
{
    public class NavMenuViewComponent : ViewComponent
    {
        private ProductRepoInterface repository;
        public NavMenuViewComponent(ProductRepoInterface repo)
        {
            repository = repo;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(repository.Products
            .Select(x => x.Category)
            .Distinct()
            .OrderBy(x => x));
        }
    }
}