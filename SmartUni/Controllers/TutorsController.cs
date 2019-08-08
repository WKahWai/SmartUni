using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartUni.Models;

namespace SmartUni.Controllers
{
    public class TutorsController : Controller
    {
        private readonly SmartUniContext _context;

        public TutorsController(SmartUniContext context)
        {
            _context = context;
        }

        // GET: Tutors
        public async Task<IActionResult> Index()
        {
            var smartUniContext = _context.Tutor.Include(t => t.TutorStatus).Include(t => t.TutorType);
            return View(await smartUniContext.ToListAsync());
        }

        public IActionResult Settings()
        {
            return View();
        }

        // GET: Tutors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutor = await _context.Tutor
                .Include(t => t.TutorStatus)
                .Include(t => t.TutorType)
                .FirstOrDefaultAsync(m => m.TutorId == id);

            var tutorId = new SqlParameter("@TutorId", id);
            ViewData["SubjectList"] = _context.Subject.FromSql("EXEC GetSubjectsByTutorId @TutorId", tutorId).ToList();

            if (tutor == null)
            {
                return NotFound();
            }

            return View(tutor);
        }

        // GET: Tutors/Create
        public IActionResult Create()
        {
            ViewData["TutorStatusId"] = new SelectList(_context.TutorStatus, "TutorStatusId", "TutorStatusDesc");
            ViewData["TutorTypeId"] = new SelectList(_context.TutorType, "TutorTypeId", "TutorTypeDesc");
            return View();
        }

        // POST: Tutors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TutorName,Email,PhoneNo,TutorStatusId,TutorTypeId")] Tutor tutor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tutor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TutorStatusId"] = new SelectList(_context.TutorStatus, "TutorStatusId", "TutorStatusDesc", tutor.TutorStatusId);
            ViewData["TutorTypeId"] = new SelectList(_context.TutorType, "TutorTypeId", "TutorTypeDesc", tutor.TutorTypeId);
            return View(tutor);
        }

        // GET: Tutors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutor = await _context.Tutor.FindAsync(id);
            if (tutor == null)
            {
                return NotFound();
            }
            ViewData["TutorStatusId"] = new SelectList(_context.TutorStatus, "TutorStatusId", "TutorStatusDesc", tutor.TutorStatusId);
            ViewData["TutorTypeId"] = new SelectList(_context.TutorType, "TutorTypeId", "TutorTypeDesc", tutor.TutorTypeId);
            return View(tutor);
        }

        // POST: Tutors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TutorId,TutorName,Email,PhoneNo,TutorStatusId,TutorTypeId")] Tutor tutor)
        {
            if (id != tutor.TutorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tutor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TutorExists(tutor.TutorId))
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
            ViewData["TutorStatusId"] = new SelectList(_context.TutorStatus, "TutorStatusId", "TutorStatusDesc", tutor.TutorStatusId);
            ViewData["TutorTypeId"] = new SelectList(_context.TutorType, "TutorTypeId", "TutorTypeDesc", tutor.TutorTypeId);
            return View(tutor);
        }

        // GET: Tutors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutor = await _context.Tutor
                .Include(t => t.TutorStatus)
                .Include(t => t.TutorType)
                .FirstOrDefaultAsync(m => m.TutorId == id);
            if (tutor == null)
            {
                return NotFound();
            }

            return View(tutor);
        }

        // POST: Tutors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tutor = await _context.Tutor.FindAsync(id);
            _context.Tutor.Remove(tutor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TutorExists(int id)
        {
            return _context.Tutor.Any(e => e.TutorId == id);
        }
    }
}
