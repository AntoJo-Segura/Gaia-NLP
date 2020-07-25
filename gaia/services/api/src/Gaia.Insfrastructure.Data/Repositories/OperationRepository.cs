// <copyright file="OperationRepository.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using System;
using Amazon.DynamoDBv2;
using AutoMapper;
using Gaia.Core.Entities;
using Gaia.Core.Repositories;

namespace Gaia.Insfrastructure.Data.Repositories
{
    public class OperationRepository : Repository<Operation, OperationId, OperationFlat>, IOperationRepository
    {
        private readonly IMapper _mapper;

        public OperationRepository(
                IAmazonDynamoDB client,
                IMapper mapper)
            : base(client)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public OperationId GenerateIdendity() => OperationId.Create(Guid.NewGuid());

        protected override OperationFlat MapToDynamoEntity(Operation entity) =>
            _mapper.Map<OperationFlat>(entity);

        protected override Operation MapToEntity(OperationFlat dynamoEntity) =>
            _mapper.Map<Operation>(dynamoEntity);
    }
}
