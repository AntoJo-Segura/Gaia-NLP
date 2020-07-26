// <copyright file="OperationDto.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using System;
using Gaia.Core.Enums;

namespace Gaia.Application.Dtos
{
    public class OperationDto
    {
        public Guid OperationId { get; internal set; }

        public Status Status { get; private set; }

        public string InputFile { get; internal set; }

        public string OutputFile { get; internal set; }
    }
}
