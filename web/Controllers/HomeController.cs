using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.Entities;
using Microsoft.AspNetCore.Mvc;
using web.Rspositories;
using web.ViewModels;

namespace web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPortfolio<Owner> owner;
        private readonly IPortfolio<portfolioItem> portfolio;

        public HomeController(IPortfolio<Owner> owner, IPortfolio<portfolioItem> portfolio)
        {
            this.owner = owner;
            this.portfolio = portfolio;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                owner = owner.GetAll().First(),
                portfolioItems = portfolio.GetAll().ToList()


            };

            return View(homeViewModel);
        }
    }
}
