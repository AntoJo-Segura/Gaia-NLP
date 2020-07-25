// <copyright file="OperationAppService.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using System;
using System.Threading.Tasks;
using AutoMapper;
using Gaia.Application.Commands;
using Gaia.Application.Dtos;
using Gaia.Application.Queries;
using Gaia.Application.Services.Contracts;
using Gaia.Core.Entities;
using Gaia.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Gaia.Application.Services
{
    public class OperationAppService : IOperationAppService
    {
        private readonly ILogger<OperationAppService> _logger;
        private readonly IOperationRepository _operationRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OperationAppService(
                ILogger<OperationAppService> logger,
                IOperationRepository operationRepository,
                IMediator mediator,
                IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _operationRepository = operationRepository ?? throw new ArgumentNullException(nameof(operationRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<OperationDto> GetOperationByIdAsync(Guid operationId)
        {
            try
            {
                var operationDto = await _mediator.Send(new OperationByIdQuery(operationId));

                return operationDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<OperationDto> InsertNewOperationAsync(InsertOperationDto operationDto)
        {
            try
            {
                var operationId = OperationId.Create(Guid.NewGuid());

                string documentPath = await _mediator.Send(new UploadDocumentCommand(operationDto, operationId));
                Operation operation = await _mediator.Send(new InsertOperationCommand(documentPath, operationId));

                return _mapper.Map<OperationDto>(operation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
