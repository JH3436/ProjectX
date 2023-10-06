using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace MVC_Project.Controllers

{

    public class MemberController : Controller
    {
        private readonly ProjectXContext _context;

        public MemberController(ProjectXContext context)
        {
            _context = context;
        }

        public IActionResult Registration(int page = 1)
        {
            int pageSize = 6; // 每頁顯示的項目數
            var userId = 1; // 從當前登錄的使用者取得UserId

            // 根據當前頁數和每頁顯示的項目數計算跳過的項目數
            int skip = (page - 1) * pageSize;

            var registeredGroups = _context.Registration
                .Where(r => r.ParticipantID == userId)
                .Include(r => r.Group)
                .Select(r => new MemberUseViewModel
                {
                    RegistrationID = r.RegistrationID,
                    GroupName = r.Group.GroupName,
                    EndDate = r.Group.EndDate
                })
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            // 計算是否有下一頁的邏輯
            bool hasNextPage = _context.Registration
                .Where(r => r.ParticipantID == userId)
                .Skip(skip + pageSize)
                .Any();

            ViewBag.RegisteredGroups = registeredGroups;
            ViewBag.PageNumber = page;
            ViewBag.HasNextPage = hasNextPage;

            return View();
        }


        ////public IActionResult Registration(Group group)
        ////{


        ////    var userId = 1; // 從當前登錄的使用者取得UserId

        ////    var registeredGroups = _context.Registration
        ////        .Where(r => r.ParticipantID == userId)
        ////        .Include(r => r.Group)
        ////        .Select(r => new MemberUseViewModel
        ////        {
        ////            RegistrationID = r.RegistrationID,
        ////            GroupName = r.Group.GroupName,
        ////            EndDate = r.Group.EndDate
        ////        })
        ////        .ToList();
        ////    ViewBag.registeredGroups = registeredGroups;
        ////    return View();

        ////}


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

        public IActionResult LikeRecord(int page = 1)
        {
            int pageSize = 6; // 每頁顯示的項目數
            var userId = 1; // 從當前登錄的使用者取得UserId

            var currentDate = DateTime.Now; // 取得當前日期

            // 根據當前頁數和每頁顯示的項目數計算跳過的項目數
            int skip = (page - 1) * pageSize;

            // 取得這個UserId的LikeRecords以及相對應的MyActivity資料，根據分頁參數進行分頁
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
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            // 計算是否有下一頁的邏輯
            bool hasNextPage = _context.LikeRecord
                .Where(lr => lr.UserID == userId)
                .Skip(skip + pageSize)
                .Any();

            ViewBag.LikedActivities = likedActivities;
            ViewBag.PageNumber = page;
            ViewBag.HasNextPage = hasNextPage;

            return View();
        }

        //public IActionResult LikeRecord()
        //{
        //    var userId = 1; // 從當前登錄的使用者取得UserId

        //    var currentDate = DateTime.Now; // 取得當前日期

        //    // 取得這個UserId所有的LikeRecords以及相對應的MyActivity資料
        //    var likedActivities = _context.LikeRecord
        //   .Where(lr => lr.UserID == userId)
        //   .Include(lr => lr.Activity)
        //   .Select(lr => new MemberUseViewModel
        //   {
        //       LikeRecordID = lr.LikeRecordID,  //為了刪除
        //       ActivityName = lr.Activity.ActivityName,
        //       VoteDate = lr.Activity.VoteDate,
        //       // 判斷是否可以編輯
        //       CanEdit = currentDate >= lr.Activity.VoteDate && currentDate <= lr.Activity.VoteDate.Value.AddDays(5)
        //   })
        //   .ToList();

        //    ViewBag.LikedActivities = likedActivities;

        //    return View();
        //}

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

   
        public IActionResult PersonGroup()
        {
            return View();
        }




        public IActionResult Member()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdateUserPhoto(int userId, string imageBase64)
        {
            var member = _context.Member.FirstOrDefault(m => m.UserID == userId);
            if (member != null && !string.IsNullOrEmpty(imageBase64))
            {
                byte[] photoBytes = Convert.FromBase64String(imageBase64.Split(',')[1]);
                member.UserPhoto = photoBytes;
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Failed to update photo" });
        }
       
        [HttpGet]
        public IActionResult GetUserPhoto(int userId)
        {
            var member = _context.Member.FirstOrDefault(m => m.UserID == userId);
            if (member != null && member.UserPhoto != null)
            {
                string imageBase64Data = Convert.ToBase64String(member.UserPhoto);
                string imageDataURL = $"data:image/jpg;base64,{imageBase64Data}";
                return Json(new { imageUrl = imageDataURL });
            }
            return Json(new { imageUrl = "" }); // 或者返回預設照片的URL
        }



    }
    /*var member = GetCurrentUser();*/ // 這裡需要您自己實現獲取當前用戶的方法
}

