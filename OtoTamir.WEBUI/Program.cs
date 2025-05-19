using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OtoTamir.BLL.Abstract;
using OtoTamir.BLL.Concrete;
using OtoTamir.CORE.Identity;
using OtoTamir.CORE.Mapping;
using OtoTamir.DAL.Abstract;
using OtoTamir.DAL.Concrete.EfCore;
using OtoTamir.DAL.Context;

namespace OtoTamir.WEBUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Laptop")));

            builder.Services.AddIdentity<Mechanic, IdentityRole>()
                          .AddEntityFrameworkStores<DataContext>()
                          .AddDefaultTokenProviders();

            builder.Services.AddScoped<IMechanicService, MechanicService>();
            builder.Services.AddScoped<IMechanicDal, EfCoreMechanicDal>();

            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<IClientDal, EfCoreClientDal>();

            builder.Services.AddScoped<IVehicleDal, EfCoreVehicleDal>();
            builder.Services.AddScoped<IVehicleService, VehicleService>();
            
            builder.Services.AddScoped<IServiceRecordDal, EfCoreServiceRecordDal>();
            builder.Services.AddScoped<IServiceRecordService, ServiceRecordService>();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                //password
                options.Password.RequiredLength = 6; // min pass uzunlu�u               
                options.Password.RequireLowercase = false; // K���k harf zorunlulu�unu kapat
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Lockout.MaxFailedAccessAttempts = 5; //Max hatal� giri� say�s�
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Max hatal� giri� sonras� account kilitlenme s�resi
                options.Lockout.AllowedForNewUsers = true; //Her yeni account i�in uygula
            });
            //Configure Cookie
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromHours(48); //Oturum S�resi
                options.SlidingExpiration = true; // Her kullan�c� hareketinde oturum s�resini resetle
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = "OtoTamir.Security.Cookie",
                    SameSite = SameSiteMode.Strict //Oturumu serverdan kullan�c� browserina ta��d�k.
                };
            });
            builder.Services.AddAutoMapper(typeof(MappingProfile));

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
