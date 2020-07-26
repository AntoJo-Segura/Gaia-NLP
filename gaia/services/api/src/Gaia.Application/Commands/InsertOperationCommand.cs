// <copyright file="InsertOperationCommand.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using Dawn;
using Gaia.Core.Entities;
using MediatR;

namespace Gaia.Application.Commands
{
    public class InsertOperationCommand : IRequest<Operation>
    {
        public InsertOperationCommand(string documentPath, OperationId operationId)
        {
            DocumentPath = Guard.Argument(documentPath, nameof(documentPath)).NotNull().NotEmpty();
            OperationId = operationId ?? throw new System.ArgumentNullException(nameof(operationId));
        }

        public string DocumentPath { get; }

        public OperationId OperationId { get; }
    }
}
