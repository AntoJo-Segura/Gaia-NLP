// <copyright file="CPVDto.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using Gaia.Core.Entities;

namespace Gaia.Application.Dtos
{
    public class CPVDto
    {
        public string Code { get; set; }

        public string Description { get; set; }

        public CPVType Type { get; set; }
    }
}
