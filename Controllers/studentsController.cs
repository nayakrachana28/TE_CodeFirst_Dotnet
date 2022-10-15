using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using TE_CodeFirst_Dotnet.Data;
using TE_CodeFirst_Dotnet.Models;
//EncryptDecrypt
using TE_CodeFirst_Dotnet.Utilities;

namespace TE_CodeFirst_Dotnet.Controllers
{
    public class studentsController : Controller
    {
        //NToastNotify
        private readonly ILogger<studentsController> _logger;
        private readonly IToastNotification _toastNotification;
        private readonly studentDbContext _context;

        public studentsController(ILogger<studentsController> logger, IToastNotification toastNotification, studentDbContext context)
        {
            _context = context;
            _toastNotification = toastNotification;
            _logger = logger;
        }

        // GET: students
        public async Task<IActionResult> Index()
        {
              return View(await _context.student.ToListAsync());
        }

        // GET: students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.student == null)
            {
                return NotFound();
            }

            var students = await _context.student
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }

        // GET: students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,StudentName,StudentAge,StudentMobile,StudentEmail,StudentPassword,StudentConfirmPassword")] students students)
        {
            if (ModelState.IsValid)
            {
                //EncryptDecrypt
                students.StudentPassword= EncryptDecrypt.Encrypt(students.StudentPassword);

                _context.Add(students);
                await _context.SaveChangesAsync();
                //toastNotification in green color
                _toastNotification.AddSuccessToastMessage("Student Information Added Successfully");
                return RedirectToAction(nameof(Index));
            }
            return View(students);
        }

        // GET: students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.student == null)
            {
                return NotFound();
            }

            var students = await _context.student.FindAsync(id);
            if (students == null)
            {
                return NotFound();
            }
            return View(students);
        }

        // POST: students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,StudentName,StudentAge,StudentMobile,StudentEmail,StudentPassword,StudentConfirmPassword")] students students)
        {
            if (id != students.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //EncryptDecrypt
                    students.StudentPassword = EncryptDecrypt.Encrypt(students.StudentPassword);

                    _context.Update(students);
                    await _context.SaveChangesAsync();
                    //toastNotification in yellow color
                    _toastNotification.AddWarningToastMessage("Student Information Updated Successfully");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!studentsExists(students.StudentId))
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
            return View(students);
        }

        // GET: students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.student == null)
            {
                return NotFound();
            }

            var students = await _context.student
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }

        // POST: students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.student == null)
            {
                return Problem("Entity set 'studentDbContext.student'  is null.");
            }
            var students = await _context.student.FindAsync(id);
            if (students != null)
            {
                _context.student.Remove(students);
            }
            
            await _context.SaveChangesAsync();
            //toastNotification in red color
            _toastNotification.AddErrorToastMessage("Student Information Deleted Successfully");
            return RedirectToAction(nameof(Index));
        }

        private bool studentsExists(int id)
        {
          return _context.student.Any(e => e.StudentId == id);
        }
    }
}
