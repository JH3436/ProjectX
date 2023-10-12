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
    public class VoteRecordsController : Controller
    {
        private readonly ProjectXContext _context;

        public VoteRecordsController(ProjectXContext context)
        {
            _context = context;
        }

        // GET: VoteRecords
        public async Task<IActionResult> Index()
        {
            var projectXContext = _context.VoteRecord.Include(v => v.Activity).Include(v => v.User);
            return View(await projectXContext.ToListAsync());
        }

        // GET: VoteRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VoteRecord == null)
            {
                return NotFound();
            }

            var voteRecord = await _context.VoteRecord
                .Include(v => v.Activity)
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.RecordID == id);
            if (voteRecord == null)
            {
                return NotFound();
            }

            return View(voteRecord);
        }

        // GET: VoteRecords/Create
        public IActionResult Create()
        {
            ViewData["ActivityID"] = new SelectList(_context.MyActivity, "ActivityID", "ActivityID");
            ViewData["UserID"] = new SelectList(_context.Member, "UserID", "UserID");
            return View();
        }

        // POST: VoteRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecordID,UserID,ActivityID,VoteResult")] VoteRecord voteRecord)
        {
            if (ModelState.IsValid)
            {
				// 分割 VoteResult 字串以取得開始和結束日期
				//string[] dateRange = voteRecord.VoteResult.Split(" - ");
				//// 只使用開始日期
				//voteRecord.VoteResult = dateRange[0];

				_context.Add(voteRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActivityID"] = new SelectList(_context.MyActivity, "ActivityID", "ActivityID", voteRecord.ActivityID);
            ViewData["UserID"] = new SelectList(_context.Member, "UserID", "UserID", voteRecord.UserID);
            return View(voteRecord);
        }

        // GET: VoteRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VoteRecord == null)
            {
                return NotFound();
            }

            var voteRecord = await _context.VoteRecord.FindAsync(id);
            if (voteRecord == null)
            {
                return NotFound();
            }
            ViewData["ActivityID"] = new SelectList(_context.MyActivity, "ActivityID", "ActivityID", voteRecord.ActivityID);
            ViewData["UserID"] = new SelectList(_context.Member, "UserID", "UserID", voteRecord.UserID);
            return View(voteRecord);
        }

        // POST: VoteRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecordID,UserID,ActivityID,VoteResult")] VoteRecord voteRecord)
        {
            if (id != voteRecord.RecordID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voteRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoteRecordExists(voteRecord.RecordID))
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
            ViewData["ActivityID"] = new SelectList(_context.MyActivity, "ActivityID", "ActivityID", voteRecord.ActivityID);
            ViewData["UserID"] = new SelectList(_context.Member, "UserID", "UserID", voteRecord.UserID);
            return View(voteRecord);
        }

        // GET: VoteRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VoteRecord == null)
            {
                return NotFound();
            }

            var voteRecord = await _context.VoteRecord
                .Include(v => v.Activity)
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.RecordID == id);
            if (voteRecord == null)
            {
                return NotFound();
            }

            return View(voteRecord);
        }

        // POST: VoteRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VoteRecord == null)
            {
                return Problem("Entity set 'ProjectXContext.VoteRecord'  is null.");
            }
            var voteRecord = await _context.VoteRecord.FindAsync(id);
            if (voteRecord != null)
            {
                _context.VoteRecord.Remove(voteRecord);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoteRecordExists(int id)
        {
          return (_context.VoteRecord?.Any(e => e.RecordID == id)).GetValueOrDefault();
        }
    }
}
