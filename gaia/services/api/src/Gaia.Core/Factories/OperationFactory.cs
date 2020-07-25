// <copyright file="OperationFactory.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using System;
using Dawn;
using Gaia.Core.Entities;
using Gaia.Core.Enums;

namespace Gaia.Core.Factories
{
    public class OperationFactory : IOperationFactory
    {
        public Operation Create(OperationId id, string inputFile)
        {
            Guard.Argument(id, nameof(id)).NotNull();
            Guard.Argument(inputFile, nameof(inputFile)).NotNull().NotEmpty();

            Operation operation = new Operation
            {
                Id = id,
                InputFile = inputFile,
                CreatedAt = DateTime.UtcNow,
            };

            operation.SetStatus(Status.Pending);

            return operation;
        }
    }
}
