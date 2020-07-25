// <copyright file="AWSTools.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.S3;
using Microsoft.Extensions.Configuration;

namespace Gaia.API.Tools
{
    public sealed class AWSTools
    {
        private static AWSOptions _awsOptions;

        public static AWSOptions GetAWSOptions(IConfiguration configuration)
        {
            if (_awsOptions == null)
            {
                // Get settings.
                AWSSettings awsSettings = configuration.GetSection(nameof(AWSSettings)).Get<AWSSettings>();

                _awsOptions = configuration.GetAWSOptions();
                _awsOptions.Region = RegionEndpoint.EUWest1;

                if (!string.IsNullOrEmpty(awsSettings?.ServiceURL))
                {
                    _awsOptions.DefaultClientConfig.ServiceURL = awsSettings.ServiceURL;
                }

                if (!string.IsNullOrEmpty(awsSettings?.Profile))
                {
                    _awsOptions.Profile = awsSettings.Profile;
                }
            }

            return _awsOptions;
        }

        public static IAmazonS3 GetAWSAmazonS3(IConfiguration configuration)
        {
            // Get settings.
            AWSSettings awsSettings = configuration.GetSection(nameof(AWSSettings)).Get<AWSSettings>();
            AmazonS3Client client;

            if (!string.IsNullOrEmpty(awsSettings?.ServiceURL))
            {
                AmazonS3Config config = new AmazonS3Config()
                {
                    ServiceURL = awsSettings.ServiceURL,
                    ForcePathStyle = true,
                };

                client = new AmazonS3Client(config);
            }
            else
            {
                client = new AmazonS3Client();
            }

            return client;
        }
    }
}