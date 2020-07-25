// <copyright file="IServiceCollectionExtensions.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using Amazon.DynamoDBv2;
using Amazon.S3;
using Amazon.S3.Transfer;
using AutoMapper;
using AutoWrapper.Extensions;
using AutoWrapper.Wrappers;
using Gaia.API;
using Gaia.API.Tools;
using Gaia.Application.Profiles;
using Gaia.Application.Services;
using Gaia.Application.Services.Contracts;
using Gaia.Core.Factories;
using Gaia.Core.Repositories;
using Gaia.Insfrastructure.Data.Profiles;
using Gaia.Insfrastructure.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            // Application services
            services.AddScoped<IOperationAppService, OperationAppService>();
            services.AddScoped<ICPVAppService, CPVAppService>();

            // Factories
            services.AddScoped<IOperationFactory, OperationFactory>();

            // Repositories
            services.AddScoped<IOperationRepository, OperationRepository>();
            services.AddScoped<ICPVRepository, CPVRepository>();

            // Transfer Utility
            services.AddScoped<ITransferUtility, TransferUtility>();

            // S3 service
            services.AddScoped<IAmazonS3>(sp =>
            {
                return AWSTools.GetAWSAmazonS3(sp.GetService<IConfiguration>());
            });

            return services;
        }

        public static IServiceCollection AddAwsServices(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDefaultAWSOptions(AWSTools.GetAWSOptions(configuration))
                    .AddAWSService<IAmazonDynamoDB>();

        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services) =>
            services.AddAutoMapper(typeof(GaiaProfile).Assembly, typeof(RepositoryProfile).Assembly);

        public static IServiceCollection AddCustomApiBehaviour(this IServiceCollection services)
        {
            return services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = false;
                options.SuppressInferBindingSourcesForParameters = false;

                options.InvalidModelStateResponseFactory = context =>
                {
                    throw new ApiException(context.ModelState.AllErrors());
                };
            });
        }

        /// <summary>
        /// Adds the open API.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddOpenApi(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GAIA API", Version = "v1" });
            });

            services.AddSwaggerGenNewtonsoftSupport();

            return services;
        }
    }
}
