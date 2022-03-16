using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Master.Models;

namespace Master.Controllers
{
    [Authorize]
    //[Authorize(Roles = "Admin")]
    public class LogsController : Controller
    {
        private readonly TasksContext _context;

        public LogsController(TasksContext context)
        {
            _context = context;
        }

        //[Authorize]
        // GET: Logs
        public async Task<IActionResult> Index()
        {
            var tasksContext = _context.Logs.Include(l => l.Job).OrderByDescending(l => l.Id);
            return View(await tasksContext.ToListAsync());
        }

        // GET: Logs/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logs = await _context.Logs
                .Include(l => l.Job)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logs == null)
            {
                return NotFound();
            }

            return View(logs);
        }

        // GET: Logs/Create
        public IActionResult Create()
        {
            ViewData["JobId"] = new SelectList(_context.Jobs, "Id", "Command");
            return View();
        }

        // POST: Logs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JobId,Timestamp,Output,ExitCode,Pid")] Logs logs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(logs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobId"] = new SelectList(_context.Jobs, "Id", "Command", logs.JobId);
            return View(logs);
        }

        // GET: Logs/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logs = await _context.Logs.FindAsync(id);
            if (logs == null)
            {
                return NotFound();
            }
            ViewData["JobId"] = new SelectList(_context.Jobs, "Id", "Command", logs.JobId);
            return View(logs);
        }

        // POST: Logs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,JobId,Timestamp,Output,ExitCode,Pid")] Logs logs)
        {
            if (id != logs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(logs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LogsExists(logs.Id))
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
            ViewData["JobId"] = new SelectList(_context.Jobs, "Id", "Command", logs.JobId);
            return View(logs);
        }

        // GET: Logs/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logs = await _context.Logs
                .Include(l => l.Job)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logs == null)
            {
                return NotFound();
            }

            return View(logs);
        }

        // POST: Logs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var logs = await _context.Logs.FindAsync(id);
            _context.Logs.Remove(logs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LogsExists(long id)
        {
            return _context.Logs.Any(e => e.Id == id);
        }
    }
}
