using CompetitionResults.Data;
using CompetitionResults.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CompetitionResults
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("AppDb"));
            });

            services.AddRazorPages();
            services.AddServerSideBlazor();

            RegisterRepositoriesAuto(services);

        }

        private void RegisterRepositoriesAuto(IServiceCollection services)
        {
            var baseRepoType = typeof(BaseRepository<>);

            Assembly
                .GetAssembly(baseRepoType)
                .GetTypes()
                .Where(type =>
                    type.BaseType != null
                    && type.BaseType.IsGenericType
                    && type.BaseType.GetGenericTypeDefinition() == baseRepoType)
                .ToList()
                .ForEach(type => SmartAddScope(services, type));
        }

        private void SmartAddScope(IServiceCollection services, Type ourType)
        {
            services.AddScoped(ourType, serviceProvider =>
            {
                var constructor = ourType
                    .GetConstructors()
                    .OrderByDescending(x => x.GetParameters().Length)
                    .First();

                var parameters = constructor
                    .GetParameters()
                    .Select(x =>
                        serviceProvider.GetService(x.ParameterType)
                    )
                    .ToArray();

                return constructor.Invoke(parameters);
            });
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
