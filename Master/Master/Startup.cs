using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Master.Data;
using Master.Models;

namespace Master
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddControllersWithViews();

            //services.AddDbContext<TasksContext>(options => 
            //options.UseSqlite(Configuration.GetConnectionString("Default")));
            services.AddDbContext<TasksContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("Default"));
                options.EnableSensitiveDataLogging();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Northwind API", Version = "v1" });
            });

            services.AddIdentity<SiteUser, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<TasksContext>()
            .AddDefaultUI()
            .AddEntityFrameworkStores<TasksContext>()
            .AddDefaultTokenProviders();

            services.AddDbContext<TasksContext>(options =>
            {
                options.EnableSensitiveDataLogging();
            });

            services.AddAuthentication()
                .AddCookie()
                .AddJwtBearer(config =>
                {
                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

            services.AddScoped<TasksDataSeed>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())//Se siamo nell'ambiente di sviluppo (e non produzione)
            {
                app.UseDeveloperExceptionPage();//ci vengono mostrate le eccezioni in una pagina di dettaglio
            }
            else//altrimenti
            {
                app.UseExceptionHandler("/Home/Error");//mostra questa pagina
                app.UseHsts();
            }

            //Componenti di middleware, es:app.UseStaticFiles(), app.UseAuthentication();, ...

            //app.UseHttpsRedirection();//redirect automatico sull'equivalente pagina in https
            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Northwind API V1");
            });

            app.UseAuthentication();
            app.UseRouting();//per poter definire delle regole di routing
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    //id?: id può essere opzionale;action=Index: action se non specificata assume il valore index
                    pattern: "{controller=Jobs}/{action=Index}/{id?}");//regola di routing universale
                endpoints.MapRazorPages();
            });

            using var scope = app.ApplicationServices.CreateScope();
            var seed = scope.ServiceProvider.GetService<TasksDataSeed>();
            seed.SeedAsync().Wait();
        }
    }
}
