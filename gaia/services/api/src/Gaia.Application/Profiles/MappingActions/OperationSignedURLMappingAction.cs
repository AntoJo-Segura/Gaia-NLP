// <copyright file="OperationSignedURLMappingAction.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using System;
using Amazon.S3;
using Amazon.S3.Model;
using AutoMapper;
using Gaia.Application.Dtos;
using Gaia.Core.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Gaia.Application.Profiles.MappingActions
{
    public class OperationSignedURLMappingAction : IMappingAction<Operation, OperationDto>
    {
        private readonly ILogger<OperationSignedURLMappingAction> _logger;
        private readonly IAmazonS3 _amazonS3;
        private readonly Settings _settings;

        public OperationSignedURLMappingAction(
                ILogger<OperationSignedURLMappingAction> logger,
                IOptions<Settings> options,
                IAmazonS3 amazonS3)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _amazonS3 = amazonS3 ?? throw new ArgumentNullException(nameof(amazonS3));
            _settings = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public void Process(Operation source, OperationDto destination, ResolutionContext context)
        {
            try
            {
                if (!string.IsNullOrEmpty(source?.InputFile))
                {
                    destination.InputFile = GetURL(source.InputFile);
                }

                if (!string.IsNullOrEmpty(source?.OutputFile))
                {
                    destination.OutputFile = GetURL(source.OutputFile);
                }
            }
            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        private string GetURL(string key)
        {
            GetPreSignedUrlRequest request = new GetPreSignedUrlRequest
            {
                BucketName = _settings.BucketName,
                Key = key,
                Expires = DateTime.Now.AddMinutes(15),
            };

            return _amazonS3.GetPreSignedURL(request);
        }
    }
}
