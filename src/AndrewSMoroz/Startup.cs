using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AndrewSMoroz.Data;
using AndrewSMoroz.Models;
using AndrewSMoroz.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;

namespace AndrewSMoroz
{

    public class Startup
    {

        //--------------------------------------------------------------------------------------------------------------
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        //--------------------------------------------------------------------------------------------------------------
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // Add framework services.
            services.AddDbContext<ContactsDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<ExploreDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();         // Uses cookies to implement the TempData controller dictionary, which lasts for one additional request
                                                                                        // Without this, TempData would use (and require) Session


            // Enable session
            // Also requires call to app.UseSession() in Configure method
            // Class SessionExtensions is also very helpful
            services.AddDistributedMemoryCache();                                       // Adds a default in-memory implementation of IDistributedCache.
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.CookieHttpOnly = true;
            });

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });

            services.AddOptions();                                                                      // Adds ability to inject IOptions<T>
            services.Configure<ContactsUISettings>(Configuration.GetSection("ContactsUISettings"));     // Registers ContactsUISettings object so it can be injected
            services.Configure<ExploreUISettings>(Configuration.GetSection("ExploreUISettings"));       // Registers ExploreUISettings object so it can be injected

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddTransient<IBusinessServices, BusinessServices>();
            services.AddTransient<IModelAdapter, ModelAdapter>();
            services.AddTransient<IUserContext, UserContext>();
            services.AddTransient<IExploreBusinessManager, ExploreBusinessManager>();
            services.AddTransient<IExploreDTOAdapter, ExploreDTOAdapter>();

        }

        //--------------------------------------------------------------------------------------------------------------
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, 
                              ContactsDbContext contactsDbContext, IOptions<ContactsUISettings> contactsUISettings,
                              ExploreDbContext exploreDbContext, IOptions<ExploreUISettings> exploreUISettings)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            if (contactsUISettings.Value.InitializeDatabase)
            {
                ContactsDbInitializer.Initialize(contactsDbContext);
            }

            if (exploreUISettings.Value.InitializeDatabase)
            {
                ExploreDbInitializer.Initialize(exploreDbContext);
            }

        }

    }

}
