using Hospital.Data;
using Hospital.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace Hospital.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class Job_titleController : Controller
    {
        private readonly ApplicationDbContext _db;

        public Job_titleController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Job_title> objList = _db.JobTitle;
            return View(objList);
        }

        // GET - CREATE
        public IActionResult Create() 
        {
            return View();
        }

        // POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Job_title obj)
        {
            if(ModelState.IsValid)
            {
                _db.JobTitle.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // EDIT - GET   
        public IActionResult Edit(int? id )
        {
            if(id == null || id==0)
            {
                return NotFound();
            }
            var obj = _db.JobTitle.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // EDIT - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Job_title obj)
        {
            if(ModelState.IsValid)
            {
                _db.JobTitle.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // DELETE - GET
        public IActionResult Delete(int? id)
        {
            if(id ==null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.JobTitle.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // DELETE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.JobTitle.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            _db.JobTitle.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
