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
    public class SubjectsController : Controller
    {
        private readonly SmartUniContext _context;

        public SubjectsController(SmartUniContext context)
        {
            _context = context;
        }

        // GET: Subjects
        public async Task<IActionResult> Index()
        {
            var smartUniContext = _context.Subject.Include(s => s.Tutor).Include(ss => ss.StudentSubject);
            return View(await smartUniContext.ToListAsync());
        }

        // GET: Subjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Errors");
            }

            StudentSubjectList studentSubjectList = new StudentSubjectList();

            var subject = await _context.Subject
                .Include(s => s.Tutor)
                .FirstOrDefaultAsync(m => m.SubjectId == id);

            List<StudentSubject> studentList = await _context.StudentSubject
                .Include(s => s.Student)
                .Where(m => m.SubjectId == id)
                .ToListAsync();

            studentSubjectList.Subject = subject;
            studentSubjectList.StudentSubjectListing = studentList;

            if (subject == null)
            {
                return RedirectToAction("Index", "Errors");
            }

            return View(studentSubjectList);
        }

        // GET: Subjects/Create
        public IActionResult Create()
        {
            ViewData["TutorId"] = new SelectList(_context.Tutor, "TutorId", "TutorName");
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubjectId,SubjectName,TutorId")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TutorId"] = new SelectList(_context.Tutor, "TutorId", "TutorId", subject.TutorId);
            return View(subject);
        }

        // GET: Subjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Errors");
            }

            var subject = await _context.Subject.FindAsync(id);
            if (subject == null)
            {
                return RedirectToAction("Index", "Errors");
            }
            ViewData["TutorId"] = new SelectList(_context.Tutor, "TutorId", "TutorName", subject.TutorId);
            return View(subject);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubjectId,SubjectName,TutorId")] Subject subject)
        {
            if (id != subject.SubjectId)
            {
                return RedirectToAction("Index", "Errors");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectExists(subject.SubjectId))
                    {
                        return RedirectToAction("Index", "Errors");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TutorId"] = new SelectList(_context.Tutor, "TutorId", "TutorName", subject.TutorId);
            return View(subject);
        }

        // GET: Subjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Errors");
            }

            var subject = await _context.Subject
                .Include(s => s.Tutor)
                .FirstOrDefaultAsync(m => m.SubjectId == id);
            if (subject == null)
            {
                return RedirectToAction("Index", "Errors");
            }

            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subject = await _context.Subject.FindAsync(id);
            _context.Subject.Remove(subject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectExists(int id)
        {
            return _context.Subject.Any(e => e.SubjectId == id);
        }
    }
}
