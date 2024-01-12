using Online_Shopping_Application.API.Repository.Interface;
using Online_Shopping_Application.API.Services;
using Online_Shopping_Application.Response;

namespace Online_Shopping_Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x => x.LoginPath = "/UserLogin/Login");
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("Jwt"));
            builder.Services.AddHttpNamedClients(builder.Configuration);
            builder.Services.AddTransient<HttpAPIWrapper>();
            builder.Services.AddScoped<JWTResponse>();
           builder.Services.AddTransient<JWTServices>();
    
            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
            name: "admin",
        pattern: "{area=Admin}/{controller=AdminLogin}/{action=SignIn}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}