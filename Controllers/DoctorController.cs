using Hospital.Data;
using Hospital.Models;
using Hospital.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hospital.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DoctorController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<Doctor> objlist = _db.Doctor
                .Include(u => u.Job_Title)
                .Include(u => u.User_N);
            
            return View(objlist);
        }

        // GET - UPSERT
        public IActionResult Upsert(int? id)
        {
            DoctorVM doctorVM = new DoctorVM()
            {
                Doctor = new Doctor(),
                JobSelectList = _db.JobTitle.Select(i => new SelectListItem
                {
                    Text = i.Title,
                    Value = i.Id.ToString()
                }),
                UserSelectList = _db.Users
                       .Where(u => _db.UserRoles.Any(ur => ur.UserId == u.Id && ur.RoleId == "doctor-role123"))
                       .Select(u => new SelectListItem
                {
                    Text = ((ApplicationUser)u).FullName,
                    Value = u.Id
                })
            };
            if (id == null)
            {
                //this is create
                return View(doctorVM);
            }
            else
            {
                doctorVM.Doctor = _db.Doctor.Find(id);
                if (doctorVM.Doctor == null)
                {
                    return NotFound();
                }
                return View(doctorVM);
            }
        }

        // POST - UPSERT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(DoctorVM doctorVM)
        {
            if(ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if(doctorVM.Doctor.Id == 0)
                {
                    //Creating
                    string upload = webRootPath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using(var filestream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    doctorVM.Doctor.Image = fileName + extension;
                    _db.Doctor.Add(doctorVM.Doctor);
                }
                else
                {
                    //updating
                    // извлечения файла из БД без отслеживания
                    var objFromDb = _db.Doctor.AsNoTracking().FirstOrDefault(u => u.Id == doctorVM.Doctor.Id);

                    //проверка получен ли файл
                    if (files.Count() > 0)
                    {
                        // получение имени
                        string upload = webRootPath + WC.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);
                        using (var filesteam = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(filesteam);
                        }
                        // тут удаляем старый файл
                        var oldFile = Path.Combine(upload, objFromDb.Image);

                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }
                        doctorVM.Doctor.Image = fileName + extension;

                    }
                    else
                    {
                        doctorVM.Doctor.Image = objFromDb.Image;
                    }
                    _db.Doctor.Update(doctorVM.Doctor);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
