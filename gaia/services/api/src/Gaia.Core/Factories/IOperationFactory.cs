// <copyright file="IOperationFactory.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using System;
using Gaia.Core.Entities;

namespace Gaia.Core.Factories
{
    public interface IOperationFactory
    {
        Operation Create(OperationId id, string inputFile);
    }
}
