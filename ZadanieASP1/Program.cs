using ZadanieASP1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using ZadanieASP1.Repository;
using ZadanieASP1.Services;
using FluentValidation.AspNetCore;
using ZadanieASP1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using ZadanieASP1.Validators;

namespace ZadanieASP1
{
    public class Program
    {
        public class PasswordExpiryMiddleware
        {
            private readonly RequestDelegate _next;

            public PasswordExpiryMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            public async Task InvokeAsync(HttpContext context, UserManager<ApplicationUser> userManager)
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    var user = await userManager.GetUserAsync(context.User);
                    if (user != null && user.PasswordExpiryDate.HasValue && user.PasswordExpiryDate.Value < DateTime.UtcNow)
                    {
                        context.Response.Redirect("/Account/ChangePassword");
                        return;
                    }
                }

                await _next(context);
            }
        }
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

            builder.Services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("pl-PL")
                };

                options.DefaultRequestCulture = new RequestCulture("pl-PL");
                options.SupportedUICultures = supportedCultures;
            });

            builder.Services.AddControllersWithViews().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Validators.TripValidator>());
            builder.Services.AddControllersWithViews().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Validators.ReservationValidator>());
            builder.Services.AddDbContext<TravelAgencyContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => 
            { options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false; // Wy³¹cz wymóg co najmniej jednej ma³ej litery
                options.Password.RequireUppercase = false; // Wy³¹cz wymóg co najmniej jednej wielkiej litery
                options.Password.RequireNonAlphanumeric = false; // Wy³¹cz wymóg co najmniej jednego znaku specjalnego
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15); // Czas blokady po przekroczeniu limitu prób logowania
                options.Lockout.MaxFailedAccessAttempts = 5; // Liczba prób logowania przed blokad¹
                options.Lockout.AllowedForNewUsers = true; // W³¹czenie blokady dla nowych u¿ytkowników

            }).AddRoles<IdentityRole>()

                .AddEntityFrameworkStores<TravelAgencyContext>().AddPasswordValidator<CustomPasswordValidator<ApplicationUser>>(); ;

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOrManager", policy => policy.RequireRole("Admin", "Manager"));
            });

            builder.Services.AddRazorPages();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
            builder.Services.AddScoped<ITripRepository, TripRepository>();
            builder.Services.AddScoped<ITripService, TripService>();


            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<Reservation, ReservationViewModel>()
                    .ForMember(dest => dest.ReservationID, act => act.MapFrom(src => src.ReservationID))
                    .ForMember(dest => dest.Title, act => act.MapFrom(src => src.Trip.Title))
                    .ForMember(dest => dest.CustomerName, act => act.MapFrom(src => src.Customer.FirstName + " " + src.Customer.LastName))
                    .ForMember(dest => dest.ReservationDate, act => act.MapFrom(src => src.ReservationDate))
                    .ForMember(dest => dest.DateOfDeparture, act => act.MapFrom(src => src.DateOfDeparture))
                    .ForMember(dest => dest.DateOfReturn, act => act.MapFrom(src => src.DateOfReturn));
            });
            



            var app = builder.Build();
            app.UseRequestLocalization();

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

            // Place authentication and authorization here
            app.UseAuthentication();
            app.UseAuthorization();

            // Add custom middleware after authentication and authorization
            app.UseMiddleware<PasswordExpiryMiddleware>();

            app.MapRazorPages();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var roles = new[] { "Admin", "Manager", "Member" };

                foreach (var role in roles)
                {
                    if(!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            
            using(var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                string email = "adminTravelAgency@gmail.com";
                string password = "#ScisleT4jne";
                string userName = "ADMIN";
                string email2 = "managerTravelAgency@gmail.com";
                string password2 = "#RownieT4jne";


               if(await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new ApplicationUser();
                    user.UserName = userName;
                    user.Email = email;

                    await userManager.CreateAsync(user, password);

                    await userManager.AddToRoleAsync(user, "Admin");
                }
                
                if(await userManager.FindByEmailAsync(email2) == null)
                {
                    var user2 = new ApplicationUser();
                    user2.UserName = email2;
                    user2.Email = email2;

                    await userManager.CreateAsync(user2, password2);

                    await userManager.AddToRoleAsync(user2, "Manager");
                }

            }

            app.Run();
        }

    }
    
}
