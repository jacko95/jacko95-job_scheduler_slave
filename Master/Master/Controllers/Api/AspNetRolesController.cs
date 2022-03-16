using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Master.ViewModels;
using Master.Infrastructure;
using Master.Models;

namespace Master.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class AspNetRolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole/*SiteUser*/> _roleManager;

        public AspNetRolesController(RoleManager<IdentityRole/*SiteUser*/> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<ActionResult<QueryResponse<IdentityRole/*SiteUser*/>>> Get([FromQuery] RoleQuery query = null)
        {
            return await _roleManager.Roles.ToQueryResponse(query);
        }

        [HttpPost]
        public async Task<ActionResult<IdentityRole/*SiteUser*/>> Post([FromBody] IdentityRole/*SiteUser*/ role)
        {
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return role;
            }
            else
            {
                throw Error.Identity(result.Errors);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IdentityRole/*SiteUser*/>> Put(string id, [FromBody] IdentityRole/*SiteUser*/ role)
        {
            var r = await _roleManager.FindByIdAsync(id);
            if (r == null) return NotFound();

            //r.Name = role.Name;
            var result = await _roleManager.UpdateAsync(r);

            if (result.Succeeded)
            {
                return r;
            }
            else
            {
                throw Error.Identity(result.Errors);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IdentityRole/*SiteUser*/>> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return role;
            }
            else
            {
                throw Error.Identity(result.Errors);
            }
        }
    }
}
