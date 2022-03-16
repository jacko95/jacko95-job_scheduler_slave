using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Master.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Master.Data
{
    public class TasksDataSeed
    {
        private readonly UserManager<SiteUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        //private readonly IConfiguration _config;

        public TasksDataSeed(UserManager<SiteUser> userManager, RoleManager<IdentityRole> roleManager/*, IConfiguration config*/)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            //_config = config;
        }

        public async Task SeedAsync()
        {

            //Configuration["AppSettings:UserName"],

            //var config = serviceProvider.GetRequiredService<IConfiguration>();

            const string userName = /*"admin@northwind.com"*/"admin@northwind.com"/*"AppSettings:User Id"*/;
            const string password =  "Pa$$w0rd"/*"AppSettings:DefaultConnection:password"*/;
            //string password = _config.GetValue<string>("AppSettings:Password");

            var role = await _roleManager.FindByNameAsync("Admin");
            if (role == null)
            {
                role = new IdentityRole { Name = "Admin" };
                await _roleManager.CreateAsync(role);
            }

            var user = await _userManager.FindByEmailAsync(userName);
            if (user == null)
            {
                user = new SiteUser
                {
                    UserName = userName,
                    Email = userName,
                    FirstName = "John",
                    LastName = "Smith",
                };

                var result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded) throw new InvalidOperationException("Cannot create default user");
            }

            if (!await _userManager.IsInRoleAsync(user, "Admin"))
            {
                var result = await _userManager.AddToRoleAsync(user, "Admin");
                if (!result.Succeeded) throw new InvalidOperationException("Cannot add role Admin to default user");
            }
        }
    }
}
