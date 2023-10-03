﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;

namespace MVC_Project.Controllers
{
    public class PersonalPhotoesController : Controller
    {
        private readonly ProjectXContext _context;

        public PersonalPhotoesController(ProjectXContext context)
        {
            _context = context;
        }

        // GET: PersonalPhotoes PersonalPhoto
        public async Task<IActionResult> Index()
        {
            var projectXContext = _context.PersonalPhoto.Include(p => p.Group);
            return View(await projectXContext.ToListAsync());
        }

        // GET: PersonalPhotoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PersonalPhoto == null)
            {
                return NotFound();
            }

            var personalPhoto = await _context.PersonalPhoto
                .Include(p => p.Group)
                .FirstOrDefaultAsync(m => m.PersonalPhotoID == id);
            if (personalPhoto == null)
            {
                return NotFound();
            }

            return View(personalPhoto);
        }

        // GET: PersonalPhotoes/Create
        public IActionResult Create()
        {
            ViewData["GroupID"] = new SelectList(_context.Group, "GroupID", "GroupID");
            return View();
        }

        // POST: PersonalPhotoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonalPhotoID,GroupID,PhotoData")] PersonalPhoto personalPhoto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personalPhoto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupID"] = new SelectList(_context.Group, "GroupID", "GroupID", personalPhoto.GroupID);
            return View(personalPhoto);
        }

        // GET: PersonalPhotoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PersonalPhoto == null)
            {
                return NotFound();
            }

            var personalPhoto = await _context.PersonalPhoto.FindAsync(id);
            if (personalPhoto == null)
            {
                return NotFound();
            }
            ViewData["GroupID"] = new SelectList(_context.Group, "GroupID", "GroupID", personalPhoto.GroupID);
            return View(personalPhoto);
        }

        // POST: PersonalPhotoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonalPhotoID,GroupID,PhotoData")] PersonalPhoto personalPhoto)
        {
            if (id != personalPhoto.PersonalPhotoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personalPhoto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonalPhotoExists(personalPhoto.PersonalPhotoID))
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
            ViewData["GroupID"] = new SelectList(_context.Group, "GroupID", "GroupID", personalPhoto.GroupID);
            return View(personalPhoto);
        }

        // GET: PersonalPhotoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PersonalPhoto == null)
            {
                return NotFound();
            }

            var personalPhoto = await _context.PersonalPhoto
                .Include(p => p.Group)
                .FirstOrDefaultAsync(m => m.PersonalPhotoID == id);
            if (personalPhoto == null)
            {
                return NotFound();
            }

            return View(personalPhoto);
        }

        // POST: PersonalPhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PersonalPhoto == null)
            {
                return Problem("Entity set 'ProjectXContext.PersonalPhoto'  is null.");
            }
            var personalPhoto = await _context.PersonalPhoto.FindAsync(id);
            if (personalPhoto != null)
            {
                _context.PersonalPhoto.Remove(personalPhoto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonalPhotoExists(int id)
        {
          return (_context.PersonalPhoto?.Any(e => e.PersonalPhotoID == id)).GetValueOrDefault();
        }
    }
}
