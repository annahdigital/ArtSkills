using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArtSkills.Data;
using ArtSkills.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace ArtSkills
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            var supportedCultures = new[]
{
                new CultureInfo("en"),
                new CultureInfo("fr"),
                new CultureInfo("ru"),
                new CultureInfo("be")
            };

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders = new List<IRequestCultureProvider>
        {
            new QueryStringRequestCultureProvider(),
            new CookieRequestCultureProvider()
        };
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(
                        Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddSignalR();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                 .AddViewLocalization()
                 .AddDataAnnotationsLocalization();         }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseRequestLocalization();

            app.UseSignalR(route =>
            {
                route.MapHub<NotificationHub>("/notifications");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "index", template: "/", defaults: new { controller = "Home", action = "Index" });
                routes.MapRoute(name: "privacy", template: "Home/Privacy", defaults: new { controller = "Home", action = "Privacy" });
                routes.MapRoute(name: "userWall", template: "UserWall/Index/{id?}", defaults: new { controller = "UserWall", action = "Index" });
                routes.MapRoute(name: "followers", template: "Followers/{id?}", defaults: new { controller = "UserWall", action = "Followers" });
                routes.MapRoute(name: "followUser", template: "Follow/{id?}", defaults: new { controller = "UserWall", action = "FollowUser" });
                routes.MapRoute(name: "unfollowUser", template: "Unfollow/{id?}", defaults: new { controller = "UserWall", action = "UnfollowUser" });
                routes.MapRoute(name: "aboutUser", template: "About/{id?}", defaults: new { controller = "UserWall", action = "About" });
                routes.MapRoute(name: "uploadUserPic", template: "UploadUserPic", defaults: new { controller = "UserWall", action = "UploadUserPic" });
                routes.MapRoute(name: "addArt", template: "AddArt", defaults: new { controller = "UserWall", action = "AddArt" });
                routes.MapRoute(name: "addTaskList", template: "AddTaskList", defaults: new { controller = "UserWall", action = "AddTaskList" });
                routes.MapRoute(name: "userTaskLists", template: "UserTaskLists/{id?}", defaults: new { controller = "UserWall", action = "TaskListIndex" });
                routes.MapRoute(name: "changeLanguage", template: "ChangeLanguage", defaults: new { controller = "Home", action = "SetLanguage" });
                routes.MapRoute(name: "administrationMenu", template: "administrationMenu", defaults: new { controller = "Administration", action = "Index" });
                routes.MapRoute(name: "editRoles", template: "administrationMenu/EditUserRoles/{id?}", defaults: new { controller = "Administration", action = "EditUserRoles" });
                routes.MapRoute(name: "art", template: "art/{id?}", defaults: new { controller = "Art", action = "Art" });
                routes.MapRoute(name: "artsCollection", template: "artsCollection", defaults: new { controller = "Art", action = "ArtsCollection" });
                routes.MapRoute(name: "deleteArt", template: "DeleteArt/{id?}", defaults: new { controller = "Art", action = "DeleteArt" });
                routes.MapRoute(name: "likeArt", template: "LikeArt/{id?}", defaults: new { controller = "Art", action = "LikeArt" });
                routes.MapRoute(name: "unlikeArt", template: "UnlikeArt/{id?}", defaults: new { controller = "Art", action = "UnlikeArt" });
                routes.MapRoute(name: "commentArt", template: "CommentArt/{id?}/{text?}", defaults: new { controller = "Art", action = "CommentArt" });
                routes.MapRoute(name: "comment", template: "Comment/{id?}", defaults: new { controller = "Comment", action = "Comment" });
                routes.MapRoute(name: "deleteComment", template: "DeleteComment/{artId?}/{commentId?}", defaults: new { controller = "Comment", action = "DeleteComment" });
                routes.MapRoute(name: "search", template: "Search/{name:maxlength(25)?}", defaults: new { controller = "Search", action = "ArtistSearch" });
                routes.MapRoute(name: "taskList", template: "TaskList/{id?}", defaults: new { controller = "TaskList", action = "TaskList" });
                routes.MapRoute(name: "editTasklist", template: "EditTaskList/{id?}", defaults: new { controller = "TaskList", action = "EditTaskList" });
                routes.MapRoute(name: "taskLists", template: "TaskLists/{id?}/{completed:bool?}", defaults: new { controller = "TaskList", action = "TaskListsCollection" });
                routes.MapRoute(name: "completed", template: "Completed/{id?}", defaults: new { controller = "TaskList", action = "Completed" });
                routes.MapRoute(name: "inProgress", template: "InProgress/{id?}", defaults: new { controller = "TaskList", action = "InProgress" });
                routes.MapRoute(name: "deleteTaskList", template: "DeleteTaskList/{id?}", defaults: new { controller = "TaskList", action = "DeleteTaskList" });
                routes.MapRoute(name: "addTask", template: "AddTask/{id?}/{name:maxlength(25)?}", defaults: new { controller = "TaskList", action = "AddTask" });
                routes.MapRoute(name: "addTask", template: "AddTask/{id?}/{name:maxlength(25)?}", defaults: new { controller = "TaskList", action = "AddTask" });
                routes.MapRoute(name: "deleteTask", template: "DeleteTask/{taskID?}/{taskListId?}", defaults: new { controller = "TaskList", action = "DeleteTask" });
                routes.MapRoute(name: "checkTask", template: "CheckTask/{taskID?}/{taskListId?}", defaults: new { controller = "TaskList", action = "CheckTask" });
                routes.MapRoute(name: "uncheckTask", template: "UncheckTask/{taskID?}/{taskListId?}", defaults: new { controller = "TaskList", action = "UncheckTask" });
            });
        }
    }
}
