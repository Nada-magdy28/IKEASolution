using IKEA.DAL.persistance.Data;
using IKEA.DAL.persistance.Reposatrios.Departments;
using IKEA.PL.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using IKEA.BLL.Services.DepartmentServices;
using IKEA.DAL.persistance.Reposatrios.Employees;
using IKEA.BLL.Services.EmployeeServices;
using IKEA.PL.Mapping;
using IKEA.DAL.persistance.UnitOfWork;
using IKEA.BLL.Common.Services.Attachments;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Identity;
using IKEA.DAL.Models.Identity;

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
                options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                
               
                options.Password.RequiredLength = 5;
                //options.Password.RequireDigit = true;
                //options.Password.RequireLowercase = true;
                //options.Password.RequireUppercase = true;
                //options.Password.RequireNonAlphanumeric = true;
                //options.Password.RequiredUniqueChars = 1;

                //options.User.RequireUniqueEmail = true;

                //options.Lockout.AllowedForNewUsers = true;
                //options.Lockout.MaxFailedAccessAttempts = 5;
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddAuthentication().AddCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Home/Erorr";
                options.ExpireTimeSpan = TimeSpan.FromDays(2);
                options.ForwardSignOut = "/Account/Logout";
            });

            // builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            // builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IDepartmentServices,DepartmentServices>();
            builder.Services.AddScoped<IEmployeeServices, EmployeeServices>();
            builder.Services.AddScoped<IAttachmentServices, AttachmentServices>();

            builder.Services.AddAutoMapper(M =>M.AddProfile(typeof(MappingProfile)));

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

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            #endregion

            app.Run();
        }
    }
}
