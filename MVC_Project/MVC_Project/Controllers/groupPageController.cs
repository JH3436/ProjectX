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
                                  _context.Chat.Where(chat => chat.ActivityID == g.GroupID).ToList() : new List<Chat>(),
                           OrganizerNavigation = _context.Member.FirstOrDefault(m => m.UserID == g.Organizer),
                           OriginalActivity = _context.MyActivity.FirstOrDefault(a => a.ActivityID == g.OriginalActivityID),
                           PersonalPhoto = _context.PersonalPhoto.Where(pp => pp.GroupID == g.GroupID).ToList(),
                           Registration = _context.Registration.Where(r => r.GroupID == g.GroupID).ToList()
                       };




            var temp = data.ToList();

            return View(temp);
        }



        [HttpGet]
        public IActionResult CommentCreate()
        {
            return View();
        }

        // POST: YourController/commentCreate
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult CommentCreate([Bind("ChatContent")] Chat chat)
        //{
        //    // 在這裡處理接收到的 ChatContent


        //    try
        //    {

        //            int id = int.Parse(Request.Form["id"]);
        //            chat.ActivityID = id;
        //            // 設置固定值
        //            chat.UserID = 1;
        //            chat.ToWhom = null;
        //            chat.ChatTime = DateTime.Now;

        //            // 添加到數據庫
        //            _context.Chat.Add(chat);
        //            _context.SaveChanges();

        //        // 可以在這裡進行重定向到其他頁面或返回相應的視圖
        //        return RedirectToAction("GroupPage"); // 假設 GroupPage 接受一個 id 參數
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exceptions
        //        return View("Error");
        //    }
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CommentCreate(Chat chat)
        {
            int id = int.Parse(Request.Form["id"]);
            chat.ActivityID = id;
            chat.UserID = 1;
            chat.ToWhom = null;
            chat.ChatTime = DateTime.Now;

            _context.Add(chat);
                await _context.SaveChangesAsync();

            return NoContent();
        }
            
        }
    }
