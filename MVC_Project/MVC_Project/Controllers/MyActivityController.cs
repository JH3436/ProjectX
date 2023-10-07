using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;

namespace MVC_Project.Controllers
{
    public class MyActivityController : Controller
    {
        private readonly ProjectXContext _context;

        public MyActivityController(ProjectXContext context)
        {
            _context = context;
        }

        public IActionResult HomePage()
        {
            // 創建一個亂數生成器
            Random random = new Random();
            List<int> selectedIds = new List<int>();
            int maxActivityID = 4; // 假設資料庫中的最大 ActivityId 是 5

            while (selectedIds.Count < 3)
            {
                int randomId = random.Next(1, maxActivityID + 1); // 隨機生成 Id，範圍是 1 到 maxActivityID
                if (!selectedIds.Contains(randomId))
                {
                    selectedIds.Add(randomId);
                }
            }

            //官方資料讀取
            var myActivityData = from m in _context.MyActivity
                                 join o in _context.OfficialPhoto
                                 on m.ActivityID equals o.ActivityID
                                 where selectedIds.Contains(m.ActivityID)
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
                            where selectedIds.Contains(g.GroupID)
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
                                PhotoData = pp.PhotoData
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
