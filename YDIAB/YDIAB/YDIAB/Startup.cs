using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using YDIAB.Data;
using YDIAB.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using YDIAB.Models;
using System.Text;

namespace YDIAB
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        // I have made this a small case from big case, dont know if this is important.
        public IConfiguration configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddIdentity<StoreUser, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddAuthentication()
                    .AddCookie()
                    .AddJwtBearer(cfg =>
                    {
                        cfg.TokenValidationParameters = new TokenValidationParameters()
                        {
                            NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name",
                            ValidIssuer = _config["Tokens:Issuer"],
                            ValidAudience = _config["Tokens:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]))
                        };
                    });

            services.AddDbContext<AppDbContext>(cfg =>
            {
                cfg.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
            });

            
            services.AddScoped<IListRepository, ListRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<ITagRepository, TagRepository>();

            services.AddAutoMapper(typeof(Startup));

            // the request, hereunder the session
            services.AddHttpContextAccessor();
            // add session support, remember to also add middleware support
            //services.AddSession();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } else
            {
                //app.UseExceptionHandler("/Lists");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            app.UseHttpsRedirection();

            // my app will serve static files, fx index.html, by default it will search the ..root folder, 
            app.UseStaticFiles();
            //app.UseSession();
            //app.UseCookiePolicy();

            // requires install of nuget packet: usenodemodules
            //app.UseNodeModules();

            //must be before routing
            app.UseAuthentication();           
            // should be between routing and endpoints.

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
