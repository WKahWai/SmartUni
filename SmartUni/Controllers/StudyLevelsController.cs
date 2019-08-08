using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartUni.Models;

namespace SmartUni.Controllers
{
    public class StudyLevelsController : Controller
    {
        private readonly SmartUniContext _context;

        public StudyLevelsController(SmartUniContext context)
        {
            _context = context;
        }

        // GET: StudyLevels
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudyLevel.ToListAsync());
        }

        // GET: StudyLevels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyLevel = await _context.StudyLevel
                .FirstOrDefaultAsync(m => m.StudyLevelId == id);
            if (studyLevel == null)
            {
                return NotFound();
            }

            return View(studyLevel);
        }

        // GET: StudyLevels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudyLevels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudyLevelId,StudyLevelDesc")] StudyLevel studyLevel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studyLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studyLevel);
        }

        // GET: StudyLevels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyLevel = await _context.StudyLevel.FindAsync(id);
            if (studyLevel == null)
            {
                return NotFound();
            }
            return View(studyLevel);
        }

        // POST: StudyLevels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudyLevelId,StudyLevelDesc")] StudyLevel studyLevel)
        {
            if (id != studyLevel.StudyLevelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studyLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudyLevelExists(studyLevel.StudyLevelId))
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
            return View(studyLevel);
        }

        // GET: StudyLevels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyLevel = await _context.StudyLevel
                .FirstOrDefaultAsync(m => m.StudyLevelId == id);
            if (studyLevel == null)
            {
                return NotFound();
            }

            return View(studyLevel);
        }

        // POST: StudyLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studyLevel = await _context.StudyLevel.FindAsync(id);
            _context.StudyLevel.Remove(studyLevel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudyLevelExists(int id)
        {
            return _context.StudyLevel.Any(e => e.StudyLevelId == id);
        }
    }
}
