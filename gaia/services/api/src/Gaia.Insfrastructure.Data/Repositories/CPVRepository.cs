// <copyright file="CPVRepository.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using System;
using Amazon.DynamoDBv2;
using AutoMapper;
using Gaia.Core.Entities;
using Gaia.Core.Repositories;
using Gaia.Insfrastructure.Data.Models;

namespace Gaia.Insfrastructure.Data.Repositories
{
    public class CPVRepository : Repository<CPV, string, CPVFlat>, ICPVRepository
    {
        private readonly IMapper _mapper;

        public CPVRepository(
                IAmazonDynamoDB client,
                IMapper mapper)
            : base(client)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected override CPVFlat MapToDynamoEntity(CPV entity) =>
            _mapper.Map<CPVFlat>(entity);

        protected override CPV MapToEntity(CPVFlat dynamoEntity) =>
            _mapper.Map<CPV>(dynamoEntity);
    }
}
