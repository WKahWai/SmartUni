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
    public class StudentsController : Controller
    {
        private readonly SmartUniContext _context;

        public StudentsController(SmartUniContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var smartUniContext = _context.Student.Include(s => s.Class).Include(s => s.StudyStatus);
            //var studList = _context.Student.FromSql("GetStudents").ToList();
            return View(await smartUniContext.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id.Equals(null))
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.Class)
                .Include(s => s.StudyStatus)
                .FirstOrDefaultAsync(m => m.StudId.Equals(id));
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Settings
        public IActionResult Settings()
        {
            return View();
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.Class, "ClassId", "ClassId");
            ViewData["StudyStatusId"] = new SelectList(_context.StudyStatus, "StudyStatusId", "StudyStatusDesc");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudId,StudName,Email,PhoneNo,StudyStatusId,ClassId")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.Class, "ClassId", "ClassId", student.ClassId);
            ViewData["StudyStatusId"] = new SelectList(_context.StudyStatus, "StudyStatusId", "StudyStatusDesc", student.StudyStatusId);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id.Equals(null))
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.Class, "ClassId", "ClassId", student.ClassId);
            ViewData["StudyStatusId"] = new SelectList(_context.StudyStatus, "StudyStatusId", "StudyStatusDesc", student.StudyStatusId);

            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudId,StudName,Email,PhoneNo,StudyStatusId,ClassId")] Student student)
        {
            if (!(id.Equals(student.StudId)))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudId))
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
            ViewData["ClassId"] = new SelectList(_context.Class, "ClassId", "ClassId", student.ClassId);
            ViewData["StudyStatusId"] = new SelectList(_context.StudyStatus, "StudyStatusId", "StudyStatusDesc", student.StudyStatusId);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id.Equals(null))
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.Class)
                .Include(s => s.StudyStatus)
                .FirstOrDefaultAsync(m => m.StudId.Equals(id));
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Student.FindAsync(id);
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.StudId.Equals(id));
        }
    }
}
