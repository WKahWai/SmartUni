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
    public class TutorTypesController : Controller
    {
        private readonly SmartUniContext _context;

        public TutorTypesController(SmartUniContext context)
        {
            _context = context;
        }

        // GET: TutorTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TutorType.ToListAsync());
        }

        // GET: TutorTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutorType = await _context.TutorType
                .FirstOrDefaultAsync(m => m.TutorTypeId == id);
            if (tutorType == null)
            {
                return NotFound();
            }

            return View(tutorType);
        }

        // GET: TutorTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TutorTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TutorTypeId,TutorTypeDesc")] TutorType tutorType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tutorType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tutorType);
        }

        // GET: TutorTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutorType = await _context.TutorType.FindAsync(id);
            if (tutorType == null)
            {
                return NotFound();
            }
            return View(tutorType);
        }

        // POST: TutorTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TutorTypeId,TutorTypeDesc")] TutorType tutorType)
        {
            if (id != tutorType.TutorTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tutorType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TutorTypeExists(tutorType.TutorTypeId))
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
            return View(tutorType);
        }

        // GET: TutorTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutorType = await _context.TutorType
                .FirstOrDefaultAsync(m => m.TutorTypeId == id);
            if (tutorType == null)
            {
                return NotFound();
            }

            return View(tutorType);
        }

        // POST: TutorTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tutorType = await _context.TutorType.FindAsync(id);
            _context.TutorType.Remove(tutorType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TutorTypeExists(int id)
        {
            return _context.TutorType.Any(e => e.TutorTypeId == id);
        }
    }
}
