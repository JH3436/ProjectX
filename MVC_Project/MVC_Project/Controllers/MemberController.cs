using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
        public ActionResult UploadImage(string imageData)
        {
            if (!string.IsNullOrEmpty(imageData))
            {
                // 轉換Base64字串為位元組數組
                byte[] imageBytes = Convert.FromBase64String(imageData);

                // 假設您要處理UserID為1的用戶
                int userId = 1;

                // 查找用戶
                var user = _context.Member.FirstOrDefault(u => u.UserID == userId);
                if (user != null)
                {
                    // 更新用戶的UserPhoto欄位
                    user.UserPhoto = imageBytes;

                    // 保存更改到資料庫
                    _context.SaveChanges();
                }

                // 返回成功的JSON響應，您可以在此處返回更新後的圖像URL
                return Json(new { success = true, imageUrl = "/Member/GetUserImage" }); // 更新為實際的圖像路徑
            }

            // 如果圖像數據為空，返回失敗的JSON響應
            return Json(new { success = false });
        }

        [HttpGet]
        public ActionResult GetUserImage()
        {
            // 假設您要處理UserID為1的用戶
            int userId = 1;

            // 查找用戶
            var user = _context.Member.FirstOrDefault(u => u.UserID == userId);
            if (user != null && user.UserPhoto != null)
            {
                // 返回用戶的圖像
                return File(user.UserPhoto, "image/jpeg"); // 此處假設圖像類型為jpeg，根據您的實際情況修改
            }

            // 如果找不到用戶或圖像為空，返回一個預設圖像或錯誤圖像，或者根據您的需求返回404錯誤
            return NotFound(); // 或者返回一個預設圖像的File結果
        }
    }
    /*var member = GetCurrentUser();*/ // 這裡需要您自己實現獲取當前用戶的方法
}

