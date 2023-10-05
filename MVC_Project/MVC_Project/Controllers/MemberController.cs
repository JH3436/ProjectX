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


        public IActionResult Registration(Group group)
        {

           
            var userId = 1; // 從當前登錄的使用者取得UserId

            var registeredGroups = _context.Registration
                .Where(r => r.ParticipantID == userId)
                .Include(r => r.Group)
                .Select(r => new MemberUseViewModel
                {
                    RegistrationID = r.RegistrationID,
                    GroupName = r.Group.GroupName,
                    EndDate = r.Group.EndDate
                })
                .ToList();
            ViewBag.registeredGroups = registeredGroups;
            return View();

        }

       
        [HttpPost]
        public IActionResult DeleteRegistration(int RegistrationID)
        {
            var record = _context.Registration.Find(RegistrationID);
            if (record != null)
            {
                
                _context.Registration.Remove(record);
                _context.SaveChanges();
            }
            return RedirectToAction("Registration");
        }


        public IActionResult LikeRecord()
        {
            var userId = 1; // 從當前登錄的使用者取得UserId

            var currentDate = DateTime.Now; // 取得當前日期

                 // 取得這個UserId所有的LikeRecords以及相對應的MyActivity資料
                 var likedActivities = _context.LikeRecord
                .Where(lr => lr.UserID == userId)
                .Include(lr => lr.Activity)
                .Select(lr => new MemberUseViewModel
                {
                    LikeRecordID = lr.LikeRecordID,  //為了刪除
                    ActivityName = lr.Activity.ActivityName,
                    VoteDate = lr.Activity.VoteDate,
                    // 判斷是否可以編輯
                    CanEdit = currentDate >= lr.Activity.VoteDate && currentDate <= lr.Activity.VoteDate.Value.AddDays(5)
                })
                .ToList();

            ViewBag.LikedActivities = likedActivities;

            return View();
        }

        [HttpPost]
    public IActionResult DeleteLikeRecord(int likeRecordID)
    {
    var record = _context.LikeRecord.Find(likeRecordID);
    if (record != null)
    {
        _context.LikeRecord.Remove(record);
        _context.SaveChanges();
    }
    return RedirectToAction("LikeRecord");
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
