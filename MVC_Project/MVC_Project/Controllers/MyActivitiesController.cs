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
    public class MyActivitiesController : Controller
    {
        private readonly ProjectXContext _context;

        public MyActivitiesController(ProjectXContext context)
        {
            _context = context;
        }

        // GET: MyActivities
        public async Task<IActionResult> Index()
        {
              return _context.MyActivities != null ? 
                          View(await _context.MyActivities.ToListAsync()) :
                          Problem("Entity set 'ProjectXContext.MyActivities'  is null.");
        }

        // GET: MyActivities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MyActivities == null)
            {
                return NotFound();
            }

            var myActivity = await _context.MyActivities
                .FirstOrDefaultAsync(m => m.ActivityId == id);
            if (myActivity == null)
            {
                return NotFound();
            }

            return View(myActivity);
        }

        // GET: MyActivities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MyActivities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActivityId,ActivityName,Category,Map,SuggestedAmount,ActivityContent,MinAttendee,VoteDate,ExpectedDepartureMonth")] MyActivity myActivity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(myActivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(myActivity);
        }

        // GET: MyActivities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MyActivities == null)
            {
                return NotFound();
            }

            var myActivity = await _context.MyActivities.FindAsync(id);
            if (myActivity == null)
            {
                return NotFound();
            }
            return View(myActivity);
        }

        // POST: MyActivities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActivityId,ActivityName,Category,Map,SuggestedAmount,ActivityContent,MinAttendee,VoteDate,ExpectedDepartureMonth")] MyActivity myActivity)
        {
            if (id != myActivity.ActivityId)
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
                    if (!MyActivityExists(myActivity.ActivityId))
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

        // GET: MyActivities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MyActivities == null)
            {
                return NotFound();
            }

            var myActivity = await _context.MyActivities
                .FirstOrDefaultAsync(m => m.ActivityId == id);
            if (myActivity == null)
            {
                return NotFound();
            }

            return View(myActivity);
        }

        // POST: MyActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MyActivities == null)
            {
                return Problem("Entity set 'ProjectXContext.MyActivities'  is null.");
            }
            var myActivity = await _context.MyActivities.FindAsync(id);
            if (myActivity != null)
            {
                _context.MyActivities.Remove(myActivity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MyActivityExists(int id)
        {
          return (_context.MyActivities?.Any(e => e.ActivityId == id)).GetValueOrDefault();
        }
    }
}
