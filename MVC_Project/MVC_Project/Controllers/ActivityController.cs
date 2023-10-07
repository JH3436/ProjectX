using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;


namespace MVC_Project.Controllers
{
    public class ActivityController : Controller
    {
        private  ProjectXContext _context;

        public ActivityController(ProjectXContext context)
        {
            _context = context;
        }

        // GET: ActivityController
        public IActionResult Index(int? id)
        {
            if (id == null || _context.MyActivity == null)
            {
                return NotFound();
            }
            var data =
            from m in _context.MyActivity
            join o in _context.OfficialPhoto
            on m.ActivityID equals o.ActivityID
            where m.ActivityID == id
            select new responseActivity
            {
                ActivityName = m.ActivityName,
                Category = m.Category,
                SuggestedAmount = m.SuggestedAmount,
                ActivityContent = m.ActivityContent,
                MinAttendee = m.MinAttendee,
                VoteDate = m.VoteDate,
                ExpectedDepartureMonth = m.ExpectedDepartureMonth,
                PhotoPath = o.PhotoPath
            };


            var temp = data.ToList();
            
            return View(temp);
        }

        
        // POST: ActivityController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Chat chat)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ActivityController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ActivityController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
