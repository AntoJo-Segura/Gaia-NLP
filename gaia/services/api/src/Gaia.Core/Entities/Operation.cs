// <copyright file="Operation.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using System;
using Gaia.Core.Enums;

namespace Gaia.Core.Entities
{
    public class Operation : AuditableEntity
    {
        internal Operation()
        {
        }

        public OperationId Id { get; internal set; }

        public Status Status { get; private set; }

        public string InputFile { get; internal set; }

        public string OutputFile { get; internal set; }

        public void SetStatus(Status status)
        {
            if (Status == Status.Processed && status == Status.Pending)
            {
                throw new InvalidOperationException($"Invalid status transition");
            }

            Status = status;
        }
    }
}
