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
    public class MyActivityController : Controller
    {
        private readonly ProjectXContext _context;

        public MyActivityController(ProjectXContext context)
        {
            _context = context;
        }

        [DefaultBreadcrumb("首頁")]
		public IActionResult HomePage()
        {
            // 讀取所有有效的 ActivityID 到一個列表中
            var validActivityIds = _context.MyActivity.Select(a =>a.ActivityID).ToList();

            // 設定要生成的亂數ID數量
            int numberOfRandomIds = Math.Min(3, validActivityIds.Count);

            // 使用亂數生成器來選擇有效的 ActivityID
            Random random = new Random();
            List<int> selectedActivityIds = new List<int>();


            while (selectedActivityIds.Count < numberOfRandomIds)
            {
                int randomId = validActivityIds[random.Next(validActivityIds.Count)];

                if (!selectedActivityIds.Contains(randomId))
                {
                    selectedActivityIds.Add(randomId);
                }
            }

            // 讀取所有有效的 GroupID 到一個列表中
            var validGroupIds = _context.Group.Select(g => g.GroupID).ToList();

            // 設定要生成的亂數ID數量
            int numberOfRandomIds_2 = Math.Min(3, validGroupIds.Count);


            // 使用亂數生成器來選擇有效的 GroupID
            Random random2 = new Random();
            List<int> selectedGroupIds = new List<int>();

            while (selectedGroupIds.Count < numberOfRandomIds_2)
            {
                int randomId = validGroupIds[random.Next(validGroupIds.Count)];

                if (!selectedGroupIds.Contains(randomId))
                {
                    selectedGroupIds.Add(randomId);
                }
            }

            // 官方活動資料讀取，按 ActivityID 分組，並對相同的 ActivityID 的照片進行隨機排序
            var myActivityData = from m in _context.MyActivity
                                       join o in _context.OfficialPhoto
                                       on m.ActivityID equals o.ActivityID
                                       where selectedActivityIds.Contains(m.ActivityID)
                                       group new { m, o } by m.ActivityID into grouped
                                       select new ResponseActivity
                                       {
                                           ActivityName = grouped.FirstOrDefault().m.ActivityName,
                                           Category = grouped.FirstOrDefault().m.Category,
                                           SuggestedAmount = grouped.FirstOrDefault().m.SuggestedAmount,
                                           ActivityContent = grouped.FirstOrDefault().m.ActivityContent,
                                           MinAttendee = grouped.FirstOrDefault().m.MinAttendee,
                                           VoteDate = grouped.FirstOrDefault().m.VoteDate,
                                           ExpectedDepartureMonth = grouped.FirstOrDefault().m.ExpectedDepartureMonth,
                                           // 在這裡對相同 ActivityID 的照片進行隨機排序，然後選取第一張照片
                                           PhotoPath = grouped.OrderBy(x => Guid.NewGuid()).FirstOrDefault().o.PhotoPath
                                       };

            //個人開團資料讀取，按 GroupID 分組，並對相同的 GroupID 的照片進行隨機排序
            var groupData = from g in _context.Group
                            join m in _context.Member on g.Organizer equals m.UserID
                            join pp in _context.PersonalPhoto on g.GroupID equals pp.GroupID into personalPhotos
                            from pp in personalPhotos.DefaultIfEmpty()
                            where selectedGroupIds.Contains(g.GroupID)
                            group new { g, pp, m } by g.GroupID into grouped
                            select new ResponseGroup
                            {
                                GroupName = grouped.FirstOrDefault().g.GroupName,
                                GroupCategory = grouped.FirstOrDefault().g.GroupCategory,
                                GroupContent = grouped.FirstOrDefault().g.GroupContent,
                                MinAttendee = grouped.FirstOrDefault().g.MinAttendee,
                                MaxAttendee = grouped.FirstOrDefault().g.MaxAttendee,
                                StartDate = grouped.FirstOrDefault().g.StartDate,
                                EndDate = grouped.FirstOrDefault().g.EndDate,
                                Nickname = grouped.FirstOrDefault().m.Nickname,
                                // 在這裡對相同 GroupID 的照片進行隨機排序，然後選取第一張照片
                                PhotoData = grouped.OrderBy(x => Guid.NewGuid()).FirstOrDefault().pp.PhotoData
                            };



            var activities = myActivityData.ToList();
            var groups = groupData.ToList();

            //處理PhotoData可能為0的狀況
            foreach (var group in groups)
            {
                if (group.PhotoData == null)
                {
                    group.PhotoData = new byte[0];
                }
            }

            var viewModel = new HomePageViewModel
            {
                Activities = activities,
                Groups = groups
            };

            // 計算 DurationInDays 並設定給 ResponseGroup
            foreach (var group in viewModel.Groups)
            {
                TimeSpan? timeSpan = group.EndDate - group.StartDate;
                group.DurationInDays = (int)timeSpan.Value.TotalDays;
            }

            return View(viewModel);
        }

        [Breadcrumb("所有活動", FromAction = nameof(MyActivityController.HomePage), FromController = typeof(MyActivityController))]
        public IActionResult ACT()
        {
            var myActivityData = from m in _context.MyActivity
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
                                 };

            //個人開團資料讀取
            var groupData = from g in _context.Group
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
                            };
            var activities = myActivityData.ToList();
            var groups = groupData.ToList();

            var viewModel = new HomePageViewModel
            {
                Activities = activities,
                Groups = groups
            };

            // 計算 DurationInDays 並設定給 ResponseGroup
            foreach (var group in viewModel.Groups)
            {
                TimeSpan? timeSpan = group.EndDate - group.StartDate;
                group.DurationInDays = (int)timeSpan.Value.TotalDays;
            }

            return View(viewModel);
        }

        // GET: MyActivity
        public async Task<IActionResult> Index()
        {
            return _context.MyActivity != null ?
                        View(await _context.MyActivity.ToListAsync()) :
                        Problem("Entity set 'ProjectXContext.MyActivity'  is null.");
        }

        // GET: MyActivity/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MyActivity == null)
            {
                return NotFound();
            }

            var myActivity = await _context.MyActivity
                .FirstOrDefaultAsync(m => m.ActivityID == id);
            if (myActivity == null)
            {
                return NotFound();
            }

            return View(myActivity);
        }

        // GET: MyActivity/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MyActivity/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActivityID,ActivityName,Category,SuggestedAmount,ActivityContent,MinAttendee,VoteDate,ExpectedDepartureMonth")] MyActivity myActivity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(myActivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(myActivity);
        }

        // GET: MyActivity/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MyActivity == null)
            {
                return NotFound();
            }

            var myActivity = await _context.MyActivity.FindAsync(id);
            if (myActivity == null)
            {
                return NotFound();
            }
            return View(myActivity);
        }

        // POST: MyActivity/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActivityID,ActivityName,Category,SuggestedAmount,ActivityContent,MinAttendee,VoteDate,ExpectedDepartureMonth")] MyActivity myActivity)
        {
            if (id != myActivity.ActivityID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(myActivity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MyActivityExists(myActivity.ActivityID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(myActivity);
        }

        // GET: MyActivity/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MyActivity == null)
            {
                return NotFound();
            }

            var myActivity = await _context.MyActivity
                .FirstOrDefaultAsync(m => m.ActivityID == id);
            if (myActivity == null)
            {
                return NotFound();
            }

            return View(myActivity);
        }

        // POST: MyActivity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MyActivity == null)
            {
                return Problem("Entity set 'ProjectXContext.MyActivity'  is null.");
            }
            var myActivity = await _context.MyActivity.FindAsync(id);
            if (myActivity != null)
            {
                _context.MyActivity.Remove(myActivity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MyActivityExists(int id)
        {
            return (_context.MyActivity?.Any(e => e.ActivityID == id)).GetValueOrDefault();
        }
    }
}
