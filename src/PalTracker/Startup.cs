﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.CloudFoundry.Connector.MySql.EFCore;

namespace PalTracker
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<TimeEntryContext>(options => options.UseMySql(Configuration));

            services.AddScoped<ITimeEntryRepository, MySqlTimeEntryRepository>();

            services.AddSingleton(sp => new WelcomeMessage(
                Configuration.GetValue<string>("WELCOME_MESSAGE", "WELCOME_MESSAGE not configured.")
            ));

            services.AddSingleton(sp => new CloudFoundryInfo(
                Configuration.GetValue<string>("PORT", "PORT not configured."),
                Configuration.GetValue<string>("MEMORY_LIMIT", "MEMORY_LIMIT not configured."),
                Configuration.GetValue<string>("CF_INSTANCE_INDEX", "CF_INSTANCE_INDEX not configured."),
                Configuration.GetValue<string>("CF_INSTANCE_ADDR", "CF_INSTANCE_ADDR not configured.")
            ));
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
