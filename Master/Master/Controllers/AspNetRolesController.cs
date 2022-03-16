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
    public class AspNetRolesController : Controller
    {
        private readonly TasksContext _context;

        public AspNetRolesController(TasksContext context)
        {
            _context = context;
        }

        // GET: AspNetRoles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Roles.ToListAsync());
        }

        // GET: AspNetRoles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetRoles = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspNetRoles == null)
            {
                return NotFound();
            }

            return View(aspNetRoles);
        }

        // GET: AspNetRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AspNetRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NormalizedName,ConcurrencyStamp")] AspNetRoles aspNetRoles)
        {
            if (ModelState.IsValid)
            {
                AspNetRoles Roles = new AspNetRoles
                {
                    //Id = model.Id,
                    //Name = model.Name,
                    //NormalizedName = model.NormalizedName
                    //ConcurrencyStamp = model.ConcurrencyStamp
                };
                await _context.AddAsync(aspNetRoles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aspNetRoles);
        }

        // GET: AspNetRoles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetRoles = await _context.Roles.FindAsync(id);
            if (aspNetRoles == null)
            {
                return NotFound();
            }
            return View(aspNetRoles);
        }

        // POST: AspNetRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,NormalizedName,ConcurrencyStamp")] AspNetRoles aspNetRoles)
        {
            if (id != aspNetRoles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aspNetRoles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AspNetRolesExists(aspNetRoles.Id))
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
            return View(aspNetRoles);
        }

        // GET: AspNetRoles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetRoles = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspNetRoles == null)
            {
                return NotFound();
            }

            return View(aspNetRoles);
        }

        // POST: AspNetRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var aspNetRoles = await _context.Roles.FindAsync(id);
            _context.Roles.Remove(aspNetRoles);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AspNetRolesExists(string id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}
