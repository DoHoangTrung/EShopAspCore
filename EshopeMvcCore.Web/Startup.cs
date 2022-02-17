using EshopAspCore.ApiIntegration;
using EshopAspCore.Application.Utilities.Slides;
using EshopAspCore.Utilities.Constants;
using EshopAspCore.ViewModels.System.Users;
using EshopeMvcCore.Web.LocalizationResources;
using FluentValidation.AspNetCore;
using LazZiya.ExpressLocalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EshopeMvcCore.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }
        public IWebHostEnvironment Env { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //bypass-invalid-ssl-certificate (when deploy to iss)
            services.AddHttpClient(SystemConstants.AppSettings.HttpClientWithSSLUntrusted)
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    ServerCertificateCustomValidationCallback =
                    (httpRequestMessage, cert, cetChain, policyErrors) =>
                    {
                        return true;
                    }
                });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IUserApiClient, UserApiClient>();
            services.AddTransient<IRoleApiClient, RoleApiClient>();
            services.AddTransient<ISlideApiClient, SlideApiClient>();
            services.AddTransient<IProductApiClient, ProductApiClient>();
            services.AddTransient<ICategoryApiClient, CategoryApiClient>();
            services.AddTransient<IOrderApiClient, OrderApiClient>();

            var cultures = new[]
            {
                new CultureInfo("vi"),
                new CultureInfo("en"),
            };
            services
                .AddControllersWithViews()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>()) //register all validator file in same url with login request validator
                .AddExpressLocalization<ExpressLocalizationResource, ViewLocalizationResource>(ops =>
            {
                // When using all the culture providers, the localization process will
                // check all available culture providers in order to detect the request culture.
                // If the request culture is found it will stop checking and do localization accordingly.
                // If the request culture is not found it will check the next provider by order.
                // If no culture is detected the default culture will be used.

                // Checking order for request culture:
                // 1) RouteSegmentCultureProvider
                //      e.g. http://localhost:1234/tr
                // 2) QueryStringCultureProvider
                //      e.g. http://localhost:1234/?culture=tr
                // 3) CookieCultureProvider
                //      Determines the culture information for a request via the value of a cookie.
                // 4) AcceptedLanguageHeaderRequestCultureProvider
                //      Determines the culture information for a request via the value of the Accept-Language header.
                //      See the browsers language settings

                // Uncomment and set to true to use only route culture provider
                ops.UseAllCultureProviders = false;
                ops.ResourcesPath = "LocalizationResources";
                ops.RequestLocalizationOptions = o =>
                {
                    o.SupportedCultures = cultures;
                    o.SupportedUICultures = cultures;
                    o.DefaultRequestCulture = new RequestCulture("vi");
                };
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = false;
                //options.Cookie.IsEssential = true;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option =>
                {
                    option.LoginPath = "/User/login";
                    option.AccessDeniedPath = "/User/Forbidden";
                });


            //razor runtime compilation
            var builder = services.AddControllersWithViews();
            if (Env.IsDevelopment() || Env.IsProduction())
            {
                builder.AddRazorRuntimeCompilation();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction())
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
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();

            app.UseRequestLocalization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "product en",
                    pattern: "{culture}/product/{id?}",
                    new
                    {
                        Controller = "Product",
                        Action = "Details"
                    });

                endpoints.MapControllerRoute(
                    name: "product vi",
                    pattern: "{culture}/san-pham/{id?}",
                    new
                    {
                        Controller = "Product",
                        Action = "Details"
                    });

                endpoints.MapControllerRoute(
                    name: "category en",
                    pattern: "{culture}/category/{id?}",
                    new
                    {
                        Controller = "Product",
                        Action = "Category"
                    });

                endpoints.MapControllerRoute(
                    name: "category vi",
                    pattern: "{culture}/danh-muc/{id?}",
                    new
                    {
                        Controller = "Product",
                        Action = "Category"
                    });

                endpoints.MapControllerRoute(
                    name: "category vi",
                    pattern: "{culture}/login",
                    new
                    {
                        Controller = "User",
                        Action = "Login"
                    });

                endpoints.MapControllerRoute(
                    name: "category vi",
                    pattern: "{culture}/dang-nhap",
                    new
                    {
                        Controller = "User",
                        Action = "Login"
                    });
                endpoints.MapControllerRoute(
                    name: "search vi",
                    pattern: "{culture}/tim-kiem",
                    new
                    {
                        Controller = "Product",
                        Action = "Search"
                    });
                endpoints.MapControllerRoute(
                    name: "search en",
                    pattern: "{culture}/search",
                    new
                    {
                        Controller = "Product",
                        Action = "Search"
                    });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{culture=vi}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
