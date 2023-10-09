using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using MVC_Project.Models;
using SmartBreadcrumbs.Nodes;

namespace MVC_Project.Controllers
{
    public class groupPageController : Controller
    {
        private ProjectXContext _context;
        // GET: groupPageController
        public groupPageController(ProjectXContext context)
        {
            _context = context;
        }


        public IActionResult groupPage(int? id)
        {
            if (id == null || _context.MyActivity == null)
            {
                return NotFound();
            }
            var data = from g in _context.Group
                       join c in _context.Chat
                       on g.GroupID equals c.ActivityID
                       where g.GroupID == id
                       select new Group
                       {
                           GroupID = g.GroupID,
                           GroupName = g.GroupName,
                           GroupCategory = g.GroupCategory,
                           GroupContent = g.GroupContent,
                           MinAttendee = g.MinAttendee,
                           MaxAttendee = g.MaxAttendee,
                           StartDate = g.StartDate,
                           EndDate = g.EndDate,
                           Organizer = g.Organizer,
                           Chat = _context.Chat.Where(chat => chat.ActivityID == g.GroupID).ToList(),
                           OrganizerNavigation = _context.Member.FirstOrDefault(m => m.UserID == g.Organizer), // Set OrganizerNavigation based on Member Id
                           OriginalActivity = _context.MyActivity.FirstOrDefault(a => a.ActivityID == g.OriginalActivityID), // Set OriginalActivity based on ActivityID
                           PersonalPhoto = _context.PersonalPhoto.Where(pp => pp.GroupID == g.GroupID).ToList(), // Set PersonalPhoto based on GroupID
                           Registration = _context.Registration.Where(r => r.GroupID == g.GroupID).ToList() // Set Registration based on GroupID

                       };

            //var data = from g in _context.Group
            //                      join p in _context.PersonalPhoto
            //                      on g.GroupID equals p.GroupID
            //                      join c in _context.Chat
            //                      on g.GroupID equals c.ActivityID
            //                      where g.GroupID == id
            //                      select new Group
            //                      {
            //                          GroupName = g.GroupName,
            //                          GroupCategory = g.GroupCategory,
            //                          GroupContent = g.GroupContent,
            //                          MinAttendee = g.MinAttendee,
            //                          MaxAttendee = g.MaxAttendee,
            //                          StartDate = g.StartDate,
            //                          EndDate = g.EndDate,
            //                          Organizer = g.Organizer,
            //                          Chat = _context.Chat.Where(chat => chat.ActivityID == g.GroupID).ToList(),
            //                          OrganizerNavigation = _context.Member.FirstOrDefault(m => m.UserID == g.Organizer), // Set OrganizerNavigation based on Member Id
            //                          OriginalActivity = _context.MyActivity.FirstOrDefault(a => a.ActivityID == g.OriginalActivityID), // Set OriginalActivity based on ActivityID
            //                          PersonalPhoto = _context.PersonalPhoto.Where(pp => pp.GroupID == g.GroupID).ToList(), // Set PersonalPhoto based on GroupID
            //                          Registration = _context.Registration.Where(r => r.GroupID == g.GroupID).ToList() // Set Registration based on GroupID

            //                      };

            //-----麵包屑-----
            var childNode1 = new MvcBreadcrumbNode("ACT", "MyActivity", "所有活動");

            var childNode2 = new MvcBreadcrumbNode("ACT", "MyActivity", "ViewData.GroupCategory")
            {
                OverwriteTitleOnExactMatch = true,
                Parent = childNode1
            };

            var childNode3 = new MvcBreadcrumbNode("ACT", "MyActivity", "ViewData.GroupName")
            {
                OverwriteTitleOnExactMatch = true,
                Parent = childNode2
            };

            ViewData["BreadcrumbNode"] = childNode3;
            //-----麵包屑結束-----


            var temp = data.ToList();

            return View(temp);
        }



        // POST: groupPageController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
