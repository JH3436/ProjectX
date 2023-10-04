using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;

namespace MVC_Project.Controllers
{

    public class MemberController : Controller
    {
         private readonly ProjectXContext _context;

            public MemberController(ProjectXContext context)
            {
                _context = context;
            }


            public IActionResult Registration()
        {

            return View();
        }
        public IActionResult LikeRecord()
        {
            var userId = 1; // 從當前登錄的使用者取得UserId

            // 取得這個UserId所有的LikeRecords以及相對應的MyActivity資料
            var likedActivities = _context.LikeRecord
    .Where(lr => lr.UserID == userId)
    .Include(lr => lr.Activity)
    .Select(lr => new LikedActivityViewModel
    {
        ActivityName = lr.Activity.ActivityName,
        VoteDate = lr.Activity.VoteDate
    })
    .ToList();

            ViewBag.LikedActivities = likedActivities;

            return View();
        }
        public IActionResult Member()
        {
            return View();
        }
        public IActionResult PersonGroup()
        {
            return View();
        }
    }
}
