using Dz_identity.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Dz_identity.Service;

namespace Dz_identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Добавляем DbContext с использованием строки подключения
            builder.Services.AddDbContext<AplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Добавляем службу Identity для управления пользователями
            builder.Services.AddIdentity<IdentityUser, IdentityRole>() // Updated to AddIdentity
                .AddEntityFrameworkStores<AplicationDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddScoped<ProjectService>();
            builder.Services.AddScoped<TaskService>();

            // Добавляем службы для контроллеров с представлениями
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Настройка конвейера обработки HTTP-запросов
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Добавляем аутентификацию и авторизацию
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
