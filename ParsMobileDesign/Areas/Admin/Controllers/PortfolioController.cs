using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParsMobileDesign.Data;
using ParsMobileDesign.Models;

namespace ParsMobileDesign.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PortfolioController : Controller
    {
        ApplicationDbContext db;
        [BindProperty]
        public Portfolio Portfolio { get; set; }
        public PortfolioController(ApplicationDbContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            return View(db.Portfolio.ToList());
        }
        public IActionResult Create()
        {
            return View("Edit", new Portfolio());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save()
        {
            if (ModelState.IsValid)
            {
                if (Portfolio.PortfolioId == 0)
                    db.Portfolio.Add(Portfolio);
                else
                    db.Portfolio.Update(Portfolio);
                db.SaveChanges();
            }
            else
                return View("Edit", Portfolio);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? Id)
        {
            if (Id == null)
                return NotFound();
            var port = db.Portfolio.Find(Id);
            if (port == null)
                return NotFound();
            db.Portfolio.Remove(port);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Edit(int? Id)
        {
            if (Id == null)
                return NotFound();
            var port = db.Portfolio.Find(Id);
            if (port == null)
                return NotFound();
            return View(port);

        }
        
    }
}
