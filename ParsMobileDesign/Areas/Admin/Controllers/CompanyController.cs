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
    public class CompanyController : Controller
    {
        ApplicationDbContext db;
        [BindProperty]
        public CompanyInfo companyInfo { get; set; }
        public CompanyController(ApplicationDbContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            var company = db.CompanyInfo.ToList();
            var obj = new CompanyInfo();
            if (company.Count > 0)
                obj = company.FirstOrDefault();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save()
        {
            if (ModelState.IsValid)
            {
                if (db.CompanyInfo.Count() == 0)
                    db.CompanyInfo.Add(companyInfo);
                else
                    db.CompanyInfo.Update(companyInfo);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
