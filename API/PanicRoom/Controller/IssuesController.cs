using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PanicRoom.DAL;
using PanicRoom.Entities;
using System.Threading.Tasks;
using System.Linq;

namespace PanicRoom.Controllers
{
    // API and MVC functionality combined
    public class IssuesController : Controller
    {
        private readonly PanicRoomDbContext _context;

        public IssuesController(PanicRoomDbContext context)
        {
            _context = context;
        }

        // GET: api/issues (API Route)
        [HttpGet("api/issues")]
        public async Task<IActionResult> GetIssues()
        {
            var issues = await _context.Issues.ToListAsync();
            return Ok(issues);  // API response
        }

        // GET: api/issues/{id} (API Route)
        [HttpGet("api/issues/{id}")]
        public async Task<IActionResult> GetIssue(int id)
        {
            var issue = await _context.Issues.FindAsync(id);
            if (issue == null)
                return NotFound();
            return Ok(issue);  // API response
        }

        // GET: Issues (MVC Route)
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var issues = await _context.Issues.ToListAsync();
            return View(issues);  // Returns the view to render the list of issues
        }

        // GET: Issues/Create (MVC Route)
        [HttpGet]
        public IActionResult Create()
        {
            return View();  // Return the view for creating an issue
        }

        // POST: Issues/Create (MVC Route)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Issue issue)
        {
            if (ModelState.IsValid)
            {
                issue.Created = DateTime.UtcNow;
                issue.Updated = DateTime.UtcNow;
                _context.Issues.Add(issue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List)); // Redirect to the list of issues after successful creation
            }
            return View(issue); // Return the create view with validation errors
        }

        // GET: Issues/Edit/{id} (MVC Route)
        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var issue = await _context.Issues.FindAsync(id);
            if (issue == null)
                return NotFound();
            return View(issue); // Return the view for editing the issue
        }

        // POST: Issues/Edit/{id} (MVC Route)
        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Issue issue)
        {
            if (id != issue.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                issue.Updated = DateTime.UtcNow;
                _context.Entry(issue).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List)); // Redirect to list after editing
            }
            return View(issue); // Return to the edit view with validation errors
        }

        // POST: Issues/Delete/{id} (MVC Route)
        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var issue = await _context.Issues.FindAsync(id);
            if (issue == null)
                return NotFound();

            _context.Issues.Remove(issue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List)); // Redirect to the list of issues after deleting
        }
    }
}
