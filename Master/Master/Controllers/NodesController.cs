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
    [Authorize(Roles = "Admin")]
    public class NodesController : Controller
    {
        private readonly TasksContext _context;

        public NodesController(TasksContext context)
        {
            _context = context;
        }

        //[Authorize]
        // GET: Nodes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Nodes.ToListAsync());
        }

        // GET: Nodes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nodes = await _context.Nodes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nodes == null)
            {
                return NotFound();
            }

            return View(nodes);
        }

        // GET: Nodes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Host,Port,Master")] Nodes nodes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nodes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nodes);
        }

        // GET: Nodes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nodes = await _context.Nodes.FindAsync(id);
            if (nodes == null)
            {
                return NotFound();
            }
            return View(nodes);
        }

        // POST: Nodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Host,Port,Master")] Nodes nodes)
        {
            if (id != nodes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nodes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NodesExists(nodes.Id))
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
            return View(nodes);
        }

        // GET: Nodes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nodes = await _context.Nodes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nodes == null)
            {
                return NotFound();
            }

            return View(nodes);
        }

        // POST: Nodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var nodes = await _context.Nodes.FindAsync(id);
            _context.Nodes.Remove(nodes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NodesExists(long id)
        {
            return _context.Nodes.Any(e => e.Id == id);
        }
    }
}
