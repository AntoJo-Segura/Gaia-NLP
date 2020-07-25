// <copyright file="CPVFlat.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;
using Gaia.Core.Entities;
using Gaia.Insfrastructure.Data.Converters;

namespace Gaia.Insfrastructure.Data.Models
{
    [DynamoDBTable("gaia-cpvs")]
    public class CPVFlat
    {
        [DynamoDBRangeKey("Code")]
        public string Code { get; set; }

        [DynamoDBHashKey("Type")]
        [DynamoDBProperty("Type", typeof(EnumPropertyConverter<CPVType>))]
        public CPVType Type { get; set; }

        [DynamoDBProperty("Descriptions")]
        public List<string> Descriptions { get; set; }
    }
}
