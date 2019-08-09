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
    public class TutorStatusController : Controller
    {
        private readonly SmartUniContext _context;

        public TutorStatusController(SmartUniContext context)
        {
            _context = context;
        }

        // GET: TutorStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.TutorStatus.ToListAsync());
        }

        // GET: TutorStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Errors");
            }

            var tutorStatus = await _context.TutorStatus
                .FirstOrDefaultAsync(m => m.TutorStatusId == id);
            if (tutorStatus == null)
            {
                return RedirectToAction("Index", "Errors");
            }

            return View(tutorStatus);
        }

        // GET: TutorStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TutorStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TutorStatusId,TutorStatusDesc")] TutorStatus tutorStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tutorStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tutorStatus);
        }

        // GET: TutorStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Errors");
            }

            var tutorStatus = await _context.TutorStatus.FindAsync(id);
            if (tutorStatus == null)
            {
                return RedirectToAction("Index", "Errors");
            }
            return View(tutorStatus);
        }

        // POST: TutorStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TutorStatusId,TutorStatusDesc")] TutorStatus tutorStatus)
        {
            if (id != tutorStatus.TutorStatusId)
            {
                return RedirectToAction("Index", "Errors");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tutorStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TutorStatusExists(tutorStatus.TutorStatusId))
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
            return View(tutorStatus);
        }

        // GET: TutorStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Errors");
            }

            var tutorStatus = await _context.TutorStatus
                .FirstOrDefaultAsync(m => m.TutorStatusId == id);
            if (tutorStatus == null)
            {
                return RedirectToAction("Index", "Errors");
            }

            return View(tutorStatus);
        }

        // POST: TutorStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tutorStatus = await _context.TutorStatus.FindAsync(id);
            _context.TutorStatus.Remove(tutorStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TutorStatusExists(int id)
        {
            return _context.TutorStatus.Any(e => e.TutorStatusId == id);
        }
    }
}
