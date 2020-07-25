// <copyright file="UploadDocumentCommandHandler.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Gaia.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Gaia.Application.CommandsHandlers
{
    public class UploadDocumentCommandHandler : IRequestHandler<UploadDocumentCommand, string>
    {
        private readonly ILogger<UploadDocumentCommandHandler> _logger;
        private readonly IAmazonS3 _amazonS3;
        private readonly Settings _settings;

        public UploadDocumentCommandHandler(
                ILogger<UploadDocumentCommandHandler> logger,
                IOptions<Settings> options,
                IAmazonS3 amazonS3)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _amazonS3 = amazonS3 ?? throw new ArgumentNullException(nameof(amazonS3));
            _settings = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<string> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
        {
            IFormFile document = request?.OperationDto.Document;
            string key = $"{DateTime.UtcNow.Date:yyyyMMdd}/{request.OperationId.Id}{Path.GetExtension(document.FileName)}";

            _logger.LogInformation($"Uploading '{document.FileName}' to S3 Bucket. Key: '{key}'.");

            using (Stream stream = new MemoryStream())
            {
                await document.CopyToAsync(stream);

                var putRequest = new PutObjectRequest
                {
                    BucketName = _settings.BucketName,
                    Key = key,
                    InputStream = stream,
                };

                PutObjectResponse s3Response = await _amazonS3.PutObjectAsync(putRequest);

                _logger.LogInformation($"S3 response: {s3Response.HttpStatusCode}");
            }

            return key;
        }
    }
}
