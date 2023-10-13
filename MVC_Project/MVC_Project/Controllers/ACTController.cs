using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;
using SmartBreadcrumbs.Attributes;

namespace MVC_Project.Controllers
{
    public class ACTController : Controller
    {
        private readonly ProjectXContext _context;

        public ACTController(ProjectXContext context)
        {
            _context = context;
        }

        [Breadcrumb("所有活動", FromAction = nameof(MyActivityController.HomePage), FromController = typeof(MyActivityController))]
        public IActionResult ACT(int? page)
        {
            int pageSize = 9;             // 計算要跳過的項目數量
            int pageNumber = (page ?? 1); // 如果 page 為空，默認為第 1 頁

            var myActivityData = (from m in _context.MyActivity
                                  join o in _context.OfficialPhoto
                                  on m.ActivityID equals o.ActivityID
                                  /*orderby m.CreatedDate*/ // 依照 CreatedDate 進行排序
                                  select new ResponseActivity
                                  {
                                      ActivityName = m.ActivityName,
                                      Category = m.Category,
                                      SuggestedAmount = m.SuggestedAmount,
                                      ActivityContent = m.ActivityContent,
                                      MinAttendee = m.MinAttendee,
                                      VoteDate = m.VoteDate,
                                      ExpectedDepartureMonth = m.ExpectedDepartureMonth,
                                      PhotoPath = o.PhotoPath
                                  });

            //個人開團資料讀取
            var groupData = (from g in _context.Group
                             join m in _context.Member on g.Organizer equals m.UserID
                             join ma in _context.MyActivity on g.OriginalActivityID equals ma.ActivityID
                             join pp in _context.PersonalPhoto on g.GroupID equals pp.GroupID into personalPhotos
                             from pp in personalPhotos.DefaultIfEmpty()
                                 //where selectedIds.Contains(g.GroupID)
                             select new ResponseGroup
                             {
                                 GroupName = g.GroupName,
                                 GroupCategory = g.GroupCategory,
                                 GroupContent = g.GroupContent,
                                 MinAttendee = g.MinAttendee,
                                 MaxAttendee = g.MaxAttendee,
                                 StartDate = g.StartDate,
                                 EndDate = g.EndDate,
                                 Nickname = m.Nickname,
                                 PhotoData = pp != null ? pp.PhotoData : null // PersonalPhoto 的 PhotoData，如果存在的話
                             });

            int itemsToSkip = (pageNumber - 1) * pageSize;

            int totalItems = _context.MyActivity.Count() + _context.Group.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);


            var activities = myActivityData.ToList();
            var groups = groupData.ToList();

            var viewModel = new HomePageViewModel
            {
                Activities = myActivityData.Skip(itemsToSkip).Take(pageSize).ToList(),
                Groups = groupData.Skip(itemsToSkip).Take(pageSize).ToList(),
                TotalPages = totalPages,
                CurrentPage = pageNumber
            };

            // 計算 DurationInDays 並設定給 ResponseGroup
            foreach (var group in viewModel.Groups)
            {
                TimeSpan? timeSpan = group.EndDate - group.StartDate;
                group.DurationInDays = (int)timeSpan.Value.TotalDays;
            }
            return View(viewModel);
        }



    }
}