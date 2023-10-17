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
using NuGet.Protocol;
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


        public IActionResult groupPage(int id)
        {
            //if (id == null || _context.MyActivity == null)
            //{
            //    return NotFound();
            //}
            var data = from g in _context.Group
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
                           Chat = (_context.Chat.Count(chat => chat.ActivityID == g.GroupID) > 0) ?
                                                        _context.Chat.Where(chat => chat.ActivityID == g.GroupID)
                                                        .Include(chat => chat.User)  // Include Member data related to Chat
                                                        .ToList() : new List<Chat>(),
                           OriginalActivity = _context.MyActivity.FirstOrDefault(a => a.ActivityID == g.OriginalActivityID),
                           PersonalPhoto = _context.PersonalPhoto.Where(pp => pp.GroupID == g.GroupID).ToList(),
                           Registration = _context.Registration.Where(r => r.GroupID == g.GroupID).ToList()
                       };

            //-----麵包屑----- 

            var childNode1 = new MvcBreadcrumbNode("ACT", "MyActivity", "所有活動");

            var childNode2 = new MvcBreadcrumbNode("ACT", "MyActivity", "ViewData.Category")
            {
                OverwriteTitleOnExactMatch = true,
                Parent = childNode1
            };

            var childNode3 = new MvcBreadcrumbNode("ACT", "MyActivity", "ViewData.ActivityName")
            {
                OverwriteTitleOnExactMatch = true,
                Parent = childNode2
            };

            ViewData["BreadcrumbNode"] = childNode3;

            //-----麵包屑結束----- 


            var temp = data.ToList();

            return View(temp);
        }

        public IActionResult groupPageAccount(int id, int account)
        {
            account = 1;
            var data = from g in _context.Group
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
                           Chat = (_context.Chat.Count(chat => chat.ActivityID == g.GroupID) > 0) ?
                                                        _context.Chat.Where(chat => chat.ActivityID == g.GroupID)
                                                        .Include(chat => chat.User)  // Include Member data related to Chat
                                                        .ToList() : new List<Chat>(),
                           OriginalActivity = _context.MyActivity.FirstOrDefault(a => a.ActivityID == g.OriginalActivityID),
                           PersonalPhoto = _context.PersonalPhoto.Where(pp => pp.GroupID == g.GroupID).ToList(),
                           Registration = _context.Registration.Where(r => r.GroupID == g.GroupID).ToList()
                       };

            //-----麵包屑----- 

            var childNode1 = new MvcBreadcrumbNode("ACT", "MyActivity", "所有活動");

            var childNode2 = new MvcBreadcrumbNode("ACT", "MyActivity", "ViewData.Category")
            {
                OverwriteTitleOnExactMatch = true,
                Parent = childNode1
            };

            var childNode3 = new MvcBreadcrumbNode("ACT", "MyActivity", "ViewData.ActivityName")
            {
                OverwriteTitleOnExactMatch = true,
                Parent = childNode2
            };

            ViewData["BreadcrumbNode"] = childNode3;

            //-----麵包屑結束----- 
            var userInfo = from m in _context.Member
                           where m.UserID == account
                           select m;
            ViewBag.userInfo = userInfo.ToList();

            var signOrNot = _context.Registration
                .Where(lr => lr.ParticipantID == account && lr.GroupID == id)
                .Select(lr => lr.RegistrationID)
                .ToList();
            ViewBag.signOrNot = (signOrNot.Count() == 0 ? false : true);

            var temp = data.ToList();

            return View(temp);
        }




        [HttpGet]
        public IActionResult CommentCreate()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CommentCreate(Chat chat)
        {
            int id = int.Parse(Request.Form["id"]);
            chat.ActivityID = id;
            chat.ToWhom = null;
            chat.ChatTime = DateTime.Now;

            _context.Add(chat);
                await _context.SaveChangesAsync();

            return RedirectToAction("groupPage", new { id });
        }

        [HttpGet]
        public IActionResult ReplyCreate()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReplyCreate(Chat chat)
        {
            int id = int.Parse(Request.Form["id"]);
            chat.ActivityID = id;
            chat.ChatTime = DateTime.Now;

            _context.Add(chat);
            await _context.SaveChangesAsync();

            return RedirectToAction("groupPage", new { id });
        }
       
        [HttpPost]
        public IActionResult register(int groupId, int userId )
        {
            int id = groupId;
            int account = userId;
            var newRegistration = new Registration
            {
                ParticipantID = userId,
                GroupID = groupId
            };

            _context.Registration.Add(newRegistration);
            _context.SaveChanges();

            // 返回成功的回應，例如JSON對象
            return RedirectToAction("groupPageAccount", new { id, account });
        }
    }
}
