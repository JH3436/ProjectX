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
            int? account = HttpContext.Session.GetString("UserId") != null ?
                int.Parse(HttpContext.Session.GetString("UserId")) :
                (int?)null;
            if (account == null)
            {
                ViewBag.signOrNot = "noAccount";
            }
            else
            {
                var userInfo = from m in _context.Member
                               where m.UserID == account
                               select m;
                ViewBag.userInfo = userInfo.ToList();


                var signOrNot = _context.Registration
                    .Where(lr => lr.ParticipantID == account && lr.GroupID == id)
                    .Select(lr => lr.RegistrationID)
                    .ToList();
                ViewBag.signOrNot = (signOrNot.Count() == 0 ? "false" : "true");
            }
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


        //留言API
        [HttpGet("api/chatData/{id}")]
        public IActionResult chatData(int id)
        {
            var chatData = from c in _context.Chat
                           join m in _context.Member
                           on c.UserID equals m.UserID
                           where c.ActivityID == id
                           select new responeChat
                           {
                               ChatID = c.ChatID,
                               ActivityID = c.ActivityID,
                               UserID = c.UserID,
                               ChatContent = c.ChatContent,
                               ToWhom = c.ToWhom,
                               ChatTime = c.ChatTime,
                               Nickname = m.Nickname,
                               UserPhoto = m.UserPhoto
                           };

            return Ok(chatData);
        }

        [HttpGet("api/getUserInfo/{currentUserId}")]
        public IActionResult getUserInfo(int currentUserId)
        {
            var userInfo = from m in _context.Member
                           where m.UserID == currentUserId
                           select m;

            return Ok(userInfo);
        }


        //討論上傳API
        [HttpPost]
        [Route("api/discussUpdate")]
        public async Task<IActionResult> discussUpdate([FromBody] Chat chat)
        {
            chat.ChatTime = DateTime.Now;
            _context.Add(chat);
            await _context.SaveChangesAsync();

            return Ok(chat);
        }

        //留言上傳API
        [HttpPost]
        [Route("api/replyUpdate")]
        public async Task<IActionResult> replyUpdate([FromBody] Chat chat)
        {
            chat.ChatTime = DateTime.Now;
            _context.Add(chat);
            await _context.SaveChangesAsync();

            return Ok(chat);
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
