// <copyright file="OperationId.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using System;
using System.Collections.Generic;
using Dawn;
using Gbm.Cash.Spei.Domain.SeedWork;

namespace Gaia.Core.Entities
{
    /// <summary>
    /// OperationId value object for Operation Entity.
    /// </summary>
    /// <seealso cref="Gbm.Cash.Spei.Domain.SeedWork.ValueObject" />
    public class OperationId : ValueObject
    {
        private OperationId(Guid id)
        {
            Guard.Argument(id, nameof(id)).NotDefault();

            Id = id;
        }

        public Guid Id { get; }

        /// <summary>
        /// Factory method to creates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>New OperationId.</returns>
        public static OperationId Create(Guid id) => new OperationId(id);

        public override string ToString() => Id.ToString();

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
        }
    }
}
