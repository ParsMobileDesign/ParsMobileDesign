using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParsMobileDesign.Data;
using ParsMobileDesign.Models;

namespace ParsMobileDesign.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PortfolioImageController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment webHostEnvironment;

        [BindProperty]
        public PortfolioImage PortfolioImg { get; set; }
        public PortfolioImageController(ApplicationDbContext _db, IWebHostEnvironment iWebHostEnvironment)
        {
            db = _db;
            webHostEnvironment = iWebHostEnvironment;
        }
        public IActionResult Index()
        {
            return View(db.PortfolioImage.Include(e => e.Portfolio).ToList());
        }
        public IActionResult Create()
        {
            var model = new PortfolioAndImages { PortfolioItems = db.Portfolio.ToList(), PortfolioImage = new PortfolioImage() };

            return View("Edit", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SavePortfolioImage(int Id, PortfolioAndImages model)
        {
            if (!ModelState.IsValid)
                return View("Edit", PortfolioImg);
            try
            {
                if (model.PortfolioImage.Id == 0)
                    db.PortfolioImage.Add(model.PortfolioImage);
                else
                    db.PortfolioImage.Update(model.PortfolioImage);
                db.SaveChanges();
                //Saving Image 
                string webRootPath = webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                var portfolioImageInDB = db.PortfolioImage.Find(model.PortfolioImage.Id);
                var filenameComplete = model.PortfolioImage.PortfolioId.ToString() + "_" + model.PortfolioImage.Id.ToString();
                portfolioImageInDB.ImageAddr = U.SaveFileThenGetFileName(webHostEnvironment, "PortfolioImages", files, filenameComplete);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        public ActionResult Delete(int? Id)
        {
            var port = db.PortfolioImage.SingleOrDefault(e => e.Id == Id);
            if (port != null)
            {
                db.PortfolioImage.Remove(port);
                db.SaveChanges();
                string webroot = webHostEnvironment.WebRootPath;
                System.IO.File.Delete(webroot + port.ImageAddr);
            }
            return RedirectToAction(nameof(Index));
        }
        //public IActionResult Delete(int? Id)
        //{
        //    if (Id == null)
        //        return NotFound();
        //    var port = db.PortfolioImage.Find(Id);
        //    if (port == null)
        //        return NotFound();
        //    db.PortfolioImage.Remove(port);
        //    db.SaveChanges();
        //    return RedirectToAction(nameof(Index));

        //}
        public IActionResult Edit(int? Id)
        {
            if (Id == null)
                return NotFound();
            var port = db.PortfolioImage.Find(Id);
            if (port == null)
                return NotFound();
            return View(new PortfolioAndImages { PortfolioItems = db.Portfolio.ToList(), PortfolioImage = port });

        }
    }
}
