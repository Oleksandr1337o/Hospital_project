using Hospital.Data;
using Hospital.Models;
using Hospital.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Hospital.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        private const string SessionKey = "_currentMonth";
        private readonly ApplicationDbContext _db;
        private DateTime _currentMonth;

        public CalendarController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Calendar(string doctorId)
        {
            DateTime currentMonth;
            if (HttpContext.Session.TryGetValue(SessionKey, out var storedMonth))
            {
                currentMonth = DateTime.FromBinary(BitConverter.ToInt64(storedMonth));
            }
            else
            {
                currentMonth = DateTime.Now;
                HttpContext.Session.Set(SessionKey, BitConverter.GetBytes(currentMonth.ToBinary()));
            }

            _currentMonth = currentMonth;
            TempData["FullName"] = doctorId;
            return View(new CalendarVMBuilder(currentMonth).Build());
        }

        public IActionResult ChangeMonth(int direction)
        {
            if (HttpContext.Session.TryGetValue(SessionKey, out var storedMonth))
            {
                _currentMonth = DateTime.FromBinary(BitConverter.ToInt64(storedMonth));
            }
            else
            {
                _currentMonth = DateTime.Now;
            }

            if (direction == 1)
            {
                _currentMonth = _currentMonth.AddMonths(1);
            }
            else if (direction == -1)
            {
                _currentMonth = _currentMonth.AddMonths(-1);
            }
            else
            {
                throw new ArgumentOutOfRangeException("direction");
            }

            HttpContext.Session.Set(SessionKey, BitConverter.GetBytes(_currentMonth.ToBinary()));

            return View("Calendar", new CalendarVMBuilder(_currentMonth).Build());
        }


        [HttpPost]
        public IActionResult Confirm(Dictionary<string, string> SelectedTimes)
        {
            var doctorId = TempData["FullName"].ToString();

            if (SelectedTimes != null && SelectedTimes.Any())
            {
                foreach (var kvp in SelectedTimes)
                {
                    if (!string.IsNullOrEmpty(kvp.Value) && kvp.Value != "Select time:" && DateTime.TryParse(kvp.Key, out DateTime date))
                    {
                        var appointment = new AppointmentModel
                        {
                            Date = date,
                            SelectedTime = kvp.Value,
                            UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                            DoctorId = doctorId
                        };

                        _db.Appointments.Add(appointment);
                    }
                }

                _db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

    }
}