using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using core.Entities;
using web;
using web.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using web.Rspositories;

namespace web.Controllers
{
    public class portfolioItemsController : Controller
    {
        private readonly DataContext _context;
        private readonly IHostingEnvironment host;
        private readonly IPortfolio<portfolioItem> iportfolio;

        public portfolioItemsController(DataContext context ,IHostingEnvironment host , IPortfolio<portfolioItem> iportfolio)
        {
            _context = context;
            this.host = host;
            this.iportfolio = iportfolio;
        }

        // GET: portfolioItems
        public IActionResult Index()
        {
            return View(iportfolio.GetAll().ToList());
        }

        // GET: portfolioItems/Details/5
        public IActionResult Details(int id)
        {


            var portfolioItem = iportfolio.find(id);
              
            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem);
        }

        // GET: portfolioItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: portfolioItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PortfolioItemsViewModel model)
        {
           
            if (ModelState.IsValid)
            {
                if(model.File !=null)
                {
                    var uploads = Path.Combine(host.WebRootPath, @"img\portfolio");
                    string fullPath = Path.Combine(uploads, model.File.FileName);
                    model.File.CopyTo(new FileStream(fullPath,FileMode.Create));
                    


                }

                portfolioItem portfolio = new portfolioItem
                {
                     projectName = model.projectName,
                     description=model.description,
                     imageUrl=model.File.FileName
                };


                iportfolio.add(portfolio);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: portfolioItems/Edit/5
        public IActionResult Edit(int id)
        {


            var portfolioItem = iportfolio.find(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            PortfolioItemsViewModel model = new PortfolioItemsViewModel
            {
                id = portfolioItem.Id,
                description = portfolioItem.description,
                projectName = portfolioItem.projectName,
                imageUrl = portfolioItem.imageUrl

            };
            return View(model);
        }

        // POST: portfolioItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PortfolioItemsViewModel model )
        {
           

            if (ModelState.IsValid)
            {
                try
                {
                    string uploads = Path.Combine(host.WebRootPath, @"img\portfolio");
                    string NewfullPath = Path.Combine(uploads, model.File.FileName);
                    //old Path
                    string oldFullPath = Path.Combine(uploads, model.imageUrl);


                    if (oldFullPath != NewfullPath)
                    {
                        //delete old path
                        System.IO.File.Delete(oldFullPath);

                        //save new  path
                        model.File.CopyTo(new FileStream(NewfullPath, FileMode.Create));

                    }

                    portfolioItem portfolioItem = new portfolioItem()
                    {
                        Id = model.id,
                        description=model.description,
                        imageUrl=model.File.FileName,
                        projectName=model.projectName

                    };

                    iportfolio.update(portfolioItem);
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                }
                return RedirectToAction(nameof(Index));
            }
            else
            return View(model);
        }

        // GET: portfolioItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = await _context.PortfolioItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem);
        }

        // POST: portfolioItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var portfolioItem = await _context.PortfolioItem.FindAsync(id);
            _context.PortfolioItem.Remove(portfolioItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool portfolioItemExists(int id)
        {
            return _context.PortfolioItem.Any(e => e.Id == id);
        }
    }
}
