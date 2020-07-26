// <copyright file="Startup.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using AutoWrapper;
using FluentValidation.AspNetCore;
using Gaia.Application;
using Gaia.Application.Commands;
using Gaia.Application.Validators;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Gaia.API
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
            services.AddControllers()
                    .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<InsertOperationDtoValidator>())
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.Converters.Add(new StringEnumConverter());
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    });

            services.AddMediatR(typeof(InsertOperationCommand).Assembly)
                    .AddCustomServices()
                    .AddAutoMapperProfiles()
                    .AddAwsServices(Configuration)
                    .AddCustomApiBehaviour()
                    .AddOpenApi();

            services.Configure<Settings>(Configuration.GetSection(nameof(Settings)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gaia V1");
            });

            app.UseApiResponseAndExceptionWrapper(GetAutoWrapperOptions());
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private AutoWrapperOptions GetAutoWrapperOptions()
        {
            return new AutoWrapperOptions()
            {
                IgnoreWrapForOkRequests = true,
            };
        }
    }
}
