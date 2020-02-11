using System;
using Autofac;
using AutoMapper;
using Common.Data;
using Common.Database.Data;
using Common.Database.Services;
using Common.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Priority.Abstractions.Services;
using Priority.Services;

namespace Priority.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddMvc(option => option.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder
                .RegisterGeneric(typeof(EntityService<>))
                .As(typeof(IEntityService<>))
                .InstancePerLifetimeScope();

            builder
                .RegisterType(typeof(UnitOfWork))
                .As(typeof(IUnitOfWork))
                .InstancePerLifetimeScope();

            builder
                .RegisterGeneric(typeof(ListRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            builder
                .RegisterType(typeof(PriorityTaskService))
                .As(typeof(IPriorityTaskService))
                .SingleInstance();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync($"Hello Ivan!");
                });
            });
        }
    }
}
