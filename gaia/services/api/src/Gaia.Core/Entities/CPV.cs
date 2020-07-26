// <copyright file="CPV.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using System.Collections.Generic;
using Dawn;

namespace Gaia.Core.Entities
{
    public class CPV
    {
        private List<string> _descriptions;

        public CPV(string code, CPVType type)
        {
            Code = Guard.Argument(code, nameof(code)).NotNull().NotEmpty();
            Type = type;
            _descriptions = new List<string>();
        }

        public string Code { get; private set; }

        public IReadOnlyList<string> Descriptions => _descriptions.AsReadOnly();

        public CPVType Type { get; private set; }

        public void AddDescription(string description)
        {
            Guard.Argument(description, nameof(description)).NotNull().NotEmpty();

            _descriptions.Add(description);
        }
    }
}
