using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using MusicNation.Data.Interfaces;
using MusicNation.Data.Mocks;
using MusicNation.Models.Database;

namespace MusicNation
{
    public class Startup
    {
        private readonly IConfigurationRoot configurationString;

        public Startup(IWebHostEnvironment hostEnv)
        {
            configurationString = new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("dbsettings.json").Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            string connection = configurationString.GetConnectionString("SongsConnection");
            services.AddDbContext<MusicContext>(options =>
                options.UseSqlServer(connection));
            services.AddControllersWithViews();
            services.AddTransient<ISongs, MockSongs>();
            services.AddTransient<IAlbums, MockAlbums>();
            services.AddTransient<IArtists, MockArtists>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
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
            app.UseRouting();
            app.UseAuthorization();

            //app.UseMvcWithDefaultRoute();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Songs}/{action=List}/{id?}");
                //endpoints.MapControllerRoute(
                //    title: "List",
                //    pattern: "{controller=Songs}/{action=List}/{id?}");
            });
        }
    }
}
