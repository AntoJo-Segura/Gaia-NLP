// <copyright file="OperationFlat.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using System;
using Amazon.DynamoDBv2.DataModel;
using Gaia.Core.Enums;
using Gaia.Insfrastructure.Data.Converters;

namespace Gaia.Core.Entities
{
    [DynamoDBTable("gaia-operations")]
    public class OperationFlat
    {
        [DynamoDBHashKey("Id", typeof(OperationIdConverter))]
        public OperationId Id { get; set; }

        [DynamoDBProperty("Status", typeof(EnumPropertyConverter<Status>))]
        public Status Status { get; set; }

        [DynamoDBProperty("InputFile")]
        public string InputFile { get; set; }

        [DynamoDBProperty("OutputFile")]
        public string OutputFile { get; set; }

        [DynamoDBProperty("CreatedAt")]
        public DateTime CreatedAt { get; internal set; }

        [DynamoDBProperty("UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }
}
