using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationsX.WebClient.Hubs;
using NotificationsXDomain;

namespace NotificationsX.WebClient
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


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSignalR();

            services.AddSingleton<IPushNotificationsStream, PushNotificationsStream>();
            services.AddSingleton<Action<string>>((m) => Console.WriteLine(m));
            services.AddSingleton<IPushNotificationConsumer, PushNotificationConsumer>();
            services.AddSingleton<NotificationRelay>();
            //services.AddSingleton<IRelay, NotificationRelay>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSignalR(routes =>
            {
                routes.MapHub<NotificationHub>("/notificationHub");
            });

            app.UseStaticFiles();
            app.UseDefaultFiles();
            var notificationService = app.ApplicationServices.GetService<NotificationRelay>();

            //Task.Factory.StartNew(() => app.ApplicationServices.GetService<IRelay>().CreateSubscriber();

            Task.Factory.StartNew(() => app.ApplicationServices.GetService<IPushNotificationConsumer>().Listen());

            app.UseMvc();
            
            
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}
