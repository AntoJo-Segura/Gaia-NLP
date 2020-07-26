// <copyright file="OperationIdConverter.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using System;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Dawn;
using Gaia.Core.Entities;

namespace Gaia.Insfrastructure.Data.Converters
{
    /// <summary>
    /// OperationId DynamoDB converter to OperationId.
    /// </summary>
    /// <seealso cref="Amazon.DynamoDBv2.DataModel.IPropertyConverter" />
    public class OperationIdConverter : IPropertyConverter
    {
        public object FromEntry(DynamoDBEntry entry)
        {
            Guid id = entry.AsGuid();

            return OperationId.Create(id);
        }

        public DynamoDBEntry ToEntry(object value)
        {
            OperationId operationId = value as OperationId;

            Guard.Argument(operationId, nameof(operationId)).NotNull();

            DynamoDBEntry entry = new Primitive
            {
                Type = DynamoDBEntryType.String,
                Value = operationId.Id.ToString(),
            };

            return entry;
        }
    }
}
