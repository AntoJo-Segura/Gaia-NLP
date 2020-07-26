// <copyright file="InsertOperationCommandHandler.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using System;
using System.Threading;
using System.Threading.Tasks;
using Gaia.Application.Commands;
using Gaia.Core.Entities;
using Gaia.Core.Factories;
using Gaia.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Gaia.Application.CommandsHandlers
{
    public class InsertOperationCommandHandler : IRequestHandler<InsertOperationCommand, Operation>
    {
        private readonly ILogger<InsertOperationCommandHandler> _logger;
        private readonly IOperationFactory _operationFactory;
        private readonly IOperationRepository _operationRepository;

        public InsertOperationCommandHandler(
                ILogger<InsertOperationCommandHandler> logger,
                IOperationFactory operationFactory,
                IOperationRepository operationRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _operationFactory = operationFactory ?? throw new ArgumentNullException(nameof(operationFactory));
            _operationRepository = operationRepository ?? throw new ArgumentNullException(nameof(operationRepository));
        }

        public async Task<Operation> Handle(InsertOperationCommand request, CancellationToken cancellationToken)
        {
            OperationId operationId = request.OperationId;
            Operation operation = _operationFactory.Create(id: operationId, inputFile: request.DocumentPath);

            _logger.LogInformation($"Creating new operation with Id: {operationId}");

            await _operationRepository.InsertOrUpdateAsync(operation);

            _logger.LogInformation($"Operation '{operationId}' created.");

            return operation;
        }
    }
}
