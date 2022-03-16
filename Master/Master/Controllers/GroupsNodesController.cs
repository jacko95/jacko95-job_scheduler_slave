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
    public class GroupsNodesController : Controller
    {
        private readonly TasksContext _context;

        public GroupsNodesController(TasksContext context)
        {
            _context = context;
        }

        //[Authorize]
        // GET: GroupsNodes
        public async Task<IActionResult> Index()
        {
            var tasksContext = _context.GroupsNodes.Include(g => g.Group).Include(g => g.Node);
            return View(await tasksContext.ToListAsync());
        }

        // GET: GroupsNodes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupsNodes = await _context.GroupsNodes
                .Include(g => g.Group)
                .Include(g => g.Node)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupsNodes == null)
            {
                return NotFound();
            }

            return View(groupsNodes);
        }

        // GET: GroupsNodes/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name");
            ViewData["NodeId"] = new SelectList(_context.Nodes, "Id", "Host");
            return View();
        }

        // POST: GroupsNodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NodeId,GroupId")] GroupsNodes groupsNodes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groupsNodes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", groupsNodes.GroupId);
            ViewData["NodeId"] = new SelectList(_context.Nodes, "Id", "Host", groupsNodes.NodeId);
            return View(groupsNodes);
        }

        // GET: GroupsNodes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupsNodes = await _context.GroupsNodes.FindAsync(id);
            if (groupsNodes == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", groupsNodes.GroupId);
            ViewData["NodeId"] = new SelectList(_context.Nodes, "Id", "Host", groupsNodes.NodeId);
            return View(groupsNodes);
        }

        // POST: GroupsNodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,NodeId,GroupId")] GroupsNodes groupsNodes)
        {
            if (id != groupsNodes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupsNodes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupsNodesExists(groupsNodes.Id))
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
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", groupsNodes.GroupId);
            ViewData["NodeId"] = new SelectList(_context.Nodes, "Id", "Host", groupsNodes.NodeId);
            return View(groupsNodes);
        }

        // GET: GroupsNodes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupsNodes = await _context.GroupsNodes
                .Include(g => g.Group)
                .Include(g => g.Node)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupsNodes == null)
            {
                return NotFound();
            }

            return View(groupsNodes);
        }

        // POST: GroupsNodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var groupsNodes = await _context.GroupsNodes.FindAsync(id);
            _context.GroupsNodes.Remove(groupsNodes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupsNodesExists(long id)
        {
            return _context.GroupsNodes.Any(e => e.Id == id);
        }
    }
}
