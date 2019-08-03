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
    public class ClassesController : Controller
    {
        private readonly SmartUniContext _context;

        public ClassesController(SmartUniContext context)
        {
            _context = context;
        }

        // GET: Classes
        public async Task<IActionResult> Index()
        {
            var smartUniContext = _context.Class.Include(@a => @a.StudyLevel).Include(@b => @b.Tutor);
            return View(await smartUniContext.ToListAsync());
        }

        // GET: Classes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Class
                .Include(@a => @a.StudyLevel)
                .Include(@b => @b.Tutor)
                .FirstOrDefaultAsync(m => m.ClassId == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // GET: Classes/Create
        public IActionResult Create()
        {
            ViewData["StudyLevelId"] = new SelectList(_context.StudyLevel, "StudyLevelId", "StudyLevelDesc");
            ViewData["TutorId"] = new SelectList(_context.Tutor, "TutorId", "TutorId");
            return View();
        }

        // POST: Classes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClassId,ClassDesc,StudyLevelId,TutorId,Year")] Class @class)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@class);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudyLevelId"] = new SelectList(_context.StudyLevel, "StudyLevelId", "StudyLevelDesc", @class.StudyLevelId);
            ViewData["TutorId"] = new SelectList(_context.Tutor, "TutorId", "TutorId", @class.TutorId);
            return View(@class);
        }

        // GET: Classes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Class.FindAsync(id);
            if (@class == null)
            {
                return NotFound();
            }
            ViewData["StudyLevelId"] = new SelectList(_context.StudyLevel, "StudyLevelId", "StudyLevelDesc", @class.StudyLevelId);
            ViewData["TutorId"] = new SelectList(_context.Tutor, "TutorId", "TutorId", @class.TutorId);
            return View(@class);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ClassId,ClassDesc,StudyLevelId,TutorId,Year")] Class @class)
        {
            if (id != @class.ClassId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@class);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassExists(@class.ClassId))
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
            ViewData["StudyLevelId"] = new SelectList(_context.StudyLevel, "StudyLevelId", "StudyLevelDesc", @class.StudyLevelId);
            ViewData["TutorId"] = new SelectList(_context.Tutor, "TutorId", "TutorId", @class.TutorId);
            return View(@class);
        }

        // GET: Classes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Class
                .Include(@a => @a.StudyLevel)
                .Include(@b => @b.Tutor)
                .FirstOrDefaultAsync(m => m.ClassId == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var @class = await _context.Class.FindAsync(id);
            _context.Class.Remove(@class);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassExists(string id)
        {
            return _context.Class.Any(e => e.ClassId == id);
        }
    }
}
