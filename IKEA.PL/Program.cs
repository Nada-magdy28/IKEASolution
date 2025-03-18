using IKEA.DAL.persistance.Data;
using IKEA.DAL.persistance.Reposatrios.Departments;
using IKEA.PL.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using IKEA.BLL.Services.DepartmentServices;

namespace IKEA.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Configure services
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentServices,DepartmentServices>();


            //builder.Services.AddScoped<ApplictionDbContext>();
            //builder.Services.AddScoped<DbContextOptions<ApplictionDbContext>>((service) =>
            //{
            //    var optionsBuilder = new DbContextOptionsBuilder<ApplictionDbContext>();
            //    optionsBuilder.UseSqlServer("Server=.;Database=IKEA;Trusted_Connection=True;TrustServerCertificate=True");
            //    var options = optionsBuilder.Options;
            //    return options;
            //});

            #endregion


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            #region Configure pipelines (middleware)
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

           

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            #endregion

            app.Run();
        }
    }
}
