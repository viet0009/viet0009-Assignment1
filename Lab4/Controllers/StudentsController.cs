using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab4.Data;
using Lab4.Models;
using Lab4.ViewModels;

namespace Lab4.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolCommunityContext _context;

        public StudentsController(SchoolCommunityContext context)
        {
            _context = context;
        }

        // GET: Students
        public ActionResult Index(int? id)
        {
            return View(new StudentsViewModel
            {
                Students = _context.Students.ToList(),
                Selected = id.HasValue ? _context.Students.Include(s => s.Membership)
                .ThenInclude(m => m.Community)
                .Where(s => s.ID == id.Value)
                .First() : null
            });
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstName,EnrollmentDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstName,EnrollmentDate")] Student student)
        {
            if (id != student.ID)
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
                    if (!StudentExists(student.ID))
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
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.ID == id);
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
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
                public ActionResult EditMemberships(int id)
        {
            var student = _context.Students.Include(s => s.Membership).ThenInclude(m => m.Community).Where(c => c.ID == id).First();
            var communities = _context.Communities.OrderBy(n => n.Title).ToList();
            return View(new StudentMembershipsViewModel
            {
                Student = student,
                Communities = communities
            });
        }

        
        [HttpGet]
        public ActionResult AddMemberships(CommunityMembership membership)
        {
            _context.CommunityMemberships.Add(membership);
            _context.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }

       
        [HttpGet]
        public ActionResult RemoveMemberships(CommunityMembership membership)
        {
            _context.CommunityMemberships.Remove(membership);
            _context.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.ID == id);
        }
    }
}
