// <copyright file="UploadDocumentCommand.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using Gaia.Application.Dtos;
using Gaia.Core.Entities;
using MediatR;

namespace Gaia.Application.Commands
{
    public class UploadDocumentCommand : IRequest<string>
    {
        public UploadDocumentCommand(InsertOperationDto operationDto, OperationId operationId)
        {
            OperationDto = operationDto ?? throw new System.ArgumentNullException(nameof(operationDto));
            OperationId = operationId ?? throw new System.ArgumentNullException(nameof(operationId));
        }

        public InsertOperationDto OperationDto { get; }

        public OperationId OperationId { get; }
    }
}
