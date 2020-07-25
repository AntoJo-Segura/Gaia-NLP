// <copyright file="IOperationRepository.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using System;
using Gaia.Core.Entities;

namespace Gaia.Core.Repositories
{
    public interface IOperationRepository : IRepository<Operation, OperationId>
    {
        OperationId GenerateIdendity();
    }
}
