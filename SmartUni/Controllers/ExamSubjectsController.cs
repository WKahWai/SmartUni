using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartUni.Models;
using SmartUni.Helpers;
using System.Data.SqlClient;

namespace SmartUni.Controllers
{
    public class ExamSubjectsController : Controller
    {
        private readonly SmartUniContext _context;

        public ExamSubjectsController(SmartUniContext context)
        {
            _context = context;
        }

        [HttpGet]
        // GET: ExamSubjects
        public async Task<IActionResult> Index([FromQuery(Name = "examId")]int examId, [FromQuery(Name = "classId")]int classId, [FromQuery(Name = "subjectId")]int subjectId)
        {
            ViewData["ExamList"] = SetSelected.SetSelectedValue(new SelectList(_context.Exam, "ExamId", "ExamDesc"), examId.ToString());
            ViewData["ClassList"] = SetSelected.SetSelectedValue(new SelectList(_context.Class, "ClassId", "ClassDesc"), classId.ToString());

            var p_examId = new SqlParameter("@p0", examId);
            var p_classId = new SqlParameter("@p1", classId);

            if (classId != 0)
            {
                var subjectList = await _context.SubjectList.FromSql("EXEC GetSubjectListByExamIdClassId @p0, @p1", p_examId, p_classId).ToListAsync();
                ViewData["SubjectList"] = SetSelected.SetSelectedValue(new SelectList(subjectList, "SubjectId", "SubjectName"), subjectId.ToString());

                if (subjectId > 0)
                {
                    var p_subjectId = new SqlParameter("@p2", subjectId);
                    var studentList = await _context.ExamStudentList.FromSql("EXEC GetStudentListByExamIdClassIdSubjectId @p0, @p1, @p2", p_examId, p_classId, p_subjectId).ToListAsync();

                    ViewData["StudentList"] = studentList;
                }
            }

            return View();
        }

        // POST: ExamSubjects
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit()
        {
            var smartUniContext = _context.ExamSubject.Include(e => e.Exam).Include(e => e.StudSubject);
            return View(await smartUniContext.ToListAsync());
        }

        // GET: ExamSubjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Errors");
            }

            var examSubject = await _context.ExamSubject
                .Include(e => e.Exam)
                .Include(e => e.StudSubject)
                .FirstOrDefaultAsync(m => m.ExamId == id);
            if (examSubject == null)
            {
                return RedirectToAction("Index", "Errors");
            }

            return View(examSubject);
        }

        // GET: ExamSubjects/Create
        public async Task<IActionResult> Create(int id, [FromQuery(Name = "selectedId")] int selectedId)
        {
            ViewData["ExamId"] = id;
            var examList = new SelectList(_context.Exam, "ExamId", "ExamDesc");
            ViewData["ExamList"] = SetSelected.SetSelectedValue(examList, id.ToString());
            var subjectList = new SelectList(_context.Subject, "SubjectId", "SubjectName");
            ViewData["Subject"] = SetSelected.SetSelectedValue(subjectList, selectedId.ToString());

            var studentSubject = await _context.StudentSubject.Include(s => s.Student).Where(s => s.SubjectId == selectedId).ToListAsync();

            PopulateStudentSubjectList(id, studentSubject);
            return View();
        }

        private void PopulateStudentSubjectList(int examId, List<StudentSubject> studentSubject)
        {
            var viewModel = new List<SelectedStudentList>();
            var examList = new HashSet<int>(_context.ExamSubject.Where(e => e.ExamId == examId).Select(e => e.StudSubjectId));
            foreach( var item in studentSubject)
            {
                if (!examList.Contains(item.StudSubjectId))
                {
                    viewModel.Add(new SelectedStudentList
                    {
                        StudId = item.StudId,
                        StudName = item.Student.StudName,
                        ClassId = item.Student.ClassId,
                        StudSubjectId = item.StudSubjectId,
                        Selected = false,
                    }); ;
                }
            }
            ViewData["StudentSubjectList"] = viewModel;
        }

        // POST: ExamSubjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int examId, int[] selectedStudent)
        {
            if (examId != 0 && selectedStudent != null)
            {
                foreach( var item in selectedStudent )
                {
                    _context.Add(new ExamSubject
                    {
                        ExamId = examId,
                        StudSubjectId = item,
                    });
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var examSubject = new ExamSubject();
            ViewData["ExamId"] = new SelectList(_context.Exam, "ExamId", "ExamDesc", examSubject.ExamId);
            ViewData["StudSubjectId"] = new SelectList(_context.StudentSubject, "StudSubjectId", "StudId", examSubject.StudSubjectId);
            return View(examSubject);
        }

        // POST: ExamSubjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModifyMark([FromForm] ExamSubjectList examSubjectList)
        {
            if (ModelState.IsValid)
            {
                foreach(var item in examSubjectList.ExamSubjectListing)
                {
                    if( item.Mark >= 0)
                    {
                        if (item.Mark > 100) item.Grade = "Error";
                        else if (item.Mark >= 90) item.Grade = "A+";
                        else if (item.Mark >= 80) item.Grade = "A";
                        else if (item.Mark >= 70) item.Grade = "B+";
                        else if (item.Mark >= 60) item.Grade = "B";
                        else if (item.Mark >= 50) item.Grade = "C+";
                        else if (item.Mark >= 30) item.Grade = "C";
                        else item.Grade = "F";
                        _context.Update(item);
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // GET: ExamSubjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Errors");
            }

            var examSubject = await _context.ExamSubject.FindAsync(id);
            if (examSubject == null)
            {
                return RedirectToAction("Index", "Errors");
            }
            ViewData["ExamId"] = new SelectList(_context.Exam, "ExamId", "ExamDesc", examSubject.ExamId);
            ViewData["StudSubjectId"] = new SelectList(_context.StudentSubject, "StudSubjectId", "StudId", examSubject.StudSubjectId);
            return View(examSubject);
        }

        // POST: ExamSubjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExamId,StudSubjectId,Mark,Grade")] ExamSubject examSubject)
        {
            if (id != examSubject.ExamId)
            {
                return RedirectToAction("Index", "Errors");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(examSubject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamSubjectExists(examSubject.ExamId))
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
            ViewData["ExamId"] = new SelectList(_context.Exam, "ExamId", "ExamDesc", examSubject.ExamId);
            ViewData["StudSubjectId"] = new SelectList(_context.StudentSubject, "StudSubjectId", "StudId", examSubject.StudSubjectId);
            return View(examSubject);
        }

        // GET: ExamSubjects/Delete/5
        public async Task<IActionResult> Delete(int? id, int studSubjetId)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Errors");
            }

            var examSubject = await _context.ExamSubject
                .Include(e => e.Exam)
                .Include(e => e.StudSubject)
                .FirstOrDefaultAsync(m => m.StudSubjectId ==  studSubjetId && m.ExamId == id);
            if (examSubject == null)
            {
                return RedirectToAction("Index", "Errors");
            }

            return View(examSubject);
        }

        // POST: ExamSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int ExamId, int StudSubjectId)
        {
            var examSubject = await _context.ExamSubject.FirstOrDefaultAsync(m => m.StudSubjectId == StudSubjectId && m.ExamId == ExamId);
            _context.ExamSubject.Remove(examSubject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamSubjectExists(int id)
        {
            return _context.ExamSubject.Any(e => e.ExamId == id);
        }
    }
}
