using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using web.Rspositories;

namespace web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup( IConfiguration  configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<DataContext>(Options =>
            {
                Options.UseSqlServer(Configuration.GetConnectionString("sqlCon"));



            });

            services.AddScoped<IPortfolio<Owner>, OwnerRepositoryDb>();
            services.AddScoped<IPortfolio<portfolioItem>, PortfolioRepositoryDb>();


       
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseAuthentication();
           

            app.UseMvc(route =>
            {
                route.MapRoute("defualt", "{controller=Home}/{action=index}/{id?}");

            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            
           
        }
    }
}
