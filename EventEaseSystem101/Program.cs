using EventEaseSystem.Services;
using Microsoft.EntityFrameworkCore;
using EventEaseSystem.Data;

namespace EventEaseSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddSingleton<BlobService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            using(var scope = app.Services.CreateScope())
{
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.EnsureCreated();
            }


            app.Run();
        }
    }
}
// References 
/* Troelsen, A. and Japikse, P. (2021). Pro C# 9 with .NET 5: 
     Foundational Principles and Practices in Programming. 10th ed. 
     Berkeley, CA: Apress.

 * Microsoft Corporation. (2026). ASP.NET Core MVC Overview.
  Available at: https://learn.microsoft.com/en-us/aspnet/core/mvc/overview
  (Accessed: 13 April 2026)

* Online Video.(ASP.NET Core MVC 2022 -4 controllers). Teddy Smith
https://youtu.be/709kkA8v_WA?si=Lzf-9NOWOyQZOCbc
(Accessed: 13 April 2026)

*Online Video.(Tips for working with HTML in Visual Studio). MS Visual Studio
https://youtu.be/i5eewBXEPRc?si=9_8-TY2mNbcg2cDS
(Accessed: 13 April 2026)

*YouTube Video.(Implementing Search Functionality in ASP.NET MVC)
https://youtu.be/uE9nXpPNzBE?si=5cnm8wDms12gMcSP
(Accessed: 07 May 2026)
*/
