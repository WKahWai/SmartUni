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
    public class StudyStatusController : Controller
    {
        private readonly SmartUniContext _context;

        public StudyStatusController(SmartUniContext context)
        {
            _context = context;
        }

        // GET: StudyStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudyStatus.ToListAsync());
        }

        // GET: StudyStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyStatus = await _context.StudyStatus
                .FirstOrDefaultAsync(m => m.StudyStatusId == id);
            if (studyStatus == null)
            {
                return NotFound();
            }

            return View(studyStatus);
        }

        // GET: StudyStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudyStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudyStatusId,StudyStatusDesc")] StudyStatus studyStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studyStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studyStatus);
        }

        // GET: StudyStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyStatus = await _context.StudyStatus.FindAsync(id);
            if (studyStatus == null)
            {
                return NotFound();
            }
            return View(studyStatus);
        }

        // POST: StudyStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudyStatusId,StudyStatusDesc")] StudyStatus studyStatus)
        {
            if (id != studyStatus.StudyStatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studyStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudyStatusExists(studyStatus.StudyStatusId))
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
            return View(studyStatus);
        }

        // GET: StudyStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyStatus = await _context.StudyStatus
                .FirstOrDefaultAsync(m => m.StudyStatusId == id);
            if (studyStatus == null)
            {
                return NotFound();
            }

            return View(studyStatus);
        }

        // POST: StudyStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studyStatus = await _context.StudyStatus.FindAsync(id);
            _context.StudyStatus.Remove(studyStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudyStatusExists(int id)
        {
            return _context.StudyStatus.Any(e => e.StudyStatusId == id);
        }
    }
}
