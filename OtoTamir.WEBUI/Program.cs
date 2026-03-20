using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OtoTamir.BLL.Abstract;
using OtoTamir.BLL.Concrete;
using OtoTamir.BLL.Managers;
using OtoTamir.CORE.Identity;
using OtoTamir.CORE.Mapping;
using OtoTamir.DAL.Abstract;
using OtoTamir.DAL.Concrete.EfCore;
using OtoTamir.DAL.Context;
using OtoTamir.WEBUI.Services.Filters;
using Serilog;
using System.Globalization;


namespace OtoTamir.WEBUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information() 
                .WriteTo.Console() 
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) 
                .CreateLogger();

          
            builder.Host.UseSerilog();
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

            builder.Services.AddScoped<ISymptomDal, EfCoreSymptomDal>();
            builder.Services.AddScoped<ISymptomService, SymptomService>();

            builder.Services.AddScoped<ITransactionCategoryDal, EfCoreTransactionCategoryDal>();
            builder.Services.AddScoped<ITransactionCategoryService, TransactionCategoryService>();

            builder.Services.AddScoped<IBankCardDal, EfCoreBankCardDal>();
            builder.Services.AddScoped<IBankCardService, BankCardService>();
            
            builder.Services.AddScoped<IBankDal, EfCoreBankDal>();
            builder.Services.AddScoped<IBankService, BankService>();
            
            builder.Services.AddScoped<ITreasuryTransactionDal, EfCoreTreasuryTransactionDal>();
            builder.Services.AddScoped<ITreasuryTransactionService, TreasuryTransactionService>();
            
            builder.Services.AddScoped<ITreasuryDal, EfCoreTreasuryDal>();
            builder.Services.AddScoped<ITreasuryService, TreasuryService>();

            builder.Services.AddScoped<IPosTerminalDal,EfCorePosTerminalDal>();
            builder.Services.AddScoped<IPosTerminalService,PosTerminalService>();
            builder.Services.AddScoped<IAnnouncementDal, EfCoreAnnouncementDal>();
            builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
            builder.Services.AddScoped<IServiceProcessManager, ServiceProcessManager>();
            builder.Services.AddScoped<Services.MailHelper.IMailHelper, Services.MailHelper.MailHelper>();
            builder.Services.Configure<IdentityOptions>(options =>
            {
                //password
                options.Password.RequiredLength = 6; // min pass uzunluðu               
                options.Password.RequireLowercase = false; // Küçük harf zorunluluðunu kapat
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Lockout.MaxFailedAccessAttempts = 5; //Max hatalý giriþ sayýsý
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Max hatalý giriþ sonrasý account kilitlenme süresi
                options.Lockout.AllowedForNewUsers = true; //Her yeni account için uygula
            });
            //Configure Cookie
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromHours(48); //Oturum Süresi
                options.SlidingExpiration = true; // Her kullanýcý hareketinde oturum süresini resetle
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = "OtoTamir.Security.Cookie",
                    SameSite = SameSiteMode.Strict 
                };
            });
            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>(), typeof(MappingProfile).Assembly);
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllersWithViews(options =>
            {
                options.ModelBinderProviders.Insert(0, new Services.SmartDecimalModelBinderProvider());
                options.Filters.Add<SubscriptionCheckFilter>();
            });
            var app = builder.Build();

            var defaultDateCulture = "tr-TR";
            var ci = new CultureInfo(defaultDateCulture);

            ci.NumberFormat.NumberDecimalSeparator = ",";
            ci.NumberFormat.CurrencyDecimalSeparator = ",";

            // Uygulama genelinde Türkçe ayarlarýný tanýmla
            var supportedCultures = new[] { ci };
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(ci),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            app.UseRequestLocalization(localizationOptions);
            // --- BURADA BÝTÝR ---
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
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
