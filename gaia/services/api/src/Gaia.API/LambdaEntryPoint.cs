// <copyright file="LambdaEntryPoint.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using Gaia.API.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Gaia.API
{
    public class LambdaEntryPoint : Amazon.Lambda.AspNetCoreServer.APIGatewayProxyFunction
    {
        protected override void Init(IHostBuilder builder)
        {
            builder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                    .ConfigureAWSLambdaLogger()
                    .UseStartup<Startup>();
            });
        }
    }
}
