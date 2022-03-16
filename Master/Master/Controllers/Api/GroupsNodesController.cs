using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Master.Models;

namespace Master.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsNodesController : ControllerBase
    {
        private readonly TasksContext _context;

        public GroupsNodesController(TasksContext context)
        {
            _context = context;
        }

        // GET: api/GroupsNodes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupsNodes>>> GetGroupsNodes()
        {
            return await _context.GroupsNodes.ToListAsync();
        }

        // GET: api/GroupsNodes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupsNodes>> GetGroupsNodes(long id)
        {
            var groupsNodes = await _context.GroupsNodes.FindAsync(id);

            if (groupsNodes == null)
            {
                return NotFound();
            }

            return groupsNodes;
        }

        // PUT: api/GroupsNodes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroupsNodes(long id, GroupsNodes groupsNodes)
        {
            if (id != groupsNodes.Id)
            {
                return BadRequest();
            }

            _context.Entry(groupsNodes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupsNodesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GroupsNodes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<GroupsNodes>> PostGroupsNodes(GroupsNodes groupsNodes)
        {
            _context.GroupsNodes.Add(groupsNodes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroupsNodes", new { id = groupsNodes.Id }, groupsNodes);
        }

        // DELETE: api/GroupsNodes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GroupsNodes>> DeleteGroupsNodes(long id)
        {
            var groupsNodes = await _context.GroupsNodes.FindAsync(id);
            if (groupsNodes == null)
            {
                return NotFound();
            }

            _context.GroupsNodes.Remove(groupsNodes);
            await _context.SaveChangesAsync();

            return groupsNodes;
        }

        private bool GroupsNodesExists(long id)
        {
            return _context.GroupsNodes.Any(e => e.Id == id);
        }
    }
}
