using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParsMobileDesign.Data;
using ParsMobileDesign.Models;

namespace ParsMobileDesign.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        ApplicationDbContext db;

        //[BindProperty]
        //public Employee employee { get; set; }
        //[BindProperty]
        //public VmEmployeeExperience empExp { get; set; }
        public EmployeeController(ApplicationDbContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            return View(db.Employee.ToList());
        }
        public IActionResult Create()
        {
            return View("Edit", new Employee());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(List<IFormFile> Picture, Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (Picture.Count > 0)
                    using (var ms = new MemoryStream())
                    {
                        Picture[0].CopyTo(ms);
                        employee.Picture = ms.ToArray();
                    }
                if (employee.Id == 0)
                    db.Employee.Add(employee);
                else
                {
                    var inDb = db.Employee.FirstOrDefault(e => e.Id == employee.Id);
                    if (inDb != null)
                    {
                        inDb.Fname = employee.Fname;
                        inDb.LName = employee.LName;
                        inDb.Email = employee.Email;
                        inDb.Tel = employee.Tel;
                        inDb.Address = employee.Address;
                        inDb.Description = employee.Description;
                        if (Picture.Count > 0)
                            inDb.Picture = employee.Picture;
                    }
                }
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? Id)
        {
            if (Id == null)
                return NotFound();
            var emp = db.Employee.Find(Id);
            if (emp == null)
                return NotFound();
            db.Employee.Remove(emp);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));

        }


        public IActionResult Edit(int? Id)
        {
            if (Id == null)
                return NotFound();
            var emp = db.Employee.Include(e => e.Specialities).Include(e => e.Experiences).FirstOrDefault(e=>e.Id == Id);
            if (emp == null)
                return NotFound();
            return View(emp);

        }



        //--------Employee Experience
        public IActionResult CreateExperience(int Id)
        {
            if (Id <= 0)
                return NotFound();
            var emp = db.Employee.FirstOrDefault(e => e.Id == Id);
            var obj = new EmployeeExperience(emp);
            return View("EditExp", obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEmpExp(EmployeeExperience exp)
        {
            if (!ModelState.IsValid)
                return View("EditExp", exp);
            if (exp.Id == 0)
                db.EmployeeExperience.Add(exp);
            else
                db.EmployeeExperience.Update(exp);
            db.SaveChanges();
            return RedirectToAction(nameof(Edit),new { Id = exp.EmployeeId });


        }
        
        //--------Employee Specialty
        public IActionResult CreateSpecialty(int Id)
        {
            if (Id <= 0)
                return NotFound();
            var emp = db.Employee.FirstOrDefault(e => e.Id == Id);
            var obj = new EmployeeSpecialty(emp);
            return View("EditSpec", obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEmpSpec(EmployeeSpecialty spec)
        {
            if (!ModelState.IsValid)
                return View("EditSpec", spec);
            if (spec.Id == 0)
                db.EmployeeSpecialty.Add(spec);
            else
                db.EmployeeSpecialty.Update(spec);
            db.SaveChanges();
            return RedirectToAction(nameof(Edit),new { Id = spec.EmployeeId });


        }
    }
}
