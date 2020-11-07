using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ParsMobileDesign.Data;
using ParsMobileDesign.Models;

namespace ParsMobileDesign.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext db;
        private string message { get; set; }
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext _db)
        {
            _logger = logger;
            db = _db;
            message = "";
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Contact()
        {
            var com = db.CompanyInfo.ToList();
            CompanyInfo obj;
            if (com.Count > 0)
                obj = com.ElementAt(0);
            else
                obj = new CompanyInfo();
            return View(new VmContactMessage { CompanyInfo = obj, Message = new Message(), msg = message != null ? message : "" });
        }
        public IActionResult SendMessage(VmContactMessage iMsg)
        {
            db.Message.Add(iMsg.Message);
            db.SaveChanges();
            message = "message has successfully sent !";
            return RedirectToAction("Contact");
        }
        public IActionResult About()
        {
            var com = db.CompanyInfo.ToList();
            CompanyInfo obj;
            if (com.Count > 0)
                obj = com.ElementAt(0);
            else
                obj = new CompanyInfo();
            return View(obj);
        }
        public IActionResult Portfolio()
        {
            var ports = db.Portfolio.Include(e => e.Images).ToList();
            return View(ports);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
