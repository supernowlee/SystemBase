using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemBase.Repository;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;
using SystemBase.Repository.Interfaces;
using SystemBase.Repository.Models;
using SystemBase.Repository.Repositories;
using SystemBase.Service.Interfaces;
using SystemBase.Service.Services;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace SystemBase.Web
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
            services.AddControllersWithViews();

            // Di 注入
            services.AddMvc();

            services.AddDbContext<StaffContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IRepository<Staff, int>, StaffRepository>();
            services.AddScoped<IStaffService, StaffService>();
        }

        public void Configure(IApplicationBuilder app, StaffContext dbContext)
        {
            // 建立資料庫            
            dbContext.Database.EnsureCreated();
            app.UseMvcWithDefaultRoute();

            // 設定錯誤處理
            app.UseExceptionHandler(new ExceptionHandlerOptions()
            {
                ExceptionHandler = async context =>
                {
                    bool isApi = Regex.IsMatch(context.Request.Path.Value, "^/api/", RegexOptions.IgnoreCase);
                    if (isApi)
                    {
                        context.Response.ContentType = "application/json";
                        var json = @"{ ""Message"": ""Internal Server Error"" }";
                        await context.Response.WriteAsync(json);
                        return;
                    }
                    context.Response.Redirect("/error");
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
