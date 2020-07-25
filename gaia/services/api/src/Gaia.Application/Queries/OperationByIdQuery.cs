// <copyright file="OperationByIdQuery.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using System;
using Dawn;
using Gaia.Application.Dtos;
using MediatR;

namespace Gaia.Application.Queries
{
    public class OperationByIdQuery : IRequest<OperationDto>
    {
        public OperationByIdQuery(Guid operationId)
        {
            OperationId = Guard.Argument(operationId, nameof(operationId)).NotDefault();
        }

        public Guid OperationId { get; }
    }
}
