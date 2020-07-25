// <copyright file="AuditableEntity.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using System;

namespace Gaia.Core.Entities
{
    public abstract class AuditableEntity
    {
        public DateTime CreatedAt { get; internal set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
