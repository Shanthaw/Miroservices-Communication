using AutoMapper;
using GloboTicket.Services.EventCatalog.DbContexts;
using GloboTicket.Services.EventCatalog.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Net.Http;
using Polly;
using Polly.Extensions.Http;
using GloboTicket.Integration.MessagingBus;

namespace GloboTicket.Services.EventCatalog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<EventCatalogDbContext>(
            //    options => options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection"),
            //        providerOptions => providerOptions.EnableRetryOnFailure(
            //            maxRetryCount: 10,
            //            maxRetryDelay: TimeSpan.FromSeconds(30),
            //            errorNumbersToAdd: null)
            //        )
            //    );

            services.AddDbContext<EventCatalogDbContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    providerOptions => providerOptions.EnableRetryOnFailure()));

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            //services.AddScoped<IEventRepository, EventRepository>();
            
            // provides access to IEventRepository and IEventLogRepository
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSingleton<IMessageBus, AzServiceBusMessageBus>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EventDto Catalog API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "EventDto Catalog API V1");

            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
