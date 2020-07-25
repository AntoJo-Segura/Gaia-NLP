// <copyright file="OperationByIdQueryHandler.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Gaia.Application.Dtos;
using Gaia.Application.Exceptions;
using Gaia.Application.Queries;
using Gaia.Core.Entities;
using Gaia.Core.Repositories;
using MediatR;
using Microsoft.AspNetCore.Server.HttpSys;

namespace Gaia.Application.QueriesHandlers
{
    public class OperationByIdQueryHandler : IRequestHandler<OperationByIdQuery, OperationDto>
    {
        private readonly IOperationRepository _operationRepository;
        private readonly IMapper _mapper;

        public OperationByIdQueryHandler(
                IOperationRepository operationRepository,
                IMapper mapper)
        {
            _operationRepository = operationRepository ?? throw new ArgumentNullException(nameof(operationRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<OperationDto> Handle(OperationByIdQuery request, CancellationToken cancellationToken)
        {
            OperationId operationId = OperationId.Create(request.OperationId);
            Operation operation = await _operationRepository.GetAsync(operationId);

            if (operation == null)
            {
                throw new OperationNotFoundException($"Operation '{request.OperationId}' doesn't exist.");
            }

            return _mapper.Map<OperationDto>(operation);
        }
    }
}
